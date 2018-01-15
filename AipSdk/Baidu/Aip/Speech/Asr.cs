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
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Speech
{
    /// <summary>
    ///     语音识别相关接口
    /// </summary>
    public class Asr : Base
    {
        private const string UrlAsr = "https://vop.baidu.com/server_api";

        public Asr(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json
            };
        }

        /// <summary>
        ///     识别语音数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject Recognize(byte[] data, string format, int rate, Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(data, "data");
            CheckNotNull(format, "format");
            var req = DefaultRequest(UrlAsr);
            req.Bodys["format"] = format;
            req.Bodys["rate"] = rate;

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;

            if (!req.Bodys.ContainsKey("channel"))
                req.Bodys["channel"] = 1;
            req.Bodys["len"] = data.Length;
            req.Bodys["speech"] = Convert.ToBase64String(data);
            req.Bodys["token"] = Token;
            return PostAction(req);
        }

        /// <summary>
        ///     识别URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject Recognize(string url, string callback, string format, int rate,
            Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(url, "url");
            CheckNotNull(format, "format");
            CheckNotNull(callback, "callback");
            var req = DefaultRequest(UrlAsr);

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            req.Bodys["url"] = url;
            req.Bodys["callback"] = callback;
            req.Bodys["format"] = format;
            req.Bodys["rate"] = rate;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;

            if (!req.Bodys.ContainsKey("channel"))
                req.Bodys["channel"] = 1;
            req.Bodys["token"] = Token;
            return PostAction(req);
        }
    }
}