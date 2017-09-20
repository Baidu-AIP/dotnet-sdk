using System;
using System.Collections.Generic;
using System.IO;
using Baidu.Aip.Speech;

namespace Baidu.Aip.Demo
{
    internal class SpeechDemo
    {
        private readonly Asr _asrClient;
        private readonly Tts _ttsClient;

        public SpeechDemo()
        {
            _asrClient = new Asr("Api Key", "Secret Key");
            _ttsClient = new Tts("Api Key", "Secret Key");
        }

        // 识别本地文件
        public void AsrData()
        {
            var data = File.ReadAllBytes("语音pcm文件地址");
            var result = _asrClient.Recognize(data, "pcm", 16000);
            Console.Write(result);
        }

        // 识别URL中的语音文件
        public void AsrUrl()
        {
            var result = _asrClient.Recoginze(
                "http://xxx.com/待识别的pcm文件地址",
                "http://xxx.com/识别结果回调地址",
                "pcm",
                16000);
            Console.WriteLine(result);
        }

        // 合成
        public void Tts()
        {
            // 可选参数
            var option = new Dictionary<string, object>
            {
                {"spd", 5}, // 语速
                {"vol", 7}, // 音量
                {"per", 4} // 发音人，4：情感度丫丫童声
            };
            var result = _ttsClient.Synthesis("众里寻他千百度", option);

            if (result.ErrorCode == 0) // 或 result.Success
                File.WriteAllBytes("合成的语音文件本地存储地址.mp3", result.Data);
        }
    }
}