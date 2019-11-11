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
        
        private const string TOPIC =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/topic";
        
        private const string ECNET =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/ecnet";
        
        private const string EMOTION =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/emotion";
        
        private const string NEWS_SUMMARY =
            "https://aip.baidubce.com/rpc/2.0/nlp/v1/news_summary";
        
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
        /// 文章标签接口
        /// 文章标签服务能够针对网络各类媒体文章进行快速的内容理解，根据输入含有标题的文章，输出多个内容标签以及对应的置信度，用于个性化推荐、相似文章聚合、文本内容分析等场景。
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

        /// <summary>
        /// 文章分类接口
        /// 对文章按照内容类型进行自动分类，首批支持娱乐、体育、科技等26个主流内容类型，为文章聚类、文本内容分析等应用提供基础技术支持。
        /// </summary>
        /// <param name="title">篇章的标题，最大80字节</param>
        /// <param name="content">篇章的正文，最大65535字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Topic(string title, string content, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TOPIC);
            
            aipReq.Bodys["title"] = title;
            aipReq.Bodys["content"] = content;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 文本纠错接口
        /// 识别输入文本中有错误的片段，提示错误并给出正确的文本结果。支持短文本、长文本、语音等内容的错误识别，纠错是搜索引擎、语音识别、内容审查等功能更好运行的基础模块之一。
        /// </summary>
        /// <param name="text">待纠错文本，输入限制511字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Ecnet(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(ECNET);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 对话情绪识别接口接口
        /// 针对用户日常沟通文本背后所蕴含情绪的一种直观检测，可自动识别出当前会话者所表现出的情绪类别及其置信度，可以帮助企业更全面地把握产品服务质量、监控客户服务质量
        /// </summary>
        /// <param name="text">待识别情感文本，输入限制512字节</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>scene</c>: default（默认项-不区分场景），talk（闲聊对话-如度秘聊天等），task（任务型对话-如导航对话等），customer_service（客服对话-如电信/银行客服等） </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject Emotion(string text, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(EMOTION);
            
            aipReq.Bodys["text"] = text;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 新闻摘要接口接口
        /// 自动抽取新闻文本中的关键信息，进而生成指定长度的新闻摘要
        /// </summary>
        /// <param name="content">字符串（限3000字符数以内）字符串仅支持GBK编码，长度需小于3000字符数（即6000字节），请输入前确认字符数没有超限，若字符数超长会返回错误。正文中如果包含段落信息，请使用"\n"分隔，段落信息算法中有重要的作用，请尽量保留</param>
        /// <param name="maxSummaryLen">此数值将作为摘要结果的最大长度。例如：原文长度1000字，本参数设置为150，则摘要结果的最大长度是150字；推荐最优区间：200-500字</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>title</c>: 字符串（限200字符数）字符串仅支持GBK编码，长度需小于200字符数（即400字节），请输入前确认字符数没有超限，若字符数超长会返回错误。标题在算法中具有重要的作用，若文章确无标题，输入参数的“标题”字段为空即可 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject NewsSummary(string content, int maxSummaryLen, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(NEWS_SUMMARY);
            
            aipReq.Bodys["content"] = content;
            aipReq.Bodys["max_summary_len"] = maxSummaryLen;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}