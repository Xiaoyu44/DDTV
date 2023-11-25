﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Network
{
    internal class Post
    {
        /// <summary>
        /// POST方法
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">要POST发送的文本</param>
        /// <param name="cookie">cookies集合实例</param>
        /// <param name="contenttype">数据类型</param>
        /// <param name="referer">Referer</param>
        /// <param name="user_agent">User-agent</param>
        /// <param name="specialheaders">除前面之外的Headers</param>
        /// <returns>请求返回体</returns>
        internal static string PostBody(string url, string data = "", CookieCollection cookie = null,
            string contenttype = "application/x-www-form-urlencoded;charset=utf-8", string referer = "", string user_agent = "",
            WebHeaderCollection specialheaders = null)
        {
            string result = "";
            HttpWebRequest req = null;
            HttpWebResponse rep = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);

                if (specialheaders != null) req.Headers = specialheaders;

                req.Method = "POST";

                if (cookie != null)
                {
                    req.CookieContainer = new CookieContainer(cookie.Count)
                    {
                        PerDomainCapacity = cookie.Count
                    };
                    req.CookieContainer.Add(cookie);
                }

                req.ContentType = contenttype;

                byte[] bdata = Encoding.UTF8.GetBytes(data);
                Stream sdata = req.GetRequestStream();
                sdata.Write(bdata, 0, bdata.Length);
                sdata.Close();

                if (!string.IsNullOrEmpty(referer)) req.Referer = referer;
                if (!string.IsNullOrEmpty(user_agent)) req.UserAgent = user_agent;

                rep = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(rep.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if (rep != null) rep.Close();
                if (req != null) req.Abort();
            }
            return result;
        }
    }
}
