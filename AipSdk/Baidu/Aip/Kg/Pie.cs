using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Kg
{
    public class Pie : Base
    {
        private const string UrlTaskQuey = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_query";
        private const string UrlTaskInfo = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_info";
        private const string UrlTaskCreate = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_create";
        private const string UrlTaskUpdate = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_update";
        private const string UrlTaskStart = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_start";
        private const string UrlTaskStatus = "https://aip.baidubce.com/rest/2.0/kg/v1/pie/task_status";

        public Pie(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        ///     以分页的方式查询当前用户所有的任务信息。默认为第1页，每页10个。
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject GetTasks(Dictionary<string, object> options = null)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskQuey);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            return PostAction(aipReq);
        }

        /// <summary>
        ///     根据任务id获取单个任务的详细信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public JObject GetTaskInfo(int taskId)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskInfo);
            aipReq.Bodys.Add("id", taskId);
            return PostAction(aipReq);
        }

        /// <summary>
        ///     更新一个已经创建的信息抽取任务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="templateContent"></param>
        /// <param name="inputMappingFile"></param>
        /// <param name="urlPattern"></param>
        /// <param name="outputFile"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject TaskCreate(string name, string templateContent, string inputMappingFile, string urlPattern,
            string outputFile, Dictionary<string, object> options = null)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskCreate);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["name"] = name;
            aipReq.Bodys["template_content"] = templateContent;
            aipReq.Bodys["input_mapping_file"] = inputMappingFile;
            aipReq.Bodys["url_pattern"] = urlPattern;
            aipReq.Bodys["output_file"] = outputFile;
            return PostAction(aipReq);
        }

        /// <summary>
        ///     更新一个已经创建的信息抽取任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject UpdateTask(int id, Dictionary<string, object> options = null)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskUpdate);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["id"] = id;
            return PostAction(aipReq);
        }

        /// <summary>
        ///     启动一个已经创建的信息抽取任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JObject StartTask(int id)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskStart);
            aipReq.Bodys["id"] = id;
            return PostAction(aipReq);
        }

        /// <summary>
        ///     查询指定的任务的最新执行状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JObject GetTaskStatus(int id)
        {
            PreAction();
            var aipReq = DefaultRequest(UrlTaskStatus);
            aipReq.Bodys["id"] = id;
            return PostAction(aipReq);
        }
    }
}