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

namespace Baidu.Aip.ImageProcess
{
    /// <summary>
    /// 图像处理
    /// </summary>
    public class ImageProcess : AipServiceBase   {
        
        private const string IMAGE_QUALITY_ENHANCE =
            "https://aip.baidubce.com/rest/2.0/image-process/v1/image_quality_enhance";
        
        private const string DEHAZE =
            "https://aip.baidubce.com/rest/2.0/image-process/v1/dehaze";
        
        private const string CONTRAST_ENHANCE =
            "https://aip.baidubce.com/rest/2.0/image-process/v1/contrast_enhance";
        
        private const string COLOURIZE =
            "https://aip.baidubce.com/rest/2.0/image-process/v1/colourize";
        
        private const string STRETCH_RESTORE =
            "https://aip.baidubce.com/rest/2.0/image-process/v1/stretch_restore";
        
        public ImageProcess(string apiKey, string secretKey) : base(apiKey, secretKey)
        {

        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed,
                ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        /// 图像无损放大接口
        /// 输入一张图片，可以在尽量保持图像质量的条件下，将图像在长宽方向各放大两倍。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ImageQualityEnhance(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(IMAGE_QUALITY_ENHANCE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 图像去雾接口
        /// 对浓雾天气下拍摄，导致细节无法辨认的图像进行去雾处理，还原更清晰真实的图像。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Dehaze(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DEHAZE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 图像对比度增强接口
        /// 调整过暗或者过亮图像的对比度，使图像更加鲜明。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ContrastEnhance(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(CONTRAST_ENHANCE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 黑白图像上色接口
        /// 智能识别黑白图像内容并填充色彩，使黑白图像变得鲜活。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Colourize(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(COLOURIZE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 拉伸图像恢复接口
        /// 自动识别过度拉伸的图像，将图像内容恢复成正常比例。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject StretchRestore(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(STRETCH_RESTORE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}