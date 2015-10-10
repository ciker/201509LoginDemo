<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

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

            var streamReader = new StreamReader(stream);
            var str = streamReader.ReadToEnd();
            var res = "";
            if (type.Equals("xml"))
            {
                var serializer = new XmlSerializer(typeof(feed));
                str = str.Replace("xmlns=\"http://www.w3.org/2005/Atom\"", "");
                var sR = new StringReader(str);
                var blogs = (feed)serializer.Deserialize(sR);
                res = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(blogs);
            }

            context.Response.Write(res);
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


[XmlRoot]
public class feed
{
    [XmlElementAttribute]
    public string title { get; set; }
    [XmlElementAttribute]
    public string id { get; set; }
    [XmlElementAttribute]
    public string updated { get; set; }
    [XmlElementAttribute]
    public string link { get; set; }
    [XmlElementAttribute("entry")]
    public Entry[] entry { get; set; }
}
[XmlRoot]
public class Entry
{
    [XmlElementAttribute]
    public long id { get; set; }
    [XmlElementAttribute]
    public string title { get; set; }
    [XmlElementAttribute]
    public string summary { get; set; }
    [XmlElementAttribute]
    public string published { get; set; }
    [XmlElementAttribute]
    public string updated { get; set; }
    [XmlElementAttribute]
    public author author { get; set; }
    [XmlElementAttribute]
    public string link { get; set; }
    [XmlElementAttribute]
    public string blogapp { get; set; }
    [XmlElementAttribute]
    public int diggs { get; set; }
    [XmlElementAttribute]
    public int views { get; set; }
    [XmlElementAttribute]
    public int comments { get; set; }

}

[XmlRoot]
public class author
{
    [XmlElementAttribute]
    public string name { get; set; }
    [XmlElementAttribute]
    public string uri { get; set; }
    [XmlElementAttribute]
    public string avatar { get; set; }
}