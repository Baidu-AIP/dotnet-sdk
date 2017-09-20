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
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Ocr
{
    /// <summary>
    ///     表格识别
    /// </summary>
    public class Form : Base
    {
        private const string UrlFormRequest = "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request";

        private const string UrlFormResult =
            "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/get_request_result";


        public Form(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        protected override void Log(string msg)
        {
            base.Log("Ocr-Form: " + msg);
        }

        #region Async interface

        /// <summary>
        ///     发起识别请求，返回JObject, 包含requestId
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject BeginRecognition(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(UrlFormRequest);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["image"] = Convert.ToBase64String(image);
            return PostAction(aipReq);
        }

        /// <summary>
        ///     根据requestId 获得Json结果
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject GetRecognitionResult(string requestId, Dictionary<string, object> options = null)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlFormResult);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["request_id"] = requestId;
            return PostAction(aipReq);
        }

        #endregion

        #region Synchronized Interface

        /// <summary>
        ///     识别表格
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds"></param>
        /// <param name="beginOptions">请求识别时的可选参数</param>
        /// <param name="resultOptions">轮询结果时的可选参数</param>
        /// <returns></returns>
        /// <exception cref="AipException"></exception>
        public JObject Recognize(
            byte[] image,
            long timeoutMiliseconds = 10000,
            Dictionary<string, object> beginOptions = null,
            Dictionary<string, object> resultOptions = null
        )
        {
            var watch = Stopwatch.StartNew();
            var formBeginResp = BeginRecognition(image, beginOptions);
            if (!(formBeginResp["result"] is JArray) || ((JArray) formBeginResp["result"]).Count != 1)
                return formBeginResp;
            var requestId = formBeginResp["result"][0]["request_id"].ToString();
            Log("Form recognize: wait for result...");
            while (true)
            {
                if (watch.ElapsedMilliseconds >= timeoutMiliseconds)
                {
                    Log("Timeout!");
                    throw new AipException("SDK Error: Timeout for form recognition");
                }
                var tempResp = GetRecognitionResult(requestId, resultOptions);
                JToken tp;
                if (tempResp.TryGetValue("error_code", out tp))
                {
                    Log("Fail!");
                    return tempResp;
                }
                if ((int) tempResp["result"]["ret_code"] == 3)
                {
                    // 成功
                    Log("Success!");
                    return tempResp;
                }
                Log("Not ready, wait 1s..." + tempResp);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        ///     识别为Json结果返回。 会在后台自动刷新结果，直至返回结果或超时
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds">超时时间</param>
        /// <param name="beginOptions"></param>
        /// <param name="resultOptions"></param>
        /// <returns></returns>
        public JObject RecognizeToJson(
            byte[] image,
            long timeoutMiliseconds = 10000,
            Dictionary<string, object> beginOptions = null,
            Dictionary<string, object> resultOptions = null)
        {
            if (resultOptions == null)
                resultOptions = new Dictionary<string, object>();
            resultOptions["result_type"] = "json";
            return Recognize(image, timeoutMiliseconds, beginOptions, resultOptions);
        }

        /// <summary>
        ///     识别为Excel结果返回。 会在后台自动刷新结果，直至返回结果或超时
        /// </summary>
        /// <param name="image"></param>
        /// <param name="timeoutMiliseconds"></param>
        /// <param name="beginOptions"></param>
        /// <param name="resultOptions"></param>
        /// <returns></returns>
        public JObject RecognizeToExcel(
            byte[] image,
            long timeoutMiliseconds = 10000,
            Dictionary<string, object> beginOptions = null,
            Dictionary<string, object> resultOptions = null)
        {
            if (resultOptions == null)
                resultOptions = new Dictionary<string, object>();
            resultOptions["result_type"] = "excel";
            return Recognize(image, timeoutMiliseconds, beginOptions, resultOptions);
        }

//        
//        /// <summary>
//        /// 识别为excel，并存入文件
//        /// </summary>
//        /// <param name="image"></param>
//        /// <param name="outputFile"></param>
//        /// <param name="timeoutMiliseconds">超时时间</param>
//        /// <returns></returns>
//        public JObject RecognizeToExcel(byte[] image, out string outputFile, long timeoutMiliseconds)
//        {
//            
//        }

        #endregion
    }
}