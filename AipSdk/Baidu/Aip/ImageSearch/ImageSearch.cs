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
        
        private const string SAME_HQ_UPDATE =
            "https://aip.baidubce.com/rest/2.0/realtime_search/same_hq/update";
        
        private const string SAME_HQ_DELETE =
            "https://aip.baidubce.com/rest/2.0/realtime_search/same_hq/delete";
        
        private const string SIMILAR_ADD =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/add";
        
        private const string SIMILAR_SEARCH =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/search";
        
        private const string SIMILAR_UPDATE =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/update";
        
        private const string SIMILAR_DELETE =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/similar/delete";
        
        private const string PRODUCT_ADD =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/add";
        
        private const string PRODUCT_SEARCH =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/search";
        
        private const string PRODUCT_UPDATE =
            "https://aip.baidubce.com/rest/2.0/image-classify/v1/realtime_search/product/update";
        
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
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）；同时可提交分类维度信息（具体变量为tags，最多可传入2个tag），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
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
        /// 相同图检索—入库接口
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）；同时可提交分类维度信息（具体变量为tags，最多可传入2个tag），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqAddUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_ADD);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相同图检索—检索接口
        /// 完成入库后，可使用该接口实现相同图检索。**支持传入指定分类维度（具体变量tags）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息。**
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///           <item>  <c>tag_logic</c>: 检索时tag之间的逻辑， 0：逻辑and，1：逻辑or </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
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
        /// 相同图检索—检索接口
        /// 完成入库后，可使用该接口实现相同图检索。**支持传入指定分类维度（具体变量tags）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息。**
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///           <item>  <c>tag_logic</c>: 检索时tag之间的逻辑， 0：逻辑and，1：逻辑or </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqSearchUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_SEARCH);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相同图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqUpdate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_UPDATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相同图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqUpdateUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_UPDATE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相同图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="contSign">图片签名</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqUpdateContSign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_UPDATE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相同图检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

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
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SameHqDeleteByUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SAME_HQ_DELETE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相同图检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="contSign">图片签名</param>
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
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）；同时可提交分类维度信息（具体变量为tags，最多可传入2个tag），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
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
        /// 相似图检索—入库接口
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）；同时可提交分类维度信息（具体变量为tags，最多可传入2个tag），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。 </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarAddUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_ADD);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—检索接口
        /// 完成入库后，可使用该接口实现相似图检索。**支持传入指定分类维度（具体变量tags）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息。**
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///           <item>  <c>tag_logic</c>: 检索时tag之间的逻辑， 0：逻辑and，1：逻辑or </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
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
        /// 相似图检索—检索接口
        /// 完成入库后，可使用该接口实现相似图检索。**支持传入指定分类维度（具体变量tags）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息。**
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///           <item>  <c>tag_logic</c>: 检索时tag之间的逻辑， 0：逻辑and，1：逻辑or </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarSearchUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_SEARCH);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarUpdate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_UPDATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相似图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarUpdateUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_UPDATE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相似图检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、tags）**

        /// </summary>
        /// <param name="contSign">图片签名</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>tags</c>: 1 - 65535范围内的整数，tag间以逗号分隔，最多2个tag。样例："100,11" ；检索时可圈定分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarUpdateContSign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_UPDATE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 相似图检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

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
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SimilarDeleteByUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMILAR_DELETE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 相似图检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="contSign">图片签名</param>
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
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）。同时可提交分类维度信息（具体变量为class_id1、class_id2），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

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
        /// 商品检索—入库接口
        /// **该接口实现单张图片入库，入库时需要同步提交图片及可关联至本地图库的摘要信息（具体变量为brief，具体可传入图片在本地标记id、图片url、图片名称等）。同时可提交分类维度信息（具体变量为class_id1、class_id2），方便对图库中的图片进行管理、分类检索。****注：重复添加完全相同的图片会返回错误。**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 检索时原样带回,最长256B。**请注意，检索接口不返回原图，仅反馈当前填写的brief信息，所以调用该入库接口时，brief信息请尽量填写可关联至本地图库的图片id或者图片url、图片名称等信息** </item>
        ///           <item>  <c>class_id1</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>class_id2</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductAddUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_ADD);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—检索接口
        /// 完成入库后，可使用该接口实现商品检索。**支持传入指定分类维度（具体变量class_id1、class_id2）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息**
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>class_id1</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>class_id2</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
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
        /// 商品检索—检索接口
        /// 完成入库后，可使用该接口实现商品检索。**支持传入指定分类维度（具体变量class_id1、class_id2）进行检索，返回结果支持翻页（具体变量pn、rn）。****请注意，检索接口不返回原图，仅反馈当前填写的brief信息，请调用入库接口时尽量填写可关联至本地图库的图片id或者图片url等信息**
        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>class_id1</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>class_id2</c>: 商品分类维度1，支持1-60范围内的整数。检索时可圈定该分类维度进行检索 </item>
        ///           <item>  <c>pn</c>: 分页功能，起始位置，例：0。未指定分页时，默认返回前300个结果；接口返回数量最大限制1000条，例如：起始位置为900，截取条数500条，接口也只返回第900 - 1000条的结果，共计100条 </item>
        ///           <item>  <c>rn</c>: 分页功能，截取条数，例：250 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductSearchUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_SEARCH);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、class_id1/class_id2）**

        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>class_id1</c>: 更新的商品分类1，支持1-60范围内的整数。 </item>
        ///           <item>  <c>class_id2</c>: 更新的商品分类2，支持1-60范围内的整数。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductUpdate(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_UPDATE);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 商品检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、class_id1/class_id2）**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>class_id1</c>: 更新的商品分类1，支持1-60范围内的整数。 </item>
        ///           <item>  <c>class_id2</c>: 更新的商品分类2，支持1-60范围内的整数。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductUpdateUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_UPDATE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 商品检索—更新接口
        /// **更新图库中图片的摘要和分类信息（具体变量为brief、class_id1/class_id2）**

        /// </summary>
        /// <param name="contSign">图片签名</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>brief</c>: 更新的摘要信息，最长256B。样例：{"name":"周杰伦", "id":"666"} </item>
        ///           <item>  <c>class_id1</c>: 更新的商品分类1，支持1-60范围内的整数。 </item>
        ///           <item>  <c>class_id2</c>: 更新的商品分类2，支持1-60范围内的整数。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductUpdateContSign(string contSign, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_UPDATE);
            
            aipReq.Bodys["cont_sign"] = contSign;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 商品检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

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
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="url">图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式，当image字段存在时url字段失效</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject ProductDeleteByUrl(string url, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PRODUCT_DELETE);
            
            aipReq.Bodys["url"] = url;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }
        /// <summary>
        /// 商品检索—删除接口
        /// **删除图库中的图片，支持批量删除，批量删除时请传cont_sign参数，勿传image，最多支持1000个cont_sign**

        /// </summary>
        /// <param name="contSign">图片签名</param>
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