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
            "https://aip.baidubce.com/rest/2.0/face/v2/detect";
        
        private const string MATCH =
            "https://aip.baidubce.com/rest/2.0/face/v2/match";
        
        private const string IDENTIFY =
            "https://aip.baidubce.com/rest/2.0/face/v2/identify";
        
        private const string VERIFY =
            "https://aip.baidubce.com/rest/2.0/face/v2/verify";
        
        private const string MULTI_IDENTIFY =
            "https://aip.baidubce.com/rest/2.0/face/v2/multi-identify";
        
        private const string USER_ADD =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/add";
        
        private const string USER_UPDATE =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/update";
        
        private const string USER_DELETE =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/delete";
        
        private const string USER_GET =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/get";
        
        private const string GROUP_GETLIST =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/getlist";
        
        private const string GROUP_GETUSERS =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/getusers";
        
        private const string GROUP_ADDUSER =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/adduser";
        
        private const string GROUP_DELETEUSER =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/deleteuser";
        
        public Face(string apiKey, string secretKey) : base(apiKey, secretKey)
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
        /// 人脸检测接口
        /// </summary>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>max_face_num</c>: 最多处理人脸数目，默认值1 </item>
        ///           <item>  <c>face_fields</c>: 包括age,beauty,expression,faceshape,gender,glasses,landmark,race,qualities信息，逗号分隔，默认只返回人脸框、概率和旋转角度 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Detect(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DETECT);
            
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸比对接口
        /// </summary>
        /// <param name="images">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>ext_fields</c>: 返回质量信息，取值固定:目前支持qualities(质量检测)。(对所有图片都会做改处理) </item>
        ///           <item>  <c>image_liveness</c>: 返回的活体信息，“faceliveness,faceliveness” 表示对比对的两张图片都做活体检测；“,faceliveness” 表示对第一张图片不做活体检测、第二张图做活体检测；“faceliveness,” 表示对第一张图片做活体检测、第二张图不做活体检测；<br>**注：需要用于判断活体的图片，图片中的人脸像素面积需要不小于100px\*100px，人脸长宽与图片长宽比例，不小于1/3** </item>
        ///           <item>  <c>types</c>: 请求对比的两张图片的类型，示例：“7,13”<br>**12**表示带水印证件照：一般为带水印的小图，如公安网小图<br>**7**表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等<br>**13**表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片，**注**：需要确保人脸部分不可太小，通常为100px\*100px </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Match(IEnumerable<byte[]> images, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(MATCH);
            
            CheckNotNull(images, "images");
            aipReq.Bodys["images"] = ImagesToParams(images);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸识别接口
        /// </summary>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br/>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>ext_fields</c>: 特殊返回信息，多个用逗号分隔，取值固定: 目前支持faceliveness(活体检测)。**注：需要用于判断活体的图片，图片中的人脸像素面积需要不小于100px\*100px，人脸长宽与图片长宽比例，不小于1/3** </item>
        ///           <item>  <c>user_top_num</c>: 返回用户top数，默认为1，最多返回5个 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Identify(string groupId, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(IDENTIFY);
            
            aipReq.Bodys["group_id"] = groupId;
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸认证接口
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br/>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>top_num</c>: 返回用户top数，默认为1 </item>
        ///           <item>  <c>ext_fields</c>: 特殊返回信息，多个用逗号分隔，取值固定: 目前支持faceliveness(活体检测)。**注：需要用于判断活体的图片，图片中的人脸像素面积需要不小于100px\*100px，人脸长宽与图片长宽比例，不小于1/3** </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Verify(string uid, string groupId, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(VERIFY);
            
            aipReq.Bodys["uid"] = uid;
            aipReq.Bodys["group_id"] = groupId;
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// M:N 识别接口
        /// </summary>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br/>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>ext_fields</c>: 特殊返回信息，多个用逗号分隔，取值固定: 目前支持faceliveness(活体检测)。**注：需要用于判断活体的图片，图片中的人脸像素面积需要不小于100px\*100px，人脸长宽与图片长宽比例，不小于1/3** </item>
        ///           <item>  <c>detect_top_num</c>: 检测多少个人脸进行比对，默认值1（最对返回10个） </item>
        ///           <item>  <c>user_top_num</c>: 返回识别结果top人数”，当同一个人有多张图片时，只返回比对最高的1个分数（即，scores参数只有一个值），默认为1（最多返回20个） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject MultiIdentify(string groupId, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(MULTI_IDENTIFY);
            
            aipReq.Bodys["group_id"] = groupId;
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸注册接口
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="userInfo">用户资料，长度限制256B</param>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>action_type</c>: 参数包含append、replace。**如果为“replace”，则每次注册时进行替换replace（新增或更新）操作，默认为append操作**。例如：uid在库中已经存在时，对此uid重复注册时，新注册的图片默认会**追加**到该uid下，如果手动选择`action_type:replace`，则会用新图替换库中该uid下所有图片。 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserAdd(string uid, string userInfo, string groupId, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_ADD);
            
            aipReq.Bodys["uid"] = uid;
            aipReq.Bodys["user_info"] = userInfo;
            aipReq.Bodys["group_id"] = groupId;
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸更新接口
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="userInfo">用户资料，长度限制256B</param>
        /// <param name="groupId">更新指定groupid下uid对应的信息</param>
        /// <param name="image">二进制图像数据</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>action_type</c>: 目前仅支持replace，uid不存在时，不报错，会自动变为注册操作；未选择该参数时，如果uid不存在会提示错误 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserUpdate(string uid, string userInfo, string groupId, byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_UPDATE);
            
            aipReq.Bodys["uid"] = uid;
            aipReq.Bodys["user_info"] = userInfo;
            aipReq.Bodys["group_id"] = groupId;
            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 人脸删除接口
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>group_id</c>: 删除指定groupid下uid对应的信息 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserDelete(string uid, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DELETE);
            
            aipReq.Bodys["uid"] = uid;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 用户信息查询接口
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>group_id</c>: 选择指定group_id则只查找group列表下的uid内容，如果不指定则查找所有group下对应uid的信息 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UserGet(string uid, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_GET);
            
            aipReq.Bodys["uid"] = uid;
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
        ///           <item>  <c>end</c>: 返回数量，默认值100，最大值1000 </item>
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
        /// 组内用户列表查询接口
        /// </summary>
        /// <param name="groupId">用户组id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>start</c>: 默认值0，起始序号 </item>
        ///           <item>  <c>end</c>: 返回数量，默认值100，最大值1000 </item>
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
        /// 组间复制用户接口
        /// </summary>
        /// <param name="srcGroupId">从指定group里复制信息</param>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br/>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupAdduser(string srcGroupId, string groupId, string uid, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_ADDUSER);
            
            aipReq.Bodys["src_group_id"] = srcGroupId;
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["uid"] = uid;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 组内删除用户接口
        /// </summary>
        /// <param name="groupId">用户组id，标识一组用户（由数字、字母、下划线组成），长度限制128B。如果需要将一个uid注册到多个group下，group\_id需要用多个逗号分隔，每个group_id长度限制为48个英文字符。**注：group无需单独创建，注册用户时则会自动创建group。**<br/>**产品建议**：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费等，这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率</param>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject GroupDeleteuser(string groupId, string uid, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(GROUP_DELETEUSER);
            
            aipReq.Bodys["group_id"] = groupId;
            aipReq.Bodys["uid"] = uid;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}