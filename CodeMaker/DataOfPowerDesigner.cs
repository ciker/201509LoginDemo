// Decompiled with JetBrains decompiler
// Type: CodeMaker.DataOfPowerDesigner
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

namespace CodeMaker
{
  public class DataOfPowerDesigner : BaseClass, IData
  {
    public DataSourse GetData(string ini)
    {
      DataSourse dataSourse = new DataSourse();
      if (!string.IsNullOrWhiteSpace(ini))
        AnalyticPDM.TableReference(ini, ref dataSourse);
      return dataSourse;
    }
  }
}
