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

namespace Baidu.Aip.ImageClassify
{
    /// <summary>
    /// 图像识别
    /// </summary>
    public class ImageClassify : AipServiceBase   {
        
        private const string DISH_DETECT =
            "https://aip.baidubce.com/rest/2.0/image-classify/v2/dish";
        
        private const string CAR_DETECT =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/car";
        
        private const string LOGO_SEARCH =
            "https://aip.baidubce.com/rest/2.0/image-classify/v2/logo";
        
        private const string LOGO_ADD =
            "https://aip.baidubce.com/rest/2.0/realtime_search/v1/logo/add";
        
        private const string LOGO_DELETE =
            "https://aip.baidubce.com/rest/2.0/realtime_search/v1/logo/delete";
        
        private const string ANIMAL_DETECT =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/animal";
        
        private const string PLANT_DETECT =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/plant";
        
        private const string OBJECT_DETECT =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/object_detect";
        
        public ImageClassify(string apiKey, string secretKey) : base(apiKey, secretKey)
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
        /// 菜品识别接口
        /// 该请求用于菜品识别。即对于输入的一张图片（可正常解码，且长宽比适宜），输出图片的菜品名称、卡路里信息、置信度。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>top_num</c>: 返回预测得分top结果数，默认为5 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject DishDetect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DISH_DETECT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 车辆识别接口
        /// 该请求用于检测一张车辆图片的具体车型。即对于输入的一张图片（可正常解码，且长宽比适宜），输出图片的车辆品牌及型号。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>top_num</c>: 返回预测得分top结果数，默认为5 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject CarDetect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(CAR_DETECT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// logo商标识别接口
        /// 该请求用于检测和识别图片中的品牌LOGO信息。即对于输入的一张图片（可正常解码，且长宽比适宜），输出图片中LOGO的名称、位置和置信度。 当效果欠佳时，可以建立子库（请加入QQ群：649285136 联系工作人员申请建库）并自定义logo入库，提高识别效果。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>custom_lib</c>: 是否只使用自定义logo库的结果，默认false：返回自定义库+默认库的识别结果 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LogoSearch(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LOGO_SEARCH);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// logo商标识别—添加接口
        /// 该接口尚在邀测阶段，使用该接口之前需要线下联系工作人员完成建库方可使用，请加入QQ群：649285136 联系相关人员。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="brief">brief，检索时带回。此处要传对应的name与code字段，name长度小于100B，code长度小于150B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LogoAdd(byte[] image, string brief, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LOGO_ADD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            aipReq.Bodys["brief"] = brief;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// logo商标识别—删除接口
        /// 该接口尚在邀测阶段，使用该接口之前需要线下联系工作人员完成建库方可使用，请加入QQ群：649285136 联系相关人员。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LogoDeleteByImage(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LOGO_DELETE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// logo商标识别—删除接口
        /// 该接口尚在邀测阶段，使用该接口之前需要线下联系工作人员完成建库方可使用，请加入QQ群：649285136 联系相关人员。
        /// </summary>
        /// <param name="contSign">图片签名（和image二选一，image优先级更高）</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LogoDeleteBySign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LOGO_DELETE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 动物识别接口
        /// 该请求用于识别一张图片。即对于输入的一张图片（可正常解码，且长宽比适宜），输出动物识别结果
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>top_num</c>: 返回预测得分top结果数，默认为6 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject AnimalDetect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(ANIMAL_DETECT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 植物识别接口
        /// 该请求用于识别一张图片。即对于输入的一张图片（可正常解码，且长宽比适宜），输出植物识别结果。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject PlantDetect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PLANT_DETECT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 图像主体检测接口
        /// 用户向服务请求检测图像中的主体位置。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>with_face</c>: 如果检测主体是人，主体区域是否带上人脸部分，0-不带人脸区域，其他-带人脸区域，裁剪类需求推荐带人脸，检索/识别类需求推荐不带人脸。默认取1，带人脸。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ObjectDetect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(OBJECT_DETECT);
            
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