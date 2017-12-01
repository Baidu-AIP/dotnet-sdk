using System;
using System.Collections.Generic;
using System.IO;

namespace Baidu.Aip.Demo
{
    internal class ImageClassifyDemo
    {
        private ImageClassify.ImageClassify client;

        public static void main()
        {
        }

        public void init()
        {
            client = new ImageClassify.ImageClassify("你的Api Key", "你的Secret Key");
        }

        public void DishDetectDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用菜品识别
            var result = client.DishDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"top_num", 3}
            };
            // 带参数调用菜品识别
            result = client.DishDetect(image, options);
            Console.WriteLine(result);
        }


        public void CarDetectDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用车辆识别
            var result = client.CarDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"top_num", 3}
            };
            // 带参数调用车辆识别
            result = client.CarDetect(image, options);
            Console.WriteLine(result);
        }

        public void LogoSearchDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用logo商标识别
            var result = client.LogoSearch(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"custom_lib", true}
            };
            // 带参数调用logo商标识别
            result = client.LogoSearch(image, options);
            Console.WriteLine(result);
        }

        public void LogoAddDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            var brief = "{\"name\": \"宝马\",\"code\":\"666\"}";

            // 调用logo入库
            var result = client.LogoAdd(image, brief);
            Console.WriteLine(result);
        }

        public void LogoDeleteByImageDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用删除logo，传入参数为图片
            var result = client.LogoDeleteByImage(image);
            Console.WriteLine(result);
        }

        public void LogoDeleteBySignDemo()
        {
            var contSign = "8cnn32frvrr2cd901";

            // 调用删除logo，传入参数为图片签名
            var result = client.LogoDeleteBySign(contSign);
            Console.WriteLine(result);
        }

        public void ObjectDetectDemo()
        {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用图像主体检测
            var result = client.ObjectDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"with_face", 0}
            };
            // 带参数调用图像主体检测
            result = client.ObjectDetect(image, options);
            Console.WriteLine(result);
        }
        
        public void PlantDetectDemo() {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用植物识别
            var result = client.PlantDetect(image);
            Console.WriteLine(result);
        }
        
        public void AnimalDetectDemo() {
            var image = File.ReadAllBytes("图片文件路径");
            // 调用动物识别
            var result = client.AnimalDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"top_num", 3 } 
            };
            // 带参数调用动物识别
            result = client.AnimalDetect(image, options);
            Console.WriteLine(result);
        }
        
    }
}