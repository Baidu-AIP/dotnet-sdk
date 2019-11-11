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

using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.ContentCensor
{
    /// <summary>
    /// 文本审核
    /// </summary>
    public class TextCensor : AipServiceBase   {
        
        private const string ANTI_SPAM =
            "https://aip.baidubce.com/rest/2.0/antispam/v2/spam";

        private const string USER_DEFINED =
            "https://aip.baidubce.com/rest/2.0/solution/v1/text_censor/v2/user_defined";

        public TextCensor(string apiKey, string secretKey) : base(apiKey, secretKey)
        {

        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed,
                ContentEncoding = Encoding.GetEncoding("UTF-8")
            };
        }

        /// <summary>
        /// 文本审核接口
        /// 运用业界领先的深度学习技术，判断一段文本内容是否符合网络发文规范，实现自动化、智能化的文本审核。审核内容目前分为5大方向：色情文本、政治敏感、恶意推广、网络暴恐、低俗辱骂。v2正式版支持审核文本的黑白名单配置，且可通过调整阈值控制审核的松紧度标准，大幅度满足个性化文本内容审核的需求，详细配置说明详见AI官网社区说明帖。
        /// </summary>
        /// <param name="content">待审核的文本内容，不可为空，长度不超过20000字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        [System.Obsolete("AntiSpam is deprecated.")]
        public JObject AntiSpam(string content, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(ANTI_SPAM);
            
            aipReq.Bodys["content"] = content;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 内容审核文本API接口
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject TextCensorUserDefined(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DEFINED);

            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

    }
}