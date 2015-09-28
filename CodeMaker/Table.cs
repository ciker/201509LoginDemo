// Decompiled with JetBrains decompiler
// Type: CodeMaker.Table
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;

namespace CodeMaker
{
  public class Table : TableData
  {
    public string childTableColumnRef { get; set; }

    public List<RefIdName> refId { get; set; }

    public List<RefIdName> refNotId { get; set; }

    public string PrimaryKeyType { get; set; }

    public string NameSpace { get; set; }
  }
}
