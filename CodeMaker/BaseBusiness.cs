// Decompiled with JetBrains decompiler
// Type: CodeMaker.BaseBusiness
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Resources;
using System.Windows.Forms;

namespace CodeMaker
{
  public class BaseBusiness
  {
    private static ResourceManager rm;
    private static string culture;

    public static string Culture
    {
      get
      {
        return BaseBusiness.culture;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          return;
        if (value != BaseBusiness.culture || BaseBusiness.rm == null)
        {
          try
          {
            BaseBusiness.culture = value;
            BaseBusiness.rm = ResourceManager.CreateFileBasedResourceManager(BaseBusiness.culture, Application.StartupPath, (Type) null);
            BaseBusiness.rm.IgnoreCase = true;
          }
          catch
          {
          }
        }
      }
    }

    public static bool IsNotDefault()
    {
      return !(BaseBusiness.culture == "ZH-CN");
    }

    public static string ValidateCulture(string culture)
    {
      string str = "ZH-CN";
      if (culture.ToUpper() != "ZH-CN")
        str = "EN-US";
      return str;
    }

    public static string GetResourceValue(string key)
    {
      string str = "";
      if (!string.IsNullOrWhiteSpace(key) && BaseBusiness.rm != null)
      {
        try
        {
          str = BaseBusiness.rm.GetString(key);
        }
        catch
        {
        }
      }
      return str;
    }
  }
}
