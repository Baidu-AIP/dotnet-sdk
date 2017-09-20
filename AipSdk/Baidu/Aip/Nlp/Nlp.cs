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
using System.Text;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Nlp
{
    using Options = Dictionary<string, object>;

	/// <summary>
	///     自然语言处理
	/// </summary>
	public class Nlp : AipServiceBase
    {
        private const string NLP_WordSeg = "https://aip.baidubce.com/rpc/2.0/nlp/v1/wordseg";
        private const string NLP_WordPos = "https://aip.baidubce.com/rpc/2.0/nlp/v1/wordpos";
        private const string NLP_WordEmbeddingVector = "https://aip.baidubce.com/rpc/2.0/nlp/v2/word_emb_vec";
        private const string NLP_WordEmbeddingSimilarity = "https://aip.baidubce.com/rpc/2.0/nlp/v2/word_emb_sim";
        private const string NLP_DNNLM = "https://aip.baidubce.com/rpc/2.0/nlp/v2/dnnlm_cn";
        private const string NLP_SimNet = "https://aip.baidubce.com/rpc/2.0/nlp/v2/simnet";
        private const string NLP_CommentTag = "https://aip.baidubce.com/rpc/2.0/nlp/v2/comment_tag";
        private const string NLP_Lexer = "https://aip.baidubce.com/rpc/2.0/nlp/v1/lexer";

        private const string NLP_SentimentClassify = "https://aip.baidubce.com/rpc/2.0/nlp/v1/sentiment_classify";
        private const string NLP_DepParsery = "https://aip.baidubce.com/rpc/2.0/nlp/v1/depparser";


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

//	    /// <summary>
//	    /// 接口调用
//	    /// </summary>
//	    /// <param name="type"></param>
//	    /// <param name="options"></param>
//	    /// <returns></returns>
//	    public JObject Invoke(Type type, Dictionary<string, object> options)
//	    {
//	        CheckNotNull(options, "options");
//	        PreAction();
//	        var req = new AipHttpRequest(type.Url)
//	        {
//	            Method = "POST",
//	            BodyType = AipHttpRequest.BodyFormat.Json,
//	            ContentEncoding = Encoding.GetEncoding("GBK")
//	        };
//            foreach (var kv in options)
//            {
//                req.Bodys.Add(kv.Key, kv.Value);
//            }
//	        return PostAction(req);
//	    }


	    /// <summary>
	    ///     中文分词
	    /// </summary>
	    /// <param name="query">文本内容</param>
	    /// <param name="options">附加参数</param>
	    /// <returns></returns>
	    [Obsolete("Use Lexer instead.")]
        public JObject WordSeg(string query, Dictionary<string, object> options = null)
        {
            PreAction();
            var req = DefaultRequest(NLP_WordSeg);
            if (options != null)
                foreach (var kv in options)
                    req.Bodys.Add(kv.Key, kv.Value);
            req.Bodys["query"] = query;
            return PostAction(req);
        }

	    /// <summary>
	    ///     词性标注
	    /// </summary>
	    /// <param name="query">文本内容</param>
	    /// <returns></returns>
	    [Obsolete("Use Lexer instead.")]
        public JObject WordPos(string query)
        {
            PreAction();
            var req = DefaultRequest(NLP_WordPos);
            req.Bodys["query"] = query;
            return PostAction(req);
        }

	    /// <summary>
	    ///     短文本相似度v2
	    /// </summary>
	    /// <param name="text1">文本1</param>
	    /// <param name="text2">文本2</param>
	    /// <param name="options"></param>
	    /// <returns></returns>
	    public JObject Simnet(string text1, string text2, Dictionary<string, object> options = null)
        {
            PreAction();
            var req = DefaultRequest(NLP_SimNet);
            if (options != null)
                foreach (var kv in options)
                    req.Bodys.Add(kv.Key, kv.Value);
            req.Bodys["text_1"] = text1;
            req.Bodys["text_2"] = text2;
            return PostAction(req);
        }


	    /// <summary>
	    ///     中文词向量表示
	    /// </summary>
	    /// <param name="query">文本内容</param>
	    /// <param name="options">附加参数</param>
	    /// <returns></returns>
	    public JObject WordEmbeddingVector(string query, Dictionary<string, object> options = null)
        {
            PreAction();
            var req = DefaultRequest(NLP_WordEmbeddingVector);
            if (options != null)
                foreach (var kv in options)
                    req.Bodys.Add(kv.Key, kv.Value);
            req.Bodys["word"] = query;

            return PostAction(req);
        }


	    /// <summary>
	    ///     中文词义相似度
	    /// </summary>
	    /// <param name="word1">单词1</param>
	    /// <param name="word2">单词2</param>
	    /// <param name="options">附加参数</param>
	    /// <returns></returns>
	    public JObject WordEmbeddingSimilarity(string word1, string word2, Dictionary<string, object> options = null)
        {
            PreAction();
            var req = DefaultRequest(NLP_WordEmbeddingSimilarity);
            if (options != null)
                foreach (var kv in options)
                    req.Bodys.Add(kv.Key, kv.Value);
            req.Bodys["word_1"] = word1;
            req.Bodys["word_2"] = word2;

            return PostAction(req);
        }


	    /// <summary>
	    ///     中文DNN语言模型
	    /// </summary>
	    /// <param name="text">文本内容</param>
	    /// <returns></returns>
	    public JObject DNN_LM_Cn(string text)
        {
            PreAction();
            var req = DefaultRequest(NLP_DNNLM);
            req.Bodys["text"] = text;
            return PostAction(req);
        }

	    /// <summary>
	    ///     评论观点抽取
	    /// </summary>
	    /// <param name="text">文本内容</param>
	    /// <param name="type">供12个类别,默认类别为4（餐厅）</param>
	    /// <returns></returns>
	    public JObject CommentTag(string text, int type)
        {
            PreAction();
            var req = DefaultRequest(NLP_CommentTag);
            req.Bodys["text"] = text;
            req.Bodys["type"] = type;
            return PostAction(req);
        }

	    /// <summary>
	    ///     词法分析
	    /// </summary>
	    /// <param name="text">文本内容</param>
	    /// <returns></returns>
	    public JObject Lexer(string text)
        {
            PreAction();
            var req = DefaultRequest(NLP_Lexer);
            req.Bodys["text"] = text;
            return PostAction(req);
        }

	    /// <summary>
	    ///     情感倾向分析
	    /// </summary>
	    /// <param name="text">文本内容</param>
	    /// <returns></returns>
	    public JObject SentimentClassify(string text)
        {
            PreAction();
            var req = DefaultRequest(NLP_SentimentClassify);
            req.Bodys["text"] = text;
            return PostAction(req);
        }

	    /// <summary>
	    ///     依存句法分析
	    /// </summary>
	    /// <param name="text"></param>
	    /// <param name="options"></param>
	    /// <returns></returns>
	    public JObject DepParser(string text, Dictionary<string, object> options = null)
        {
            PreAction();
            var req = DefaultRequest(NLP_DepParsery);
            if (options != null)
                foreach (var kv in options)
                    req.Bodys.Add(kv.Key, kv.Value);
            req.Bodys["text"] = text;
            return PostAction(req);
        }
    }
}