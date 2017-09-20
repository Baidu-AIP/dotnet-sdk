using System;
using System.Collections.Generic;

namespace Baidu.Aip.Demo
{
    internal class NlpDemo
    {
        public void Lexer()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.Lexer("今天天气不错");
            Console.Write(result);
        }

        public void WordSeg()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.WordSeg("今天天气不错");
            Console.Write(result);
        }


        public static void WordPos()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.WordPos("今天天气不错");
            Console.Write(result);
        }

        public static void WordEmbedding()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            // 词相似度
            var result = nlp.WordEmbeddingSimilarity("北京", "上海");
            Console.Write(result);
            // 词向量
            result = nlp.WordEmbeddingVector("北京");
            Console.Write(result);
        }

        public static void Dnnlm()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.DNN_LM_Cn("今天天气不错");
            Console.Write(result);
        }

        public static void SimNet()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.Simnet("你好百度", "你好世界");
            Console.Write(result);
        }

        public static void CommentTag()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.CommentTag("个人觉得这车不错，外观漂亮年轻，动力和操控性都不错", 10);
            Console.Write(result);
        }

        public static void SentimentClassify()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var result = nlp.SentimentClassify("个人觉得这车不错，外观漂亮年轻，动力和操控性都不错");
            Console.Write(result);
        }

        public static void DepParser()
        {
            var nlp = new Nlp.Nlp("Api Key", "Secret Key");
            var options = new Dictionary<string, object>
            {
                {"mode", 1}
            };
            var result = nlp.DepParser("今天天气不错", options);
            Console.Write(result);
        }
    }
}