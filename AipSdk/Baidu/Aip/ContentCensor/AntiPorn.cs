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
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.ContentCensor
{
    /// <summary>
    ///     黄反相关
    /// </summary>
    public class AntiPorn : Base
    {
        public const string ANTI_PORN_URL = "https://aip.baidubce.com/rest/2.0/antiporn/v1/detect";
        public const string ANTI_PORN_GIF_URL = "https://aip.baidubce.com/rest/2.0/antiporn/v1/detect_gif";

        public AntiPorn(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        ///     黄反识别
        /// </summary>
        /// <param name="image">图像字节数组</param>
        /// <returns>识别结果</returns>
        public JObject Detect(byte[] image)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ANTI_PORN_URL);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return PostAction(aipReq);
        }

        /// <summary>
        ///     GIF色情图像识别
        /// </summary>
        /// <param name="image">图像字节数组</param>
        /// <returns>识别结果</returns>
        public JObject DetectGif(byte[] image)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ANTI_PORN_GIF_URL);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return PostAction(aipReq);
        }
    }
}