// Decompiled with JetBrains decompiler
// Type: CodeMaker.Wcf
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.IO;

namespace CodeMaker
{
  public class Wcf : BaseClass
  {
    public void DoWcf(Table replaceClass, ref List<string> fileName)
    {
      string str = "<%@ ServiceHost Language=!C#! Debug=!true! Service=!" + replaceClass.NameSpace + "BLL." + replaceClass.Code + "BLL!  %>";
      string path = BaseClass.m_RootDirectory + "/WcfHost/";
      Directory.CreateDirectory(path);
      Common.Write(path + replaceClass.Code + ".svc", str.Replace('!', '"'));
      fileName.Add(replaceClass.Code);
    }
  }
}
