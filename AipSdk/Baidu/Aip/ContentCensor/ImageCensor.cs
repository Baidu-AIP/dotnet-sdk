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

namespace Baidu.Aip.ContentCensor
{
    /// <summary>
    /// 图像审核
    /// </summary>
    public class ImageCensor : Base
    {
        public const string USER_DEFINED = "https://aip.baidubce.com/rest/2.0/solution/v1/img_censor/v2/user_defined";
        
        public ImageCensor(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }
        
        /// <summary>
        /// 图像审核接口
        /// 为用户提供色情识别、暴恐识别、政治敏感人物识别、广告识别、图像垃圾文本识别（反作弊）、恶心图像识别等一系列图像识别接口的一站式服务调用，
        /// 并且支持用户在控制台中自定义配置所有接口的报警阈值和疑似区间，上传自定义文本黑库和敏感人物名单等。
        /// 相比于组合服务接口，本接口除了支持自定义配置外，还对返回结果进行了总体的包装，按照用户在控制台中配置的规则直接返回是否合规，如果不合规则指出具体不合规的内容。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject UserDefined(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DEFINED);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        
        /// <summary>
        /// 图像审核接口
        /// 为用户提供色情识别、暴恐识别、政治敏感人物识别、广告识别、图像垃圾文本识别（反作弊）、恶心图像识别等一系列图像识别接口的一站式服务调用，
        /// 并且支持用户在控制台中自定义配置所有接口的报警阈值和疑似区间，上传自定义文本黑库和敏感人物名单等。
        /// 相比于组合服务接口，本接口除了支持自定义配置外，还对返回结果进行了总体的包装，按照用户在控制台中配置的规则直接返回是否合规，如果不合规则指出具体不合规的内容。
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject UserDefinedUrl(string imageUrl, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DEFINED);
            
            CheckNotNull(imageUrl, "imageUrl");
            aipReq.Bodys["imgUrl"] = imageUrl;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        

    }
}