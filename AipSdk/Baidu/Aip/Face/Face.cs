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

namespace Baidu.Aip.Face
{
    public class Face : Base
    {
        public Face(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
            Group = new Group(apiKey, secretKey);
            User = new User(apiKey, secretKey);
        }

        public Group Group { get; private set; }
        public User User { get; private set; }

        /// <summary>
        ///     人脸检测
        /// </summary>
        /// <param name="image">图片二进制数组</param>
        /// <param name="options">可选参数</param>
        /// <returns>检测结果</returns>
        public JObject FaceDetect(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            PreAction();
            var req = DefaultRequest(FACE_DETECT_URL);
            req.Bodys.Add("image", Convert.ToBase64String(image));

            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    req.Bodys.Add(pair.Key, pair.Value);
            }
            return PostAction(req);
        }

        /// <summary>
        ///     人脸对比
        /// </summary>
        public JObject FaceMatch(IEnumerable<byte[]> images, Dictionary<string, string> options = null)
        {
            CheckNotNull(images, "images");
            PreAction();
            var req = DefaultRequest(FACE_MATCH_URL);

            req.Bodys.Add("images", ImagesToParams(images));
            if (options != null)
            {
                options.Remove("images");
                foreach (var pair in options)
                    req.Bodys.Add(pair.Key, pair.Value);
            }

            return PostAction(req);
        }
    }
}