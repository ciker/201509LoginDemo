// Decompiled with JetBrains decompiler
// Type: CodeMaker.Workflow
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.Text;

namespace CodeMaker
{
  internal class Workflow : BaseClass
  {
    public void DoWorkflow(Table replaceClass, ref List<string> fileName)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      foreach (Column column in replaceClass.Columns)
      {
        if (Common.IsWorkFlow(column.Comment))
        {
          StringBuilder stringBuilder2 = new StringBuilder();
          string oldValue = "<Compile Include=@Properties\\AssemblyInfo.cs@ />".Replace('@', '"');
          string str1 = column.Comment.Replace('【', '[').Replace('】', ']').Replace('，', ',');
          string[] strArray = str1.Substring(str1.IndexOf('[') + 1, str1.IndexOf(']') - str1.IndexOf('[') - 1).Split(',');
          string newValue1 = str1.Substring(0, str1.ToUpper().IndexOf("WORKFLOW"));
          for (int index = 0; index < strArray.Length; ++index)
          {
            string newValue2 = replaceClass.Code + column.Code + index.ToString();
            string content = Common.Read(BaseClass.m_DempDirectory + "/CodeActivity1.cs").Replace("DAL", replaceClass.NameSpace + "DAL").Replace("WFActivitys", replaceClass.NameSpace + "WFActivitys").Replace("CodeActivity1", newValue2).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceAttribute, strArray[index]).Replace("^State^", column.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace("WF", newValue1);
            Common.Write(BaseClass.m_RootDirectory + "/WFActivitys/" + newValue2 + ".cs", content);
            string str2 = "    <Compile Include=@Framework@ />\r\n            ".Replace('@', '"');
            stringBuilder2.Append(str2.Replace("Framework", newValue2 + ".cs"));
          }
          stringBuilder2.Append(oldValue);
          string path = BaseClass.m_RootDirectory + "/WFActivitys/WFActivitys.csproj";
          Common.Write(path, Common.Read(path).Replace(oldValue, stringBuilder2.ToString()).Replace("<RootNamespace>WFActivitys</RootNamespace>", "<RootNamespace>" + replaceClass.NameSpace + "WFActivitys</RootNamespace>"));
          stringBuilder2.Clear();
        }
      }
    }
  }
}
