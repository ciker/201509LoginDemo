// Decompiled with JetBrains decompiler
// Type: CodeMaker.Details
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeMaker
{
  internal class Details : BaseClass
  {
    public string m_DetailsRef = "        \r\n                <div class=@display-label@>\r\n                      <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@display-field@>\r\n                    <% if (Model.^ReplaceClassCode^ != null && null!=(Model.^ReplaceClassCode^.^m_Name^))\r\n                       { %>\r\n                    <%: Model.^ReplaceClassCode^.^m_Name^%>\r\n                    <%} %>\r\n                </div>";
    public string m_DetailsNotRef = "    \r\n                <div class=@display-label@>\r\n                      <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@display-field@>            \r\n                    <% string ids = string.Empty;\r\n                       foreach (var item in Model.^ReplaceClassCode^)\r\n                       {\r\n                           ids += item.^m_Name^;\r\n                           ids += @ , @;\r\n                    %>               \r\n                    <%}%>\r\n                    \r\n                <%= ids %>   \r\n                  \r\n                </div>";
    public string m_TextAreaForDetails = "\r\n                <br style='clear: both;' />\r\n                <div class='display-label'>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class='textarea-box'>\r\n                    <%: Html.TextAreaFor(model => model.^ReplaceAttribute^, new {  @readonly=true}) %>                  \r\n                </div>";
    public string m_DetailsString = "      \r\n                <div class=@display-label@>\r\n                      <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@display-field@>\r\n                    <%: Html.DisplayFor(model => model.^ReplaceAttribute^) %>\r\n                </div>";
    public string m_DetailsStringDropDownList = "      \r\n                <div class=@display-label@>\r\n                      <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@display-field@>\r\n                   <%: Models.SysFieldModels.GetMyTextsById(Model.^ReplaceAttribute^)%> \r\n                </div>";
    public string m_Details = "^m_Details^";
    public string m_DetailsmMster = "^m_DetailsmMster^";
    public string m_DetailsSmall = "/Small";

    public void DoDetails(Table replaceClass, ref List<string> fileName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string newValue = string.Empty;
      int num = 0;
      foreach (Column column in replaceClass.Columns)
      {
        ++num;
        if (!Common.IsPrimaryKey(replaceClass, column.Id) && !Common.IsStampType(column.DataType) && !Common.IsNotDisplay(column.Comment))
        {
          if (!string.IsNullOrWhiteSpace(replaceClass.childTableColumnRef) && replaceClass.childTableColumnRef == column.Id)
          {
            newValue += this.m_DetailsRef.Replace(this.m_ReplaceAttribute, column.Code).Replace(this.m_ReplaceClassCode, replaceClass.Code + "2").Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace('@', '"');
          }
          else
          {
            RefIdName foreignKey = Common.GetForeignKey(replaceClass, column);
            if (foreignKey != null)
              newValue += this.m_DetailsRef.Replace(this.m_ReplaceAttribute, column.Code).Replace(this.m_ReplaceClassCode, foreignKey.RefTableCode).Replace(this.m_Id, foreignKey.Id).Replace(this.m_Name, foreignKey.Name).Replace('@', '"');
            else if (!string.IsNullOrWhiteSpace(column.Code) && !string.IsNullOrWhiteSpace(column.DataType))
              newValue = !Common.IsStringType(column.DataType) ? newValue + this.m_DetailsString.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"') : (string.IsNullOrWhiteSpace(column.Length) || Convert.ToInt32(column.Length) <= 200 ? newValue + this.m_DetailsString.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"') : newValue + this.m_TextAreaForDetails.Replace(this.m_ReplaceAttribute, column.Code).Replace('\'', '"'));
          }
        }
      }
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          ++num;
          newValue += this.m_DetailsNotRef.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace("ids", "ids" + num.ToString()).Replace("item", "item" + num.ToString()).Replace('@', '"');
        }
      }
      string content = Common.Read(BaseClass.m_DempDirectory + "/Details.aspx").Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace(this.m_Details, newValue).Replace(this.m_DetailsmMster, this.m_DetailsSmall).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
      string path = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
      Directory.CreateDirectory(path);
      Common.Write(path + "/Details.aspx", content);
      fileName.Add(replaceClass.Code + "/Details.aspx");
    }
  }
}
