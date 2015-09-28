// Decompiled with JetBrains decompiler
// Type: CodeMaker.Repository
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.Linq;

namespace CodeMaker
{
  internal class Repository : BaseClass
  {
    public string m_ViewDanMoban = "\\ViewReplaceClassRepositoryDan.cs";
    public string m_DanMoban = "\\ReplaceClassRepositoryDan.cs";
    public string m_DuoMoban = "\\ReplaceClassRepositoryDuo.cs";
    public string m_ReposDan = "^m_ReposDan^";
    public string m_ReposLocalDan = "\r\n                    if (queryDic.ContainsKey(@^ReplaceAttribute^@) && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Value == @noway@ && item.Key == @^ReplaceAttribute^@)\r\n                    {//查询一对多关系的列名\r\n                        where += @it.^ReplaceAttribute^ is null@;\r\n                        continue;\r\n                    }";
    public string m_Repos = "^m_Repos^";
    public string m_ReposLocal = "\r\n                    if (queryDic.ContainsKey(@^ReplaceClassCode^@)&& !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == @^ReplaceClassCode^@)\r\n                    {//查询多对多关系的列名\r\n                        where += @EXISTS(select p from it.^ReplaceClassCode^ as p where p.^m_Id^='@ + item.Value + @')@;\r\n                        continue;\r\n                    }";
    public string m_RefGetSelectDuo = "\r\n        /// <summary>\r\n        /// 获取在该表一条数据中，出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public IQueryable<^ReplaceClassCode^> GetRef^ReplaceClassCode^(^string^ id)\r\n        {\r\n            using (SysEntities db = new SysEntities())\r\n            {\r\n                return GetRef^ReplaceClassCode^(db, id);\r\n            }\r\n        }\r\n        /// <summary>\r\n        /// 获取在该表一条数据中，出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public IQueryable<^ReplaceClassCode^> GetRef^ReplaceClassCode^(SysEntities db, ^string^ id)\r\n        {\r\n                return from m in db.^SysRole^\r\n                       from f in m.^ReplaceClassCode^\r\n                       where m.^m_Id^ == id\r\n                       select f;\r\n\r\n        }\r\n        /// <summary>\r\n        /// 获取在该表中出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public IQueryable<^ReplaceClassCode^> GetRef^ReplaceClassCode^(SysEntities db)\r\n        {\r\n            return from m in db.^SysRole^\r\n                   from f in m.^ReplaceClassCode^\r\n                   select f;\r\n        }\r\n        /// <summary>\r\n        /// 获取在该表中出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public IQueryable<^ReplaceClassCode^> GetRef^ReplaceClassCode^()\r\n        {\r\n            using (SysEntities db = new SysEntities())\r\n            {\r\n                return GetRef^ReplaceClassCode^(db);\r\n            }\r\n        }\r\n";
    public string m_ByRefId = "\r\n        /// <summary>\r\n        /// 根据^ReplaceClassCode^，获取所有^ReplaceAttribute^数据\r\n        /// </summary>\r\n        /// <param name=@id@>外键的主键</param>\r\n        /// <returns></returns>\r\n        public IQueryable<^ReplaceClassName^> GetByRef^ReplaceClassCode^(SysEntities db, ^string^ id)\r\n        {\r\n            return from c in db.^ReplaceClassName^\r\n                        where c.^ReplaceClassCode^ == id\r\n                        select c;\r\n                      \r\n        }\r\n";
    public string m_GetByRefId = "^m_GetByRefId^";
    public string m_RefGetSelectList = "^m_RefGetSelectList^";

    public void DoRepository(Table replaceClass, ref List<string> fileName)
    {
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      string str1 = string.Empty;
      string newValue4 = string.Empty;
      string primaryKeyType = replaceClass.PrimaryKeyType;
      if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refId)
        {
          newValue3 += this.m_ReposLocalDan.Replace(this.m_ReplaceAttribute, refIdName.Ref).Replace('@', '"');
          newValue4 += this.m_ByRefId.Replace(this.m_String, refIdName.RefType).Replace(this.m_ReplaceClassCode, refIdName.Ref).Replace(this.m_ReplaceAttribute, replaceClass.Name).Replace(this.m_ReplaceClassName, replaceClass.Code).Replace('@', '"');
        }
      }
      string str2;
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          newValue2 += this.m_ReposLocal.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, refIdName.Id).Replace('@', '"');
          newValue1 += this.m_RefGetSelectDuo.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace('@', '"').Replace("^SysRole^", replaceClass.Code);
        }
        str2 = Common.Read(BaseClass.m_DempDirectory + this.m_DuoMoban).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_GetByRefId, newValue4).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_ReposDan, newValue3).Replace(this.m_RefGetSelectList, newValue1).Replace(this.m_Repos, newValue2).Replace(this.m_String, primaryKeyType);
      }
      else
        str2 = Common.Read(BaseClass.m_DempDirectory + this.m_DanMoban).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_GetByRefId, newValue4).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_ReposDan, newValue3).Replace(this.m_String, primaryKeyType);
      string content = str2.Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_DAL + "/" + replaceClass.Code + "Repository.cs", content);
      fileName.Add(replaceClass.Code);
    }

    public void DoRepository(View replaceClass, ref List<string> fileName)
    {
      string newValue = string.Empty;
      string content = Common.Read(BaseClass.m_DempDirectory + this.m_ViewDanMoban).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, replaceClass.Columns[0].Code).Replace(this.m_ReposDan, newValue).Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_DAL + "/" + replaceClass.Code + "Repository.cs", content);
      fileName.Add(replaceClass.Code);
    }
  }
}
