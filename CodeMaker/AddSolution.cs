// Decompiled with JetBrains decompiler
// Type: CodeMaker.AddSolution
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeMaker
{
  public class AddSolution : BaseClass
  {
    public List<string> WorkflowTableAndSys
    {
      get
      {
        return new List<string>()
        {
          "FileUploader",
          "SysDepartment",
          "SysException",
          "SysField",
          "SysLog",
          "SysMenu",
          "SysMenuSysOperation",
          "SysMenuSysRoleSysOperation",
          "SysOperation",
          "SysPerson",
          "SysRole",
          "SysRoleSysPerson"
        };
      }
    }

    public void DoAddSolution(List<string> fileNameRepository, List<string> fileNameBLL, List<string> fileNameIBLL, List<string> fileNameWcf, string nameSpace)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string oldValue1 = "<Compile Include=@Properties\\AssemblyInfo.cs@ />\r\n".Replace('@', '"');
      string str1 = "    <Compile Include=@Framework.cs@ />\r\n    <Compile Include=@FrameworkRepository.cs@ />\r\n".Replace('@', '"');
      string path1 = BaseClass.m_RootDirectory + "/" + this.m_DAL + "/" + this.m_DAL + ".csproj";
      stringBuilder.Append(oldValue1);
      foreach (string newValue in fileNameRepository)
        stringBuilder.Append(str1.Replace("Framework", newValue));
      Common.Write(path1, Common.Read(path1).Replace(oldValue1, stringBuilder.ToString()).Replace("<RootNamespace>DAL</RootNamespace>", "<RootNamespace>" + nameSpace + "DAL</RootNamespace>"));
      stringBuilder.Clear();
      string str2 = "    <Compile Include=@FrameworkBLL.cs@ />\r\n".Replace('@', '"');
      string path2 = BaseClass.m_RootDirectory + "/BLL/BLL.csproj";
      stringBuilder.Append(oldValue1);
      foreach (string newValue in fileNameBLL)
        stringBuilder.Append(str2.Replace("Framework", newValue));
      Common.Write(path2, Common.Read(path2).Replace(oldValue1, stringBuilder.ToString()).Replace("<RootNamespace>BLL</RootNamespace>", "<RootNamespace>" + nameSpace + "BLL</RootNamespace>"));
      stringBuilder.Clear();
      string str3 = "    <Compile Include=@IFrameworkBLL.cs@ />\r\n            ".Replace('@', '"');
      string path3 = BaseClass.m_RootDirectory + "/IBLL/IBLL.csproj";
      stringBuilder.Append(oldValue1);
      foreach (string newValue in fileNameIBLL)
        stringBuilder.Append(str3.Replace("Framework", newValue));
      Common.Write(path3, Common.Read(path3).Replace(oldValue1, stringBuilder.ToString()).Replace("<RootNamespace>IBLL</RootNamespace>", "<RootNamespace>" + nameSpace + "IBLL</RootNamespace>"));
      stringBuilder.Clear();
      List<string> fileControllers1 = Common.GetFileControllers(BaseClass.m_RootDirectory + "/" + this.m_App + "/Controllers", "Controllers");
      List<string> fileViews = Common.GetFileViews(BaseClass.m_RootDirectory + "/" + this.m_App + "/Views", "Views");
      List<string> fileControllers2 = Common.GetFileControllers(BaseClass.m_RootDirectory + "/" + this.m_App + "/Models", "Models");
      string path4 = BaseClass.m_RootDirectory + "/" + this.m_App + "/" + this.m_App + ".csproj";
      string str4 = "    \n<Compile Include=@Framework@ />\n".Replace('@', '"');
      string oldValue2 = "<ItemGroup />";
      string str5 = "    \n<Content Include=@Framework@ />\n".Replace('@', '"');
      stringBuilder.Append("     <ItemGroup>");
      foreach (string str6 in fileViews)
      {
        string item = str6;
        if (!item.Contains("\\Account\\") && !item.Contains("\\Shared\\") && (!item.Contains("\\NotFound\\") && !item.Contains("\\Home\\")) && !item.Contains("\\Exception\\") && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => item.Contains("\\" + a + "\\"))) && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => item.Contains("\\" + a + "\\Tree"))))
          stringBuilder.Append(str5.Replace("Framework", item.Replace('/', '\\')));
      }
      stringBuilder.Append("     </ItemGroup>");
      stringBuilder.Append("     <ItemGroup>");
      foreach (string str6 in fileControllers1)
      {
        string item = str6;
        if (!item.Contains("\\AccountController.cs") && !item.Contains("\\HomeController.cs") && !item.Contains("\\ExceptionController.cs") && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => item.Contains("\\" + a + "Controller.cs"))) && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => item.Contains("\\" + a + "TreeController.cs"))))
          stringBuilder.Append(str4.Replace("Framework", item.Replace('/', '\\')));
      }
      foreach (string str6 in fileControllers2)
      {
        string item = str6;
        if (!Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => item.Contains("\\" + a + "TreeModel.cs"))) && item.Contains("Tree"))
          stringBuilder.Append(str4.Replace("Framework", item.Replace('/', '\\')));
      }
      stringBuilder.Append("     </ItemGroup>");
      stringBuilder.ToString();
      Common.Write(path4, Common.Read(path4).Replace(oldValue2, stringBuilder.ToString()).Replace("<RootNamespace>App</RootNamespace>", "<RootNamespace>" + nameSpace + "App</RootNamespace>"));
      stringBuilder.Clear();
      string str7 = ("\r\n      <service behaviorConfiguration=@metadataSupport@ name=@" + nameSpace + "BLL.ReplaceBLL@>\r\n        <endpoint binding=@basicHttpBinding@ bindingConfiguration=@CustomizeServiceSoapBinding@ contract=@" + nameSpace + "IBLL.IClassBLL@/>\r\n      </service>\r\n").Replace('@', '"');
      string newValue1 = "    <services>\r\n            ";
      string str8 = "    <Content Include=@Web.config@ />\r\n            ".Replace('@', '"');
      string path5 = BaseClass.m_RootDirectory + "/WcfHost/WcfHost.csproj";
      string path6 = BaseClass.m_RootDirectory + "/WcfHost/Web.config";
      foreach (string newValue2 in fileNameWcf)
      {
        newValue1 += str7.Replace("Replace", newValue2).Replace("Class", newValue2);
        stringBuilder.Append(str8.Replace("Web.config", newValue2 + ".svc"));
      }
      stringBuilder.Append("<Content Include=@Web.config@>".Replace('@', '"'));
      Common.Write(path5, Common.Read(path5).Replace("<Content Include=@Web.config@>".Replace('@', '"'), stringBuilder.ToString()));
      stringBuilder.Clear();
      Common.Write(path6, Common.Read(path6).Replace("<services>", newValue1));
    }
  }
}
