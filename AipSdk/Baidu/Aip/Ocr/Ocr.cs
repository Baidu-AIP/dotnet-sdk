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
using System.Diagnostics;
using System.Threading;

namespace Baidu.Aip.Ocr
{
    /// <summary>
    /// 通用文字识别
    /// </summary>
    public class Ocr : AipServiceBase   {
        
        private const string GENERAL_BASIC =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic";
        
        private const string ACCURATE_BASIC =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic";
        
        private const string GENERAL =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/general";
        
        private const string ACCURATE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate";
        
        private const string GENERAL_ENHANCED =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/general_enhanced";
        
        private const string WEB_IMAGE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/webimage";
        
        private const string IDCARD =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard";
        
        private const string BANKCARD =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard";
        
        private const string DRIVING_LICENSE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/driving_license";
        
        private const string VEHICLE_LICENSE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/vehicle_license";
        
        private const string LICENSE_PLATE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/license_plate";
        
        private const string BUSINESS_LICENSE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/business_license";
        
        private const string RECEIPT =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/receipt";
        
        private const string CUSTOM =
            "https://aip.baidubce.com/rest/2.0/solution/v1/iocr/recognise";
        
        private const string TABLE_RECOGNIZE =
            "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request";
        
        private const string TABLE_RESULT_GET =
            "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/get_request_result";
        
