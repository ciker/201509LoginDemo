// Decompiled with JetBrains decompiler
// Type: CodeMaker.Models
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.Linq;

namespace CodeMaker
{
  public class Models : BaseClass
  {
    public string m_CustomEntity = "^CustomEntity^";
    public string m_CustomMetadataNotRef = "\r\n        [Display(Name = @主键@)]\r\n        public string Id { get; set; }\r\n        [Display(Name = @主键@)]\r\n        public string IdOld { get; set; }\r\n        ";
    public string m_CustomMetadataRef = "\r\n        [Display(Name = @主键@)]\r\n        public string IdOld { get; set; }\r\n        ";
    public string m_parentId = "     \r\n    public class ^ReplaceClassCode^Sef\r\n    {    \r\n            public string state { get; set; }\r\n            public string _parentId { get; set; }\r\n\r\n        ";

    public bool DoModels(View replaceClass, ref List<string> fileName)
    {
      int num = 1;
      string newValue = string.Empty;
      foreach (Column column in replaceClass.Columns)
      {
        if (!string.IsNullOrWhiteSpace(column.Code))
        {
          if (!string.IsNullOrWhiteSpace(column.Name))
            this.m_Content.Append("\t\t\t[Display(Name = \"" + (object) column.Name + "\", Order = " + (string) (object) num + ")]\n");
          else
            this.m_Content.Append("\t\t\t[Display(Name = \"" + (object) column.Code + "\", Order = " + (string) (object) num + ")]\n");
          this.m_Content.Append("\t\t\tpublic object " + column.Code + " { get; set; }\n\n");
          ++num;
        }
      }
      string content = Common.Read(BaseClass.m_DempDirectory + "/ViewModel.cs").Replace(this.m_CustomEntity, newValue).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceAttribute, this.m_Content.ToString()).Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_DAL + "/" + replaceClass.Code + ".cs", content);
      fileName.Add(replaceClass.Code);
      this.m_Content.Clear();
      return true;
    }

    public bool DoModels(Table replaceClass, ref List<string> fileName)
    {
      int num = 1;
      string newValue = string.Empty;
      if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refId)
          newValue += this.m_CustomMetadataRef.Replace('@', '"').Replace("Id", refIdName.Ref).Replace("主键", refIdName.RefName);
      }
      if (replaceClass.childTableColumnRef != null)
      {
        Column columnByKey = Common.GetColumnByKey(replaceClass, replaceClass.childTableColumnRef);
        newValue += this.m_CustomMetadataRef.Replace('@', '"').Replace("Id", columnByKey.Code).Replace("主键", columnByKey.Name);
      }
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
          newValue += this.m_CustomMetadataNotRef.Replace('@', '"').Replace("Id", refIdName.RefTableCode + refIdName.Id).Replace("主键", refIdName.RefTableName);
      }
      foreach (Column column in replaceClass.Columns)
      {
        if (!string.IsNullOrWhiteSpace(column.Code))
        {
          if (Common.IsPrimaryKey(replaceClass, column.Id))
          {
            this.m_Content.Append("\t\t\t[ScaffoldColumn(false)]\n");
            this.m_Content.Append("\t\t\t[Display(Name = \"" + (object) column.Name + "\", Order = " + (string) (object) num + ")]\n");
            this.m_Content.Append("\t\t\tpublic object " + column.Code + " { get; set; }\n\n");
            ++num;
          }
          else
          {
            this.m_Content.Append("\t\t\t[ScaffoldColumn(true)]\n");
            this.m_Content.Append("\t\t\t[Display(Name = \"" + (object) column.Name + "\", Order = " + (string) (object) num + ")]\n");
            if (!string.IsNullOrWhiteSpace(column.Mandatory) && column.Mandatory.Trim() == "1")
              this.m_Content.Append("\t\t\t[Required(ErrorMessage = @不能为空@)]\n".Replace('@', '"'));
            if (!string.IsNullOrEmpty(column.Length) && Common.IsStringType(column.DataType))
              this.m_Content.Append("\t\t\t[StringLength(" + column.Length + ", ErrorMessage = \"长度不可超过" + column.Length + "\")]\n");
            string code = this.GetCode(column.Code);
            if (string.IsNullOrWhiteSpace(code))
              this.SetAttributeName(column);
            else
              this.m_Content.Append(code);
            if (Common.IsIntType(column.DataType))
              this.m_Content.Append("\t\t\tpublic int? " + column.Code + " { get; set; }\n\n");
            else if (Common.IsDateType(column.DataType))
            {
              if (Common.IsCreateTime(column) || Common.IsUpdateTime(column))
              {
                this.m_Content.Append("\t\t\tpublic DateTime? " + column.Code + " { get; set; }\n\n");
              }
              else
              {
                this.m_Content.Append("\t\t\t[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = \"{0:d}\")]\n\n");
                this.m_Content.Append("\t\t\tpublic DateTime? " + column.Code + " { get; set; }\n\n");
              }
            }
            else
              this.m_Content.Append("\t\t\tpublic object " + column.Code + " { get; set; }\n\n");
            ++num;
          }
        }
      }
      string content = Common.Read(BaseClass.m_DempDirectory + "/Model.cs").Replace(this.m_CustomEntity, newValue).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceAttribute, this.m_Content.ToString()).Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_DAL + "/" + replaceClass.Code + ".cs", content);
      fileName.Add(replaceClass.Code);
      this.m_Content.Clear();
      return true;
    }

    public string GetCode(string attributeCode)
    {
      attributeCode = attributeCode.ToUpper();
      switch (attributeCode)
      {
        case "TIME":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Time)]\n";
        case "DATE":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Date)]\n";
        case "DURATION":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Duration)]\n";
        case "PHONENUMBER":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber,ErrorMessage=\"号码格式不正确\")]\n";
        case "MultilineText":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText,ErrorMessage=\"多文本格式不正确\")]\n";
        case "EMIALADDRESS":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress,ErrorMessage=\"邮件格式不正确\")]\n";
        case "PASSWORD":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]\n";
        case "SUREPASSWORD":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]\n\t\t\t[System.Web.Mvc.Compare(\"Password\", ErrorMessage = \"两次密码不一致\")]\n";
        case "IMAGEURL":
          return "\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.ImageUrl,ErrorMessage=\"图片地址格式不正确\")]\n";
        default:
          return string.Empty;
      }
    }

    public bool SetAttributeName(Column attribute)
    {
      string dataType = attribute.DataType;
      string str1 = "0";
      string str2 = "2147483646";
      if (Common.IsIntType(attribute.DataType))
      {
        if (!string.IsNullOrEmpty(attribute.LowValue))
          str1 = attribute.LowValue;
        if (!string.IsNullOrEmpty(attribute.HeighValue))
          str2 = attribute.HeighValue;
        this.m_Content.Append("\t\t\t[Range(" + str1 + "," + str2 + ", ErrorMessage=\"数值超出范围\")]\n");
        return true;
      }
      if (Common.IsDateType(attribute.DataType))
      {
        this.m_Content.Append("\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage=\"时间格式不正确\")]\n");
        return true;
      }
      if (!Common.IsMoneyType(attribute.DataType))
        return false;
      this.m_Content.Append("\t\t\t[DataType(System.ComponentModel.DataAnnotations.DataType.Currency,ErrorMessage=\"货币格式不正确\")]\n");
      return true;
    }
  }
}
