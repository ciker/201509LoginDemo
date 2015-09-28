// Decompiled with JetBrains decompiler
// Type: CodeMaker.Controllers
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeMaker
{
  internal class Controllers : BaseClass
  {
    public string m_TreeController = "\\TreeController.cs";
    public string m_TreeModel = "\\TreeModel.cs";
    public string m_Models = "Models";
    public string m_TreeIndex = "\\TreeIndex.aspx";
    public string m_Controllers = "Controllers";
    public string m_ViewDanMoban = "\\ViewReplaceClassControllerDan.cs";
    public string m_DanMoban = "\\ReplaceClassControllerDan.cs";
    public string m_DuoMoban = "\\ReplaceClassControllerDan.cs";
    public string m_Create = "         \r\n            if (!string.IsNullOrWhiteSpace(id))\r\n            {\r\n                using (^ReplaceClassName^BLL bll = new ^ReplaceClassName^BLL())\r\n                {\r\n                    ^ReplaceClassName^ entityId = bll.GetById(^id^);\r\n                    if (entityId != null)\r\n                    { \r\n                        ^ReplaceClassCode^ entity = new ^ReplaceClassCode^();  \r\n                        entity.^ReplaceClassName^Id = id + '&' + entityId.^Name^;\r\n                        return View(entity);\r\n                    }\r\n                }\r\n            }\r\n";
    public string m_GetTreesSef = "^m_GetTreesSef^";
    public string m_CreateSef = "^m_CreateSef^";
    public string m_TreeSef = "^m_TreeSef^";
    public string m_GetTrees = "\r\n        /// <summary>\r\n        /// 首次设置^ReplaceClassName^\r\n        /// </summary>\r\n        /// <param name=@id@>主键</param>\r\n        /// <returns></returns> \r\n        [SupportFilter]\r\n        public ActionResult Set^ReplaceAttribute^(^string^ id)\r\n        {\r\n            var entity = m_BLL.GetById(id);\r\n            ViewData[@myname@] = entity.^Name^;\r\n            return View(entity);\r\n        }\r\n   \r\n\t\t/// <summary>\r\n        /// 设置^ReplaceClassName^\r\n        /// </summary>\r\n        /// <param name=@entity@></param>\r\n        /// <returns></returns>\r\n        [HttpPost]\r\n        [SupportFilter]\r\n        public ActionResult Set^ReplaceAttribute^(^ReplaceClassCode^ entity)\r\n        {\r\n            if (entity != null)\r\n            {\r\n                string currentPerson = GetCurrentPerson();\r\n                //entity.UpdateTime = DateTime.Now;\r\n                //entity.UpdatePerson = currentPerson;\r\n                string returnValue = string.Empty;\r\n                if (m_BLL.Set^ReplaceAttribute^(ref validationErrors, entity))\r\n                {\r\n                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + @，^ReplaceClassName^信息的Id为@ + entity.^m_Id^, @消息@\r\n                        );//写入日志                           \r\n                    return Json(Suggestion.UpdateSucceed); //提示更新成功 \r\n                }\r\n                else\r\n                {\r\n                    if (validationErrors != null && validationErrors.Count > 0)\r\n                    {\r\n                        validationErrors.All(a =>\r\n                        {\r\n                            returnValue += a.ErrorMessage;\r\n                            return true;\r\n                        });\r\n                    }\r\n                    LogClassModels.WriteServiceLog(Suggestion.DeleteFail + @，信息的Id为@ + entity.^m_Id^ + @,@ + returnValue, @消息@\r\n                        );//删除失败，写入日志\r\n                    return Json(Suggestion.UpdateFail + returnValue);                  \r\n                 }\r\n            }\r\n            else\r\n            {\r\n                return Json(Suggestion.UpdateFail + @，请核对输入的数据的格式@); //提示输入的数据的格式不对               \r\n            }\r\n        }\r\n";

    public void DoControllers(Table replaceClass, ref List<string> fileName)
    {
      string newValue1 = string.Empty;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string newValue2 = string.Empty;
      string str3 = string.Empty;
      string oldValue = "entity.^m_Id^ = Result.GetNewId();";
      string newValue3 = oldValue;
      string str4 = string.Empty;
      string str5 = string.Empty;
      string primaryKeyType = replaceClass.PrimaryKeyType;
      string nullPrimaryKeyType = Common.GetNullPrimaryKeyType(replaceClass.PrimaryKeyType);
      string newValue4;
      if (replaceClass.PrimaryKeyType == "int")
      {
        newValue3 = string.Empty;
        newValue4 = replaceClass.PrimaryKeyType + "[] deleteId = collection[@query@].GetString().Split(',').Select(s => Convert.ToInt32(s)).ToArray();".Replace('@', '"');
      }
      else if (replaceClass.PrimaryKeyType == "Guid")
      {
        newValue3 = string.Empty;
        newValue4 = replaceClass.PrimaryKeyType + "[] deleteId = collection[@query@].GetString().Split(',').Select(s => new Guid(s)).ToArray();".Replace('@', '"');
      }
      else if (replaceClass.PrimaryKeyType == "long")
      {
        newValue3 = string.Empty;
        newValue4 = replaceClass.PrimaryKeyType + "[] deleteId = collection[@query@].GetString().Split(',').Select(s => Convert.ToInt64(s)).ToArray();".Replace('@', '"');
      }
      else if (replaceClass.PrimaryKeyType == "short")
      {
        newValue3 = string.Empty;
        newValue4 = replaceClass.PrimaryKeyType + "[] deleteId = collection[@query@].GetString().Split(',').Select(s => Convert.ToInt16(s)).ToArray();".Replace('@', '"');
      }
      else
        newValue4 = "string[] deleteId = collection[@query@].GetString().Split(',');".Replace('@', '"');
      string newValue5 = string.Empty;
      int num1 = 0;
      foreach (Column column in replaceClass.Columns)
      {
        Column it = column;
        ++num1;
        RefIdName refIdName = new RefIdName();
        if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
          refIdName = Enumerable.FirstOrDefault<RefIdName>(Enumerable.Where<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId, (Func<RefIdName, bool>) (s => s.Ref == it.Code)));
        if (num1 == 1)
          newValue5 = newValue5 + it.Code + " = s." + it.Code + "\n\t\t\t\t\t";
        else if (refIdName != null && !string.IsNullOrWhiteSpace(refIdName.Id))
          newValue5 = newValue5 + "," + it.Code + " =   s." + it.Code + "Old\n\t\t\t\t\t";
        else
          newValue5 = newValue5 + "," + it.Code + " = s." + it.Code + "\n\t\t\t\t\t";
      }
      if (replaceClass.childTableColumnRef != null)
      {
        Column columnByKey = Common.GetColumnByKey(replaceClass, replaceClass.childTableColumnRef);
        string str6 = string.Empty;
        string str7 = !(replaceClass.PrimaryKeyType == "long") ? (!(replaceClass.PrimaryKeyType == "short") ? (!(replaceClass.PrimaryKeyType == "int") ? (!(replaceClass.PrimaryKeyType == "Guid") ? Common.Read(BaseClass.m_DempDirectory + this.m_TreeController).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace("^parentId^", "parentId") : Common.Read(BaseClass.m_DempDirectory + this.m_TreeController).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace("^parentId^", "new Guid(parentId)")) : Common.Read(BaseClass.m_DempDirectory + this.m_TreeController).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace("^parentId^", "Convert.ToInt32(parentId)")) : Common.Read(BaseClass.m_DempDirectory + this.m_TreeController).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace("^parentId^", "Convert.ToInt16(parentId)")) : Common.Read(BaseClass.m_DempDirectory + this.m_TreeController).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace("^parentId^", "Convert.ToInt64(parentId)");
        Common.Write(BaseClass.m_RootDirectory + "/" + this.m_App + "/" + this.m_Controllers + "/" + replaceClass.Code + "TreeController.cs", str7.Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)));
        fileName.Add("Controllers\\" + replaceClass.Code + "TreeController.cs");
        string content1 = Common.Read(BaseClass.m_DempDirectory + this.m_TreeModel).Replace("ParentId", columnByKey.Code).Replace(this.m_String, nullPrimaryKeyType).Replace("Name", Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceAttribute, columnByKey.Code).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass));
        Common.Write(BaseClass.m_RootDirectory + "/" + this.m_App + "/" + this.m_Models + "/" + replaceClass.Code + "TreeModel.cs", content1);
        fileName.Add("Models\\" + replaceClass.Code + "TreeModel.cs");
        string content2 = Common.Read(BaseClass.m_DempDirectory + this.m_TreeIndex).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassCode, replaceClass.Code);
        string path = BaseClass.m_RootDirectory + "/" + this.m_App + this.m_Views + "/" + replaceClass.Code + "Tree";
        Directory.CreateDirectory(path);
        Common.Write(path + "/Index.aspx", content2);
        fileName.Add("Views\\" + replaceClass.Code + "\\Index.aspx");
        int num2 = 0;
        foreach (Column column in replaceClass.Columns)
        {
          Column it = column;
          ++num2;
          RefIdName refIdName = new RefIdName();
          if (replaceClass.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId) > 0)
            refIdName = Enumerable.FirstOrDefault<RefIdName>(Enumerable.Where<RefIdName>((IEnumerable<RefIdName>) replaceClass.refId, (Func<RefIdName, bool>) (s => s.Ref == it.Code)));
          if (num2 == 1)
            str3 = str3 + it.Code + " = s." + it.Code + "\n\t\t\t\t\t";
          else if (it.Id == columnByKey.Id)
            str3 = str3 + ",_parentId =   s." + it.Code + "\n\t\t\t\t\t,state = s." + replaceClass.Code + "1.Any(a => a." + it.Code + " == s." + Common.GetFirstPrimaryKeyCode(replaceClass) + ") ? @closed@ : null\n\t\t\t\t\t".Replace('@', '"');
          else if (refIdName != null && !string.IsNullOrWhiteSpace(refIdName.Id))
            str3 = str3 + "," + it.Code + "Old =   s." + it.Code + "//自连接的表要注意，等号两边可能需要换位\n\t\t\t\t\t";
          else if (it.Code.ToUpper() == "ICONIC")
            str3 = str3 + ",iconCls = s." + it.Code + "\n\t\t\t\t\t";
          else
            str3 = str3 + "," + it.Code + " = s." + it.Code + "\n\t\t\t\t\t";
        }
        newValue1 = newValue1 + "\r\n        /// <summary>\r\n        /// 获取树形列表的数据\r\n        /// </summary>\r\n        /// <returns></returns>\r\n        [HttpPost]\r\n        public ActionResult GetAllMetadata(^string^ id)\r\n        {\r\n            ^ReplaceClassCode^BLL m_BLL = new ^ReplaceClassCode^BLL();\r\n            IQueryable<^ReplaceClassCode^> rows = m_BLL.GetAllMetadata(id);\r\n            if (rows.Any())\r\n            {//是否可以省\r\n                return Json(new treegrid\r\n                {\r\n                    rows = rows.Select(s =>\r\n                        new\r\n                        {\r\n                          ".Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_String, primaryKeyType) + str3 + "\r\n                        }\r\n                        ).OrderBy(o => o.^m_Id^)\r\n                });\r\n            }\r\n            return Content(\"[]\");\r\n        }";
      }
      string content;
      if (replaceClass.refNotId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) replaceClass.refNotId) > 0)
      {
        foreach (RefIdName refIdName in replaceClass.refNotId)
        {
          if (refIdName.IsRefSelf)
          {
            newValue2 = !(replaceClass.PrimaryKeyType == "int") ? (!(replaceClass.PrimaryKeyType == "Guid") ? newValue2 + this.m_Create.Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, refIdName.RefTableCode).Replace("^Name^", refIdName.Name).Replace("^id^", "id") : newValue2 + this.m_Create.Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, refIdName.RefTableCode).Replace("^Name^", refIdName.Name).Replace("^id^", "new Guid(id)")) : newValue2 + this.m_Create.Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, refIdName.RefTableCode).Replace("^Name^", refIdName.Name).Replace("^id^", "Convert.ToInt32(id)");
            newValue1 += this.m_GetTrees.Replace(this.m_ReplaceAttribute, refIdName.RefTableCode).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_ReplaceClassName, refIdName.RefTableName).Replace("^Name^", Common.GetShowColumnCode((TableView) replaceClass)).Replace(this.m_String, primaryKeyType).Replace('@', '"');
          }
        }
        content = Common.Read(BaseClass.m_DempDirectory + this.m_DuoMoban).Replace("^clomus^", newValue5).Replace(this.m_TreeSef, newValue1).Replace(oldValue, newValue3).Replace(this.m_String, primaryKeyType).Replace(this.m_StringCreate, nullPrimaryKeyType).Replace("^query^", newValue4).Replace(this.m_CreateSef, newValue2).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass)).Replace(this.m_Application, this.m_App);
      }
      else
        content = Common.Read(BaseClass.m_DempDirectory + this.m_DanMoban).Replace("^clomus^", newValue5).Replace(this.m_CreateSef, newValue2).Replace(oldValue, newValue3).Replace(this.m_String, primaryKeyType).Replace(this.m_StringCreate, nullPrimaryKeyType).Replace("^query^", newValue4).Replace(this.m_TreeSef, newValue1).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, Common.GetFirstPrimaryKeyCode(replaceClass));
      string str8 = BaseClass.m_RootDirectory + "/" + this.m_App + "/" + this.m_Controllers + "/";
      foreach (Column column in replaceClass.Columns)
      {
        if (Common.IsCreatePerson(column))
          content = content.Replace("//entity.CreatePerson = currentPerson;", "entity." + column.Code + " = currentPerson;");
        else if (Common.IsCreateTime(column))
          content = content.Replace("//entity.CreateTime = DateTime.Now;", "entity." + column.Code + " = DateTime.Now;");
        else if (Common.IsUpdatePerson(column))
          content = content.Replace("//entity.UpdatePerson = currentPerson;", "entity." + column.Code + " = currentPerson;");
        else if (Common.IsUpdateTime(column))
          content = content.Replace("//entity.UpdateTime = DateTime.Now;", "entity." + column.Code + " = DateTime.Now;");
      }
      Common.Write(str8 + replaceClass.Code + "Controller.cs", content);
      fileName.Add("Controllers\\" + replaceClass.Code + "Controller.cs");
    }

    public void DoControllers(View replaceClass, ref List<string> fileName)
    {
      string newValue1 = string.Empty;
      string newValue2 = string.Empty;
      string newValue3 = string.Empty;
      int num = 0;
      foreach (Column column in replaceClass.Columns)
      {
        ++num;
        if (num == 1)
          newValue3 = newValue3 + column.Code + " = s." + column.Code + "\n\t\t\t\t\t";
        else
          newValue3 = newValue3 + "," + column.Code + " = s." + column.Code + "\n\t\t\t\t\t";
      }
      string content = Common.Read(BaseClass.m_DempDirectory + this.m_ViewDanMoban).Replace("^clomus^", newValue3).Replace(this.m_CreateSef, newValue2).Replace(this.m_TreeSef, newValue1).Replace(this.m_Application, this.m_App).Replace(this.m_ReplaceClassName, replaceClass.Name).Replace(this.m_ReplaceClassCode, replaceClass.Code).Replace(this.m_Id, replaceClass.Columns[0].Code);
      string path = BaseClass.m_RootDirectory + "/" + this.m_App + "/" + this.m_Controllers + "/";
      Directory.CreateDirectory(path);
      Common.Write(path + replaceClass.Code + "Controller.cs", content);
      fileName.Add("Controllers\\" + replaceClass.Code + "Controller.cs");
    }
  }
}
