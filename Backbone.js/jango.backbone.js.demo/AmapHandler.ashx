<%@ WebHandler Language="C#" Class="AmapHandler" %>

using System;
using System.IO;
using System.Net;
using System.Web;

public class AmapHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        var url = "http://ditu.amap.com/service/pl/pl/json?v=" + DateTime.Now.Millisecond;

        var request = (HttpWebRequest)WebRequest.Create(url);
        var responseStream = request.GetResponse().GetResponseStream();
        var str = String.Empty;
        using (responseStream)
        {
            str = new StreamReader(responseStream).ReadToEnd();
        }

        context.Response.Write(str.Equals(String.Empty) ? "get error" : str);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}