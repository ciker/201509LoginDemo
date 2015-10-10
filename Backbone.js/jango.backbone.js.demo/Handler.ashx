<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
//using System.Xml.Serialization;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        var type = context.Request["type"] ?? "html";
        var url = context.Request["url"] ?? "http://www.cnblogs.com";
        var webRequest = (HttpWebRequest)WebRequest.Create(url);
        var stream = webRequest.GetResponse().GetResponseStream();
        if (stream != null)
        {
            //var serializer = new XmlSerializer(typeof(feed));
            //var blogs = serializer.Deserialize(stream);
            var streamReader = new StreamReader(stream);
            context.Response.Write(streamReader.ReadToEnd());
        }
        else
        {
            context.Response.Write("Hello World");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}



public class feed
{
    public string title { get; set; }
    public string id { get; set; }
    public string updated { get; set; }
    public string link { get; set; }
    public List<Entry> entry { get; set; }
}

public class Entry
{
    public long id { get; set; }
    public string title { get; set; }
    public string summary { get; set; }
    public string published { get; set; }
    public string updated { get; set; }
    public author author { get; set; }
    public string link { get; set; }
    public string blogapp { get; set; }
    public int diggs { get; set; }
    public int views { get; set; }
    public int comments { get; set; }

}

public class author
{
    public string name { get; set; }
    public string uri { get; set; }
    public string avatar { get; set; }
}