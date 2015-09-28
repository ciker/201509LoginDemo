// Decompiled with JetBrains decompiler
// Type: CodeMaker.BLL
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMaker
{
  public class BLL : BaseClass
  {
    public new string m_BLL = "BLL";
    public string m_ViewDanMoban = "\\ViewReplaceClassBLLDan.cs";
    public string m_DanMoban = "\\ReplaceClassBLLDan.cs";
    public string m_DuoMoban = "\\ReplaceClassBLLDuo.cs";
    public string m_CreateDown = "\r\n            foreach (string item in entity.^ReplaceAttribute^.GetIdSort())\r\n            {\r\n                ^ReplaceClassCode^ sys = new ^ReplaceClassCode^ { ^m_Id^ = ^item^ };\r\n                db.^ReplaceClassCode^.Attach(sys);\r\n                entity.^ReplaceClassCode^.Add(sys);\r\n                count++;\r\n            }\r\n";
    public string m_DuoRefCreateDown = "^m_DuoRefCreateDown^";
    public string m_EditDown = "\r\n            List<string> add^ReplaceAttribute^ = new List<string>();\r\n            List<string> delete^ReplaceAttribute^ = new List<string>();\r\n            DataOfDiffrent.GetDiffrent(entity.^ReplaceAttribute^.GetIdSort(), entity.^ReplaceAttribute^Old.GetIdSort(), ref add^ReplaceAttribute^, ref delete^ReplaceAttribute^);\r\n            if (add^ReplaceAttribute^ != null && add^ReplaceAttribute^.Count() > 0)\r\n            {\r\n                foreach (var item in add^ReplaceAttribute^)\r\n                {\r\n                    ^ReplaceClassCode^ sys = new ^ReplaceClassCode^ { ^m_Id^ = ^item^ };\r\n                    db.^ReplaceClassCode^.Attach(sys);\r\n                    editEntity.^ReplaceClassCode^.Add(sys);\r\n                    count++;\r\n                }\r\n            }\r\n            if (delete^ReplaceAttribute^ != null && delete^ReplaceAttribute^.Count() > 0)\r\n            {\r\n                List<^ReplaceClassCode^> listEntity = new List<^ReplaceClassCode^>();\r\n                foreach (var item in delete^ReplaceAttribute^)\r\n                {\r\n                    ^ReplaceClassCode^ sys = new ^ReplaceClassCode^ { ^m_Id^ = ^item^ };\r\n                    listEntity.Add(sys);\r\n                    db.^ReplaceClassCode^.Attach(sys);\r\n                }\r\n                foreach (^ReplaceClassCode^ item in listEntity)\r\n                {\r\n                    editEntity.^ReplaceClassCode^.Remove(item);//查询数据库\r\n                    count++;\r\n                }\r\n            } \r\n";
    public string m_DuoRefEditDown = "^m_DuoRefEditDown^";
    public string m_ReplaceFlexigrid = "^m_ReplaceFlexigrid^";
    public string m_FlexigridSelfRef = "\r\n                        if (item.^ReplaceAttribute^ != null && item.^ReplaceClassCode^2 != null)\r\n                        { \r\n                                item.^ReplaceAttribute^Old = item.^ReplaceClassCode^2.^m_Name^.GetString();//                            \r\n                        }                    \r\n";
    public string m_FlexigridRef = "\r\n                        if (item.^ReplaceAttribute^ != null && item.^ReplaceClassCode^ != null)\r\n                        { \r\n                                item.^ReplaceAttribute^Old = item.^ReplaceClassCode^.^m_Name^.GetString();//                            \r\n                        }                  \r\n";
    public string m_FlexigridNotRef = " \r\n                        if (item.^ReplaceClassCode^ != null)\r\n                        {\r\n                            item.^ReplaceAttribute^ = string.Empty;\r\n                            foreach (var it in item.^ReplaceClassCode^)\r\n                            {\r\n                                item.^ReplaceAttribute^ += it.^m_Name^ + ' ';\r\n                            }                         \r\n                        } \r\n";
    public string m_FlexigridDropDownList = "  \r\n                        if (!string.IsNullOrWhiteSpace(item.^ReplaceAttribute^))\r\n                        {\r\n                            item.^ReplaceAttribute^ = db.SysField.Where(s => s.Id == item.^ReplaceAttribute^).Select(s => s.MyTexts).FirstOrDefault();\r\n                        }\r\n";
    private string m_ReplaceFlexigridSef = "^m_ReplaceFlexigridSef^";
    private string m_ReplaceFlexigridSefContent = "\r\n            if (null != id)\r\n            {\r\n                search += @^ReplaceClassCode^&@ + id + @^@;\r\n            }\r\n            ";
    public string m_RefGetSelectDuo = "\r\n        /// <summary>\r\n        /// 获取在该表一条数据中，出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public List<^ReplaceClassCode^> GetRef^ReplaceClassCode^(^string^ id)\r\n        { \r\n            return repository.GetRef^ReplaceClassCode^(db, id).ToList();\r\n        }\r\n        /// <summary>\r\n        /// 获取在该表中出现的所有外键实体\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns>外键实体集合</returns>\r\n        public List<^ReplaceClassCode^> GetRef^ReplaceClassCode^()\r\n        { \r\n            return repository.GetRef^ReplaceClassCode^(db).ToList();\r\n        }\r\n";
    public string m_ByRefId = "\r\n        /// <summary>\r\n        /// 根据^ReplaceClassCode^Id，获取所有^ReplaceAttribute^数据\r\n        /// </summary>\r\n        /// <param name=@id@>外键的主键</param>\r\n        /// <returns></returns>\r\n        public List<^ReplaceClassName^> GetByRef^ReplaceClassCode^(^string^ id)\r\n        {\r\n            return repository.GetByRef^ReplaceClassCode^(db, id).ToList();                      \r\n        }\r\n";
    public string m_GetByRefId = "^m_GetByRefId^";
    public string m_RefGetSelectList = "^m_RefGetSelectList^";
    public string m_SetToMany = "\r\n\t\t/// <summary>\r\n        /// 设置一个^ReplaceClassName^\r\n        /// </summary>\r\n        /// <param name=@validationErrors@>返回的错误信息</param>\r\n        /// <param name=@entity@>一个^ReplaceClassName^</param>\r\n        /// <returns>是否设置成功</returns>\r\n        public bool Set^ReplaceClassCode^(ref ValidationErrors validationErrors, ^m_PickTimeCrea^ entity)\r\n        {\r\n            bool bResult = false;\r\n            int count = 0;\r\n            using (TransactionScope transactionScope = new TransactionScope())\r\n            {\r\n                try\r\n                {\r\n                    ^m_PickTimeCrea^ editEntity = repository.GetById(db, entity.^m_Id^);\r\n\r\n                    List<string> add^ReplaceAttribute^ = new List<string>();\r\n                    List<string> delete^ReplaceAttribute^ = new List<string>();\r\n                    if (entity.^ReplaceAttribute^ != null)\r\n                    {\r\n                        add^ReplaceAttribute^ = entity.^ReplaceAttribute^.Split(',').ToList();\r\n                    }\r\n                    if (entity.^ReplaceAttribute^Old != null)\r\n                    {\r\n                        delete^ReplaceAttribute^ = entity.^ReplaceAttribute^Old.Split(',').ToList();\r\n                    }\r\n                    DataOfDiffrent.GetDiffrent(add^ReplaceAttribute^, delete^ReplaceAttribute^, ref add^ReplaceAttribute^, ref delete^ReplaceAttribute^);\r\n\r\n                    if (add^ReplaceAttribute^ != null && add^ReplaceAttribute^.Count() > 0)\r\n                    {\r\n                        foreach (var item in add^ReplaceAttribute^)\r\n                        {\r\n                            ^ReplaceClassCode^ sys = new ^ReplaceClassCode^ { ^m_Id^ = ^item^ };\r\n                            db.^ReplaceClassCode^.Attach(sys);\r\n                            editEntity.^ReplaceClassCode^.Add(sys);\r\n                            count++;\r\n                        }\r\n                    }\r\n                    if (delete^ReplaceAttribute^ != null && delete^ReplaceAttribute^.Count() > 0)\r\n                    {\r\n                        List<^ReplaceClassCode^> listEntity = new List<^ReplaceClassCode^>();\r\n                        foreach (var item in delete^ReplaceAttribute^)\r\n                        {\r\n                            ^ReplaceClassCode^ sys = new ^ReplaceClassCode^ { ^m_Id^ = ^item^ };\r\n                            listEntity.Add(sys);\r\n                            db.^ReplaceClassCode^.Attach(sys);\r\n                        }\r\n                        foreach (^ReplaceClassCode^ item in listEntity)\r\n                        {\r\n                            editEntity.^ReplaceClassCode^.Remove(item);//查询数据库\r\n                            count++;\r\n                        }\r\n                    } \r\n\r\n                    if (count > 0 && count == repository.Save(db))\r\n                    {\r\n                       transactionScope.Complete();\r\n                       bResult = true;\r\n                    }\r\n                    else if(count == 0 )\r\n                    {\r\n                        validationErrors.Add(@数据没有改变@);\r\n                    }\r\n                }\r\n                catch (Exception ex)\r\n                {\r\n                    Transaction.Current.Rollback();                    \r\n                    ExceptionsHander.WriteExceptions(ex);\r\n                    validationErrors.Add(@编辑出错了。原因@+ex.Message);\r\n                }\r\n            }\r\n            \r\n            return bResult;\r\n        }\r\n";
    public string m_CopyMetadata = "^m_CopyMetadata^";
    public string m_CopyMetadata2 = "\r\n        /// <summary>\r\n        /// 获取自连接树形列表数据\r\n        /// </summary>\r\n        /// <returns>自定义的树形结构</returns>\r\n                   ^m_CopyMetadata^\r\n            ";

    public string DoBLL(Table replaceClass, ref List<string> fileName)
    {
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      string str1 = string.Empty;
      string newValue4 = string.Empty;
      string primaryKeyType = replaceClass.PrimaryKeyType;
      string nullPrimaryKeyType = Common.GetNullPrimaryKeyType(replaceClass.PrimaryKeyType);
      if (replaceClass.childTableColumnRef != null)
      {
        Column columnByKey = Common.GetColumnByKey(replaceClass, replaceClass.childTableColumnRef);
        if (string.IsNullOrWhiteSpace(newValue3))
          newValue3 += "\r\n                    foreach (var item in queryData)\r\n                    {";
        newValue3 += this.m_FlexigridSelfRef.Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceAttribute, columnByKey.Code).Replace(this.m_Name, Common.GetShowColumnCode((TableView) replaceClass)).Replace('@', '"');
        int num = 0;
        foreach (Column column in replaceClass.Columns)
        {
          Column it = column;
          if (!Common.IsStampType(it.DataType))
          {
            ++num;
            RefIdName refIdName = new RefIdName();
            if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
              refIdName = Enumerable.FirstOrDefault<RefIdName>(Enumerable.Where<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId, (Func<RefIdName, bool>) (s => s.Ref == it.Code)));
          }
        }
        newValue2 += "\r\n        public IQueryable<^ReplaceClassCode^> GetAllMetadata(^string^ id)\r\n        { \r\n            if (id == null)\r\n            {\r\n                return db.^ReplaceClassCode^.Where(w => w.^ParentId^ == null).AsQueryable();\r\n            }\r\n            else\r\n            {\r\n                return db.^ReplaceClassCode^.Where(w => w.^ParentId^ == id).AsQueryable();\r\n            }\r\n        }\r\n".Replace("^ParentId^", columnByKey.Code).Replace(this.m_String, primaryKeyType);
      }
      if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refId)
        {
          if (string.IsNullOrWhiteSpace(newValue3))
            newValue3 += "\r\n                    foreach (var item in queryData)\r\n                    {";
          newValue3 += this.m_FlexigridRef.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.Ref).Replace(this.m_Name, refIdName.Name).Replace('@', '"');
          newValue4 += this.m_ByRefId.Replace(this.m_String, refIdName.RefType).Replace(this.m_ReplaceClassCode, refIdName.Ref).Replace(this.m_ReplaceAttribute, replaceClass.Name).Replace(this.m_ReplaceClassName, replaceClass.Code).Replace('@', '"');
        }
      }
      string str2;
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        string newValue5 = string.Empty;
        string newValue6 = string.Empty;
        string newValue7 = string.Empty;
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          if (string.IsNullOrWhiteSpace(newValue3))
            newValue3 += "\r\n                    foreach (var item in queryData) \r\n                    {";
          newValue3 += this.m_FlexigridNotRef.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Name, refIdName.Name).Replace('@', '"');
          if (refIdName.RefType == "int")
          {
            newValue6 += this.m_CreateDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "Convert.ToInt32(item)");
            newValue5 += this.m_EditDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "Convert.ToInt32(item)");
          }
          else if (refIdName.RefType == "Guid")
          {
            newValue6 += this.m_CreateDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "new Guid(item)");
            newValue5 += this.m_EditDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "new Guid(item)");
          }
          else
          {
            newValue6 += this.m_CreateDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "item");
            newValue5 += this.m_EditDown.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_Id, refIdName.Id).Replace("^item^", "item");
          }
          if (refIdName.IsRefSelf)
          {
            newValue7 += this.m_ReplaceFlexigridSefContent.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace('@', '"');
            newValue1 = !(refIdName.RefType == "int") ? (!(refIdName.RefType == "Guid") ? newValue1 + this.m_SetToMany.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_PickTimeCrea, replaceClass.Code).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassName, refIdName.RefTableName).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace('@', '"').Replace("^item^", "item") : newValue1 + this.m_SetToMany.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_PickTimeCrea, replaceClass.Code).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassName, refIdName.RefTableName).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace('@', '"').Replace("^item^", "new Guid(item)")) : newValue1 + this.m_SetToMany.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace(this.m_PickTimeCrea, replaceClass.Code).Replace(this.m_ReplaceAttribute, refIdName.RefTableCode + refIdName.Id).Replace(this.m_ReplaceClassName, refIdName.RefTableName).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace('@', '"').Replace("^item^", "Convert.ToInt32(item)");
          }
          newValue1 += this.m_RefGetSelectDuo.Replace(this.m_ReplaceClassCode, refIdName.RefTableCode).Replace('@', '"');
        }
        if (!string.IsNullOrWhiteSpace(newValue3))
          newValue3 += "\r\n                    }\r\n";
        str2 = Common.Read(BaseClass.m_DempDirectory + this.m_DuoMoban).Replace(this.m_RefGetSelectList, newValue1).Replace(this.m_ReplaceFlexigridSef, newValue7).Replace(this.m_CopyMetadata, string.IsNullOrWhiteSpace(newValue2) ? "" : this.m_CopyMetadata2.Replace(this.m_CopyMetadata, newValue2)).Replace(this.m_ReplaceFlexigrid, newValue3).Replace(this.m_DuoRefCreateDown, newValue6).Replace(this.m_DuoRefEditDown, newValue5).Replace(this.m_GetByRefId, newValue4).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_String, primaryKeyType);
      }
      else
      {
        if (!string.IsNullOrWhiteSpace(newValue3))
          newValue3 += "\r\n                    }\r\n";
        str2 = Common.Read(BaseClass.m_DempDirectory + this.m_DanMoban).Replace(this.m_CopyMetadata, string.IsNullOrWhiteSpace(newValue2) ? "" : this.m_CopyMetadata2.Replace(this.m_CopyMetadata, newValue2)).Replace(this.m_ReplaceFlexigrid, newValue3).Replace(this.m_RefGetSelectList, newValue1).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_GetByRefId, newValue4).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_String, primaryKeyType);
      }
      string newValue8 = string.Empty;
      foreach (Column column in replaceClass.Columns)
      {
        if (column.Comment.Contains("Sort"))
        {
          newValue8 = newValue8 + ".OrderBy(o => o." + column.Code + ")";
          break;
        }
      }
      if (string.IsNullOrEmpty(newValue8))
      {
        if (Common.GetFirstPrimaryKey(replaceClass) == null)
          return replaceClass.Name + "，表没有主键";
        newValue8 = ".OrderBy(o => o." + Common.GetFirstPrimaryKey(replaceClass).Code + ")";
      }
      string content = str2.Replace("^m_Sort^", newValue8).Replace(BaseClass.m_BLLnamespace, replaceClass.NameSpace + "BLL").Replace(this.m_StringCreate, nullPrimaryKeyType).Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_BLL + "/" + replaceClass.Code + this.m_BLL + ".cs", content);
      fileName.Add(replaceClass.Code);
      this.m_Content.Clear();
      return string.Empty;
    }

    public bool DoBLL(View replaceClass, ref List<string> fileName)
    {
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      string str = string.Empty;
      if (!string.IsNullOrWhiteSpace(newValue3))
        newValue3 += "\r\n                    }\r\n";
      string content = Common.Read(BaseClass.m_DempDirectory + this.m_ViewDanMoban).Replace(this.m_CopyMetadata, string.IsNullOrWhiteSpace(newValue2) ? "" : this.m_CopyMetadata2.Replace(this.m_CopyMetadata, newValue2)).Replace(this.m_ReplaceFlexigrid, newValue3).Replace(this.m_RefGetSelectList, newValue1).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, replaceClass.Columns[0].Code).Replace(BaseClass.m_BLLnamespace, replaceClass.NameSpace + "BLL").Replace(BaseClass.m_DALnamespace, replaceClass.NameSpace + "DAL");
      Common.Write(BaseClass.m_RootDirectory + "/" + this.m_BLL + "/" + replaceClass.Code + this.m_BLL + ".cs", content);
      fileName.Add(replaceClass.Code);
      this.m_Content.Clear();
      return true;
    }
  }
}
