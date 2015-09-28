// Decompiled with JetBrains decompiler
// Type: CodeMaker.Program
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.Threading;
using System.Windows.Forms;

namespace CodeMaker
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      bool createdNew;
      Mutex mutex = new Mutex(true, "CodeMaker", out createdNew);
      if (!createdNew)
      {
        int num1 = (int) MessageBox.Show("【狼奔代码生成器】程序已经运行，请查看右下角的系统托盘区域！", "提示");
      }
      else if (!Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\\") + 1).ToUpper().Equals("CODEMAKER.EXE"))
      {
        int num2 = (int) MessageBox.Show("无法找到启动程序！", "提示");
      }
      else
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run((Form) new FrmStart());
      }
    }
  }
}
