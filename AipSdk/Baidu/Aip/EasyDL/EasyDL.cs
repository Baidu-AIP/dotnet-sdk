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


namespace Baidu.Aip.EasyDL
{
    public class EasyDL : AipServiceBase
    {
        
        public EasyDL(string appId, string apiKey, string secretKey) : base(appId, apiKey, secretKey)
        {
            
        }
        
        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json,
                ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        /// 图像类请求接口
        /// </summary>
        /// <param name="fullurl">请求地址</param>
        /// <param name="image">声音数据原始二进制</param>
        /// <param name="options">可选参数</param>
        /// <returns></returns>
        public JObject requestImage(string fullurl, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(fullurl);
            
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        
        /// <summary>
        /// 声音类请求接口
        /// </summary>
        /// <param name="fullurl">请求地址</param>
        /// <param name="sound">声音数据原始二进制</param>
        /// <param name="options">可选参数</param>
        /// <returns></returns>
        public JObject requestSound(string fullurl, byte[] sound, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(fullurl);
            
            aipReq.Bodys["sound"] = System.Convert.ToBase64String(sound);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
    }
}