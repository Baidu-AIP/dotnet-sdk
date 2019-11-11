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

namespace Baidu.Aip.Face
{
    /// <summary>
    /// 人脸识别
    /// </summary>
    public class Face : AipServiceBase   {
        
        private const string DETECT =
            "https://aip.baidubce.com/rest/2.0/face/v3/detect";
        
        private const string SEARCH =
            "https://aip.baidubce.com/rest/2.0/face/v3/search";
        
        private const string MULTI_SEARCH =
            "https://aip.baidubce.com/rest/2.0/face/v3/multi-search";
        
        private const string USER_ADD =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/add";
        
        private const string USER_UPDATE =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/update";
        
        private const string FACE_DELETE =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/face/delete";
        
        private const string USER_GET =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/get";
        
        private const string FACE_GETLIST =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/face/getlist";
        
        private const string GROUP_GETUSERS =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/group/getusers";
        
        private const string USER_COPY =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/copy";
        
        private const string USER_DELETE =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/delete";
        
        private const string GROUP_ADD =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/group/add";
        
        private const string GROUP_DELETE =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/group/delete";
        
        private const string GROUP_GETLIST =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceset/group/getlist";
        
        private const string PERSON_VERIFY =
            "https://aip.baidubce.com/rest/2.0/face/v3/person/verify";
        
        private const string VIDEO_SESSIONCODE =
            "https://aip.baidubce.com/rest/2.0/face/v1/faceliveness/sessioncode";
        
        public Face(string apiKey, string secretKey) : base(apiKey, secretKey)
        {

        }


        private const string MATCH = "https://aip.baidubce.com/rest/2.0/face/v3/match";
        private const string FACEVERIFY =
            "https://aip.baidubce.com/rest/2.0/face/v3/faceverify";

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
        /// 人脸对比接口
        /// 两张人脸图片相似度对比：比对两张图片中人脸的相似度，并返回相似度分值
        /// </summary>
        /// <param name="faces">
        /// 数组成员必须是JObject，每个JObject均为string:string的key:value对，具体key如下：
        ///          image: 必须，图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断
        ///          image_type: 必须，图片类型 **BASE64**:图片的base64值，base64编码后的图片数据，需urlencode，编码后的图片大小不超过2M；**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)**；FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个
        ///          face_type: 可选，人脸的类型 LIVE表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等 IDCARD表示身份证芯片照：二代身份证内置芯片中的人像照片 WATERMARK表示带水印证件照：一般为带水印的小图，如公安网小图 CERT表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片 默认LIVE
        ///          quality_control: 可选，质量控制 NONE: 不进行控制 LOW:较低的质量要求 NORMAL: 一般的质量要求 HIGH: 较高的质量要求 默认NONE
        ///          liveness_control: 可选，活体控制 NONE: 不进行控制 LOW:较低的活体要求(高通过率 低攻击拒绝率) NORMAL: 一般的活体要求(平衡的攻击拒绝率, 通过率) HIGH: 较高的活体要求(高攻击拒绝率 低通过率) 默认NONE
        /// </param>
        /// <returns></returns>
        public JObject Match(JArray faces)
        {
            CheckNotNull(faces, "faces");
            var aipReq = DefaultRequest(MATCH);
            aipReq.BodyType = AipHttpRequest.BodyFormat.JsonRaw;
            PreAction();
            aipReq.Bodys[AipHttpRequest.BodyFormatJsonRawKey] = faces;
            return PostAction(aipReq);

        }

        /// <summary>
        /// 在线活体检测接口
        /// </summary>
        /// <param name="faces">
        /// 数组成员必须是JObject，每个JObject均为string:string的key:value对，具体key如下：
        ///          image: 必须，图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断
        ///          image_type: 必须，图片类型 **BASE64**:图片的base64值，base64编码后的图片数据，需urlencode，编码后的图片大小不超过2M；**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)**；FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个
        ///          face_field: 可选，包括age,beauty,expression,faceshape,gender,glasses,landmark,race,quality,facetype,parsing信息，逗号分隔，默认只返回face_token、活体数、人脸框、概率和旋转角度
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Faceverify(JArray faces)
        {
            CheckNotNull(faces, "faces");
            var aipReq = DefaultRequest(FACEVERIFY);
            aipReq.BodyType = AipHttpRequest.BodyFormat.JsonRaw;
            PreAction();
            aipReq.Bodys[AipHttpRequest.BodyFormatJsonRawKey] = faces;
            return PostAction(aipReq);

        }


