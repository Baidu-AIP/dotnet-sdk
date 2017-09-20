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
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Speech
{
    /// <summary>
    ///     语音合成结果
    /// </summary>
    public class TtsResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string Sn { get; set; }
        public int Idx { get; set; }

        /// <summary>
        ///     语音数据内容
        /// </summary>
        public byte[] Data { get; set; }

        public bool Success
        {
            get { return ErrorCode == 0; }
        }
    }

    /// <summary>
    ///     语音合成相关接口
    /// </summary>
    public class Tts : Base
    {
        private const string UrlTts = "http://tsn.baidu.com/text2audio";

        public Tts(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed
            };
        }

        /// <summary>
        ///     语音合成
        /// </summary>
        /// <param name="text">需要合成的内容</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TtsResponse Synthesis(string text, Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(text, "text");
            var req = DefaultRequest(UrlTts);

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;


            if (!req.Bodys.ContainsKey("lang"))
                req.Bodys["lan"] = "zh";
            if (!req.Bodys.ContainsKey("ctp"))
                req.Bodys["ctp"] = 1;


            req.Bodys["tok"] = Token;
            req.Bodys["tex"] = text;
            return PostAction(req);
        }

        protected new TtsResponse PostAction(AipHttpRequest aipReq)
        {
            var ret = new TtsResponse();
            var response = SendRequetRaw(aipReq);

            if (response.ContentType.ToLower() == "application/json")
            {
                var respStr = Utils.StreamToString(response.GetResponseStream(), Encoding.UTF8);
                // 失败
                try
                {
                    var obj = JsonConvert.DeserializeObject(respStr) as JObject;
                    ret.ErrorCode = (int) obj["err_no"];
                    ret.ErrorMsg = (string) obj["err_msg"];
                    JToken temp;
                    if (obj.TryGetValue("sn", out temp))
                        ret.Sn = temp.ToString();

                    if (obj.TryGetValue("idx", out temp))
                        ret.Idx = int.Parse(temp.ToString());
                }
                catch (Exception e)
                {
                    // 返回非json
                    throw new AipException(e.Message + ": " + respStr);
                }
            }
            else
            {
                ret.ErrorCode = 0;
                ret.Data = Utils.StreamToBytes(response.GetResponseStream());
            }
            return ret;
        }
    }
}