// Decompiled with JetBrains decompiler
// Type: CodeMaker.FrmLogin
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.Properties;
using CodeMaker.ServiceReference1;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
  public class FrmLogin : BaseMain
  {
    private IContainer components = (IContainer) null;
    public FrmStart m_FrmStart;
    private TextBox txtUpwd;
    private TextBox txtUname;
    private Label label2;
    private Label label1;
    private Button btnClose;
    private FolderBrowserDialog folderBrowserDialog1;
    private Button btnLogin;
    private GroupBox groupBox3;
    private Label label3;
    private PictureBox pictureBox2;
    private PictureBox pictureBox1;
    private PictureBox close;

    public FrmLogin(FrmStart frm)
    {
      this.m_FrmStart = frm;
      this.InitializeComponent();
      if (!BaseBusiness.IsNotDefault())
        return;
      this.LoadUIText();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      try
      {
        string text1 = this.txtUname.Text;
        if (string.IsNullOrWhiteSpace(text1))
        {
          int num1 = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("youxiangbunengweikong"), MessageBoxIcon.Exclamation, MessageBoxButtons.OK);
        }
        else
        {
          string text2 = this.txtUpwd.Text;
          if (string.IsNullOrWhiteSpace(text2))
          {
            int num2 = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("yanzhengmabunengweikong"), MessageBoxIcon.Exclamation, MessageBoxButtons.OK);
          }
          else
          {
            string id = Common.GetID();
            using (VersionClient versionClient = new VersionClient())
            {
              string version = versionClient.GetVersion(text1, text2, id);
              versionClient.Close();
              if (string.IsNullOrWhiteSpace(version))
              {
                int num3 = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("qinghedui"), MessageBoxIcon.Exclamation, MessageBoxButtons.OK);
              }
              else
              {
                int num4 = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("jihuochenggong"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
                this.btnClose_Click(sender, e);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("CheckNetwork") + "\r\n" + ex.Message, MessageBoxIcon.Exclamation, MessageBoxButtons.OK);
      }
    }

    private void LoadUIText()
    {
      this.Text = BaseBusiness.GetResourceValue("denglu");
      this.groupBox3.Text = BaseBusiness.GetResourceValue("shenqing");
      this.label1.Text = BaseBusiness.GetResourceValue("youxiang");
      this.label2.Text = BaseBusiness.GetResourceValue("yanzhengma");
      this.btnLogin.Text = BaseBusiness.GetResourceValue("xitongdenglu");
      this.btnClose.Text = BaseBusiness.GetResourceValue("quxiao");
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void close_MouseHover(object sender, EventArgs e)
    {
      this.close.Image = this.close.ErrorImage;
    }

    private void close_MouseClick(object sender, MouseEventArgs e)
    {
      this.btnClose_Click(sender, (EventArgs) e);
    }

    private void close_MouseLeave(object sender, EventArgs e)
    {
      this.close.Image = this.close.InitialImage;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmLogin));
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.close = new PictureBox();
      this.label3 = new Label();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.groupBox3 = new GroupBox();
      this.label1 = new Label();
      this.txtUpwd = new TextBox();
      this.btnLogin = new Button();
      this.txtUname = new TextBox();
      this.btnClose = new Button();
      this.label2 = new Label();
      ((ISupportInitialize) this.close).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.close.ErrorImage = (Image) Resources.mousedown;
      this.close.Image = (Image) Resources.mouseup;
      this.close.InitialImage = (Image) Resources.mouseup;
      this.close.Location = new Point(504, 8);
      this.close.Name = "close";
      this.close.Size = new Size(50, 21);
      this.close.TabIndex = 27;
      this.close.TabStop = false;
      this.close.MouseClick += new MouseEventHandler(this.close_MouseClick);
      this.close.MouseLeave += new EventHandler(this.close_MouseLeave);
      this.close.MouseHover += new EventHandler(this.close_MouseHover);
      this.label3.AutoSize = true;
      this.label3.Font = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label3.ForeColor = Color.FromArgb(64, 64, 64);
      this.label3.Location = new Point(52, 9);
      this.label3.Name = "label3";
      this.label3.Size = new Size(136, 16);
      this.label3.TabIndex = 26;
      this.label3.Text = "登录，是一种状态";
      this.pictureBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox2.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox2.BackgroundImage");
      this.pictureBox2.ErrorImage = (Image) componentResourceManager.GetObject("pictureBox2.ErrorImage");
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.InitialImage = (Image) componentResourceManager.GetObject("pictureBox2.InitialImage");
      this.pictureBox2.Location = new Point(-7, 42);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(605, 3);
      this.pictureBox2.TabIndex = 25;
      this.pictureBox2.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.tanchu18;
      this.pictureBox1.Location = new Point(12, 1);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(40, 35);
      this.pictureBox1.TabIndex = 24;
      this.pictureBox1.TabStop = false;
      this.groupBox3.Controls.Add((Control) this.label1);
      this.groupBox3.Controls.Add((Control) this.txtUpwd);
      this.groupBox3.Controls.Add((Control) this.btnLogin);
      this.groupBox3.Controls.Add((Control) this.txtUname);
      this.groupBox3.Controls.Add((Control) this.btnClose);
      this.groupBox3.Controls.Add((Control) this.label2);
      this.groupBox3.Font = new Font("微软雅黑", 12f);
      this.groupBox3.ForeColor = Color.FromArgb(181, 181, 181);
      this.groupBox3.Location = new Point(35, 66);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(486, 262);
      this.groupBox3.TabIndex = 13;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "在langben.com网站的“版本升级”申请验证码";
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label1.ForeColor = Color.FromArgb(64, 64, 64);
      this.label1.Location = new Point(23, 48);
      this.label1.Name = "label1";
      this.label1.Size = new Size(57, 22);
      this.label1.TabIndex = 7;
      this.label1.Text = "邮   箱";
      this.txtUpwd.BorderStyle = BorderStyle.FixedSingle;
      this.txtUpwd.Location = new Point(103, 119);
      this.txtUpwd.Name = "txtUpwd";
      this.txtUpwd.PasswordChar = '*';
      this.txtUpwd.Size = new Size(337, 29);
      this.txtUpwd.TabIndex = 8;
      this.btnLogin.BackColor = Color.FromArgb(247, 247, 247);
      this.btnLogin.BackgroundImage = (Image) Resources.tanchukuang22;
      this.btnLogin.Cursor = Cursors.Hand;
      this.btnLogin.FlatAppearance.BorderSize = 0;
      this.btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnLogin.FlatStyle = FlatStyle.Flat;
      this.btnLogin.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnLogin.Location = new Point(103, 180);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new Size(104, 38);
      this.btnLogin.TabIndex = 10;
      this.btnLogin.Text = "登录";
      this.btnLogin.UseVisualStyleBackColor = false;
      this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
      this.txtUname.BackColor = Color.White;
      this.txtUname.BorderStyle = BorderStyle.FixedSingle;
      this.txtUname.Location = new Point(103, 45);
      this.txtUname.Name = "txtUname";
      this.txtUname.Size = new Size(337, 29);
      this.txtUname.TabIndex = 6;
      this.btnClose.BackColor = Color.FromArgb(247, 247, 247);
      this.btnClose.BackgroundImage = (Image) Resources.tanchukuang10;
      this.btnClose.Cursor = Cursors.Hand;
      this.btnClose.FlatAppearance.BorderSize = 0;
      this.btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnClose.FlatStyle = FlatStyle.Flat;
      this.btnClose.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnClose.Location = new Point(260, 180);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(104, 38);
      this.btnClose.TabIndex = 12;
      this.btnClose.Text = "取消";
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label2.ForeColor = Color.FromArgb(64, 64, 64);
      this.label2.Location = new Point(23, 122);
      this.label2.Name = "label2";
      this.label2.Size = new Size(58, 22);
      this.label2.TabIndex = 9;
      this.label2.Text = "验证码";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(247, 247, 247);
      this.ClientSize = new Size(566, 363);
      this.Controls.Add((Control) this.close);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.groupBox3);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmLogin";
      this.Text = "登录，是一种状态";
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
