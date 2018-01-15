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
using System.Text;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Nlp
{
    /// <summary>
    /// 自然语言处理
    /// </summary>
    public class Nlp : AipServiceBase   {
        
        private const string LEXER =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/lexer";
        
        private const string LEXER_CUSTOM =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/lexer_custom";
        
        private const string DEP_PARSER =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/depparser";
        
        private const string WORD_EMBEDDING =
            "https://aip.baidubce.com/rpc/2.0/nlp/v2/word_emb_vec";
        
        private const string DNNLM_CN =
            "https://aip.baidubce.com/rpc/2.0/nlp/v2/dnnlm_cn";
        
        private const string WORD_SIM_EMBEDDING =
            "https://aip.baidubce.com/rpc/2.0/nlp/v2/word_emb_sim";
        
        private const string SIMNET =
            "https://aip.baidubce.com/rpc/2.0/nlp/v2/simnet";
        
        private const string COMMENT_TAG =
            "https://aip.baidubce.com/rpc/2.0/nlp/v2/comment_tag";
        
        private const string SENTIMENT_CLASSIFY =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/sentiment_classify";
        
        private const string KEYWORD =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/keyword";
        
        public Nlp(string apiKey, string secretKey) : base(apiKey, secretKey)
        {

        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Json,
                ContentEncoding = Encoding.GetEncoding("GBK")
            };
        }

        /// <summary>
        /// 词法分析接口
        /// 词法分析接口向用户提供分词、词性标注、专名识别三大功能；能够识别出文本串中的基本词汇（分词），对这些词汇进行重组、标注组合后词汇的词性，并进一步识别出命名实体。
        /// </summary>
        /// <param name="text">待分析文本（目前仅支持GBK编码），长度不超过65536字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Lexer(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LEXER);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 词法分析（定制版）接口
        /// 词法分析接口向用户提供分词、词性标注、专名识别三大功能；能够识别出文本串中的基本词汇（分词），对这些词汇进行重组、标注组合后词汇的词性，并进一步识别出命名实体。
        /// </summary>
        /// <param name="text">待分析文本（目前仅支持GBK编码），长度不超过65536字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject LexerCustom(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(LEXER_CUSTOM);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 依存句法分析接口
        /// 依存句法分析接口可自动分析文本中的依存句法结构信息，利用句子中词与词之间的依存关系来表示词语的句法结构信息（如“主谓”、“动宾”、“定中”等结构关系），并用树状结构来表示整句的结构（如“主谓宾”、“定状补”等）。
        /// </summary>
        /// <param name="text">待分析文本（目前仅支持GBK编码），长度不超过256字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>mode</c>: 模型选择。默认值为0，可选值mode=0（对应web模型）；mode=1（对应query模型） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject DepParser(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DEP_PARSER);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 词向量表示接口
        /// 词向量表示接口提供中文词向量的查询功能。
        /// </summary>
        /// <param name="word">文本内容（GBK编码），最大64字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject WordEmbedding(string word, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(WORD_EMBEDDING);
            
            aipReq.Bodys["word"] = word;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// DNN语言模型接口
        /// 中文DNN语言模型接口用于输出切词结果并给出每个词在句子中的概率值,判断一句话是否符合语言表达习惯。
        /// </summary>
        /// <param name="text">文本内容（GBK编码），最大512字节，不需要切词</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject DnnlmCn(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(DNNLM_CN);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 词义相似度接口
        /// 输入两个词，得到两个词的相似度结果。
        /// </summary>
        /// <param name="word1">词1（GBK编码），最大64字节</param>
        /// <param name="word2">词1（GBK编码），最大64字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>mode</c>: 预留字段，可选择不同的词义相似度模型。默认值为0，目前仅支持mode=0 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject WordSimEmbedding(string word1, string word2, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(WORD_SIM_EMBEDDING);
            
            aipReq.Bodys["word_1"] = word1;
            aipReq.Bodys["word_2"] = word2;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 短文本相似度接口
        /// 短文本相似度接口用来判断两个文本的相似度得分。
        /// </summary>
        /// <param name="text1">待比较文本1（GBK编码），最大512字节</param>
        /// <param name="text2">待比较文本2（GBK编码），最大512字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>model</c>: 默认为"BOW"，可选"BOW"、"CNN"与"GRNN" </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Simnet(string text1, string text2, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SIMNET);
            
            aipReq.Bodys["text_1"] = text1;
            aipReq.Bodys["text_2"] = text2;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 评论观点抽取接口
        /// 评论观点抽取接口用来提取一条评论句子的关注点和评论观点，并输出评论观点标签及评论观点极性。
        /// </summary>
        /// <param name="text">评论内容（GBK编码），最大10240字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>type</c>: 评论行业类型，默认为4（餐饮美食） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject CommentTag(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(COMMENT_TAG);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 情感倾向分析接口
        /// 对包含主观观点信息的文本进行情感极性类别（积极、消极、中性）的判断，并给出相应的置信度。
        /// </summary>
        /// <param name="text">文本内容（GBK编码），最大102400字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject SentimentClassify(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(SENTIMENT_CLASSIFY);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 文本标签接口
        /// 文本标签服务能够针对网络各类媒体文章进行快速的内容理解，根据输入含有标题的文章，输出多个内容标签以及对应的置信度，用于个性化推荐、相似文章聚合、文本内容分析等场景。
        /// </summary>
        /// <param name="title">篇章的标题，最大80字节</param>
        /// <param name="content">篇章的正文，最大65535字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Keyword(string title, string content, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(KEYWORD);
            
            aipReq.Bodys["title"] = title;
            aipReq.Bodys["content"] = content;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}