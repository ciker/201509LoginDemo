// Decompiled with JetBrains decompiler
// Type: CodeMaker.Form1
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
  public class Form1 : Form
  {
    private IContainer components = (IContainer) null;
    private Panel panel2;
    private LinkLabel jishuboke;
    private LinkLabel guanfangwangzhan;
    private LinkLabel jiaoliuluntan;
    private Label kongjian;
    private Label weizhi;
    private Label lujing;
    private Label shengchengweizhi;
    private Label mingmingkongjian;
    private Label shujuyuancunfangweizhi;
    private Label banben;

    public Form1()
    {
      this.InitializeComponent();
    }

    private void jiaoliuluntan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://bbs.btboys.com/thread-htm-fid-27.html");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel2 = new Panel();
      this.shujuyuancunfangweizhi = new Label();
      this.mingmingkongjian = new Label();
      this.shengchengweizhi = new Label();
      this.lujing = new Label();
      this.weizhi = new Label();
      this.kongjian = new Label();
      this.jiaoliuluntan = new LinkLabel();
      this.guanfangwangzhan = new LinkLabel();
      this.jishuboke = new LinkLabel();
      this.banben = new Label();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.panel2.BackgroundImage = (Image) Resources.ad;
      this.panel2.BackgroundImageLayout = ImageLayout.None;
      this.panel2.Controls.Add((Control) this.banben);
      this.panel2.Controls.Add((Control) this.jishuboke);
      this.panel2.Controls.Add((Control) this.guanfangwangzhan);
      this.panel2.Controls.Add((Control) this.jiaoliuluntan);
      this.panel2.Controls.Add((Control) this.kongjian);
      this.panel2.Controls.Add((Control) this.weizhi);
      this.panel2.Controls.Add((Control) this.lujing);
      this.panel2.Controls.Add((Control) this.shengchengweizhi);
      this.panel2.Controls.Add((Control) this.mingmingkongjian);
      this.panel2.Controls.Add((Control) this.shujuyuancunfangweizhi);
      this.panel2.Location = new Point(98, 12);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(662, 499);
      this.panel2.TabIndex = 9;
      this.shujuyuancunfangweizhi.AutoSize = true;
      this.shujuyuancunfangweizhi.BackColor = Color.Transparent;
      this.shujuyuancunfangweizhi.Location = new Point(153, 126);
      this.shujuyuancunfangweizhi.Name = "shujuyuancunfangweizhi";
      this.shujuyuancunfangweizhi.Size = new Size(89, 12);
      this.shujuyuancunfangweizhi.TabIndex = 0;
      this.shujuyuancunfangweizhi.Text = "数据源存放路径";
      this.mingmingkongjian.AutoSize = true;
      this.mingmingkongjian.BackColor = Color.Transparent;
      this.mingmingkongjian.Location = new Point(153, 232);
      this.mingmingkongjian.Name = "mingmingkongjian";
      this.mingmingkongjian.Size = new Size(53, 12);
      this.mingmingkongjian.TabIndex = 1;
      this.mingmingkongjian.Text = "命名空间";
      this.shengchengweizhi.AutoSize = true;
      this.shengchengweizhi.BackColor = Color.Transparent;
      this.shengchengweizhi.Location = new Point(153, 177);
      this.shengchengweizhi.Name = "shengchengweizhi";
      this.shengchengweizhi.Size = new Size(53, 12);
      this.shengchengweizhi.TabIndex = 2;
      this.shengchengweizhi.Text = "生成位置";
      this.lujing.AutoSize = true;
      this.lujing.BackColor = Color.Transparent;
      this.lujing.Location = new Point(153, 151);
      this.lujing.Name = "lujing";
      this.lujing.Size = new Size(41, 12);
      this.lujing.TabIndex = 3;
      this.lujing.Text = "label4";
      this.weizhi.AutoSize = true;
      this.weizhi.BackColor = Color.Transparent;
      this.weizhi.Location = new Point(153, 202);
      this.weizhi.Name = "weizhi";
      this.weizhi.Size = new Size(41, 12);
      this.weizhi.TabIndex = 4;
      this.weizhi.Text = "label5";
      this.kongjian.AutoSize = true;
      this.kongjian.BackColor = Color.Transparent;
      this.kongjian.Location = new Point(153, 259);
      this.kongjian.Name = "kongjian";
      this.kongjian.Size = new Size(41, 12);
      this.kongjian.TabIndex = 5;
      this.kongjian.Text = "label6";
      this.jiaoliuluntan.AutoSize = true;
      this.jiaoliuluntan.BackColor = Color.Transparent;
      this.jiaoliuluntan.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.jiaoliuluntan.ForeColor = Color.Black;
      this.jiaoliuluntan.LinkBehavior = LinkBehavior.NeverUnderline;
      this.jiaoliuluntan.LinkColor = Color.Black;
      this.jiaoliuluntan.Location = new Point(509, (int) sbyte.MaxValue);
      this.jiaoliuluntan.Name = "jiaoliuluntan";
      this.jiaoliuluntan.Size = new Size(74, 21);
      this.jiaoliuluntan.TabIndex = 6;
      this.jiaoliuluntan.TabStop = true;
      this.jiaoliuluntan.Text = "权限管理系统";
      this.jiaoliuluntan.LinkClicked += new LinkLabelLinkClickedEventHandler(this.jiaoliuluntan_LinkClicked);
      this.guanfangwangzhan.AutoSize = true;
      this.guanfangwangzhan.BackColor = Color.Transparent;
      this.guanfangwangzhan.Font = new Font("微软雅黑", 12f);
      this.guanfangwangzhan.ForeColor = Color.Black;
      this.guanfangwangzhan.LinkBehavior = LinkBehavior.NeverUnderline;
      this.guanfangwangzhan.LinkColor = Color.Black;
      this.guanfangwangzhan.Location = new Point(509, 193);
      this.guanfangwangzhan.Name = "guanfangwangzhan";
      this.guanfangwangzhan.Size = new Size(74, 21);
      this.guanfangwangzhan.TabIndex = 7;
      this.guanfangwangzhan.TabStop = true;
      this.guanfangwangzhan.Text = "官方网站";
      this.jishuboke.AutoSize = true;
      this.jishuboke.BackColor = Color.Transparent;
      this.jishuboke.Font = new Font("微软雅黑", 12f);
      this.jishuboke.ForeColor = Color.Black;
      this.jishuboke.LinkBehavior = LinkBehavior.NeverUnderline;
      this.jishuboke.LinkColor = Color.Black;
      this.jishuboke.Location = new Point(509, 161);
      this.jishuboke.Name = "jishuboke";
      this.jishuboke.Size = new Size(74, 21);
      this.jishuboke.TabIndex = 8;
      this.jishuboke.TabStop = true;
      this.jishuboke.Text = "技术博客";
      this.banben.AutoSize = true;
      this.banben.BackColor = Color.Transparent;
      this.banben.Font = new Font("微软雅黑", 12f);
      this.banben.ForeColor = Color.White;
      this.banben.Location = new Point(103, 67);
      this.banben.Name = "banben";
      this.banben.Size = new Size(395, 21);
      this.banben.TabIndex = 10;
      this.banben.Text = "您好，感谢您使用狼奔代码生成器。";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(849, 510);
      this.Controls.Add((Control) this.panel2);
      this.Name = "Form1";
      this.Text = "Form1";
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
