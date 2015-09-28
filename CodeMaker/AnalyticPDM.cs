// Decompiled with JetBrains decompiler
// Type: CodeMaker.AnalyticPDM
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CodeMaker
{
  public class AnalyticPDM
  {
    public static void TableReference(string pdm, ref DataSourse dataSourse)
    {
      XElement xelement1;
      try
      {
        if (File.Exists(pdm))
        {
          xelement1 = XElement.Load(pdm);
        }
        else
        {
          dataSourse.m_ErrorMessage = "不存在" + pdm + " 文件，请在“连接数据源”->“PowerDesigner”中设置";
          return;
        }
      }
      catch
      {
        dataSourse.m_ErrorMessage = "请先关闭 " + pdm + " 文件，然后再试";
        return;
      }
      dataSourse.ListTable = new List<TableData>();
      dataSourse.ListReference = new List<Reference>();
      dataSourse.ListView = new List<ViewData>();
      XNamespace xNamespaceAttribute = (XNamespace) "attribute";
      XNamespace xnamespace1 = (XNamespace) "collection";
      XNamespace xnamespace2 = (XNamespace) "object";
      IEnumerable<XElement> enumerable1 = Enumerable.Select<XElement, XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(xelement1.Elements())), xnamespace1 + "References"), xnamespace2 + "Reference"), (Func<XElement, XElement>) (r => r));
      IEnumerable<XElement> enumerable2 = Enumerable.Select<XElement, XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(xelement1.Elements())), xnamespace1 + "Tables"), xnamespace2 + "Table"), (Func<XElement, XElement>) (x => x));
      IEnumerable<XElement> enumerable3 = Enumerable.Select<XElement, XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(xelement1.Elements())), xnamespace1 + "Views"), xnamespace2 + "View"), (Func<XElement, XElement>) (r => r));
      IEnumerable<XElement> source = Enumerable.Select<XElement, XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(xelement1.Elements())), xnamespace1 + "DBMS"), xnamespace2 + "Shortcut"), (Func<XElement, XElement>) (r => r));
      foreach (XElement xelement2 in enumerable2)
      {
        TableData table = new TableData();
        table.Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Attributes((XName) "Id"), (Func<XAttribute, string>) (t => t.Value)));
        table.Name = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(xelement2.Elements(xNamespaceAttribute + "Name"), (Func<XElement, string>) (t => t.Value)));
        table.Code = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(xelement2.Elements(xNamespaceAttribute + "Code"), (Func<XElement, string>) (t => t.Value)));
        table.PrimaryKey = new Key()
        {
          KeyId = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(Extensions.Attributes(Extensions.Elements<XElement>(xelement2.Elements(xnamespace1 + "Keys"), xnamespace2 + "Key"), (XName) "Id"), (Func<XAttribute, string>) (t => t.Value))),
          ColumnRef = Enumerable.ToArray<string>(Enumerable.Select<XAttribute, string>(Extensions.Attributes(Extensions.Elements<XElement>(Extensions.Elements<XElement>(Extensions.Elements<XElement>(xelement2.Elements(xnamespace1 + "Keys"), xnamespace2 + "Key"), xnamespace1 + "Key.Columns"), xnamespace2 + "Column"), (XName) "Ref"), (Func<XAttribute, string>) (z => z.Value)))
        };
        table.Columns = Enumerable.ToList<Column>(Enumerable.Select<XElement, Column>(Extensions.Elements<XElement>(xelement2.Elements(xnamespace1 + "Columns"), xnamespace2 + "Column"), (Func<XElement, Column>) (p => new Column()
        {
          TableId = table.Id,
          TableCode = table.Code,
          Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(p.Attributes((XName) "Id"), (Func<XAttribute, string>) (t => t.Value))),
          Code = p.Element(xNamespaceAttribute + "Code").Value,
          Name = p.Element(xNamespaceAttribute + "Name").Value,
          Displayed = p.Element(xNamespaceAttribute + "Displayed") == null ? string.Empty : p.Element(xNamespaceAttribute + "Displayed").Value,
          Mandatory = p.Element(xNamespaceAttribute + "Mandatory") == null ? string.Empty : p.Element(xNamespaceAttribute + "Mandatory").Value,
          Length = p.Element(xNamespaceAttribute + "Length") == null ? string.Empty : p.Element(xNamespaceAttribute + "Length").Value,
          LowValue = p.Element(xNamespaceAttribute + "LowValue") == null ? string.Empty : p.Element(xNamespaceAttribute + "LowValue").Value,
          HeighValue = p.Element(xNamespaceAttribute + "HighValue") == null ? string.Empty : p.Element(xNamespaceAttribute + "HighValue").Value,
          Comment = p.Element(xNamespaceAttribute + "Comment") == null ? string.Empty : p.Element(xNamespaceAttribute + "Comment").Value,
          DataType = p.Element(xNamespaceAttribute + "DataType") == null ? string.Empty : p.Element(xNamespaceAttribute + "DataType").Value
        })));
        dataSourse.ListTable.Add(table);
      }
      foreach (XElement xelement2 in enumerable3)
      {
        ViewData view = new ViewData();
        view.Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Attributes((XName) "Id"), (Func<XAttribute, string>) (t => t.Value)));
        view.Name = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(xelement2.Elements(xNamespaceAttribute + "Name"), (Func<XElement, string>) (t => t.Value)));
        view.Code = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(xelement2.Elements(xNamespaceAttribute + "Code"), (Func<XElement, string>) (t => t.Value)));
        view.SQLQuery = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(xelement2.Elements(xNamespaceAttribute + "View.SQLQuery"), (Func<XElement, string>) (t => t.Value)));
        view.Columns = Enumerable.ToList<Column>(Enumerable.Select<XElement, Column>(Extensions.Elements<XElement>(xelement2.Elements(xnamespace1 + "Columns"), xnamespace2 + "ViewColumn"), (Func<XElement, Column>) (p => new Column()
        {
          TableCode = view.Code,
          TableId = view.Id,
          Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(p.Attributes((XName) "Id"), (Func<XAttribute, string>) (t => t.Value))),
          Code = p.Element(xNamespaceAttribute + "Code") == null ? string.Empty : p.Element(xNamespaceAttribute + "Code").Value,
          Name = p.Element(xNamespaceAttribute + "Name") == null ? string.Empty : p.Element(xNamespaceAttribute + "Name").Value,
          Displayed = p.Element(xNamespaceAttribute + "Displayed") == null ? string.Empty : p.Element(xNamespaceAttribute + "Displayed").Value,
          Mandatory = p.Element(xNamespaceAttribute + "Mandatory") == null ? string.Empty : p.Element(xNamespaceAttribute + "Mandatory").Value,
          Length = p.Element(xNamespaceAttribute + "Length") == null ? string.Empty : p.Element(xNamespaceAttribute + "Length").Value,
          LowValue = p.Element(xNamespaceAttribute + "LowValue") == null ? string.Empty : p.Element(xNamespaceAttribute + "LowValue").Value,
          HeighValue = p.Element(xNamespaceAttribute + "HighValue") == null ? string.Empty : p.Element(xNamespaceAttribute + "HighValue").Value,
          Comment = p.Element(xNamespaceAttribute + "Comment") == null ? string.Empty : p.Element(xNamespaceAttribute + "Comment").Value,
          DataType = p.Element(xNamespaceAttribute + "DataType") == null ? string.Empty : p.Element(xNamespaceAttribute + "DataType").Value
        })));
        dataSourse.ListView.Add(view);
      }
      foreach (XElement xelement2 in enumerable1)
      {
        Reference reference = new Reference()
        {
          ParentTable = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Element(xnamespace1 + "ParentTable").Element(xnamespace2 + "Table").Attributes((XName) "Ref"), (Func<XAttribute, string>) (t => t.Value))),
          ChildTable = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Element(xnamespace1 + "ChildTable").Element(xnamespace2 + "Table").Attributes((XName) "Ref"), (Func<XAttribute, string>) (t => t.Value))),
          ParentTableColumnRef = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Element(xnamespace1 + "Joins").Element(xnamespace2 + "ReferenceJoin").Element(xnamespace1 + "Object1").Element(xnamespace2 + "Column").Attributes((XName) "Ref"), (Func<XAttribute, string>) (t => t.Value))),
          ChildTableColumnRef = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Element(xnamespace1 + "Joins").Element(xnamespace2 + "ReferenceJoin").Element(xnamespace1 + "Object2").Element(xnamespace2 + "Column").Attributes((XName) "Ref"), (Func<XAttribute, string>) (t => t.Value))),
          Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(xelement2.Attributes((XName) "Id"), (Func<XAttribute, string>) (t => t.Value)))
        };
        dataSourse.ListReference.Add(reference);
      }
      dataSourse.DataBaseInfor = new DBMS()
      {
        Id = Enumerable.FirstOrDefault<string>(Enumerable.Select<XAttribute, string>(Extensions.Attributes(source, (XName) "Id"), (Func<XAttribute, string>) (t => t.Value))),
        Name = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "Name"), (Func<XElement, string>) (t => t.Value))),
        Code = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "Code"), (Func<XElement, string>) (t => t.Value))),
        CreationDate = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "CreationDate"), (Func<XElement, string>) (t => t.Value))),
        Creator = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "Creator"), (Func<XElement, string>) (t => t.Value))),
        ModificationDate = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "ModificationDate"), (Func<XElement, string>) (t => t.Value))),
        Modifier = Enumerable.FirstOrDefault<string>(Enumerable.Select<XElement, string>(Extensions.Elements<XElement>(source, xNamespaceAttribute + "Modifier"), (Func<XElement, string>) (t => t.Value)))
      };
    }
  }
}
