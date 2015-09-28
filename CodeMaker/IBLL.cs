// Decompiled with JetBrains decompiler
// Type: CodeMaker.IBLL
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeMaker
{
  public class IBLL : BaseClass
  {
    public string m_IRefGetSelectDuo = "/// <summary>\r\n        /// 获取在该表中出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@></param>\r\n        /// <returns></returns>\r\n        List<^ReplaceAttribute^> GetRef^ReplaceAttribute^();\r\n        ";
    public string m_IRefSet = "\r\n        /// <summary>\r\n        ///  设置对象集合\r\n        /// </summary>\r\n        /// <param name=@validationErrors@>返回的错误信息</param>\r\n        /// <param name=@entitys@>对象集合</param>\r\n        /// <returns></returns>\r\n        [OperationContract]\r\n        bool Set^ReplaceClassCode^(ref ValidationErrors validationErrors, ^ReplaceAttribute^ entity);\r\n";
    public string m_RefByDuoId = "  /// <summary>\r\n        /// 根据外键获取关联表\r\n        /// </summary>\r\n        /// <param name='id'></param>\r\n        /// <returns></returns>\r\n        ^ReplaceClassCodeRef^ GetBy^ReplaceAttribute^(string id);\r\n      \r\n          ";
    public string m_ReplaceClassCodeRef = "^ReplaceClassCodeRef^";
    public string m_ViewIRepository = "\\ViewIReplaceClassDuo.cs";
    public string m_AreasIRepository = "\\IReplaceClassDuo.cs";
    public string m_IRefGetSelectList = "^m_IRefGetSelectList^";
    public string m_RefById = "^m_RefById^";
    public string m_IBusinessRules = "IBLL";
    public string m_ByRefId = "\r\n        /// <summary>\r\n        /// 根据^ReplaceClassCode^Id，获取所有^ReplaceAttribute^数据\r\n        /// </summary>\r\n        /// <param name=@id@>外键的主键</param>\r\n        /// <returns></returns>\r\n        List<^ReplaceClassName^> GetByRef^ReplaceClassCode^(^string^ id);\r\n";

    public void DoIRepository(Table replaceClass, ref List<string> fileName)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string str3 = string.Empty;
      string primaryKeyType = replaceClass.PrimaryKeyType;
      string nullPrimaryKeyType = Common.GetNullPrimaryKeyType(replaceClass.PrimaryKeyType);
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          newValue2 += this.m_IRefGetSelectDuo.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode).Replace('@', '"');
          if (refIdName.IsRefSelf)
            newValue2 += this.m_IRefSet.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, replaceClass.Code).Replace('@', '"');
        }
      }
      if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refId)
          newValue2 += this.m_ByRefId.Replace(this.m_String, refIdName.RefType).Replace(this.m_ReplaceClassCode, refIdName.Ref).Replace(this.m_ReplaceAttribute, replaceClass.Name).Replace(this.m_ReplaceClassName, replaceClass.Code).Replace('@', '"');
      }
      string content = Common.Read(BaseClass.m_DempDirectory + this.m_AreasIRepository).Replace(BaseClass.m_IBLLnamespace, replaceClass.NameSpace + "IBLL").Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL").Replace(this.m_IRefGetSelectList, newValue2).Replace(this.m_RefById, newValue1).Replace(this.m_String, primaryKeyType).Replace(this.m_StringCreate, nullPrimaryKeyType).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code);
      string path = BaseClass.m_RootDirectory + "/" + this.m_IBusinessRules + "/";
      Directory.CreateDirectory(path);
      Common.Write(path + "I" + replaceClass.Code + "BLL.cs", content);
      fileName.Add(replaceClass.Code);
    }

    public void DoIRepository(View replaceClass, ref List<string> fileName)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string str3 = string.Empty;
      string content = Common.Read(BaseClass.m_DempDirectory + this.m_ViewIRepository).Replace(BaseClass.m_IBLLnamespace, replaceClass.NameSpace + "IBLL").Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL").Replace(this.m_IRefGetSelectList, newValue2).Replace(this.m_RefById, newValue1).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code);
      string path = BaseClass.m_RootDirectory + "/" + this.m_IBusinessRules + "/";
      Directory.CreateDirectory(path);
      Common.Write(path + "I" + replaceClass.Code + "BLL.cs", content);
      fileName.Add(replaceClass.Code);
    }
  }
}
