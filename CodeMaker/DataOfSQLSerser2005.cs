// Decompiled with JetBrains decompiler
// Type: CodeMaker.DataOfSQLSerser2005
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using DbObjects;
using DbObjects.SQL2005;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeMaker
{
  public class DataOfSQLSerser2005 : IData
  {
    public List<Column> GetColumn(string column)
    {
      string str1 = column.Substring(column.ToUpper().IndexOf("SELECT") + 6);
      string str2 = str1.Substring(0, str1.ToUpper().IndexOf("FROM"));
      List<string> list1 = new List<string>();
      string str3 = str2;
      char[] chArray = new char[1]
      {
        ','
      };
      foreach (string str4 in str3.Split(chArray))
      {
        string str5 = str4.Trim();
        string[] strArray1 = Enumerable.ToArray<string>(Enumerable.Distinct<string>((IEnumerable<string>) str5.Split(' ')));
        if (str5.ToUpper().Contains(" AS "))
        {
          int num = str5.ToUpper().IndexOf(" AS ");
          string str6 = str5.Substring(num + 3).Trim();
          list1.Add(str6);
        }
        else if (strArray1.Length == 3 && string.IsNullOrWhiteSpace(strArray1[1]))
          list1.Add(strArray1[2]);
        else if (strArray1.Length == 1 && !string.IsNullOrWhiteSpace(strArray1[0]))
        {
          string[] strArray2 = str5.Split('.');
          list1.Add(strArray2[strArray2.Length - 1]);
        }
      }
      List<Column> list2 = new List<Column>();
      foreach (string str4 in list1)
        list2.Add(new Column()
        {
          Name = str4,
          Code = str4,
          Id = Guid.NewGuid().ToString()
        });
      return list2;
    }

    public DataSourse GetData(string constr)
    {
      DataSourse dataSourse = new DataSourse();
      string dbName = constr.Substring(0, constr.IndexOf(";")).Substring(constr.IndexOf("=") + 1);
      IDbObject dbObject = (IDbObject) new DataAccess();
      dbObject.DbConnectStr = constr;
      List<string> tables = dbObject.GetTables(dbName);
      List<TableData> list1 = new List<TableData>();
      List<Reference> list2 = new List<Reference>();
      if (tables != null && tables.Count > 0)
      {
        foreach (string tableName in tables)
        {
          TableData tableData = new TableData();
          tableData.Code = tableName;
          tableData.Name = tableName;
          tableData.Id = tableName;
          List<string> list3 = new List<string>();
          DataTable columnInfoList = dbObject.GetColumnInfoList(dbName, tableName);
          List<Column> list4 = new List<Column>();
          foreach (DataRow row in (InternalDataCollectionBase) columnInfoList.Rows)
          {
            Column column1 = new Column();
            column1.Code = DataRowExtensions.Field<string>(row, 1).Trim();
            column1.Comment = DataRowExtensions.Field<string>(row, 15).Trim();
            column1.DataType = DataRowExtensions.Field<string>(row, 2).Trim();
            column1.Displayed = "true";
            Column column2 = column1;
            string str1 = tableName;
            int num = DataRowExtensions.Field<int>(row, 0);
            string str2 = num.ToString();
            string str3 = str1 + str2;
            column2.Id = str3;
            Column column3 = column1;
            num = DataRowExtensions.Field<int>(row, 3);
            string str4 = num.ToString();
            column3.Length = str4;
            column1.Mandatory = DataRowExtensions.Field<string>(row, 13).Trim() == "√" ? "" : "1";
            column1.Name = DataRowExtensions.Field<string>(row, 1).Trim();
            column1.TableCode = tableName;
            column1.TableId = tableData.Id;
            if (DataRowExtensions.Field<string>(row, 7).Trim() != "d")
              list3.Add(column1.Id);
            list4.Add(column1);
          }
          tableData.Columns = list4;
          if (list3.Count > 0)
            tableData.PrimaryKey = new Key()
            {
              KeyId = Guid.NewGuid().ToString(),
              ColumnRef = list3.ToArray()
            };
          list1.Add(tableData);
        }
      }
      if (list1 != null && list1.Count > 0)
      {
        foreach (TableData tableData in list1)
        {
          TableData tab = tableData;
          foreach (DataRow dataRow in (InternalDataCollectionBase) dbObject.GetTableRefrence(dbName, tab.Code).Rows)
          {
            DataRow item = dataRow;
            Reference r = new Reference();
            r.ParentTable = Enumerable.SingleOrDefault<string>(Enumerable.Select<TableData, string>(Enumerable.Where<TableData>((IEnumerable<TableData>) list1, (Func<TableData, bool>) (t => t.Code == DataRowExtensions.Field<string>(item, 1).Trim())), (Func<TableData, string>) (t => t.Id)));
            r.ParentTableColumnRef = Enumerable.SingleOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.Where(Enumerable.SelectMany((IEnumerable<TableData>) list1, (Func<TableData, IEnumerable<Column>>) (t => (IEnumerable<Column>) t.Columns), (t, f) =>
            {
              var fAnonymousType2 = new
              {
                t = t,
                f = f
              };
              return fAnonymousType2;
            }), param0 => param0.t.Id == r.ParentTable), param0 => param0.f.Code == DataRowExtensions.Field<string>(item, 2).Trim()), param0 => param0.f.Id));
            r.Id = Guid.NewGuid().ToString();
            r.ChildTable = tab.Id;
            r.ChildTableColumnRef = Enumerable.SingleOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.Where(Enumerable.SelectMany((IEnumerable<TableData>) list1, (Func<TableData, IEnumerable<Column>>) (t => (IEnumerable<Column>) t.Columns), (t, f) =>
            {
              var fAnonymousType2 = new
              {
                t = t,
                f = f
              };
              return fAnonymousType2;
            }), param0 => param0.t.Id == tab.Id), param0 => param0.f.Code == DataRowExtensions.Field<string>(item, 0).Trim()), param0 => param0.f.Id));
            list2.Add(r);
          }
        }
      }
      dataSourse.ListTable = list1;
      dataSourse.ListReference = list2;
      DataTable vieWs = dbObject.GetVIEWs(dbName);
      List<ViewData> list5 = new List<ViewData>();
      if (vieWs != null && vieWs.Rows != null && vieWs.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) vieWs.Rows)
        {
          string str = DataRowExtensions.Field<string>(row, 0);
          if (!string.IsNullOrWhiteSpace(str))
          {
            string objectViewInfo = dbObject.GetObjectViewInfo(dbName, str);
            DataTable dataTable = dbObject.QueryViewInfo(dbName, str);
            ViewData viewData = new ViewData();
            viewData.Code = str;
            viewData.Name = str;
            viewData.Id = str;
            viewData.SQLQuery = objectViewInfo;
            List<Column> list3 = new List<Column>();
            foreach (DataColumn dataColumn in (InternalDataCollectionBase) dataTable.Columns)
              list3.Add(new Column()
              {
                Name = dataColumn.ColumnName,
                Code = dataColumn.ColumnName,
                Id = Guid.NewGuid().ToString()
              });
            viewData.Columns = list3;
            list5.Add(viewData);
          }
        }
      }
      dataSourse.ListView = list5;
      return dataSourse;
    }
  }
}
