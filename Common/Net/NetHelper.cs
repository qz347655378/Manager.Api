using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Common.Net
{
    /// <summary>
    /// 网络请求帮助类
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authorization">令牌</param>
        /// <returns></returns>
        public static string Get(string url, string authorization = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();

            return retString;
        }

        /// <summary>
        /// 异步get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, string authorization = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream!, Encoding.UTF8);
            var retString = await myStreamReader.ReadToEndAsync();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();

            return retString;
        }




        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="authorization">令牌</param>
        /// <returns></returns>
        public static string Post(string url, string data, string authorization = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();
            return retString;
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encode"></param>
        /// <param name="contentType"></param>
        /// <param name="authorization">令牌</param>
        /// <returns></returns>
        public static string Post(string url, string data, Encoding encode,string contentType, string authorization = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }

            byte[] bytes = encode.GetBytes(data);
            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();
            return retString;
        }


        /// <summary>
        /// 异步post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encode"></param>
        /// <param name="contentType"></param>
        /// <param name="authorization">令牌</param>
        public static async Task<string> PostAsync(string url, string data, Encoding encode, string contentType, string authorization = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }
            byte[] bytes = encode.GetBytes(data);

            request.ContentLength = bytes.Length;
            Stream myResponseStream = await request.GetRequestStreamAsync();
            await myResponseStream.WriteAsync(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream()!, Encoding.UTF8);
            string retString = await myStreamReader.ReadToEndAsync();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();
            return retString;
        }


     
        public static async Task<string> PostAsync(string url, string data, string authorization = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["Authorization"] = authorization;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            request.ContentLength = bytes.Length;
            Stream myResponseStream = await request.GetRequestStreamAsync();
            await myResponseStream.WriteAsync(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream()!, Encoding.UTF8);
            string retString = await myStreamReader.ReadToEndAsync();

            myStreamReader.Close();
            myResponseStream.Close();

            response.Close();
            request.Abort();
            return retString;
        }


        /// <summary>
        /// ping指定IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool Ping(string ip)
        {
            var ping = new Ping();
            var replay = ping.Send(ip);
            return replay != null && replay.Status == IPStatus.Success;
        }



        /// <summary>
        ///异步ping指定IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static async Task<bool> PingAsync(string ip)
        {
            var ping = new Ping();
            var replay = await ping.SendPingAsync(ip);

            return replay != null && replay.Status == IPStatus.Success;
        }

    }
}
