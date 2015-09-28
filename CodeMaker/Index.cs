// Decompiled with JetBrains decompiler
// Type: CodeMaker.Index
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeMaker
{
  public class Index : BaseClass
  {
    public string m_SearchInt = "\r\n            <div class='left02'>\r\n                <div class='editor-label-search'>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class='editor-field-to'>\r\n                    <input type='text' id='^ReplaceAttribute^Start_Int' onkeyup = 'isInt(this)' />\r\n                    <span>到</span>\r\n                    <input type='text' id='^ReplaceAttribute^End_Int' onkeyup = 'isInt(this)'  />\r\n                </div>\r\n            </div>";
    public string m_SearchDateTime = " \r\n            <div class='left02'>\r\n                <div class=@editor-label-search@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@editor-field-to@>\r\n                    <input type=@text@ id=@^ReplaceAttribute^Start_Time@ onclick=@WdatePicker({maxDate:'#F{$dp.$D(\\'^ReplaceAttribute^End_Time\\');}'})@  />\r\n                    <span>到</span>\r\n                    <input type=@text@ id=@^ReplaceAttribute^End_Time@ onclick=@WdatePicker({minDate:'#F{$dp.$D(\\'^ReplaceAttribute^Start_Time\\');}'})@ />\r\n                </div>\r\n            </div>";
    public string m_SearchZhuangtai = "\r\n            <div class=@input_search@>\r\n                <div class=@editor-label-search@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@editor-field-search@>\r\n                    <%=Html.DropDownList(@^ReplaceAttribute^DDL_String@, Models.SysFieldModels.GetSysField(@^ReplaceClassCode^@,@^ReplaceAttribute^@),@请选择@,new { id = @^ReplaceAttribute^DDL_String@ })%>\r\n                </div>\r\n            </div>";
    public string m_SearchEqString = " \r\n            <div class=@input_search@>\r\n                <div class=@editor-label-search@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@editor-field-search@>\r\n                    <input type='text' id='^ReplaceAttribute^End_String'/>\r\n                </div>\r\n            </div>";
    public string m_SearchString = " \r\n            <div class=@input_search@>\r\n                <div class=@editor-label-search@>\r\n                    <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n                </div>\r\n                <div class=@editor-field-search@>\r\n                    <input type='text' id='^ReplaceAttribute^'/>\r\n                </div>\r\n            </div>";
    public string m_SearchAuto = " $(@#^ReplaceAttribute^@).autocomplete({\r\n                                    source: @../^ReplaceClassCode^/SearchAutoComplete/^ReplaceAttribute^@,\r\n                                    minLength: 1,\r\n                                    select: function (event, ui) {\r\n                                        //  alert(ui.item.value);\r\n                                    }\r\n                                });";
    public string m_SearchAutoComplete = "^m_SearchAutoComplete^";
    public string m_SearchRefDuo = "    \r\n               <div class=@editor-label-search@>\r\n                   <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n               </div>\r\n               <div class=@editor-field-search@>\r\n                   <%=Html.DropDownListFor(model => model.^ReplaceAttribute^,Models.SysFieldModels.GetSysField(@^ReplaceClassCode^@,@^ReplaceAttribute^@),@请选择@)%>  \r\n               </div>";
    public string m_TreeIndexMain = "\\IndexMainTree.aspx";
    public string m_PickTimeReplace = "^m_PickTimeReplace^";
    public string m_Search = "^m_Search^";

    public void DoIndex(View replaceClass, ref List<string> fileName)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      bool flag = false;
      int num1 = replaceClass.Columns.Count - 4;
      if (num1 <= 0)
        num1 = 3;
      int num2 = 678 / num1;
      int num3 = 582 / num1;
      int num4 = 0;
      string str3 = string.Empty;
      string newValue4 = string.Empty;
      foreach (Column column in replaceClass.Columns)
      {
        if (Common.IsSort(column.Comment))
          newValue4 = column.Code;
        if (!Common.IsNotDisplay(column.Comment))
        {
          ++num4;
          if (num4 == 1)
            str2 = str2 + (object) "\n\t\t\t\t\t{ field: '" + column.Code + "', title: '<%: Html.DisplayNameFor(model => model." + column.Code + ") %>', width: " + (string) (object) num2 + " }";
          else if (Common.IsDateType(column.DataType))
            str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + column.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + column.Code + ") %>', width: " + (string) (object) num2 + "\r\n                    , formatter: function (value, rec) {\r\n                        if (value) {\r\n                            return dateConvert(value);\r\n                        } \r\n                    } \r\n}";
          else if (Common.IsStampType(column.DataType))
            str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + column.Code + "', title: '<%: Html.DisplayNameFor(model => model." + column.Code + ") %>', width: " + (string) (object) num2 + ", hidden: true }";
          else
            str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + column.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + column.Code + ") %>', width: " + (string) (object) num2 + " }";
          if (!string.IsNullOrWhiteSpace(column.Comment) && column.Comment.Contains("Research"))
          {
            if (Common.IsIntType(column.DataType))
              newValue2 += this.m_SearchInt.Replace(this.m_ReplaceAttribute, column.Code).Replace('\'', '"');
            else if (Common.IsDateType(column.DataType))
            {
              if (!flag)
              {
                newValue3 += this.m_PickTime.Replace(this.m_ReplaceAttribute, column.Code);
                flag = true;
              }
              newValue2 += this.m_SearchDateTime.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
            }
            else if (column.Comment.Contains("DropDown"))
              newValue2 += this.m_SearchZhuangtai.Replace(this.m_ReplaceClassCode, column.TableCode).Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
            else if (column.Comment.Contains("Equal"))
            {
              newValue2 += this.m_SearchEqString.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
            }
            else
            {
              newValue2 += this.m_SearchString.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
              newValue1 += this.m_SearchAuto.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
            }
          }
          else if (!string.IsNullOrWhiteSpace(column.Comment) && column.Comment.Contains("Equal"))
            newValue2 += this.m_SearchEqString.Replace(this.m_ReplaceAttribute, column.Code).Replace('@', '"');
        }
      }
      foreach (Column column in replaceClass.Columns)
      {
        if (Common.IsSort(column.Comment))
        {
          newValue4 = column.Code;
          break;
        }
      }
      if (string.IsNullOrEmpty(newValue4))
        newValue4 = replaceClass.Columns[0].Code;
      string newValue5 = str2 + str1;
      string content = Common.Read(BaseClass.m_DempDirectory + "/ViewIndex.aspx").Replace(this.m_Id, newValue4).Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace("App.", replaceClass.NameSpace + "App.").Replace(this.m_SearchAutoComplete, newValue1).Replace(this.m_PickTimeReplace, newValue3).Replace(this.m_Search, newValue2).Replace(this.m_ReplaceAttribute, newValue5).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
      string path = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
      Directory.CreateDirectory(path);
      Common.Write(path + "/Index.aspx", content);
      fileName.Add(replaceClass.Code + "/Index.aspx");
    }

    public void DoIndex(Table replaceClass, ref List<string> fileName)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      bool flag1 = false;
      int num1 = replaceClass.Columns.Count - 2;
      if (num1 <= 0)
        num1 = 3;
      int num2 = 678 / num1;
      int num3 = 582 / num1;
      int num4 = 0;
      string newValue4 = string.Empty;
      string newValue5 = string.Empty;
      bool flag2 = false;
      foreach (Column column in replaceClass.Columns)
      {
        Column item = column;
        if (Common.IsSort(item.Comment))
          newValue5 = item.Code;
        if (!Common.IsNotDisplay(item.Comment))
        {
          ++num4;
          if (num4 > 3)
          {
            if (Common.IsStampType(item.DataType))
              newValue4 = newValue4 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title: '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + ", hidden: true }";
            else if (Common.IsDateType(item.DataType))
              newValue4 = newValue4 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + "\r\n                    , formatter: function (value, rec) {\r\n                        if (value) {\r\n                            return dateConvert(value);\r\n                        } \r\n                    } \r\n}";
            else if (Common.GetForeignKey(replaceClass, item) != null)
              newValue4 = newValue4 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title: '<%: Html.DisplayNameFor(model => model." + item.Code + "Old) %>', width: " + (string) (object) num2 + "}";
            else
              newValue4 = newValue4 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + " }";
          }
          if (!Common.IsPrimaryKey(replaceClass, item.Id))
          {
            if (!flag2)
            {
              flag2 = true;
              str2 = str2 + (object) "\n\t\t\t\t\t{ field: '" + item.Code + "', title: '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + " }";
            }
            else if (Common.IsDateType(item.DataType))
              str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + "\r\n                    , formatter: function (value, rec) {\r\n                        if (value) {\r\n                            return dateConvert(value);\r\n                        } \r\n                    } \r\n}";
            else if (Common.IsStampType(item.DataType))
              str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title: '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + ", hidden: true }";
            else if (Common.GetForeignKey(replaceClass, item) != null)
              str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title: '<%: Html.DisplayNameFor(model => model." + item.Code + "Old) %>', width: " + (string) (object) num2 + " }";
            else
              str2 = str2 + (object) "\n\t\t\t\t\t,{ field: '" + item.Code + "', title:  '<%: Html.DisplayNameFor(model => model." + item.Code + ") %>', width: " + (string) (object) num2 + " }";
            if (!string.IsNullOrWhiteSpace(item.Comment) && item.Comment.Contains("Research"))
            {
              if (Common.IsIntType(item.DataType))
                newValue2 += this.m_SearchInt.Replace(this.m_ReplaceAttribute, item.Code).Replace('\'', '"');
              else if (Common.IsDateType(item.DataType))
              {
                if (!flag1)
                {
                  newValue3 += this.m_PickTime.Replace(this.m_ReplaceAttribute, item.Code);
                  flag1 = true;
                }
                newValue2 += this.m_SearchDateTime.Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
              }
              else if (item.Comment.Contains("DropDown"))
                newValue2 += this.m_SearchZhuangtai.Replace(this.m_ReplaceClassCode, item.TableCode).Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
              else if (item.Comment.Contains("Equal"))
              {
                newValue2 += this.m_SearchEqString.Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
              }
              else
              {
                newValue2 += this.m_SearchString.Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
                newValue1 += this.m_SearchAuto.Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
              }
            }
            else if (!string.IsNullOrWhiteSpace(item.Comment) && item.Comment.Contains("Equal"))
              newValue2 += this.m_SearchEqString.Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
            else if (replaceClass.refNotId != null && Enumerable.FirstOrDefault<RefIdName>(Enumerable.Where<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId, (Func<RefIdName, bool>) (a => item.Code.Contains(a.RefTableCode) && item.Code.Contains(a.Id)))) != null)
              newValue2 += this.m_SearchRefDuo.Replace(this.m_ReplaceClassCode, item.TableCode).Replace(this.m_ReplaceAttribute, item.Code).Replace('@', '"');
          }
        }
      }
      if (string.IsNullOrEmpty(newValue5))
        newValue5 = Common.GetFirstPrimaryKeyCode(replaceClass);
      string newValue6 = str2 + str1;
      string str3 = "/Index.aspx";
      string path1 = BaseClass.m_DempDirectory + str3;
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          newValue6 = newValue6 + (object) "\t\t\t\t\t//, { display: '<%: Html.DisplayNameFor(model => model." + refIdName.RefTableCode + refIdName.Id + ") %>', name: '" + refIdName.RefTableCode + refIdName.Id + "', width: " + (string) (object) num2 + ", sortable: false, align: 'left' }\n";
          if (refIdName.IsRefSelf)
          {
            path1 = BaseClass.m_DempDirectory + "/TreeIndexWai.aspx";
            string content1 = Common.Read(path1).Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace("^m_RefTableName^", refIdName.RefTableName).Replace("^m_Wai^", refIdName.RefTableCode).Replace(this.m_PickTimeReplace, newValue3).Replace(this.m_Search, newValue2).Replace(this.m_ReplaceAttribute, newValue6).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Sort, newValue5).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass));
            string path2 = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
            Directory.CreateDirectory(path2);
            Common.Write(path2 + "/Index.aspx", content1);
            path1 = BaseClass.m_DempDirectory + "/Index.aspx";
            string content2 = Common.Read(path1).Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace(this.m_SearchAutoComplete, newValue1).Replace(this.m_PickTimeReplace, newValue3).Replace(this.m_Search, newValue2).Replace(this.m_ReplaceAttribute, newValue6).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Sort, newValue5).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass));
            string path3 = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
            Directory.CreateDirectory(path3);
            Common.Write(path3 + "/IndexSef.aspx", content2);
            path1 = BaseClass.m_DempDirectory + "/Set.aspx";
            string content3 = Common.Read(path1).Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace(this.m_ReplaceAttribute, refIdName.RefTableCode).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
            Directory.CreateDirectory(BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code);
            Common.Write(path2 + "/Set" + refIdName.RefTableCode + ".aspx", content3);
            return;
          }
        }
      }
      if (replaceClass.childTableColumnRef != null)
      {
        Common.GetColumnByKey(replaceClass, replaceClass.childTableColumnRef);
        string content = Common.Read(BaseClass.m_DempDirectory + "/IndexTree.aspx").Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace(this.m_ReplaceAttribute, newValue4).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
        string path2 = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
        Directory.CreateDirectory(path2);
        Common.Write(path2 + str3, content);
        str3 = "/IndexSef.aspx";
      }
      string content4 = Common.Read(path1).Replace(this.m_Sort, newValue5).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace("ViewPage<DAL.", "ViewPage<" + replaceClass.NameSpace + "DAL.").Replace("App.", replaceClass.NameSpace + "App.").Replace(this.m_SearchAutoComplete, newValue1).Replace(this.m_PickTimeReplace, newValue3).Replace(this.m_Search, newValue2).Replace(this.m_ReplaceAttribute, newValue6).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, replaceClass.Name);
      string path4 = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code;
      Directory.CreateDirectory(path4);
      Common.Write(path4 + str3, content4);
      fileName.Add(replaceClass.Code + str3);
    }
  }
}
