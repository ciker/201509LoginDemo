// Decompiled with JetBrains decompiler
// Type: CodeMaker.BaseMain
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
  public class BaseMain : Form
  {
    public BaseClass baseClass = new BaseClass();
    private IContainer components = (IContainer) null;
    protected Point mouseOff;
    protected bool leftFlag;

    public List<string> WorkflowTableAndSys
    {
      get
      {
        return new List<string>()
        {
          "FileUploader",
          "SysDepartment",
          "SysException",
          "SysField",
          "SysLog",
          "SysMenu",
          "SysMenuSysOperation",
          "SysMenuSysRoleSysOperation",
          "SysOperation",
          "SysPerson",
          "SysRole",
          "SysRoleSysPerson",
          "InstanceMetadataChangesTable",
          "InstancePromotedPropertiesTable",
          "InstancesTable",
          "KeysTable",
          "LockOwnersTable",
          "RunnableInstancesTable",
          "ServiceDeploymentsTable",
          "SqlWorkflowInstanceStoreVersionTable"
        };
      }
    }

    public List<string> WorkflowView
    {
      get
      {
        return new List<string>()
        {
          "InstancePromotedProperties",
          "Instances",
          "ServiceDeployments"
        };
      }
    }

    public BaseMain()
    {
      this.InitializeComponent();
    }

    private void BaseMain_Paint(object sender, PaintEventArgs e)
    {
      RoundFormPainter.Paint(sender, e);
    }

    private void BaseMain_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.mouseOff = new Point(-e.X, -e.Y);
      this.leftFlag = true;
    }

    private void BaseMain_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.leftFlag)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseOff.X, this.mouseOff.Y);
      this.Location = mousePosition;
    }

    private void BaseMain_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.leftFlag)
        return;
      this.leftFlag = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 262);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = "BaseMain";
      this.Text = "BaseMain";
      this.Paint += new PaintEventHandler(this.BaseMain_Paint);
      this.MouseDown += new MouseEventHandler(this.BaseMain_MouseDown);
      this.MouseMove += new MouseEventHandler(this.BaseMain_MouseMove);
      this.MouseUp += new MouseEventHandler(this.BaseMain_MouseUp);
      this.ResumeLayout(false);
    }
  }
}
