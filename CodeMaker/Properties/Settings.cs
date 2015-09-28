// Decompiled with JetBrains decompiler
// Type: CodeMaker.Properties.Settings
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace CodeMaker.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings settings = Settings.defaultInstance;
        return settings;
      }
    }
  }
}
