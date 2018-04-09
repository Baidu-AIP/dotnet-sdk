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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.ContentCensor
{
    /// <summary>
    ///     图像审核解决方案
    /// </summary>
    public class Solution : Base
    {
        public const string ComboUrl = "https://aip.baidubce.com/api/v1/solution/direct/img_censor";
        public const string FaceAuditUri = "https://aip.baidubce.com/rest/2.0/solution/v1/face_audit";

        public Solution(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        protected new AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json
            };
        }

        private JObject ComboPostAction(AipHttpRequest aipReq, string[] scenes, Dictionary<string, object> options)
        {
            aipReq.Bodys.Add("scenes", scenes);

            if (options != null)
            {
                options.Remove("image");
                options.Remove("imageUrl");
                var conf = new Dictionary<string, object>();
                foreach (var pair in options)
                    if (pair.Value is string)
                        conf.Add(pair.Key, pair.Value);
                    else
                        conf.Add(pair.Key, JsonConvert.SerializeObject(pair.Value));
                aipReq.Bodys.Add("scenesConf", conf);
            }
            return PostAction(aipReq);
        }


        /// <summary>
        ///     图像审核组合接口
        /// </summary>
        /// <param name="imageUrl">图片URL</param>
        /// <param name="scenes">需要调用的服务</param>
        /// <param name="options">
        ///     服务参数。
        ///     key为服务类型，value为json string或dictionary
        /// </param>
        /// <returns></returns>
        public JObject Combo(string imageUrl, string[] scenes, Dictionary<string, object> options = null)
        {
            CheckNotNull(imageUrl, "imageUrl");
            PreAction();
            var aipReq = DefaultRequest(ComboUrl);
            aipReq.Bodys.Add("imgUrl", imageUrl);
            return ComboPostAction(aipReq, scenes, options);
        }

        /// <summary>
        ///     图像审核组合接口
        /// </summary>
        /// <param name="image">图像原始字节数据</param>
        /// <param name="scenes">调用的服务列表</param>
        /// <param name="options">额外参数</param>
        /// <returns></returns>
        public JObject Combo(byte[] image, string[] scenes, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ComboUrl);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return ComboPostAction(aipReq, scenes, options);
        }

        /// <summary>
        ///     头像审核
        /// </summary>
        /// <param name="images"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public JObject FaceAudit(byte[][] images, long? configId = null)
        {
            CheckNotNull(images, "images");
            PreAction();
            var aipReq = new AipHttpRequest(FaceAuditUri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed
            };
            if (configId.HasValue)
                aipReq.Bodys.Add("configId", configId);
            aipReq.Bodys.Add("images", ImagesToParams(images));
            return PostAction(aipReq);
        }

        /// <summary>
        ///     头像审核
        /// </summary>
        /// <param name="images"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public JObject FaceAudit(string[] images, long? configId = null)
        {
            CheckNotNull(images, "images");
            PreAction();
            var aipReq = new AipHttpRequest(FaceAuditUri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed
            };
            if (configId.HasValue)
                aipReq.Bodys.Add("configId", configId);
            aipReq.Bodys.Add("imgUrls", StrJoin(images));
            return PostAction(aipReq);
        }
    }
}