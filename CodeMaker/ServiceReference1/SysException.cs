// Decompiled with JetBrains decompiler
// Type: CodeMaker.ServiceReference1.SysException
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CodeMaker.ServiceReference1
{
  [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
  [DebuggerStepThrough]
  [DataContract(Name = "SysException", Namespace = "http://schemas.datacontract.org/2004/07/WebApplicationLangben")]
  [Serializable]
  public class SysException : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    [OptionalField]
    private string ComputerInfoField;
    [OptionalField]
    private string MessageField;
    [OptionalField]
    private string StackTraceField;

    [Browsable(false)]
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [DataMember]
    public string ComputerInfo
    {
      get
      {
        return this.ComputerInfoField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.ComputerInfoField, (object) value))
          return;
        this.ComputerInfoField = value;
        this.RaisePropertyChanged("ComputerInfo");
      }
    }

    [DataMember]
    public string Message
    {
      get
      {
        return this.MessageField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.MessageField, (object) value))
          return;
        this.MessageField = value;
        this.RaisePropertyChanged("Message");
      }
    }

    [DataMember]
    public string StackTrace
    {
      get
      {
        return this.StackTraceField;
      }
      set
      {
        if (object.ReferenceEquals((object) this.StackTraceField, (object) value))
          return;
        this.StackTraceField = value;
        this.RaisePropertyChanged("StackTrace");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
