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
using System.Text;
using Newtonsoft.Json;

namespace Baidu.Aip
{
    /// <summary>
    ///     Http请求包装
    /// </summary>
    public class AipHttpRequest
    {
        public enum BodyFormat
        {
            Formed,
            Json
        }

        public Dictionary<string, object> Bodys;

        public BodyFormat BodyType;
        public Encoding ContentEncoding;

        public Dictionary<string, string> Headers;

        public string Method;
        public Dictionary<string, string> Querys;

        /// <summary>
        ///     不带query
        /// </summary>
        public Uri Uri;

        private AipHttpRequest()
        {
            Headers = new Dictionary<string, string>();
            // 所有Url中附带aipSdk=CSharp参数
            Querys = new Dictionary<string, string> {{"aipSdk", "CSharp"}};
            Bodys = new Dictionary<string, object>();
            Method = "GET";
            BodyType = BodyFormat.Formed;
            ContentEncoding = Encoding.UTF8;
            System.Net.ServicePointManager.Expect100Continue = false;
        }

        public AipHttpRequest(string uri) : this()
        {
            Uri = new Uri(uri);
        }

        public HttpWebRequest GeneratedRequest { get; private set; }


        public string UriWithQuery
        {
            get
            {
                var query = Utils.ParseQueryString(Querys);
                return Uri + "?" + query;
            }
        }

        public byte[] ProcessHttpRequest(HttpWebRequest webRequest)
        {
            webRequest.Method = Method;
            webRequest.ReadWriteTimeout = 30000;
            foreach (var header in Headers)
                webRequest.Headers.Add(header.Key, header.Value);
            GeneratedRequest = webRequest;
            switch (BodyType)
            {
                case BodyFormat.Formed:
                {
                    var body = Bodys.Select(pair => pair.Key + "=" + Utils.UriEncode(pair.Value.ToString()))
                        .DefaultIfEmpty("")
                        .Aggregate((a, b) => a + "&" + b);
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    return ContentEncoding.GetBytes(body);
                }
                case BodyFormat.Json:
                {
                    var body = JsonConvert.SerializeObject(Bodys);
                    webRequest.ContentType = "application/json";
                    return ContentEncoding.GetBytes(body);
                }
            }
            return null;
        }

        /// <summary>
        ///     生成AI的Web请求
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public HttpWebRequest GenerateDevWebRequest(string token)
        {
            Querys.Add("access_token", token);
            var ret = (HttpWebRequest) WebRequest.Create(UriWithQuery);
            ret.ReadWriteTimeout = 30000;
            ret.Timeout = 30000;
            var body = ProcessHttpRequest(ret);
            var stream = ret.GetRequestStream();
            stream.Write(body, 0, body.Length);
            stream.Close();
            return ret;
        }

        /// <summary>
        ///     生成云的Web请求
        /// </summary>
        /// <param name="ak"></param>
        /// <param name="sk"></param>
        /// <returns></returns>
        public HttpWebRequest GenerateCloudRequest(string ak, string sk)
        {
            var ret = (HttpWebRequest) WebRequest.Create(UriWithQuery);
            ret.ReadWriteTimeout = 30000;
            ret.Timeout = 30000;
            var body = ProcessHttpRequest(ret);
            Auth.CloudRequest(this, ak, sk);
//            ret.GetRequestStream().Write(body, 0, body.Length);
//            ret.GetRequestStream().Close();
            var stream = ret.GetRequestStream();
            stream.Write(body, 0, body.Length);
            stream.Close();
            return ret;
        }

        /// <summary>
        ///     生成语音的Web请求
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest GenerateSpeechRequest()
        {
            var ret = (HttpWebRequest) WebRequest.Create(Uri);
            ret.ReadWriteTimeout = 30000;
            ret.Timeout = 30000;
            var body = ProcessHttpRequest(ret);
            ret.GetRequestStream().Write(body, 0, body.Length);
            ret.GetRequestStream().Close();
            return ret;
        }
    }
}