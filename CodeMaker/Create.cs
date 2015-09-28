// Decompiled with JetBrains decompiler
// Type: CodeMaker.Create
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
  public class Create : BaseClass
  {
    public string m_PickTimeCreate = "\r\n  $('#^ReplaceAttribute^').datepicker();            \r\n";
    private string replaceInt = string.Empty;
    public string m_SelfEdit = "\r\n        <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalOnly('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>               \r\n            </div>\r\n            <%: Html.HiddenFor(model => model.^ReplaceAttribute^)%>\r\n        </div>";
    public string m_EditRef = "\r\n        <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalOnly('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>            \r\n            </div>\r\n            <%: Html.HiddenFor(model => model.^ReplaceAttribute^)%>\r\n        </div>";
    public string m_EditNotRefTree = "   <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalMany('^ReplaceAttribute^','../../^ReplaceClassCode^Tree',456);@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>\r\n                <% \r\n                if (Model != null && !string.IsNullOrWhiteSpace(Model.^ReplaceAttribute^))\r\n                {\r\n                   foreach (var item in Model.^ReplaceAttribute^.Split('^'))\r\n                   {\r\n                        string[] it = item.Split('&');\r\n                        if (it != null && it.Length == 2 && !string.IsNullOrWhiteSpace(it[0]) && !string.IsNullOrWhiteSpace(it[1]))\r\n                        {                        \r\n                %>\r\n                <table id=@<%: item %>@\r\n                    class=@deleteStyle@>\r\n                    <tr>\r\n                        <td>\r\n                            <img  alt=@删除@ title=@点击删除@ onclick=@deleteTable('<%: item  %>','^ReplaceAttribute^');@  src=@../../../Images/deleteimge.png@ />\r\n                        </td>\r\n                        <td>\r\n                            <%: it[1] %>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                <%}}}%>\r\n               <%: Html.HiddenFor(model => model.^ReplaceAttribute^) %>\r\n            </div>\r\n        </div>";
    public string m_EditNotRef = "   <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalMany('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>\r\n                <% \r\n                if (Model != null && !string.IsNullOrWhiteSpace(Model.^ReplaceAttribute^))\r\n                {\r\n                   foreach (var item in Model.^ReplaceAttribute^.Split('^'))\r\n                   {\r\n                        string[] it = item.Split('&');\r\n                        if (it != null && it.Length == 2 && !string.IsNullOrWhiteSpace(it[0]) && !string.IsNullOrWhiteSpace(it[1]))\r\n                        {                        \r\n                %>\r\n                <table id=@<%: item %>@\r\n                    class=@deleteStyle@>\r\n                    <tr>\r\n                        <td>\r\n                            <img  alt=@删除@ title=@点击删除@ onclick=@deleteTable('<%: item  %>','^ReplaceAttribute^');@  src=@../../../Images/deleteimge.png@ />\r\n                        </td>\r\n                        <td>\r\n                            <%: it[1] %>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                <%}}}%>\r\n               <%: Html.HiddenFor(model => model.^ReplaceAttribute^) %>\r\n            </div>\r\n        </div>";
    public string m_UpLode = "           \r\n            <div class=@editor-label@>\r\n                <input id=@file_upload@ name=@file_upload@ type=@file@ />\r\n            </div>\r\n            <div id=@up@ class=@editor-field@>\r\n                <div id=@fileQueue@>\r\n                </div>\r\n                <%: Html.HiddenFor(model => model.^ReplaceAttribute^) %>                \r\n            </div> ";
    public string m_CreatePage = "^m_CreatePage^";

    public void DoCreate(Table replaceClass, ref List<string> fileName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      int num = 0;
      bool flag = false;
      string newValue3 = "";
      string newValue4 = "";
      foreach (Column column in replaceClass.Columns)
      {
        ++num;
        if (!Common.IsPrimaryKey(replaceClass, column.Id) && !Common.IsStampType(column.DataType) && (!Common.IsCreatePerson(column) && !Common.IsCreateTime(column) && !Common.IsUpdatePerson(column) && !Common.IsUpdateTime(column) && !Common.IsNotDisplay(column.Comment) && !Common.IsWorkFlow(column.Comment)))
        {
          if (!string.IsNullOrWhiteSpace(replaceClass.childTableColumnRef) && replaceClass.childTableColumnRef == column.Id)
          {
            newValue2 += this.m_SelfEdit.Replace(this.m_ReplaceAttribute, column.Code).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace('@', '"');
          }
          else
          {
            RefIdName foreignKey = Common.GetForeignKey(replaceClass, column);
            if (foreignKey != null)
              newValue2 += this.m_EditRef.Replace(this.m_ReplaceAttribute, foreignKey.Ref).Replace(this.m_ReplaceClassCode, foreignKey.RefTableCode).Replace('@', '"');
            else if (!string.IsNullOrWhiteSpace(column.Code) && !string.IsNullOrWhiteSpace(column.DataType))
            {
              if (Common.IsIntType(column.DataType))
                newValue2 += this.m_EditorForInt.Replace(this.m_ReplaceAttribute, column.Code).Replace('*', '"');
              else if (Common.IsDateType(column.DataType))
              {
                if (!flag)
                  flag = true;
                newValue2 += this.m_EditorForDate.Replace(this.m_ReplaceAttribute, column.Code).Replace('*', '"');
              }
              else if (Common.IsStringType(column.DataType))
              {
                if (column.Comment.Contains("RadioButton"))
                  newValue2 = !(column.Mandatory == "1") ? newValue2 + this.m_CreateRadioButtonFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"').Replace(this.m_RadioButtonListChecked, "false") : newValue2 + this.m_CreateRadioButtonFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"').Replace(this.m_RadioButtonListChecked, "true");
                else if (column.Comment.Contains("DropDown"))
                  newValue2 += this.m_CreateEditorFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
                else if (column.Comment.Contains("Cascade"))
                {
                  newValue2 += this.m_LianDong.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
                  string newValue5 = column.Comment.Substring(0, column.Comment.IndexOf("Cascade"));
                  newValue3 += "\r\n            get^ReplaceAttribute^(@#^ReplaceAttribute^@);\r\n            $(@#myparent@).change(function () { get^ReplaceAttribute^(@#^ReplaceAttribute^@); });\r\n".Replace(this.m_ReplaceAttribute, column.Code).Replace("myparent", newValue5).Replace('@', '"');
                  newValue4 += "\r\n        function get^ReplaceAttribute^(^ReplaceAttribute^) {\r\n            $(^ReplaceAttribute^).empty();\r\n            $(@<option></option>@)\r\n                    .val(@@)\r\n                    .text(@请选择@)\r\n                    .appendTo($(^ReplaceAttribute^));\r\n            bindDropDownList(^ReplaceAttribute^, @#myparent@);\r\n            $(^ReplaceAttribute^).change();\r\n        }\r\n".Replace(this.m_ReplaceAttribute, column.Code).Replace("myparent", newValue5).Replace('@', '"');
                }
                else
                  newValue2 = string.IsNullOrWhiteSpace(column.Length) || Convert.ToInt32(column.Length) <= 200 ? newValue2 + this.m_EditorFor.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"') : newValue2 + this.m_TextAreaFor.Replace(this.m_ReplaceAttribute, column.Code).Replace('\'', '"');
              }
              else
                newValue2 += this.m_EditorFor.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
            }
          }
        }
      }
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          ++num;
          if (!refIdName.IsRefSelf)
          {
            if (refIdName.RefTableCode.ToUpper() == "FILEUPLOADER")
            {
              newValue2 += this.m_UpLode.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace('@', '"');
              newValue3 += this.m_UpLodeJs.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace('@', '"');
              newValue1 += this.m_UpLoaderScript.Replace('@', '"');
            }
            else
              newValue2 += this.m_EditNotRef.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace("ids", "ids" + num.ToString()).Replace("item", "item" + num.ToString()).Replace('@', '"');
          }
          else
            newValue2 += this.m_EditNotRef.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace("ids", "ids" + num.ToString()).Replace("item", "item" + num.ToString()).Replace('@', '"');
        }
      }
      string content = Common.Read(BaseClass.m_DempDirectory + "/Create.aspx").Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace("App.", replaceClass.NameSpace + "App.").Replace("liandongchushihua", newValue3).Replace("liandonghanshu", newValue4).Replace(this.m_CreatePage, newValue2).Replace(this.m_PickTimeCrea, newValue1).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
      string path = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
      Directory.CreateDirectory(path);
      Common.Write(path + "/Create.aspx", content);
      fileName.Add(replaceClass.Code + "/Create.aspx");
    }
  }
}
