// Decompiled with JetBrains decompiler
// Type: CodeMaker.FrmSystemSet
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
  public class FrmSystemSet : BaseMain
  {
    public string m_SystemConfig = Application.StartupPath + "\\SystemConfig.ini";
    private IContainer components = (IContainer) null;
    public FrmStart m_FrmStart;
    private TextBox txbNameSpace;
    private Label labNameSpace;
    private Button btnShengChengMuDiDi;
    private Button btnSave;
    private Label label8;
    private TextBox txbMuDiDI;
    private Button btnClose;
    private FolderBrowserDialog folderBrowserDialog1shengcheng;
    private GroupBox groupBox3;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Label label1;
    private PictureBox close;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private Label label3;
    private LinkLabel linkLabel1;

    public FrmSystemSet(FrmStart frm)
    {
      this.m_FrmStart = frm;
      this.InitializeComponent();
      string str = Common.Read(this.m_SystemConfig);
      if (!string.IsNullOrWhiteSpace(str))
      {
        string[] strArray = str.Split(',');
        if (strArray != null && strArray.Length > 2)
        {
          if (strArray[0] == "n")
          {
            this.radioButton1.Checked = false;
            this.radioButton2.Checked = true;
          }
          this.txbMuDiDI.Text = strArray[1];
          this.txbNameSpace.Text = strArray[2];
        }
      }
      if (!BaseBusiness.IsNotDefault())
        return;
      this.LoadUIText();
    }

    private void LoadUIText()
    {
      this.Text = BaseBusiness.GetResourceValue("xitongshezhi");
      this.groupBox3.Text = BaseBusiness.GetResourceValue("duishengchengdexitong");
      this.labNameSpace.Text = BaseBusiness.GetResourceValue("mingmingkongjian");
      this.label8.Text = BaseBusiness.GetResourceValue("shengchengweizhi");
      this.btnShengChengMuDiDi.Text = BaseBusiness.GetResourceValue("xuanze");
      this.btnSave.Text = BaseBusiness.GetResourceValue("qingxianceshi");
      this.btnSave.Text = BaseBusiness.GetResourceValue("baocun");
      this.btnClose.Text = BaseBusiness.GetResourceValue("quxiao");
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string str1 = Common.Read(this.m_SystemConfig);
      if (string.IsNullOrWhiteSpace(str1))
        return;
      string[] strArray = str1.Split(',');
      if (strArray != null && strArray.Length > 2)
      {
        string str2 = string.Empty;
        Common.Write(this.m_SystemConfig, this.radioButton1.Checked ? "a," + this.txbMuDiDI.Text.Trim() + "," + this.txbNameSpace.Text.Replace("\r\n", string.Empty).Trim() : "n," + this.txbMuDiDI.Text.Trim() + "," + this.txbNameSpace.Text.Replace("\r\n", string.Empty).Trim());
        int num = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("baocunchenggong"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
        this.btnClose_Click(sender, e);
      }
    }

    private void btnShengChengMuDiDi_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(this.txbMuDiDI.Text))
      {
        this.folderBrowserDialog1shengcheng.Description = "请选择生成的位置";
        this.folderBrowserDialog1shengcheng.SelectedPath = this.txbMuDiDI.Text;
      }
      if (this.folderBrowserDialog1shengcheng.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(this.folderBrowserDialog1shengcheng.SelectedPath))
        return;
      this.txbMuDiDI.Text = this.folderBrowserDialog1shengcheng.SelectedPath;
    }

    private void close_MouseClick(object sender, MouseEventArgs e)
    {
      this.btnClose_Click(sender, (EventArgs) e);
    }

    private void close_MouseHover(object sender, EventArgs e)
    {
      this.close.Image = this.close.ErrorImage;
    }

    private void close_MouseLeave(object sender, EventArgs e)
    {
      this.close.Image = this.close.InitialImage;
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("explorer.exe", "http://langben.com/boards/topic/4/%E5%91%BD%E5%90%8D%E7%A9%BA%E9%97%B4%E7%AD%89%E9%A1%B9%E7%9B%AE%E5%BC%80%E5%8F%91%E5%AE%8C%E6%88%90%E5%90%8E%E6%9C%80%E5%90%8E%E5%85%A8%E9%83%A8%E6%9B%BF%E6%8D%A2");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmSystemSet));
      this.folderBrowserDialog1shengcheng = new FolderBrowserDialog();
      this.close = new PictureBox();
      this.label1 = new Label();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.groupBox3 = new GroupBox();
      this.linkLabel1 = new LinkLabel();
      this.radioButton2 = new RadioButton();
      this.radioButton1 = new RadioButton();
      this.label3 = new Label();
      this.txbNameSpace = new TextBox();
      this.labNameSpace = new Label();
      this.btnClose = new Button();
      this.txbMuDiDI = new TextBox();
      this.label8 = new Label();
      this.btnSave = new Button();
      this.btnShengChengMuDiDi = new Button();
      ((ISupportInitialize) this.close).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.close.ErrorImage = (Image) Resources.mousedown;
      this.close.Image = (Image) Resources.mouseup;
      this.close.InitialImage = (Image) Resources.mouseup;
      this.close.Location = new Point(574, 11);
      this.close.Name = "close";
      this.close.Size = new Size(50, 21);
      this.close.TabIndex = 28;
      this.close.TabStop = false;
      this.close.MouseClick += new MouseEventHandler(this.close_MouseClick);
      this.close.MouseLeave += new EventHandler(this.close_MouseLeave);
      this.close.MouseHover += new EventHandler(this.close_MouseHover);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label1.ForeColor = Color.FromArgb(64, 64, 64);
      this.label1.Location = new Point(44, 11);
      this.label1.Name = "label1";
      this.label1.Size = new Size(72, 16);
      this.label1.TabIndex = 23;
      this.label1.Text = "系统设置";
      this.pictureBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox2.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox2.BackgroundImage");
      this.pictureBox2.ErrorImage = (Image) componentResourceManager.GetObject("pictureBox2.ErrorImage");
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.InitialImage = (Image) componentResourceManager.GetObject("pictureBox2.InitialImage");
      this.pictureBox2.Location = new Point(-7, 51);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(678, 3);
      this.pictureBox2.TabIndex = 22;
      this.pictureBox2.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.tanchu05;
      this.pictureBox1.Location = new Point(4, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(40, 35);
      this.pictureBox1.TabIndex = 21;
      this.pictureBox1.TabStop = false;
      this.groupBox3.Controls.Add((Control) this.linkLabel1);
      this.groupBox3.Controls.Add((Control) this.radioButton2);
      this.groupBox3.Controls.Add((Control) this.radioButton1);
      this.groupBox3.Controls.Add((Control) this.label3);
      this.groupBox3.Controls.Add((Control) this.txbNameSpace);
      this.groupBox3.Controls.Add((Control) this.labNameSpace);
      this.groupBox3.Controls.Add((Control) this.btnClose);
      this.groupBox3.Controls.Add((Control) this.txbMuDiDI);
      this.groupBox3.Controls.Add((Control) this.label8);
      this.groupBox3.Controls.Add((Control) this.btnSave);
      this.groupBox3.Controls.Add((Control) this.btnShengChengMuDiDi);
      this.groupBox3.Font = new Font("微软雅黑", 12f);
      this.groupBox3.ForeColor = Color.FromArgb(181, 181, 181);
      this.groupBox3.Location = new Point(35, 66);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(554, 353);
      this.groupBox3.TabIndex = 20;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "温馨提示：对生成的系统起作用";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(446, 48);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(74, 21);
      this.linkLabel1.TabIndex = 24;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Tag = (object) "狼奔代码生成器生成的解决方案默认的命名空间是“Langben”，等项目开发完成后，最后全部替换为你需要的命名空间即可！";
      this.linkLabel1.Text = "温馨提示";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.radioButton2.AutoSize = true;
      this.radioButton2.ForeColor = SystemColors.WindowText;
      this.radioButton2.Location = new Point(239, 193);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(44, 25);
      this.radioButton2.TabIndex = 23;
      this.radioButton2.Text = "否";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.ForeColor = SystemColors.WindowText;
      this.radioButton1.Location = new Point(189, 193);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(44, 25);
      this.radioButton1.TabIndex = 22;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "是";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label3.ForeColor = Color.FromArgb(64, 64, 64);
      this.label3.Location = new Point(23, 193);
      this.label3.Name = "label3";
      this.label3.Size = new Size(138, 22);
      this.label3.TabIndex = 21;
      this.label3.Text = "集成权限管理系统";
      this.txbNameSpace.Location = new Point(103, 45);
      this.txbNameSpace.Margin = new Padding(3, 4, 3, 4);
      this.txbNameSpace.Name = "txbNameSpace";
      this.txbNameSpace.ReadOnly = true;
      this.txbNameSpace.Size = new Size(337, 29);
      this.txbNameSpace.TabIndex = 18;
      this.txbNameSpace.Text = "Langben";
      this.labNameSpace.AutoSize = true;
      this.labNameSpace.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.labNameSpace.ForeColor = Color.FromArgb(64, 64, 64);
      this.labNameSpace.Location = new Point(23, 48);
      this.labNameSpace.Name = "labNameSpace";
      this.labNameSpace.Size = new Size(74, 22);
      this.labNameSpace.TabIndex = 17;
      this.labNameSpace.Text = "命名空间";
      this.btnClose.BackColor = Color.FromArgb(247, 247, 247);
      this.btnClose.BackgroundImage = (Image) Resources.tanchukuang10;
      this.btnClose.Cursor = Cursors.Hand;
      this.btnClose.FlatAppearance.BorderSize = 0;
      this.btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnClose.FlatStyle = FlatStyle.Flat;
      this.btnClose.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnClose.Location = new Point(254, 264);
      this.btnClose.Margin = new Padding(0);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(104, 38);
      this.btnClose.TabIndex = 19;
      this.btnClose.Text = "取消";
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.txbMuDiDI.Anchor = AnchorStyles.None;
      this.txbMuDiDI.Location = new Point(103, 121);
      this.txbMuDiDI.Margin = new Padding(0);
      this.txbMuDiDI.Name = "txbMuDiDI";
      this.txbMuDiDI.Size = new Size(258, 29);
      this.txbMuDiDI.TabIndex = 15;
      this.txbMuDiDI.Text = "D:\\";
      this.label8.AutoSize = true;
      this.label8.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label8.ForeColor = Color.FromArgb(64, 64, 64);
      this.label8.Location = new Point(23, 122);
      this.label8.Name = "label8";
      this.label8.Size = new Size(74, 22);
      this.label8.TabIndex = 14;
      this.label8.Text = "生成位置";
      this.btnSave.BackColor = Color.FromArgb(247, 247, 247);
      this.btnSave.BackgroundImage = (Image) Resources.tanchukuang22;
      this.btnSave.Cursor = Cursors.Hand;
      this.btnSave.FlatAppearance.BorderSize = 0;
      this.btnSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnSave.FlatStyle = FlatStyle.Flat;
      this.btnSave.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnSave.Location = new Point(100, 264);
      this.btnSave.Margin = new Padding(3, 4, 3, 4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(104, 38);
      this.btnSave.TabIndex = 13;
      this.btnSave.Text = "保存";
      this.btnSave.UseVisualStyleBackColor = false;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnShengChengMuDiDi.BackColor = Color.FromArgb(247, 247, 247);
      this.btnShengChengMuDiDi.BackgroundImage = (Image) Resources.tanchukuang09;
      this.btnShengChengMuDiDi.Cursor = Cursors.Hand;
      this.btnShengChengMuDiDi.FlatAppearance.BorderSize = 0;
      this.btnShengChengMuDiDi.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnShengChengMuDiDi.FlatStyle = FlatStyle.Flat;
      this.btnShengChengMuDiDi.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnShengChengMuDiDi.Location = new Point(364, 114);
      this.btnShengChengMuDiDi.Margin = new Padding(3, 4, 3, 4);
      this.btnShengChengMuDiDi.Name = "btnShengChengMuDiDi";
      this.btnShengChengMuDiDi.Size = new Size(76, 36);
      this.btnShengChengMuDiDi.TabIndex = 16;
      this.btnShengChengMuDiDi.Text = "选择";
      this.btnShengChengMuDiDi.UseVisualStyleBackColor = true;
      this.btnShengChengMuDiDi.Click += new EventHandler(this.btnShengChengMuDiDi_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(247, 247, 247);
      this.ClientSize = new Size(639, 455);
      this.Controls.Add((Control) this.close);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.groupBox3);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmSystemSet";
      this.Text = "系统设置";
      ((ISupportInitialize) this.close).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