        /// <summary>
        /// 人脸检测接口
        /// </summary>
        /// <param name="image">图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>face_field</c>: 包括**age,beauty,expression,face_shape,gender,glasses,landmark,landmark72，landmark150，race,quality,eye_status,emotion,face_type信息**  <br> 逗号分隔. 默认只返回face_token、人脸框、概率和旋转角度 </item>
        ///           <item>  <c>max_face_num</c>: 最多处理人脸的数目，默认值为1，仅检测图片中面积最大的那个人脸；**最大值10**，检测图片中面积最大的几张人脸。 </item>
        ///           <item>  <c>face_type</c>: 人脸的类型 **LIVE**表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等**IDCARD**表示身份证芯片照：二代身份证内置芯片中的人像照片 **WATERMARK**表示带水印证件照：一般为带水印的小图，如公安网小图 **CERT**表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片 默认**LIVE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Detect(string image, string imageType, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DETECT);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸搜索接口
        /// </summary>
        /// <param name="image">图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="groupIdList">从指定的group中进行查找 用逗号分隔，**上限20个**</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>max_face_num</c>: 最多处理人脸的数目<br>**默认值为1(仅检测图片中面积最大的那个人脸)** **最大值10** </item>
        ///           <item>  <c>match_threshold</c>: 匹配阈值（设置阈值后，score低于此阈值的用户信息将不会返回） 最大100 最小0 默认80 <br>**此阈值设置得越高，检索速度将会越快，推荐使用默认阈值`80`** </item>
        ///           <item>  <c>quality_control</c>: 图片质量控制  **NONE**: 不进行控制 **LOW**:较低的质量要求 **NORMAL**: 一般的质量要求 **HIGH**: 较高的质量要求 **默认 NONE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///           <item>  <c>user_id</c>: 当需要对特定用户进行比对时，指定user_id进行比对。即人脸认证功能。 </item>
        ///           <item>  <c>max_user_num</c>: 查找后返回的用户数量。返回相似度最高的几个用户，默认为1，最多返回50个。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Search(string image, string imageType, string groupIdList, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SEARCH);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            aipReq.Bodys["group_id_list"] = groupIdList;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸搜索 M:N 识别接口
        /// </summary>
        /// <param name="image">图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="groupIdList">从指定的group中进行查找 用逗号分隔，**上限20个**</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>max_face_num</c>: 最多处理人脸的数目<br>**默认值为1(仅检测图片中面积最大的那个人脸)** **最大值10** </item>
        ///           <item>  <c>match_threshold</c>: 匹配阈值（设置阈值后，score低于此阈值的用户信息将不会返回） 最大100 最小0 默认80 <br>**此阈值设置得越高，检索速度将会越快，推荐使用默认阈值`80`** </item>
        ///           <item>  <c>quality_control</c>: 图片质量控制  **NONE**: 不进行控制 **LOW**:较低的质量要求 **NORMAL**: 一般的质量要求 **HIGH**: 较高的质量要求 **默认 NONE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///           <item>  <c>max_user_num</c>: 查找后返回的用户数量。返回相似度最高的几个用户，默认为1，最多返回50个。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject MultiSearch(string image, string imageType, string groupIdList, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(MULTI_SEARCH);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            aipReq.Bodys["group_id_list"] = groupIdList;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸注册接口
        /// </summary>
        /// <param name="image">图片信息(总数据大小应小于10M)，图片上传方式根据image_type来判断。注：组内每个uid下的人脸图片数目上限为20张</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>user_info</c>: 用户资料，长度限制256B </item>
        ///           <item>  <c>quality_control</c>: 图片质量控制  **NONE**: 不进行控制 **LOW**:较低的质量要求 **NORMAL**: 一般的质量要求 **HIGH**: 较高的质量要求 **默认 NONE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///           <item>  <c>action_type</c>: 操作方式  APPEND: 当user_id在库中已经存在时，对此user_id重复注册时，新注册的图片默认会追加到该user_id下,REPLACE : 当对此user_id重复注册时,则会用新图替换库中该user_id下所有图片,默认使用APPEND </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserAdd(string image, string imageType, string groupId, string userId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_ADD);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["user_id"] = userId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸更新接口
        /// </summary>
        /// <param name="image">图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="groupId">更新指定groupid下uid对应的信息</param>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>user_info</c>: 用户资料，长度限制256B </item>
        ///           <item>  <c>quality_control</c>: 图片质量控制  **NONE**: 不进行控制 **LOW**:较低的质量要求 **NORMAL**: 一般的质量要求 **HIGH**: 较高的质量要求 **默认 NONE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///           <item>  <c>action_type</c>: 操作方式  APPEND: 当user_id在库中已经存在时，对此user_id重复注册时，新注册的图片默认会追加到该user_id下,REPLACE : 当对此user_id重复注册时,则会用新图替换库中该user_id下所有图片,默认使用APPEND </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserUpdate(string image, string imageType, string groupId, string userId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_UPDATE);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["user_id"] = userId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸删除接口
        /// </summary>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="faceToken">需要删除的人脸图片token，（由数字、字母、下划线组成）长度限制64B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject FaceDelete(string userId, string groupId, string faceToken, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(FACE_DELETE);
            
