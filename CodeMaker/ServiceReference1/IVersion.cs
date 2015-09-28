// Decompiled with JetBrains decompiler
// Type: CodeMaker.ServiceReference1.IVersion
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.CodeDom.Compiler;
using System.ServiceModel;

namespace CodeMaker.ServiceReference1
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "ServiceReference1.IVersion")]
  public interface IVersion
  {
    [OperationContract(Action = "http://tempuri.org/IVersion/DoWork", ReplyAction = "http://tempuri.org/IVersion/DoWorkResponse")]
    string DoWork(string info);

    [OperationContract(Action = "http://tempuri.org/IVersion/GetVersion", ReplyAction = "http://tempuri.org/IVersion/GetVersionResponse")]
    string GetVersion(string email, string password, string info);

    [OperationContract(Action = "http://tempuri.org/IVersion/WriteExceptions", IsOneWay = true)]
    void WriteExceptions(SysException ex);
  }
}
