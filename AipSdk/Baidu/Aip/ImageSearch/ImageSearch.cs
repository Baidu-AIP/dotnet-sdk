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

namespace Baidu.Aip.ImageSearch
{
    /// <summary>
    /// 图像搜索
    /// </summary>
    public class ImageSearch : AipServiceBase   {
        
        private const string SAME_HQ_ADD =
            "https://aip.baidubce.com/rest/2.0/realtime_search/same_hq/add";
        
        private const string SAME_HQ_SEARCH =
            "https://aip.baidubce.com/rest/2.0/realtime_search/same_hq/search";
        
        private const string SAME_HQ_DELETE =
            "https://aip.baidubce.com/rest/2.0/realtime_search/same_hq/delete";
        
        private const string SIMILAR_ADD =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/add";
        
        private const string SIMILAR_SEARCH =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/search";
        
        private const string SIMILAR_DELETE =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/delete";
        
        private const string PRODUCT_ADD =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/add";
        
        private const string PRODUCT_SEARCH =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/search";
        
        private const string PRODUCT_DELETE =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/delete";
        
        public ImageSearch(string apiKey, string secretKey) : base(apiKey, secretKey)
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
        /// 相同图检索—入库接口
        /// 该请求用于实时检索相同图片集合。即对于输入的一张图片（可正常解码，且长宽比适宜），返回自建图库中相同的图片集合。相同图检索包含入库、检索、删除三个子接口；**在正式使用之前请加入QQ群：649285136 联系工作人员完成建库并调用入库接口完成图片入库**。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqAdd(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_ADD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相同图检索—检索接口
        /// 使用该接口前，请加入QQ群：649285136 ，联系工作人员完成建库。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqSearch(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_SEARCH);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相同图检索—删除接口
        /// 删除相同图
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqDeleteByImage(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_DELETE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相同图检索—删除接口
        /// 删除相同图
        /// </summary>
        /// <param name="contSign">图片签名（和image二选一，image优先级更高）</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqDeleteBySign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_DELETE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—入库接口
        /// 该请求用于实时检索相似图片集合。即对于输入的一张图片（可正常解码，且长宽比适宜），返回自建图库中相似的图片集合。相似图检索包含入库、检索、删除三个子接口；**在正式使用之前请加入QQ群：649285136 联系工作人员完成建库并调用入库接口完成图片入库**。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarAdd(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_ADD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—检索接口
        /// 使用该接口前，请加入QQ群：649285136 ，联系工作人员完成建库。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarSearch(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_SEARCH);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—删除接口
        /// 删除相似图
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarDeleteByImage(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_DELETE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相似图检索—删除接口
        /// 删除相似图
        /// </summary>
        /// <param name="contSign">图片签名（和image二选一，image优先级更高）</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarDeleteBySign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_DELETE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—入库接口
        /// 该请求用于实时检索商品类型图片相同或相似的图片集合，适用于电商平台或商品展示等场景，即对于输入的一张图片（可正常解码，且长宽比适宜），返回自建商品库中相同或相似的图片集合。
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。**请注意，检索接口不返回原图，仅反馈当前填写的brief信息，所以调用该入库接口时，brief信息请尽量填写可关联至本地图库的图片id或者图片url、图片名称等信息** </item>
        ///           <item>  <c>class_id1</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>class_id2</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductAdd(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_ADD);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—检索接口
        /// 完成入库后，可使用该接口实现商品检索。**请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息**
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>class_id1</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>class_id2</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductSearch(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_SEARCH);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—删除接口
        /// 删除商品
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductDeleteByImage(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_DELETE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 商品检索—删除接口
        /// 删除商品
        /// </summary>
        /// <param name="contSign">图片签名（和image二选一，image优先级更高）</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductDeleteBySign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_DELETE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}