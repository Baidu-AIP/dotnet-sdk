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
        
        private const string TRAIN_TICKET =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/train_ticket";
        
        private const string TAXI_RECEIPT =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/taxi_receipt";
        
        private const string FORM =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/form";
        
        private const string TABLE_RECOGNIZE =
            "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request";
        
        private const string TABLE_RESULT_GET =
            "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/get_request_result";
        
        private const string VIN_CODE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/vin_code";
        
        private const string QUOTA_INVOICE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/quota_invoice";
        
        private const string HOUSEHOLD_REGISTER =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/household_register";
        
        private const string HK_MACAU_EXITENTRYPERMIT =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/HK_Macau_exitentrypermit";
        
        private const string TAIWAN_EXITENTRYPERMIT =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/taiwan_exitentrypermit";
        
        private const string BIRTH_CERTIFICATE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/birth_certificate";
        
        private const string VEHICLE_INVOICE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/vehicle_invoice";
        
        private const string VEHICLE_CERTIFICATE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/vehicle_certificate";
        
        private const string INVOICE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/invoice";
        
        private const string AIR_TICKET =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/air_ticket";
        
        private const string INSURANCE_DOCUMENTS =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/insurance_documents";
        
        private const string VAT_INVOICE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/vat_invoice";
        
        private const string QRCODE =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/qrcode";
        
        private const string NUMBERS =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers";
        
        private const string LOTTERY =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/lottery";
        
        private const string PASSPORT =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/passport";
        
        private const string BUSINESS_CARD =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/business_card";
        
        private const string HANDWRITING =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/handwriting";
        
        private const string CUSTOM =
            "https://aip.baidubce.com/rest/2.0/solution/v1/iocr/recognise";
        
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
        /// <param name="idCardSide">front：身份证含照片的一面；back：身份证带国徽的一面</param>
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
        /// 火车票识别接口
        /// 支持对大陆火车票的车票号、始发站、目的站、车次、日期、票价、席别、姓名进行结构化识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TrainTicket(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TRAIN_TICKET);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 出租车票识别接口
        /// 针对出租车票（现支持北京）的发票号码、发票代码、车号、日期、时间、金额进行结构化识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaxiReceipt(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TAXI_RECEIPT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 表格文字识别同步接口接口
        /// 自动识别表格线及表格内容，结构化输出表头、表尾及每个单元格的文字内容。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Form(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(FORM);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
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

        /// <summary>
        /// VIN码识别接口
        /// 对车辆车架上、挡风玻璃上的VIN码进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VinCode(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VIN_CODE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 定额发票识别接口
        /// 对各类定额发票的代码、号码、金额进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject QuotaInvoice(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(QUOTA_INVOICE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 户口本识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对出生地、出生日期、姓名、民族、与户主关系、性别、身份证号码字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject HouseholdRegister(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(HOUSEHOLD_REGISTER);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 港澳通行证识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对港澳通行证证号、姓名、姓名拼音、性别、有效期限、签发地点、出生日期字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject HkMacauExitentrypermit(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(HK_MACAU_EXITENTRYPERMIT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 台湾通行证识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对台湾通行证证号、签发地、出生日期、姓名、姓名拼音、性别、有效期字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaiwanExitentrypermit(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TAIWAN_EXITENTRYPERMIT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 出生医学证明识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对台湾通行证证号、签发地、出生日期、姓名、姓名拼音、性别、有效期字段进行识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject BirthCertificate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(BIRTH_CERTIFICATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 机动车销售发票识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】识别机动车销售发票号码、代码、日期、价税合计等14个字段
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VehicleInvoice(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VEHICLE_INVOICE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 车辆合格证识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】识别车辆合格证编号、车架号、排放标准、发动机编号等12个字段
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VehicleCertificate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VEHICLE_CERTIFICATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 税务局通用机打发票识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对国家/地方税务局发行的横/竖版通用机打发票的号码、代码、日期、合计金额、类型、商品名称字段进行结构化识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>location</c>: 是否输出位置信息，true：输出位置信息，false：不输出位置信息，默认false </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Invoice(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(INVOICE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 行程单识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对飞机行程单中的姓名、始发站、目的站、航班号、日期、票价字段进行结构化识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>location</c>: 是否输出位置信息，true：输出位置信息，false：不输出位置信息，默认false </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject AirTicket(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(AIR_TICKET);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 保单识别接口
        /// 【此接口需要您在[申请页面](https://cloud.baidu.com/survey/AICooperativeConsultingApply.html)中提交合作咨询开通权限】对各类保单中投保人、受益人的各项信息、保费、保险名称等字段进行结构化识别
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>rkv_business</c>: 是否进行商业逻辑处理，rue：进行商业逻辑处理，false：不进行商业逻辑处理，默认true </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject InsuranceDocuments(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(INSURANCE_DOCUMENTS);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 增值税发票识别接口
        /// 此接口需要您在页面中提交合作咨询开通权限】 识别并结构化返回增值税发票的各个字段及其对应值，包含了发票基础信息9项，货物相关信息12项，购买方/销售方的名称、识别号、地址电话、开户行及账号，共29项结构化字段。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VatInvoice(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VAT_INVOICE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 二维码识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限识别条形码、二维码中包含的URL或其他信息内容
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Qrcode(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(QRCODE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 数字识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限】对图像中的阿拉伯数字进行识别提取，适用于快递单号、手机号、充值码提取等场景
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///           <item>  <c>detect_direction</c>: 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:<br>- true：检测朝向；<br>- false：不检测朝向。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Numbers(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(NUMBERS);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 彩票识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限】对大乐透、双色球彩票进行识别，并返回识别结果
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Lottery(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LOTTERY);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 护照识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限】支持对中国大陆居民护照的资料页进行结构化识别，包含国家码、姓名、性别、护照号、出生日期、签发日期、有效期至、签发地点。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Passport(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PASSPORT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 名片识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限】提供对各类名片的结构化识别功能，提取姓名、邮编、邮箱、电话、网址、地址、手机号字段
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject BusinessCard(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(BUSINESS_CARD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 手写文字识别接口
        /// 【此接口需要您在[页面](http://ai.baidu.com/tech/ocr)中提交合作咨询开通权限】提供对各类名片的结构化识别功能，提取姓名、邮编、邮箱、电话、网址、地址、手机号字段
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>recognize_granularity</c>: 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Handwriting(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(HANDWRITING);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 自定义模板文字识别接口
        /// 自定义模板文字识别，是针对百度官方没有推出相应的模板，但是当用户需要对某一类卡证/票据（如房产证、军官证、火车票等）进行结构化的提取内容时，可以使用该产品快速制作模板，进行识别。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>templateSign</c>: 您在自定义文字识别平台制作的模板的ID </item>
        ///           <item>  <c>classifierId</c>: 分类器Id。这个参数和templateSign至少存在一个，优先使用templateSign。存在templateSign时，表示使用指定模板；如果没有templateSign而有classifierId，表示使用分类器去判断使用哪个模板 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Custom(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(CUSTOM);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
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