// Decompiled with JetBrains decompiler
// Type: CodeMaker.DataFactory
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

namespace CodeMaker
{
  public class DataFactory
  {
    public IData CreateDataSource(DataType dataType)
    {
      switch (dataType)
      {
        case DataType.PowerDesigner:
          return (IData) new DataOfPowerDesigner();
        case DataType.MSSQLSRV2008:
        case DataType.MSSQLSRV2005:
          return (IData) new DataOfSQLSerser2005();
        default:
          return (IData) null;
      }
    }
  }
}