        public Ocr(string apiKey, string secretKey) : base(apiKey, secretKey)
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
        /// 通用文字识别接口
        /// 用户向服务请求识别某张图中的所有文字
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GeneralBasic(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL_BASIC);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 通用文字识别接口
        /// 用户向服务请求识别某张图中的所有文字
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GeneralBasicUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL_BASIC);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 通用文字识别（高精度版）接口
        /// 用户向服务请求识别某张图中的所有文字，相对于通用文字识别该产品精度更高，但是识别耗时会稍长。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject AccurateBasic(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(ACCURATE_BASIC);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 通用文字识别（含位置信息版）接口
        /// 用户向服务请求识别某张图中的所有文字，并返回文字在图中的位置信息。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>vertexes_location</c>: 是否返回文字外接多边形顶点位置，不支持单字位置。默认为false </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject General(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 通用文字识别（含位置信息版）接口
        /// 用户向服务请求识别某张图中的所有文字，并返回文字在图中的位置信息。
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>vertexes_location</c>: 是否返回文字外接多边形顶点位置，不支持单字位置。默认为false </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GeneralUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 通用文字识别（含位置高精度版）接口
        /// 用户向服务请求识别某张图中的所有文字，并返回文字在图片中的坐标信息，相对于通用文字识别（含位置信息版）该产品精度更高，但是识别耗时会稍长。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>vertexes_location</c>: 是否返回文字外接多边形顶点位置，不支持单字位置。默认为false </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Accurate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(ACCURATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 通用文字识别（含生僻字版）接口
        /// 某些场景中，图片中的中文不光有常用字，还包含了生僻字，这时用户需要对该图进行文字识别，应使用通用文字识别（含生僻字版）。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GeneralEnhanced(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL_ENHANCED);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 通用文字识别（含生僻字版）接口
        /// 某些场景中，图片中的中文不光有常用字，还包含了生僻字，这时用户需要对该图进行文字识别，应使用通用文字识别（含生僻字版）。
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>language_type</c>: 识别语言类型，默认为CHN_ENG。可选值包括：<br>- CHN_ENG：中英文混合；<br>- ENG：英文；<br>- POR：葡萄牙语；<br>- FRE：法语；<br>- GER：德语；<br>- ITA：意大利语；<br>- SPA：西班牙语；<br>- RUS：俄语；<br>- JAP：日语；<br>- KOR：韩语； </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GeneralEnhancedUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GENERAL_ENHANCED);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 网络图片文字识别接口
        /// 用户向服务请求识别一些网络上背景复杂，特殊字体的文字。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject WebImage(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(WEB_IMAGE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 网络图片文字识别接口
        /// 用户向服务请求识别一些网络上背景复杂，特殊字体的文字。
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_language</c>: 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject WebImageUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(WEB_IMAGE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 身份证识别接口
        /// 用户向服务请求识别身份证，身份证识别包括正面和背面。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="idCardSide">front：身份证正面；back：身份证背面</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>detect_risk</c>: 是否开启身份证风险类型(身份证复印件、临时身份证、身份证翻拍、修改过的身份证)功能，默认不开启，即：false。可选值:true-开启；false-不开启 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Idcard(byte[] image, string idCardSide, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(IDCARD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            aipReq.Bodys["id_card_side"] = idCardSide;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 银行卡识别接口
        /// 识别银行卡并返回卡号和发卡行。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Bankcard(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(BANKCARD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 驾驶证识别接口
        /// 对机动车驾驶证所有关键字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject DrivingLicense(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DRIVING_LICENSE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 行驶证识别接口
        /// 对机动车行驶证正本所有关键字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///           <item>  <c>accuracy</c>: normal 使用快速服务，1200ms左右时延；缺省或其它值使用高精度服务，1600ms左右时延 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VehicleLicense(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VEHICLE_LICENSE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 车牌识别接口
        /// 识别机动车车牌，并返回签发地和号牌。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>multi_detect</c>: 是否检测多张车牌，默认为false，当置为true的时候可以对一张图片内的多张车牌进行识别 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LicensePlate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LICENSE_PLATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 营业执照识别接口
        /// 识别营业执照，并返回关键字段的值，包括单位名称、法人、地址、有效期、证件编号、社会信用代码等。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject BusinessLicense(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(BUSINESS_LICENSE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 通用票据识别接口
        /// 用户向服务请求识别医疗票据、发票、的士票、保险保单等票据类图片中的所有文字，并返回文字在图中的位置信息。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///           <item>  <c>probability</c>: 是否返回识别结果中每一行的置信度 </item>
        ///           <item>  <c>accuracy</c>: normal 使用快速服务，1200ms左右时延；缺省或其它值使用高精度服务，1600ms左右时延 </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Receipt(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(RECEIPT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 自定义模版文字识别接口
        /// 自定义模版文字识别，是针对百度官方没有推出相应的模版，但是当用户需要对某一类卡证/票据（如房产证、军官证、火车票等）进行结构化的提取内容时，可以使用该产品快速制作模版，进行识别。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="templateSign">您在自定义文字识别平台制作的模版的ID</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Custom(byte[] image, string templateSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(CUSTOM);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            aipReq.Bodys["templateSign"] = templateSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 表格文字识别接口
        /// 自动识别表格线及表格内容，结构化输出表头、表尾及每个单元格的文字内容。表格文字识别接口为异步接口，分为两个API：提交请求接口、获取结果接口。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TableRecognitionRequest(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TABLE_RECOGNIZE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 表格识别结果接口
        /// 获取表格文字识别结果
        /// </summary>
        /// <param name="requestId">发送表格文字识别请求时返回的request id</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>result_type</c>: 期望获取结果的类型，取值为“excel”时返回xls文件的地址，取值为“json”时返回json格式的字符串,默认为”excel” </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TableRecognitionGetResult(string requestId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TABLE_RESULT_GET);
            
            aipReq.Bodys["request_id"] = requestId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


#region Synchronized Table Recognize Interface

        /// <summary>
        ///     识别表格（同步接口）
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds"></param>
        /// <param name="options">请求识别时的可选参数</param>
        /// <returns></returns>
        /// <exception cref="AipException"></exception>
        public JObject TableRecognition(
            byte[] image,
            long timeoutMiliseconds = 20000,
            Dictionary<string, object> options = null
        )
        {
            var watch = Stopwatch.StartNew();
            var formBeginResp = TableRecognitionRequest(image);
            if (!(formBeginResp["result"] is JArray) || ((JArray) formBeginResp["result"]).Count != 1)
                return formBeginResp;
            var requestId = formBeginResp["result"][0]["request_id"].ToString();
            Log("Table recognize: wait for result...");
            while (true)
            {
                if (watch.ElapsedMilliseconds >= timeoutMiliseconds)
                {
                    Log("Timeout!");
                    throw new AipException("SDK Error: Timeout for form recognition");
                }
                var tempResp = TableRecognitionGetResult(requestId, options);
                JToken tp;
                if (tempResp.TryGetValue("error_code", out tp))
                {
                    Log("Table recognize: fail!");
                    return tempResp;
                }
                if ((int) tempResp["result"]["ret_code"] == 3)
                {
                    // 成功
                    Log("Table recognize: success!");
                    return tempResp;
                }
                Log("Table recognize: not ready yet, wait 1s..." + tempResp);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        ///     识别为Json结果返回。 会在后台自动刷新结果，直至返回结果或超时
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds">超时时间</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject TableRecognitionToJson(
            byte[] image,
            long timeoutMiliseconds = 20000,
            Dictionary<string, object> options = null)
        {
            if (options == null)
                options = new Dictionary<string, object>();
            options["result_type"] = "json";
            return TableRecognition(image, timeoutMiliseconds, options);
        }

        /// <summary>
        ///     识别为Excel结果返回。 会在后台自动刷新结果，直至返回结果或超时
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject TableRecognitionToExcel(
            byte[] image,
            long timeoutMiliseconds = 20000,
            Dictionary<string, object> options = null)
        {
            if (options == null)
                options = new Dictionary<string, object>();
            options["result_type"] = "excel";
            return TableRecognition(image, timeoutMiliseconds, options);
        }

        #endregion


    }
}