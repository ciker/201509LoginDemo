// Decompiled with JetBrains decompiler
// Type: CodeMaker.ServiceReference1.VersionClient
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CodeMaker.ServiceReference1
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [DebuggerStepThrough]
  public class VersionClient : ClientBase<IVersion>, IVersion
  {
    public VersionClient()
    {
    }

    public VersionClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public VersionClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public VersionClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public VersionClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public string DoWork(string info)
    {
      return this.Channel.DoWork(info);
    }

    public string GetVersion(string email, string password, string info)
    {
      return this.Channel.GetVersion(email, password, info);
    }

    public void WriteExceptions(SysException ex)
    {
      this.Channel.WriteExceptions(ex);
    }
  }
}
