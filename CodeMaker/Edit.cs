// Decompiled with JetBrains decompiler
// Type: CodeMaker.Edit
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
  internal class Edit : BaseClass
  {
    public string m_SelfEdit = "       \r\n            <div class=@editor-label@>\r\n                <a class=@anUnderLine@ onclick=@showModalOnly('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n                </a>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <div id=@check^ReplaceAttribute^@>\r\n                    <% if(Model!=null)\r\n                        {\r\n                        if (!string.IsNullOrWhiteSpace(Model.^ReplaceAttribute^))\r\n                        {%>\r\n                    <table  id=@<%: Model.^ReplaceAttribute^ %>@\r\n                        class=@deleteStyle@>\r\n                        <tr>\r\n                            <td>\r\n                                <img  alt=@删除@  title=@点击删除@ src=@../../../Images/deleteimge.png@ onclick=@deleteTable('<%: Model.^ReplaceAttribute^ %>','^ReplaceAttribute^');@/>\r\n                            </td>\r\n                            <td>\r\n                                <%: Model.^ReplaceClassCode^2.^m_Name^%>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <%}}%>\r\n                </div>\r\n                <%: Html.HiddenFor(model => model.^ReplaceAttribute^)%>\r\n            </div>";
    public string m_EditRef = "      \r\n            <div class=@editor-label@>\r\n                <a class=@anUnderLine@ onclick=@showModalOnly('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n                </a>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <div id=@check^ReplaceAttribute^@>\r\n                    <%  if(Model!=null)\r\n                        {\r\n                        if (null != Model.^ReplaceAttribute^)                      \r\n                        {%>\r\n                    <table id=@<%: Model.^ReplaceAttribute^ %>@\r\n                        class=@deleteStyle@>\r\n                        <tr>\r\n                            <td>\r\n                                <img alt=@删除@  title=@点击删除@ onclick=@deleteTable('<%: Model.^ReplaceAttribute^ %>','^ReplaceAttribute^');@ src=@../../../Images/deleteimge.png@ />\r\n                            </td>\r\n                            <td>\r\n                                <%: Model.^ReplaceClassCode^.^m_Name^%>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <%}}%>\r\n                </div>\r\n                <%: Html.HiddenFor(model => model.^ReplaceAttribute^)%>\r\n            </div>";
    public string m_EditNotRefUp = "   \r\n                <div class=@editor-label@>\r\n                    <input id=@file_upload@ name=@file_upload@ type=@file@ />\r\n                </div>\r\n                <div id=@up@ class=@editor-field@> <div id=@fileQueue@>\r\n                    </div>\r\n                <div id=@check^ReplaceAttribute^@>\r\n                    <% string ids = string.Empty;\r\n                    if(Model!=null)\r\n                        {\r\n                       foreach (var item in Model.^ReplaceClassCode^)\r\n                       {\r\n                           string it = string.Empty;\r\n                           it += item.^m_Id^ + @&@ + item.^m_Name^;\r\n                           if (ids.Length > 0)\r\n                           {\r\n                               ids += @^@ + it;\r\n                           }\r\n                           else\r\n                           {\r\n                               ids += it;\r\n                           }\r\n \r\n                    %>\r\n                    <table id=@<%: it %>@\r\n                        class=@deleteStyle@>\r\n                        <tr>\r\n                            <td>\r\n                                <img  alt=@删除@ title=@点击删除@ onclick=@deleteTable('<%: it %>','^ReplaceAttribute^');@  src=@../../../Images/deleteimge.png@ />\r\n                            </td>\r\n                            <td>\r\n                                <%: item.^m_Name^ %>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                    <%}}%>\r\n                    <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^@ id=@^ReplaceAttribute^@ />\r\n                    <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^Old@ id=@^ReplaceAttribute^Old@ />\r\n                </div>\r\n            </div>";
    public string m_EditNotRef = "  \r\n        <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalMany('^ReplaceAttribute^','../../^ReplaceClassCode^');@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>\r\n                <% string ids = string.Empty;\r\n                if(Model!=null)\r\n                {\r\n                   foreach (var item in Model.^ReplaceClassCode^)\r\n                   {\r\n                       string item1 = string.Empty;\r\n                       item1 += item.^m_Id^ + @&@ + item.^m_Name^;\r\n                       if (ids.Length > 0)\r\n                       {\r\n                           ids += @^@ + item1;\r\n                       }\r\n                       else\r\n                       {\r\n                           ids += item1;\r\n                       }\r\n                %>\r\n                <table id=@<%: item1 %>@\r\n                    class=@deleteStyle@>\r\n                    <tr>\r\n                        <td>\r\n                            <img  alt=@删除@ title=@点击删除@ onclick=@deleteTable('<%: item1 %>','^ReplaceAttribute^');@  src=@../../../Images/deleteimge.png@ />\r\n                        </td>\r\n                        <td>\r\n                            <%: item.^m_Name^ %>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                <%}}%>\r\n                <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^Old@ id=@^ReplaceAttribute^Old@ />\r\n                <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^@ id=@^ReplaceAttribute^@ />\r\n            </div>\r\n        </div>";
    public string m_EditNotRefTree = "  \r\n        <div class=@editor-label@>\r\n            <a class=@anUnderLine@ onclick=@showModalMany('^ReplaceAttribute^','../../^ReplaceClassCode^Tree',456);@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>\r\n            </a>：\r\n        </div>\r\n        <div class=@editor-field@>\r\n            <div id=@check^ReplaceAttribute^@>\r\n                <% string ids = string.Empty;\r\n                if(Model!=null)\r\n                {\r\n                   foreach (var item in Model.^ReplaceClassCode^)\r\n                   {\r\n                       string item1 = string.Empty;\r\n                       item1 += item.^m_Id^ + @&@ + item.^m_Name^;\r\n                       if (ids.Length > 0)\r\n                       {\r\n                           ids += @^@ + item1;\r\n                       }\r\n                       else\r\n                       {\r\n                           ids += item1;\r\n                       }\r\n                %>\r\n                <table id=@<%: item1 %>@\r\n                    class=@deleteStyle@>\r\n                    <tr>\r\n                        <td>\r\n                            <img  alt=@删除@ title=@点击删除@ onclick=@deleteTable('<%: item1 %>','^ReplaceAttribute^');@  src=@../../../Images/deleteimge.png@ />\r\n                        </td>\r\n                        <td>\r\n                            <%: item.^m_Name^ %>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                <%}}%>\r\n                <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^Old@ id=@^ReplaceAttribute^Old@ />\r\n                <input type=@hidden@ value=@<%= ids %>@ name=@^ReplaceAttribute^@ id=@^ReplaceAttribute^@ />\r\n            </div>\r\n        </div>";
    public string m_CreatePage = "^m_CreatePage^";

    public void DoEdit(Table replaceClass, ref List<string> fileName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      int num = 0;
      string newValue3 = "";
      string newValue4 = "";
      bool flag = false;
      foreach (Column column in replaceClass.Columns)
      {
        ++num;
        if (Common.IsPrimaryKey(replaceClass, column.Id))
          newValue2 = newValue2 + "<%: Html.HiddenFor(model => model." + column.Code + " ) %>";
        else if (Common.IsStampType(column.DataType) || Common.IsCreatePerson(column) || Common.IsCreateTime(column))
          newValue2 = newValue2 + "<%: Html.HiddenFor(model => model." + column.Code + " ) %>";
        else if (!Common.IsUpdatePerson(column) && !Common.IsUpdateTime(column) && !Common.IsCreatePerson(column) && (!Common.IsCreateTime(column) && !Common.IsNotDisplay(column.Comment)) && !Common.IsWorkFlow(column.Comment))
        {
          if (!string.IsNullOrWhiteSpace(replaceClass.childTableColumnRef) && replaceClass.childTableColumnRef == column.Id)
          {
            newValue2 += this.m_SelfEdit.Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceAttribute, column.Code).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace('@', '"');
          }
          else
          {
            RefIdName foreignKey = Common.GetForeignKey(replaceClass, column);
            if (foreignKey != null)
              newValue2 += this.m_EditRef.Replace(this.m_Name, foreignKey.Name).Replace(this.m_ReplaceAttribute, foreignKey.Ref).Replace(this.m_ReplaceClassCode, foreignKey.RefTableCode).Replace('@', '"');
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
              else if (Common.IsDecimalType(column.DataType))
                newValue2 += this.m_EditorFor.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
              else if (Common.IsStringType(column.DataType))
              {
                if (column.Comment.Contains("RadioButton"))
                  newValue2 = !(column.Mandatory == "1") ? newValue2 + this.m_CreateRadioButtonFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"').Replace(this.m_RadioButtonListChecked, "false") : newValue2 + this.m_CreateRadioButtonFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"').Replace(this.m_RadioButtonListChecked, "true");
                else if (column.Comment.Contains("DropDown"))
                  newValue2 += this.m_CreateEditorFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
                else if (column.Comment.Contains("Cascade"))
                {
                  string newValue5 = column.Comment.Substring(0, column.Comment.IndexOf("Cascade"));
                  newValue2 += this.m_LianDongEditorFor.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace("^MyParent^", newValue5).Replace('@', '"');
                  newValue3 += "\r\n            \r\n            $(@#myparent@).change(function () { get^ReplaceAttribute^(@#^ReplaceAttribute^@); });\r\n".Replace(this.m_ReplaceAttribute, column.Code).Replace("myparent", newValue5).Replace('@', '"');
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
              newValue2 += this.m_EditNotRefUp.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace("ids", "ids" + num.ToString()).Replace("item", "item" + num.ToString()).Replace('@', '"');
              newValue3 += this.m_UpLodeJs.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace('@', '"');
              newValue1 += this.m_UpLoaderScript.Replace('@', '"');
            }
            else
              newValue2 += this.m_EditNotRef.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace("ids", "ids" + num.ToString()).Replace("item", "item" + num.ToString()).Replace('@', '"');
          }
        }
      }
      string content = Common.Read(BaseClass.m_DempDirectory + "/Edit.aspx").Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace("App.", replaceClass.NameSpace + "App.").Replace("liandongchushihua", newValue3).Replace("liandonghanshu", newValue4).Replace(this.m_CreatePage, newValue2).Replace(this.m_PickTimeCrea, newValue1).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
      string path = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
      Directory.CreateDirectory(path);
      Common.Write(path + "/Edit.aspx", content);
      fileName.Add(replaceClass.Code + "/Edit.aspx");
    }
  }
}
