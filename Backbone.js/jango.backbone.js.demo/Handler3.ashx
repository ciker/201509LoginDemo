﻿<%@ WebHandler Language="C#" Class="Handler3" %>

using System;
using System.Web;
using System.Web.Script.Serialization;

public class Handler3 : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        var strJson = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();

        JavaScriptSerializer seriliazer = new JavaScriptSerializer();
        var model = seriliazer.Deserialize<model>(strJson);
        model.name += " this name is from the server ";
        strJson = seriliazer.Serialize(model);
        context.Response.Write(strJson);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

public class model
{
    public string name { get; set; }
}