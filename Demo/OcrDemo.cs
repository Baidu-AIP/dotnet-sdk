using System;
using System.Collections.Generic;
using System.IO;
using Baidu.Aip.Ocr;

namespace Baidu.Aip.Demo
{
    internal class OcrDemo
    {
        public static void GeneralBasic()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 通用文字识别
            var result = client.GeneralBasic(image);

            // 图片url
            result = client.GeneralBasic("https://www.baidu.com/img/bd_logo1.png");
        }

        public static void GeneralEnhanced()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 带生僻字版
            var result = client.GeneralEnhanced(image);
        }

        public static void GeneralWithLocatin()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 带位置版本
            var result = client.GeneralWithLocatin(image, null);
        }

        public static void WebImage()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 网图识别
            var result = client.WebImage(image, null);
        }

        public static void Accurate()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 高精度识别
            var result = client.Accurate(image);
        }

        public static void AccurateWithLocation()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 高精度识别(带位置信息)
            var result = client.AccurateWithLocation(image);
        }


        public static void BankCard()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            // 银行卡识别
            var result = client.BankCard(image);
        }

        public static void Idcard()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");

            var options = new Dictionary<string, object>
            {
                {"detect_direction", "true"} // 检测方向
            };
            // 身份证正面识别
            var result = client.IdCardFront(image, options);
            // 身份证背面识别
            result = client.IdCardBack(image);
        }

        public static void DrivingLicense()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var result = client.DrivingLicense(image);
        }

        public static void VehicleLicense()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var result = client.VehicleLicense(image);
        }

        public static void PlateLicense()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var result = client.PlateLicense(image);
        }

        public static void Receipt()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var options = new Dictionary<string, object>
            {
                {"recognize_granularity", "small"} // 定位单字符位置
            };
            var result = client.Receipt(image, options);
        }


        public static void BusinessLicense()
        {
            var client = new Ocr.Ocr("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var result = client.BusinessLicense(image);
        }

        public static void FormBegin()
        {
            var form = new Form("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            form.DebugLog = false; // 是否开启调试日志

            var result = form.BeginRecognition(image);
            Console.Write(result);
        }

        public static void FormGetResult()
        {
            var form = new Form("Api Key", "Secret Key");
            var options = new Dictionary<string, object>
            {
                {"result_type", "json"} // 或者为excel
            };
            var result = form.GetRecognitionResult("123344", options);
            Console.Write(result);
        }

        public static void FormToJson()
        {
            var form = new Form("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            form.DebugLog = false; // 是否开启调试日志

            // 识别为Json
            var result = form.RecognizeToJson(image);
            Console.Write(result);
        }

        public static void FormToExcel()
        {
            var form = new Form("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            form.DebugLog = false; // 是否开启调试日志

            // 识别为Excel
            var result = form.RecognizeToExcel(image);
            Console.Write(result);
        }
    }
}