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

namespace Baidu.Aip.Kg
{
    /// <summary>
    /// 知识图谱
    /// </summary>
    public class Pie : AipServiceBase   {
        
        private const string CREATE_TASK =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_create";
        
        private const string UPDATE_TASK =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_update";
        
        private const string TASK_INFO =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_info";
        
        private const string TASK_QUERY =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_query";
        
        private const string TASK_START =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_start";
        
        private const string TASK_STATUS =
            "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_status";
        
        public Pie(string apiKey, string secretKey) : base(apiKey, secretKey)
        {

        }


        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed,
                ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        /// 创建任务接口
        /// 创建一个新的信息抽取任务
        /// </summary>
        /// <param name="name">任务名字</param>
        /// <param name="templateContent">json string 解析模板内容</param>
        /// <param name="inputMappingFile">抓取结果映射文件的路径</param>
        /// <param name="outputFile">输出文件名字</param>
        /// <param name="urlPattern">url pattern</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>limit_count</c>: 限制解析数量limit_count为0时进行全量任务，limit_count&gt;0时只解析limit_count数量的页面 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject CreateTask(string name, string templateContent, string inputMappingFile, string outputFile, string urlPattern, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(CREATE_TASK);
            
            aipReq.Bodys["name"] = name;
            aipReq.Bodys["template_content"] = templateContent;
            aipReq.Bodys["input_mapping_file"] = inputMappingFile;
            aipReq.Bodys["output_file"] = outputFile;
            aipReq.Bodys["url_pattern"] = urlPattern;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 更新任务接口
        /// 更新任务配置，在任务重新启动后生效
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>name</c>: 任务名字 </item>
        ///           <item>  <c>template_content</c>: json string 解析模板内容 </item>
        ///           <item>  <c>input_mapping_file</c>: 抓取结果映射文件的路径 </item>
        ///           <item>  <c>url_pattern</c>: url pattern </item>
        ///           <item>  <c>output_file</c>: 输出文件名字 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject UpdateTask(int id, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(UPDATE_TASK);
            
            aipReq.Bodys["id"] = id;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 获取任务详情接口
        /// 根据任务id获取单个任务的详细信息
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaskInfo(int id, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TASK_INFO);
            
            aipReq.Bodys["id"] = id;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 以分页的方式查询当前用户所有的任务信息接口
        /// 该请求用于菜品识别。即对于输入的一张图片（可正常解码，且长宽比适宜），输出图片的菜品名称、卡路里信息、置信度。
        /// </summary>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///           <item>  <c>id</c>: 任务ID，精确匹配 </item>
        ///           <item>  <c>name</c>: 中缀模糊匹配,abc可以匹配abc,aaabc,abcde等 </item>
        ///           <item>  <c>status</c>: 要筛选的任务状态 </item>
        ///           <item>  <c>page</c>: 页码 </item>
        ///           <item>  <c>per_page</c>: 页码 </item>
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaskQuery(Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TASK_QUERY);
            
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 启动任务接口
        /// 启动一个已经创建的信息抽取任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaskStart(int id, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TASK_START);
            
            aipReq.Bodys["id"] = id;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 查询任务状态接口
        /// 查询指定的任务的最新执行状态
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="options"> 可选参数对象，key: value都为string类型，可选的参数包括
        ///     <list type="bullet">
        ///     </list>
        /// </param>
        /// <return>JObject</return>
        ///
        public JObject TaskStatus(int id, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(TASK_STATUS);
            
            aipReq.Bodys["id"] = id;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}