using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Wboy.Infrastructure.Core.Extension
{
    public static class HttpWebHelper
    {
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static string HttpPost(string url, IDictionary<string, string> parameters, string contentType = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            if (contentType == null || contentType.Equals("") || contentType.Length < 10)
                request.ContentType = "application/x-www-form-urlencoded";
            else
                request.ContentType = contentType;
            request.ReadWriteTimeout = 30 * 1000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
            //如果需要POST数据     
            if (parameters != null && parameters.Count > 0)
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                        buffer.Append($"&{key}={parameters[key]}");
                    else
                        buffer.Append($"{key}={parameters[key]}");
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse response = null;
            string content;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                //异常请求
                response = (HttpWebResponse)e.Response;
                using (Stream errData = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }
        public static string HttpPost(string url, string bodyStr, string contentType = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            if (contentType == null || contentType.Equals("") || contentType.Length < 10)
                request.ContentType = "application/x-www-form-urlencoded";
            else
                request.ContentType = contentType;
            request.ReadWriteTimeout = 30 * 1000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
            //如果需要POST数据     
            if (!string.IsNullOrEmpty(bodyStr))
            {
                byte[] data = Encoding.UTF8.GetBytes(bodyStr);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse response = null;
            string content;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                //异常请求
                response = (HttpWebResponse)e.Response;
                using (Stream errData = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }
    }
}
