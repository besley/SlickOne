using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// REST Client的帮助类
    /// </summary>
    public class RestClientHelper
    {
        /// <summary>
        /// 创建RestClient实例
        /// </summary>
        /// <returns>rest client实例</returns>
        public static RestClient CreateRestClient()
        {
            return CreateRestClient(string.Empty);
        }

        /// <summary>
        /// 创建RestClient实例
        /// </summary>
        /// <param name="baseUrl">url串</param>
        /// <returns>rest client实例</returns>
        public static RestClient CreateRestClient(string baseUrl)
        {
            RestClient restClient;
            if (! String.IsNullOrEmpty(baseUrl))
                restClient = new RestClient(baseUrl);
            else
                restClient = new RestClient();

            return restClient;
        }

        /// <summary>
        /// 创建RestRequest实例，默认Json格式
        /// </summary>
        /// <param name="method">HttpMethod类型</param>
        /// <returns>RestRequest实例</returns>
        public static RestRequest CreateRestRequest(Method method)
        {
            return CreateRestRequest(method, DataFormat.Json);
        }

        /// <summary>
        /// 创建RestRequest实例
        /// </summary>
        /// <param name="method">HttpMethod类型</param>
        /// <param name="format">Json或Xml格式</param>
        /// <returns>RestRequest实例</returns>
        public static RestRequest CreateRestRequest(Method method, DataFormat format)
        {
            RestRequest request = new RestRequest(method);
            request.RequestFormat = format;
            return request;
        }

        /// <summary>
        /// 创建文件上传请求
        /// </summary>
        /// <param name="name">request中的参数名</param>
        /// <param name="path">文件名称</param>
        /// <returns>request 实例</returns>
        public static RestRequest CreateRestUploadFileRequest(string name, string path)
        {
            RestRequest request = CreateRestRequest(Method.POST, DataFormat.Json);
            request.AddFile(name, path);
            return request;
        }        
    }
}
