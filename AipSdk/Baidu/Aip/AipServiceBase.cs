/*
 * Copyright 2017 Baidu, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip
{
    using Options = Dictionary<string, string>;

    public abstract class AipServiceBase
    {
        protected readonly object AuthLock = new object();
        protected volatile bool HasDoneAuthoried; // 是否已经走过鉴权流程
        protected volatile bool IsDev;

        protected AipServiceBase(string apiKey, string secretKey): this("", apiKey, secretKey)
        {
            
        }
        
        protected AipServiceBase(string appId, string apiKey, string secretKey)
        {
            AppId = appId;
            ApiKey = apiKey;
            SecretKey = secretKey;
            ExpireAt = DateTime.Now;
            DebugLog = false;
            Timeout = 60000;
        }

        protected string Token { get; set; }
        protected DateTime ExpireAt { get; set; }

        public string AppId { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public bool DebugLog { get; set; }

        /// <summary>
        /// in millisecond
        /// </summary>
        public int Timeout { get; set; }


        protected virtual void DoAuthorization()
        {
            lock (AuthLock)
            {
                if (!NeetAuth())
                    return;

                var resp = Auth.OpenApiFetchToken(ApiKey, SecretKey);

                if (resp != null)
                {
                    ExpireAt = DateTime.Now.AddSeconds((int) resp["expires_in"] - 1);
                    var scopes = resp["scope"].ToString().Split(' ');
                    if (scopes.ToList().Exists(v => Consts.AipScopes.Contains(v)))
                    {
                        IsDev = true;
                        Token = (string) resp["access_token"];
                    }
                }
                HasDoneAuthoried = true;
            }
        }

        protected virtual bool NeetAuth()
        {
            if (!HasDoneAuthoried)
                return true;

            if (IsDev)
                return DateTime.Now >= ExpireAt;
            return false;
        }


        protected void PreAction()
        {
            DoAuthorization();
        }

        protected virtual JObject PostAction(AipHttpRequest aipReq)
        {
            var respStr = SendRequet(aipReq);
//		    Console.WriteLine(respStr);
            JObject respObj;
            try
            {
                respObj = JsonConvert.DeserializeObject(respStr) as JObject;
            }
            catch (Exception e)
            {
                // 非json应该抛异常
                throw new AipException(e.Message + ": " + respStr);
            }

            if (respObj == null)
                throw new AipException("Empty response, please check input");

            // 服务失败，不抛异常
//	        if (respObj["error_code"] != null)
//	        {
//	            Console.WriteLine((string)respObj["error_msg"]);
//	            throw new AipException((int)respObj["error_code"], (string)respObj["error_msg"]);
//	        }
            return respObj;
        }

        protected virtual HttpWebRequest GenerateWebRequest(AipHttpRequest aipRequest)
        {
            return IsDev
                ? aipRequest.GenerateDevWebRequest(Token, Timeout)
                : aipRequest.GenerateCloudRequest(ApiKey, SecretKey, Timeout);
        }

        protected string SendRequet(AipHttpRequest aipRequest)
        {
            return Utils.StreamToString(SendRequetRaw(aipRequest).GetResponseStream(), aipRequest.ContentEncoding);
        }

        protected HttpWebResponse SendRequetRaw(AipHttpRequest aipRequest)
        {
            var webReq = GenerateWebRequest(aipRequest);
            Log(webReq.RequestUri.ToString());
            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse) webReq.GetResponse();
            }
            catch (WebException e)
            {
                // 网络请求失败应该抛异常
                throw new AipException((int) e.Status, e.Message);
            }

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new AipException((int) resp.StatusCode, "Server response code：" + (int) resp.StatusCode);

            return resp;
        }

        protected void CheckNotNull(object obj, string name)
        {
            if (obj == null)
                throw new AipException(name + " cannot be null.");
        }

        protected string ImagesToParams(IEnumerable<byte[]> images)
        {
            return images
                .Select(Convert.ToBase64String)
                .Aggregate((a, b) => a + "," + b);
        }

        protected string StrJoin(IEnumerable<string> data, string sep = ",")
        {
            return data.Aggregate((a, b) => a + sep + b);
        }

        protected virtual void Log(string msg)
        {
            if (DebugLog)
            {
                var dateStr = DateTime.Now.ToString("[yyyyMMdd HH:mm:ss]");
                Console.WriteLine("{0} [{1}] {2}", dateStr, GetType().FullName, msg);
            }
        }

        public class Type
        {
            public Type(string url)
            {
                Url = url;
            }

            public string Url { get; set; }
        }
        
        // common service for all interface
        /// <summary>
        /// 反馈接口
        /// 用于用户反馈模型的效果，用户必须至少反馈一个 true/false 来表示对该结果是否满意，同时可选择反馈详细的评价。
        /// http://ai.baidu.com/docs#/ImageCensoring-API/top
        /// </summary>
        /// <param name="data">
        /// demo: 具体参考文档
        ///    {
        ///        "api_url": "https://aip.baidubce.com/rest/2.0/antiporn/v1/detect",
        ///        "image_logid": 123456,
        ///        "level": 1,
        ///        "correct": 1
        ///    }
        /// </param>
        /// <returns></returns>
        public JObject Report(IEnumerable<Dictionary<string, object>> data)
        {
            var aipReq = new AipHttpRequest("https://aip.baidubce.com/rpc/2.0/feedback/v1/report")
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json    
            };
            CheckNotNull(data, "data");
            aipReq.Bodys["feedback"] = data;
            PreAction();
            return PostAction(aipReq);
        }
        
    }
}