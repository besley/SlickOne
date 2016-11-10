using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// Json 数据帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 解析HttpGet有参数，且格式为JSON数据格式的对象
        /// //call ajax get method 
        /// ajaxGet:
        /// ...
        /// url: "/ProductWebApi/api/Workflow/StartProcess?data=", 
        /// data: request,
        /// ...
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static dynamic ParseHttpGetJson(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    var json = query.Substring(7, query.Length - 7);  // the number 7 is for data=
                    json = System.Web.HttpUtility.UrlDecode(json);
                    dynamic queryJson = JsonConvert.DeserializeObject<dynamic>(json);

                    return queryJson;
                }
                catch (System.Exception e)
                {
                    throw new ApplicationException("wrong json format in the query string！", e);
                }
            }
            else
            {
                return null;
            }
        }  
    }
}
