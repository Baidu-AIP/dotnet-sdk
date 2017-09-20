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

using System.Text;

namespace Baidu.Aip.Face
{
    public abstract class Base : AipServiceBase
    {
        protected const string FACE_DETECT_URL = "https://aip.baidubce.com/rest/2.0/face/v1/detect";
        protected const string FACE_MATCH_URL = "https://aip.baidubce.com/rest/2.0/face/v2/match";

        protected const string FACE_SEARCH_FACESET_USER_ADD_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/add";

        protected const string FACE_SEARCH_FACESET_USER_UPDATE_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/update";

        protected const string FACE_SEARCH_FACESET_USER_DELETE_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/delete";

        protected const string FACE_SEARCH_VERIFY_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/verify";

        protected const string FACE_SEARCH_IDENTIFY_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/identify";

        protected const string FACE_SEARCH_FACESET_USER_GET_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/get";

        protected const string FACE_SEARCH_FACESET_GROUP_GET_LIST_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/getlist";

        protected const string FACE_SEARCH_FACESET_GROUP_GET_USERS_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/getusers";

        protected const string FACE_SEARCH_FACESET_GROUP_ADD_USER_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/adduser";

        protected const string FACE_SEARCH_FACESET_GROUP_DELETE_USER_URL =
            "https://aip.baidubce.com/rest/2.0/face/v2/faceset/group/deleteuser";


        protected Base(string apiKey, string secretKey) : base(apiKey, secretKey)
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
    }
}