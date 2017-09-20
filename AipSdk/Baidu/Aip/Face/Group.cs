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
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Face
{
    public class Group : Base
    {
        public Group(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }


        /// <summary>
        ///     组内用户列表查询
        /// </summary>
        public JObject GetUsers(string groupId, int start = 0, int end = 100)
        {
            CheckNotNull(groupId, "groupId");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_GROUP_GET_USERS_URL);
            req.Bodys.Add("group_id", groupId);
            req.Bodys.Add("start", start);
            req.Bodys.Add("end", end);
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于查询所有的组的列表
        /// </summary>
        public JObject GetAllGroups(int start = 0, int end = 100)
        {
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_GROUP_GET_LIST_URL);
            req.Bodys.Add("start", start);
            req.Bodys.Add("end", end);
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于将已经存在于人脸库中的用户添加到一个新的组。
        /// </summary>
        public JObject AddUser(IEnumerable<string> groupIds, string uid, string srcGroupId)
        {
            CheckNotNull(groupIds, "groupIds");
            CheckNotNull(uid, "uid");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_GROUP_ADD_USER_URL);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            req.Bodys.Add("uid", uid);
            req.Bodys.Add("src_group_id", srcGroupId);
            return PostAction(req);
        }

        /// <summary>
        ///     该请求用于将用户从某个组中删除，但不会删除用户在其它组的信息。当用户仅属于单个分组时，本接口将返回错误。
        /// </summary>
        public JObject DeleteUser(IEnumerable<string> groupIds, string uid)
        {
            CheckNotNull(groupIds, "groupIds");
            CheckNotNull(uid, "uid");
            PreAction();
            var req = DefaultRequest(FACE_SEARCH_FACESET_GROUP_DELETE_USER_URL);
            req.Bodys.Add("group_id", StrJoin(groupIds));
            req.Bodys.Add("uid", uid);
            return PostAction(req);
        }
    }
}