/*
 * Copyright 2017 Baidu, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the LicenseZ:\Users\baidu\GodHere\Baidu\ggit-local\baidu\aip\csharp-sdk\src\CSharpSdk\Demo\Main.cs for the
 * specific language governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip
{
    public class Auth
    {
        private const string OAUTH_URL = "https://aip.baidubce.com/oauth/2.0/token";

        private Auth()
        {
        }

        /// <summary>
        ///     获取OpenApi的Token
        /// </summary>
        /// <param name="ak"></param>
        /// <param name="sk"></param>
        /// <param name="throws">是否是否throw</param>
        /// <param name="debugLog"></param>
        /// <returns>成功返回token, 失败返回null</returns>
        public static JObject OpenApiFetchToken(string ak, string sk, bool throws = false, bool debugLog = false)
        {
            var querys = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"client_id", ak},
                {"client_secret", sk}
            };
            var url = string.Format("{0}?{1}", OAUTH_URL, Utils.ParseQueryString(querys));
            if (debugLog)
                Console.WriteLine(url);
            var webReq = (HttpWebRequest) WebRequest.Create(url);
            try
            {
                var resp = (HttpWebResponse) webReq.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    var respStr = Utils.StreamToString(resp.GetResponseStream(), Encoding.UTF8);
                    var obj = JsonConvert.DeserializeObject(respStr) as JObject;
                    if (obj["access_token"] != null && obj["expires_in"] != null)
                        return obj;
                    if (throws)
                        throw new AipException("Failed to request token. " + (string) obj["error_description"]);
                    return null;
                }
                if (throws)
                    throw new AipException("Failed to request token. " + resp.StatusCode + resp.StatusDescription);
            }
            catch (Exception e)
            {
                if (throws)
                    throw new AipException("Failed to request token. " + e.Message);
                return null;
            }

            return null;
        }

        #region Cloud Auth

	    /// <summary>
	    ///     签名 host 和 Content-Type
	    /// </summary>
	    /// <param name="aipHttpRequest"></param>
	    /// <returns></returns>
	    private static string CanonicalRequest(AipHttpRequest aipHttpRequest)
        {
            var uri = aipHttpRequest.Uri;

            var canonicalUri = Utils.UriEncode(uri.AbsolutePath);
            var canonicalQuerys = aipHttpRequest.Querys
                .Where(pair => !pair.Key.Equals("authorization"))
                .Select(pair =>
                    new KeyValuePair<string, string>(Utils.UriEncode(pair.Key), Utils.UriEncode(pair.Value)))
                .ToList()
                .OrderBy(pair => pair.Key)
                .Select(pair => string.Format("{0}={1}", pair.Key, Utils.UriEncode(pair.Value, true)))
                .DefaultIfEmpty("")
                .Aggregate((a, b) => a + "&" + b);


            var host = uri.Host;
            if (!(uri.Scheme == "https" && uri.Port == 443) && !(uri.Scheme == "http" && uri.Port == 80))
                host += ":" + uri.Port;
            var canonicalHeaders = "content-type:" + Utils.UriEncode(aipHttpRequest.GeneratedRequest.ContentType, true)
                                   + "\nhost:" + Utils.UriEncode(host);
//			var canonicalHeaders = "host:" + Utils.UriEncode(host);

            var canonicalRequest = string.Format("{0}\n{1}\n{2}\n{3}",
                aipHttpRequest.Method, canonicalUri, canonicalQuerys, canonicalHeaders);
            return canonicalRequest;
        }

        public static void CloudRequest(AipHttpRequest aipReq, string ak, string sk)
        {
            var now = DateTime.Now;
            var expirationInSeconds = 1200;

            var signDate = now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
            var authStringPrefix = "bce-auth-v1/" + ak + "/" + signDate + "/" + expirationInSeconds;
            var signingKey =
                Hex(new HMACSHA256(Encoding.UTF8.GetBytes(sk)).ComputeHash(Encoding.UTF8.GetBytes(authStringPrefix)));

            var canonicalRequestString = CanonicalRequest(aipReq);

            var signature =
                Hex(new HMACSHA256(Encoding.UTF8.GetBytes(signingKey)).ComputeHash(
                    Encoding.UTF8.GetBytes(canonicalRequestString)));

            // 签名content-type 与 host
            var authorization = authStringPrefix + "/content-type;host/" + signature;

            aipReq.GeneratedRequest.Headers.Add("x-bce-date", signDate);
            aipReq.GeneratedRequest.Headers.Add("Authorization", authorization);
        }


	    /// <summary>
	    ///     16进制表示
	    /// </summary>
	    /// <param name="data"></param>
	    /// <returns></returns>
	    private static string Hex(byte[] data)
        {
            var sb = new StringBuilder();
            foreach (var b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        #endregion Cloud Auth
    }
}