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
using System.Net;

namespace Baidu.Aip.Speech
{
    public class Base : AipServiceBase
    {
        public Base(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
            IsDev = true;
        }

        protected string Cuid
        {
            get { return Utils.Md5(Token); }
        }


        protected override void DoAuthorization()
        {
            lock (AuthLock)
            {
                if (!NeetAuth())
                    return;

                var resp = Auth.OpenApiFetchToken(ApiKey, SecretKey, true);

                ExpireAt = DateTime.Now.AddSeconds((int) resp["expires_in"] - 1);
                IsDev = true;
                Token = (string) resp["access_token"];
                HasDoneAuthoried = true;
            }
        }


        protected override HttpWebRequest GenerateWebRequest(AipHttpRequest aipRequest)
        {
            return aipRequest.GenerateSpeechRequest();
        }
    }
}