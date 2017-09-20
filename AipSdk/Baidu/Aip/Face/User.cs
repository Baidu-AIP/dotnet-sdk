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
    public class User : Base
    {
        public User(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        ///     人脸查找-人脸注册
        ///     该请求用于从人脸库中新增用户，包括指定用户所在组和上传用户人脸图片。 注：每个用户（uid）所能注册的最大人脸数量为5张。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="uid"></param>
        /// <param name="userInfo"></param>
        /// <param name="groupIds"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public JObject Register(byte[] image, string uid, string userInfo, IEnumerable<string> groupIds,
            string actionType = null)
        {
            CheckNotNull(image, "image");
            CheckNotNull(uid, "uid");
            CheckNotNull(groupIds, "groupIds");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_USER_ADD_URL);
            var imageData = Convert.ToBase64String(image);
            req.Bodys.Add("image", imageData);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            req.Bodys.Add("uid", uid);
            if (!string.IsNullOrEmpty(actionType))
                req.Bodys.Add("action_type", actionType);
            if (userInfo == null)
                userInfo = "";
            req.Bodys.Add("user_info", userInfo);
            return PostAction(req);
        }


        /// <summary>
        ///     该请求用于查询某用户的详细信息。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public JObject GetInfo(string uid)
        {
            CheckNotNull(uid, "uid");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_USER_GET_URL);
            req.Bodys.Add("uid", uid);
            return PostAction(req);
        }


        /// <summary>
        ///     该请求用于从人脸库中更新用户图像，新上传的图像将覆盖原有图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="uid"></param>
        /// <param name="groupId"></param>
        /// <param name="userInfo"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public JObject Update(byte[] image, string uid, string groupId, string userInfo, string actionType = null)
        {
            CheckNotNull(image, "image");
            CheckNotNull(uid, "uid");
            CheckNotNull(groupId, "groupId");
            CheckNotNull(userInfo, "userInfo");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_USER_UPDATE_URL);

            var imageData = Convert.ToBase64String(image);
            req.Bodys.Add("image", imageData);
            req.Bodys.Add("uid", uid);
            req.Bodys.Add("group_id", groupId);
            req.Bodys.Add("user_info", userInfo);
            if (!string.IsNullOrEmpty(actionType))
                req.Bodys.Add("action_type", actionType);
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于从人脸库中删除一个用户，包括用户所有图像和身份信息，同时也将从各个组中把用户删除。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public JObject Delete(string uid)
        {
            CheckNotNull(uid, "uid");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_USER_DELETE_URL);
            req.Bodys.Add("uid", uid);
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于从人脸库指定group中删除一个用户，包括用户所有图像和身份信息。
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public JObject Delete(string uid, IEnumerable<string> groupIds)
        {
            CheckNotNull(uid, "uid");
            CheckNotNull(groupIds, "groupIds");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_USER_DELETE_URL);
            req.Bodys.Add("uid", uid);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于识别上传的图片是否为指定用户。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="uid"></param>
        /// <param name="groupIds"></param>
        /// <param name="topNum"></param>
        /// <param name="extFileds"></param>
        /// <returns></returns>
        public JObject Verify(byte[] image, string uid, IEnumerable<string> groupIds, int topNum = 1,
            IEnumerable<string> extFileds = null)
        {
            CheckNotNull(image, "image");
            CheckNotNull(uid, "uid");
            CheckNotNull(groupIds, "groupIds");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_VERIFY_URL);
            var imageData = Convert.ToBase64String(image);
            req.Bodys.Add("image", imageData);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            req.Bodys.Add("uid", uid);
            req.Bodys.Add("top_num", topNum);
            if (extFileds != null)
                req.Bodys.Add("ext_fields", StrJoin(extFileds));

            return PostAction(req);
        }

        /// <summary>
        ///     指定组内用户与上传图像的相似度。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="groupIds"></param>
        /// <param name="userTopNum"></param>
        /// <param name="faceTopNum"></param>
        /// <param name="extFileds"></param>
        /// <returns></returns>
        public JObject Identify(byte[] image, IEnumerable<string> groupIds, int userTopNum = 1, int faceTopNum = 1,
            IEnumerable<string> extFileds = null)
        {
            CheckNotNull(image, "image");
            CheckNotNull(groupIds, "groupIds");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_IDENTIFY_URL);
            var imageData = Convert.ToBase64String(image);
            req.Bodys.Add("images", imageData);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            req.Bodys.Add("user_top_num", userTopNum);
            req.Bodys.Add("fcae_top_num", faceTopNum);
            if (extFileds != null)
                req.Bodys.Add("ext_fields", StrJoin(extFileds));
            return PostAction(req);
        }
    }
}