            aipReq.Bodys["user_id"] = userId;
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["face_token"] = faceToken;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 用户信息查询接口
        /// </summary>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserGet(string userId, string groupId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_GET);
            
            aipReq.Bodys["user_id"] = userId;
            aipReq.Bodys["group_id"] = groupId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 获取用户人脸列表接口
        /// </summary>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject FaceGetlist(string userId, string groupId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(FACE_GETLIST);
            
            aipReq.Bodys["user_id"] = userId;
            aipReq.Bodys["group_id"] = groupId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 获取用户列表接口
        /// </summary>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>start</c>: 默认值0，起始序号 </item>
        ///           <item>  <c>length</c>: 返回数量，默认值100，最大值1000 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupGetusers(string groupId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_GETUSERS);
            
            aipReq.Bodys["group_id"] = groupId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 复制用户接口
        /// </summary>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>src_group_id</c>: 从指定组里复制信息 </item>
        ///           <item>  <c>dst_group_id</c>: 需要添加用户的组id </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserCopy(string userId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_COPY);
            
            aipReq.Bodys["user_id"] = userId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 删除用户接口
        /// </summary>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="userId">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserDelete(string groupId, string userId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DELETE);
            
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["user_id"] = userId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 创建用户组接口
        /// </summary>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupAdd(string groupId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_ADD);
            
            aipReq.Bodys["group_id"] = groupId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 删除用户组接口
        /// </summary>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupDelete(string groupId, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_DELETE);
            
            aipReq.Bodys["group_id"] = groupId;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 组列表查询接口
        /// </summary>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>start</c>: 默认值0，起始序号 </item>
        ///           <item>  <c>length</c>: 返回数量，默认值100，最大值1000 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupGetlist(Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_GETLIST);
            
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 身份验证接口
        /// </summary>
        /// <param name="image">图片信息(**总数据大小应小于10M**)，图片上传方式根据image_type来判断</param>
        /// <param name="imageType">图片类型     <br> **BASE64**:图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M； <br>**URL**:图片的 URL地址( 可能由于网络等原因导致下载图片时间过长)； <br>**FACE_TOKEN**: 人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。</param>
        /// <param name="idCardNumber">身份证号（真实身份证号号码）</param>
        /// <param name="name">utf8，姓名（真实姓名，和身份证号匹配）</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>quality_control</c>: 图片质量控制  **NONE**: 不进行控制 **LOW**:较低的质量要求 **NORMAL**: 一般的质量要求 **HIGH**: 较高的质量要求 **默认 NONE** </item>
        ///           <item>  <c>liveness_control</c>: 活体检测控制  **NONE**: 不进行控制 **LOW**:较低的活体要求(高通过率 低攻击拒绝率) **NORMAL**: 一般的活体要求(平衡的攻击拒绝率, 通过率) **HIGH**: 较高的活体要求(高攻击拒绝率 低通过率) **默认NONE** </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject PersonVerify(string image, string imageType, string idCardNumber, string name, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(PERSON_VERIFY);
            
            aipReq.Bodys["image"] = image;
            aipReq.Bodys["image_type"] = imageType;
            aipReq.Bodys["id_card_number"] = idCardNumber;
            aipReq.Bodys["name"] = name;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 语音校验码接口接口
        /// </summary>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>appid</c>: 百度云创建应用时的唯一标识ID </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject VideoSessioncode(Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VIDEO_SESSIONCODE);
            
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}