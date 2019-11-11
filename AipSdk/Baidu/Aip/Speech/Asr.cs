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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Speech
{
    /// <summary>
    ///     语音识别相关接口
    /// </summary>
    public class Asr : Base
    {
        public const string UrlAsr = "https://vop.baidu.com/server_api";
        public const string UrlAsrPro = "https://vop.baidu.com/pro_api";
        public const string UrlAsrStream = "https://vop.baidu.com/open/asr";

        //8k  2560 16k 5120
        private const int AsrStreamingChunkDataSize = 2560;

        public Asr(string appId, string apiKey, string secretKey) : base(appId, apiKey, secretKey)
        {

        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json
            };
        }

        /// <summary>
        ///     识别语音数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject Recognize(byte[] data, string format, int rate, Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(data, "data");
            CheckNotNull(format, "format");
            var req = DefaultRequest(UrlAsr);
            req.Bodys["format"] = format;
            req.Bodys["rate"] = rate;

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;

            if (!req.Bodys.ContainsKey("channel"))
                req.Bodys["channel"] = 1;
            req.Bodys["len"] = data.Length;
            req.Bodys["speech"] = Convert.ToBase64String(data);
            req.Bodys["token"] = Token;
            return PostAction(req);
        }

        /// <summary>
        ///     识别URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject Recognize(string url, string callback, string format, int rate,
            Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(url, "url");
            CheckNotNull(format, "format");
            CheckNotNull(callback, "callback");
            var req = DefaultRequest(UrlAsr);

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            req.Bodys["url"] = url;
            req.Bodys["callback"] = callback;
            req.Bodys["format"] = format;
            req.Bodys["rate"] = rate;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;

            if (!req.Bodys.ContainsKey("channel"))
                req.Bodys["channel"] = 1;
            req.Bodys["token"] = Token;
            return PostAction(req);
        }

        /// <summary>
        /// 语音识别极速版
        /// </summary>
        /// <param name="data">语音数据</param>
        /// <param name="format">格式：pcm amr</param>
        /// <param name="rate">16000</param>
        /// <param name="devPid"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject RecognizePro(byte[] data, string format, int rate = 16000, int devPid = 80001, Dictionary<string, object> options = null)
        {
            PreAction();
            CheckNotNull(data, "data");
            CheckNotNull(format, "format");
            var req = DefaultRequest(UrlAsrPro);
            req.Bodys["format"] = format;
            req.Bodys["rate"] = rate;

            if (options != null)
                foreach (var pair in options)
                    req.Bodys[pair.Key] = pair.Value;
            if (!req.Bodys.ContainsKey("cuid"))
                req.Bodys["cuid"] = Cuid;

            if (!req.Bodys.ContainsKey("channel"))
                req.Bodys["channel"] = 1;
            req.Bodys["len"] = data.Length;
            req.Bodys["speech"] = Convert.ToBase64String(data);
            req.Bodys["token"] = Token;
            req.Bodys["dev_pid"] = devPid;
            return PostAction(req);
        }

        /// <summary>
        /// 流式识别接口(experimental)
        /// </summary>
        /// <param name="speech">语音数据，CanRead</param>
        /// <param name="cuid">设备ID，可以填写mac地址</param>
        /// <param name="format">pcm</param>
        /// <param name="rate">16000</param>
        /// <param name="pid">模型id，具体值参考文档</param>
        /// <returns></returns>
        /// <exception cref="AipException"></exception>
        public JObject Recognize(Stream speech, string cuid, string format, int rate, int pid)
        {
            var asrBegin = new Dictionary<string, object>()
            {
                {"apikey", ApiKey},
                {"secretkey", SecretKey},
                {"appid", int.Parse(AppId)},
                {"cuid", cuid},
                {"sample_rate", rate},
                {"format", format},
                {"task_id", pid},
            };
            var asrBeginBodyStr = JsonConvert.SerializeObject(asrBegin, Formatting.None);
            var beginPktBytes = new TlvPacket(TlvType.AsrBegin, asrBeginBodyStr).ToBytes();

            var buf = new byte[AsrStreamingChunkDataSize];
            var bytesReadAmt = speech.Read(buf, 0, AsrStreamingChunkDataSize);
            if (bytesReadAmt == 0)
            {
                throw new AipException("Speech bytes stream empty");
            }

            var reqId = $"{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", "");
            var urlWithId = $"{UrlAsrStream}?id={reqId}";
            var req = (HttpWebRequest) WebRequest.Create(urlWithId);
            req.Method = "POST";
            req.ReadWriteTimeout = this.Timeout;
            req.Timeout = this.Timeout;
            req.SendChunked = true;
            req.AllowWriteStreamBuffering = false;
            req.ContentType = "application/octet-stream";

            var reqStream = req.GetRequestStream();
            reqStream.Write(beginPktBytes, 0, beginPktBytes.Length);
            while (bytesReadAmt > 0)
            {
                var doingPktBytes = new TlvPacket(TlvType.AsrData, buf, bytesReadAmt).ToBytes();
                reqStream.Write(doingPktBytes, 0, doingPktBytes.Length);
                bytesReadAmt = speech.Read(buf, 0, AsrStreamingChunkDataSize);
            }

            var endPktBytes = new TlvPacket(TlvType.AsrEnd).ToBytes();
            reqStream.Write(endPktBytes, 0, endPktBytes.Length);
            reqStream.Close();
            var resp = ((HttpWebResponse) req.GetResponse()).GetResponseStream();

            var buf2 = Utils.StreamToBytes(resp);
            var respPackets = TlvPacket.ParseFromBytes(buf2).ToList();

            if (respPackets.Count == 0)
            {
                throw new AipException("Server return empty");
            }

            var respStr = "";
            try
            {
                respStr = System.Text.Encoding.UTF8.GetString(respPackets[0].V);
                var respObj = JsonConvert.DeserializeObject(respStr) as JObject;
                return respObj;
            }
            catch (Exception e)
            {
                // 非json应该抛异常
                throw new AipException($"{e.Message}{respStr}");
            }
        }
    }
}

