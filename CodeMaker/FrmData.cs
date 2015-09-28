// Decompiled with JetBrains decompiler
// Type: CodeMaker.FrmData
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.Properties;
using DbObjects;
using DbObjects.SQL2005;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CodeMaker
{
  public class FrmData : BaseMain
  {
    public string m_Pdm = Application.StartupPath + "\\Pdm.ini";
    private IContainer components = (IContainer) null;
    public FrmStart m_FrmStart;
    public string constr;
    private ToolTip toolTip2;
    private ToolTip toolTip1;
    private FolderBrowserDialog fdbDescribe;
    private FolderBrowserDialog fdbcOrder;
    private FolderBrowserDialog fdbcStore;
    private OpenFileDialog openFileDialog1pdm;
    public Label label3;
    public TextBox txtPass;
    public Label label1;
    public Label label5;
    public Label label6;
    public Label label2;
    public GroupBox groupBox1;
    public TextBox txtUser;
    private Label label4;
    public CheckBox chk_Simple;
    private TabPage tabPage2;
    private GroupBox groupBox2;
    private Label labMessage;
    public TextBox txbServer;
    private Button button1;
    public ComboBox comboBoxVa;
    public ComboBox comboBoxDatabase;
    private Label label8;
    public TextBox txbName;
    public Label label9;
    public Label label10;
    public Label label12;
    public TextBox txbSecrity;
    public Label label13;
    private Button button2;
    private Button btnSQLServer;
    private TabPage tabPage1;
    private GroupBox groupBox3;
    private TextBox txbPDM;
    private Label label7;
    public TabControl tabControl1;
    private Label label11;
    private PictureBox pictureBox2;
    private PictureBox pictureBox1;
    private Button btnClosePdm;
    private Button btnSavePdm;
    private Button btnPathOfpdm;
    private PictureBox close;

    public FrmData(FrmStart frm)
    {
      this.m_FrmStart = frm;
      this.InitializeComponent();
      string pdmConn = frm.GetPdmConn();
      switch (frm.MyDataType)
      {
        case DataType.PowerDesigner:
          if (!string.IsNullOrWhiteSpace(pdmConn))
          {
            this.txbPDM.Text = pdmConn.Split(',')[0];
            break;
          }
          break;
        case DataType.MSSQLSRV2008:
        case DataType.MSSQLSRV2005:
          this.tabControl1.SelectedTab = this.tabControl1.TabPages["tabPage2"];
          if (!string.IsNullOrWhiteSpace(pdmConn))
          {
            this.txbServer.Text = pdmConn.Split(';')[1].Split('=')[1];
            break;
          }
          break;
      }
      this.comboBoxVa.SelectedIndex = 0;
      this.btnSQLServer.Enabled = false;
      this.comboBoxDatabase.Enabled = false;
      if (!BaseBusiness.IsNotDefault())
        return;
      this.LoadUIText();
    }

    private void LoadUIText()
    {
      this.Text = BaseBusiness.GetResourceValue("lianjieshujuyuan");
      this.groupBox3.Text = BaseBusiness.GetResourceValue("genju");
      this.btnPathOfpdm.Text = BaseBusiness.GetResourceValue("xuanze");
      this.btnSavePdm.Text = BaseBusiness.GetResourceValue("baocun");
      this.btnClosePdm.Text = BaseBusiness.GetResourceValue("quxiao");
      this.groupBox2.Text = BaseBusiness.GetResourceValue("qingxianceshi");
      this.label12.Text = BaseBusiness.GetResourceValue("fuwuqimingcheng");
      this.label10.Text = BaseBusiness.GetResourceValue("shengfenyanzheng");
      this.label9.Text = BaseBusiness.GetResourceValue("dengluming");
      this.label13.Text = BaseBusiness.GetResourceValue("mima");
      this.label8.Text = BaseBusiness.GetResourceValue("shujuku");
      this.button1.Text = BaseBusiness.GetResourceValue("ceshi");
      this.btnSQLServer.Text = BaseBusiness.GetResourceValue("baocun");
      this.button2.Text = BaseBusiness.GetResourceValue("quxiao");
    }

    public string GetSelVerified()
    {
      return this.comboBoxVa.SelectedItem.ToString() == "Windows" ? "Windows" : "SQL";
    }

    private string GetSQLVer(string connectionString)
    {
      string cmdText = "select serverproperty('productversion')";
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
        {
          try
          {
            connection.Open();
            object objA = sqlCommand.ExecuteScalar();
            if (object.Equals(objA, (object) null) || object.Equals(objA, (object) DBNull.Value))
              return string.Empty;
            string str = objA.ToString().Trim();
            if (str.Length > 1)
              return str.Substring(0, str.IndexOf('.'));
            return string.Empty;
          }
          catch (SqlException ex)
          {
            return string.Empty;
          }
          finally
          {
            sqlCommand.Dispose();
            connection.Close();
          }
        }
      }
    }

    private bool test()
    {
      this.labMessage.Text = string.Empty;
      string str1 = this.txbServer.Text.Trim();
      string str2 = this.txbName.Text.Trim();
      string str3 = this.txbSecrity.Text.Trim();
      if (string.IsNullOrWhiteSpace(str2) || string.IsNullOrWhiteSpace(str1))
        return false;
      if (this.GetSelVerified() == "Windows")
        this.constr = "initial catalog=master;Data Source=" + str1 + ";Integrated Security=SSPI;";
      else if (str3 == "")
        this.constr = "initial catalog=master;Data Source=" + str1 + ";user id=" + str2 + ";";
      else
        this.constr = "initial catalog=master;Data Source=" + str1 + ";user id=" + str2 + ";password=" + str3 + ";";
      IDbObject dbObject = (IDbObject) new DataAccess();
      dbObject.DbConnectStr = this.constr;
      if (!string.IsNullOrWhiteSpace(this.GetSQLVer(this.constr)))
      {
        List<string> dbList = dbObject.GetDBList();
        if (dbList != null && dbList.Count > 0)
        {
          this.btnSQLServer.Enabled = true;
          this.comboBoxDatabase.Enabled = true;
          this.comboBoxDatabase.Items.Clear();
          foreach (object obj in dbList)
            this.comboBoxDatabase.Items.Add(obj);
          this.comboBoxDatabase.SelectedIndex = 0;
          return true;
        }
      }
      this.btnSQLServer.Enabled = false;
      this.comboBoxDatabase.Enabled = false;
      return false;
    }

    private void btnSavePdm_Click(object sender, EventArgs e)
    {
      Common.Write(this.m_Pdm, this.txbPDM.Text.Trim() + (object) "," + (string) (object) DataType.PowerDesigner);
      this.m_FrmStart.MyDataType = DataType.PowerDesigner;
      this.m_FrmStart.DialogResult = DialogResult.OK;
      int num = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("baocunchenggong"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
      this.Close();
    }

    private void btnPathOfpdm_Click_1(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(this.txbPDM.Text))
      {
        this.openFileDialog1pdm.InitialDirectory = this.txbPDM.Text.Substring(0, this.txbPDM.Text.LastIndexOf('\\')) + "\\";
        this.openFileDialog1pdm.FileName = this.txbPDM.Text.Substring(this.txbPDM.Text.LastIndexOf('\\') + 1);
      }
      this.openFileDialog1pdm.Filter = "PowerDesigner|*.PDM";
      this.openFileDialog1pdm.DefaultExt = "*.PDM";
      if (this.openFileDialog1pdm.ShowDialog() != DialogResult.OK || !(this.openFileDialog1pdm.FileName != "openFileDialog1"))
        return;
      this.txbPDM.Text = this.openFileDialog1pdm.FileName;
    }

    private void btnClosePdm_Click(object sender, EventArgs e)
    {
      this.m_FrmStart.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.test())
        this.labMessage.Text = BaseBusiness.GetResourceValue("ceshichenggong");
      else
        this.labMessage.Text = BaseBusiness.GetResourceValue("ceshishibai");
    }

    private void comboBoxVa_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.comboBoxVa.SelectedItem.ToString() == "Windows")
      {
        this.txbName.Enabled = false;
        this.txbSecrity.Enabled = false;
      }
      else
      {
        this.txbName.Enabled = true;
        this.txbSecrity.Enabled = true;
      }
    }

    private void btnSQLServer_Click(object sender, EventArgs e)
    {
      if (this.GetSQLVer(this.constr) == "9")
      {
        Common.Write(this.m_Pdm, this.constr.Replace("master", this.comboBoxDatabase.Text.Trim()) + (object) "," + (string) (object) DataType.MSSQLSRV2005);
        this.m_FrmStart.MyDataType = DataType.MSSQLSRV2005;
        this.m_FrmStart.DialogResult = DialogResult.OK;
        int num = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("baocunchenggong"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
        this.Close();
      }
      else
      {
        Common.Write(this.m_Pdm, this.constr.Replace("master", this.comboBoxDatabase.Text.Trim()) + (object) "," + (string) (object) DataType.MSSQLSRV2008);
        this.m_FrmStart.MyDataType = DataType.MSSQLSRV2008;
        this.m_FrmStart.DialogResult = DialogResult.OK;
        int num = (int) this.m_FrmStart.MessageBoxShow(BaseBusiness.GetResourceValue("baocunchenggong"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
        this.Close();
      }
    }

    private void close_MouseClick(object sender, MouseEventArgs e)
    {
      this.btnClosePdm_Click(sender, (EventArgs) e);
    }

    private void close_MouseHover(object sender, EventArgs e)
    {
      this.close.Image = this.close.ErrorImage;
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
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmData));
      this.toolTip1 = new ToolTip(this.components);
      this.fdbDescribe = new FolderBrowserDialog();
      this.fdbcOrder = new FolderBrowserDialog();
      this.fdbcStore = new FolderBrowserDialog();
      this.toolTip2 = new ToolTip(this.components);
      this.chk_Simple = new CheckBox();
      this.openFileDialog1pdm = new OpenFileDialog();
      this.label3 = new Label();
      this.txtPass = new TextBox();
      this.label1 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.label2 = new Label();
      this.groupBox1 = new GroupBox();
      this.txtUser = new TextBox();
      this.label4 = new Label();
      this.tabPage2 = new TabPage();
      this.groupBox2 = new GroupBox();
      this.labMessage = new Label();
      this.txbServer = new TextBox();
      this.button1 = new Button();
      this.comboBoxVa = new ComboBox();
      this.comboBoxDatabase = new ComboBox();
      this.label8 = new Label();
      this.txbName = new TextBox();
      this.label9 = new Label();
      this.label10 = new Label();
      this.label12 = new Label();
      this.txbSecrity = new TextBox();
      this.label13 = new Label();
      this.button2 = new Button();
      this.btnSQLServer = new Button();
      this.tabPage1 = new TabPage();
      this.groupBox3 = new GroupBox();
      this.btnClosePdm = new Button();
      this.btnSavePdm = new Button();
      this.btnPathOfpdm = new Button();
      this.txbPDM = new TextBox();
      this.label7 = new Label();
      this.tabControl1 = new TabControl();
      this.label11 = new Label();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.close = new PictureBox();
      this.tabPage2.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.tabControl1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.close).BeginInit();
      this.SuspendLayout();
      this.chk_Simple.AutoSize = true;
      this.chk_Simple.Checked = true;
      this.chk_Simple.CheckState = CheckState.Checked;
      this.chk_Simple.Location = new Point(229, 204);
      this.chk_Simple.Name = "chk_Simple";
      this.chk_Simple.Size = new Size(96, 16);
      this.chk_Simple.TabIndex = 37;
      this.chk_Simple.Text = "高效连接模式";
      this.toolTip2.SetToolTip((Control) this.chk_Simple, "在表非常多的情况下，启用该模式提高连接速度");
      this.chk_Simple.UseVisualStyleBackColor = true;
      this.openFileDialog1pdm.FileName = "openFileDialog1";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(145, 158);
      this.label3.Name = "label3";
      this.label3.Size = new Size(59, 12);
      this.label3.TabIndex = 29;
      this.txtPass.Location = new Point(229, 154);
      this.txtPass.Name = "txtPass";
      this.txtPass.PasswordChar = '*';
      this.txtPass.Size = new Size(232, 21);
      this.txtPass.TabIndex = 30;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(109, 61);
      this.label1.Name = "label1";
      this.label1.Size = new Size(95, 12);
      this.label1.TabIndex = 24;
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(109, 85);
      this.label5.Name = "label5";
      this.label5.Size = new Size(95, 12);
      this.label5.TabIndex = 26;
      this.label5.TextAlign = ContentAlignment.MiddleCenter;
      this.label6.AutoSize = true;
      this.label6.Location = new Point(121, 109);
      this.label6.Name = "label6";
      this.label6.Size = new Size(83, 12);
      this.label6.TabIndex = 25;
      this.label6.TextAlign = ContentAlignment.MiddleCenter;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(133, 133);
      this.label2.Name = "label2";
      this.label2.Size = new Size(71, 12);
      this.label2.TabIndex = 28;
      this.groupBox1.Location = new Point(117, 227);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(380, 5);
      this.groupBox1.TabIndex = 27;
      this.groupBox1.TabStop = false;
      this.txtUser.Location = new Point(229, 129);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new Size(232, 21);
      this.txtUser.TabIndex = 31;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(133, 183);
      this.label4.Name = "label4";
      this.label4.Size = new Size(71, 12);
      this.label4.TabIndex = 32;
      this.tabPage2.BackColor = Color.FromArgb(247, 247, 247);
      this.tabPage2.Controls.Add((Control) this.groupBox2);
      this.tabPage2.Font = new Font("微软雅黑", 12f);
      this.tabPage2.Location = new Point(4, 30);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(562, 340);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "SQL Server";
      this.groupBox2.BackColor = Color.Transparent;
      this.groupBox2.Controls.Add((Control) this.labMessage);
      this.groupBox2.Controls.Add((Control) this.txbServer);
      this.groupBox2.Controls.Add((Control) this.button1);
      this.groupBox2.Controls.Add((Control) this.comboBoxVa);
      this.groupBox2.Controls.Add((Control) this.comboBoxDatabase);
      this.groupBox2.Controls.Add((Control) this.label8);
      this.groupBox2.Controls.Add((Control) this.txbName);
      this.groupBox2.Controls.Add((Control) this.label9);
      this.groupBox2.Controls.Add((Control) this.label10);
      this.groupBox2.Controls.Add((Control) this.label12);
      this.groupBox2.Controls.Add((Control) this.txbSecrity);
      this.groupBox2.Controls.Add((Control) this.label13);
      this.groupBox2.Controls.Add((Control) this.button2);
      this.groupBox2.Controls.Add((Control) this.btnSQLServer);
      this.groupBox2.Font = new Font("微软雅黑", 12f);
      this.groupBox2.ForeColor = Color.FromArgb(181, 181, 181);
      this.groupBox2.Location = new Point(14, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(531, 301);
      this.groupBox2.TabIndex = 13;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "温馨提示：请先测试，然后保存";
      this.labMessage.AutoSize = true;
      this.labMessage.ForeColor = Color.Red;
      this.labMessage.Location = new Point(263, 221);
      this.labMessage.Name = "labMessage";
      this.labMessage.Size = new Size(0, 21);
      this.labMessage.TabIndex = 56;
      this.txbServer.Location = new Point(192, 46);
      this.txbServer.Name = "txbServer";
      this.txbServer.Size = new Size(232, 29);
      this.txbServer.TabIndex = 55;
      this.txbServer.Text = "127.0.0.1";
      this.button1.BackColor = Color.FromArgb(247, 247, 247);
      this.button1.BackgroundImage = (Image) Resources.tanchukuang22;
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.ForeColor = Color.FromArgb(247, 247, 247);
      this.button1.Location = new Point(62, 246);
      this.button1.Margin = new Padding(3, 4, 3, 4);
      this.button1.Name = "button1";
      this.button1.Size = new Size(104, 38);
      this.button1.TabIndex = 54;
      this.button1.Text = "测试";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.comboBoxVa.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxVa.FormattingEnabled = true;
      this.comboBoxVa.Items.AddRange(new object[2]
      {
        (object) "SQL Server",
        (object) "Windows"
      });
      this.comboBoxVa.Location = new Point(192, 78);
      this.comboBoxVa.Name = "comboBoxVa";
      this.comboBoxVa.Size = new Size(232, 29);
      this.comboBoxVa.TabIndex = 50;
      this.comboBoxVa.SelectedIndexChanged += new EventHandler(this.comboBoxVa_SelectedIndexChanged);
      this.comboBoxDatabase.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxDatabase.Enabled = false;
      this.comboBoxDatabase.Location = new Point(192, 175);
      this.comboBoxDatabase.Name = "comboBoxDatabase";
      this.comboBoxDatabase.Size = new Size(232, 29);
      this.comboBoxDatabase.TabIndex = 49;
      this.label8.AutoSize = true;
      this.label8.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label8.ForeColor = Color.FromArgb(64, 64, 64);
      this.label8.Location = new Point(58, 175);
      this.label8.Name = "label8";
      this.label8.Size = new Size(58, 22);
      this.label8.TabIndex = 48;
      this.label8.Text = "数据库";
      this.txbName.Location = new Point(192, 111);
      this.txbName.Name = "txbName";
      this.txbName.Size = new Size(232, 29);
      this.txbName.TabIndex = 46;
      this.txbName.Text = "sa";
      this.label9.AutoSize = true;
      this.label9.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label9.ForeColor = Color.FromArgb(64, 64, 64);
      this.label9.Location = new Point(58, 111);
      this.label9.Name = "label9";
      this.label9.Size = new Size(58, 22);
      this.label9.TabIndex = 44;
      this.label9.Text = "登录名";
      this.label10.AutoSize = true;
      this.label10.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label10.ForeColor = Color.FromArgb(64, 64, 64);
      this.label10.Location = new Point(58, 80);
      this.label10.Name = "label10";
      this.label10.Size = new Size(74, 22);
      this.label10.TabIndex = 42;
      this.label10.Text = "身份验证";
      this.label10.TextAlign = ContentAlignment.MiddleCenter;
      this.label12.AutoSize = true;
      this.label12.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label12.ForeColor = Color.FromArgb(64, 64, 64);
      this.label12.Location = new Point(58, 51);
      this.label12.Name = "label12";
      this.label12.Size = new Size(90, 22);
      this.label12.TabIndex = 41;
      this.label12.Text = "服务器名称";
      this.label12.TextAlign = ContentAlignment.MiddleCenter;
      this.txbSecrity.Location = new Point(192, 143);
      this.txbSecrity.Name = "txbSecrity";
      this.txbSecrity.PasswordChar = '*';
      this.txbSecrity.Size = new Size(232, 29);
      this.txbSecrity.TabIndex = 47;
      this.label13.AutoSize = true;
      this.label13.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label13.ForeColor = Color.FromArgb(64, 64, 64);
      this.label13.Location = new Point(58, 143);
      this.label13.Name = "label13";
      this.label13.Size = new Size(42, 22);
      this.label13.TabIndex = 45;
      this.label13.Text = "密码";
      this.button2.BackColor = Color.FromArgb(247, 247, 247);
      this.button2.BackgroundImage = (Image) Resources.tanchukuang10;
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.button2.FlatStyle = FlatStyle.Flat;
      this.button2.ForeColor = Color.FromArgb(247, 247, 247);
      this.button2.Location = new Point(320, 246);
      this.button2.Margin = new Padding(3, 4, 3, 4);
      this.button2.Name = "button2";
      this.button2.Size = new Size(104, 38);
      this.button2.TabIndex = 21;
      this.button2.Text = "取消";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.btnClosePdm_Click);
      this.btnSQLServer.BackColor = Color.FromArgb(247, 247, 247);
      this.btnSQLServer.BackgroundImage = (Image) Resources.tanchukuang22;
      this.btnSQLServer.FlatAppearance.BorderSize = 0;
      this.btnSQLServer.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnSQLServer.FlatStyle = FlatStyle.Flat;
      this.btnSQLServer.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnSQLServer.Location = new Point(192, 246);
      this.btnSQLServer.Margin = new Padding(3, 4, 3, 4);
      this.btnSQLServer.Name = "btnSQLServer";
      this.btnSQLServer.Size = new Size(104, 38);
      this.btnSQLServer.TabIndex = 20;
      this.btnSQLServer.Text = "保存";
      this.btnSQLServer.UseVisualStyleBackColor = false;
      this.btnSQLServer.Click += new EventHandler(this.btnSQLServer_Click);
      this.tabPage1.BackColor = Color.FromArgb(247, 247, 247);
      this.tabPage1.Controls.Add((Control) this.groupBox3);
      this.tabPage1.Font = new Font("微软雅黑", 13f);
      this.tabPage1.Location = new Point(4, 30);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(598, 340);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "PowerDesigner";
      this.groupBox3.BackColor = Color.Transparent;
      this.groupBox3.Controls.Add((Control) this.btnClosePdm);
      this.groupBox3.Controls.Add((Control) this.btnSavePdm);
      this.groupBox3.Controls.Add((Control) this.btnPathOfpdm);
      this.groupBox3.Controls.Add((Control) this.txbPDM);
      this.groupBox3.Controls.Add((Control) this.label7);
      this.groupBox3.Font = new Font("微软雅黑", 12f);
      this.groupBox3.ForeColor = Color.FromArgb(181, 181, 181);
      this.groupBox3.Location = new Point(14, 12);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(531, 301);
      this.groupBox3.TabIndex = 12;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "根据PowerDesigner设计的数据库文件，生成一个完整的系统";
      this.btnClosePdm.BackColor = Color.FromArgb(247, 247, 247);
      this.btnClosePdm.BackgroundImage = (Image) Resources.tanchukuang10;
      this.btnClosePdm.Cursor = Cursors.Hand;
      this.btnClosePdm.FlatAppearance.BorderSize = 0;
      this.btnClosePdm.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnClosePdm.FlatStyle = FlatStyle.Flat;
      this.btnClosePdm.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnClosePdm.Location = new Point(318, 140);
      this.btnClosePdm.Margin = new Padding(0);
      this.btnClosePdm.Name = "btnClosePdm";
      this.btnClosePdm.Size = new Size(104, 38);
      this.btnClosePdm.TabIndex = 27;
      this.btnClosePdm.Text = "取消";
      this.btnClosePdm.UseVisualStyleBackColor = false;
      this.btnClosePdm.Click += new EventHandler(this.btnClosePdm_Click);
      this.btnSavePdm.BackColor = Color.FromArgb(247, 247, 247);
      this.btnSavePdm.BackgroundImage = (Image) Resources.tanchukuang22;
      this.btnSavePdm.Cursor = Cursors.Hand;
      this.btnSavePdm.FlatAppearance.BorderSize = 0;
      this.btnSavePdm.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnSavePdm.FlatStyle = FlatStyle.Flat;
      this.btnSavePdm.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnSavePdm.Location = new Point(164, 140);
      this.btnSavePdm.Margin = new Padding(3, 4, 3, 4);
      this.btnSavePdm.Name = "btnSavePdm";
      this.btnSavePdm.Size = new Size(104, 38);
      this.btnSavePdm.TabIndex = 25;
      this.btnSavePdm.Text = "保存";
      this.btnSavePdm.UseVisualStyleBackColor = false;
      this.btnSavePdm.Click += new EventHandler(this.btnSavePdm_Click);
      this.btnPathOfpdm.BackColor = Color.FromArgb(247, 247, 247);
      this.btnPathOfpdm.BackgroundImage = (Image) Resources.tanchukuang09;
      this.btnPathOfpdm.Cursor = Cursors.Hand;
      this.btnPathOfpdm.FlatAppearance.BorderSize = 0;
      this.btnPathOfpdm.FlatAppearance.MouseDownBackColor = Color.FromArgb(247, 247, 247);
      this.btnPathOfpdm.FlatStyle = FlatStyle.Flat;
      this.btnPathOfpdm.ForeColor = Color.FromArgb(247, 247, 247);
      this.btnPathOfpdm.Location = new Point(428, 66);
      this.btnPathOfpdm.Margin = new Padding(3, 4, 3, 4);
      this.btnPathOfpdm.Name = "btnPathOfpdm";
      this.btnPathOfpdm.Size = new Size(76, 36);
      this.btnPathOfpdm.TabIndex = 26;
      this.btnPathOfpdm.Text = "选择";
      this.btnPathOfpdm.UseVisualStyleBackColor = true;
      this.btnPathOfpdm.Click += new EventHandler(this.btnPathOfpdm_Click_1);
      this.txbPDM.Location = new Point(164, 72);
      this.txbPDM.Margin = new Padding(3, 4, 3, 4);
      this.txbPDM.Name = "txbPDM";
      this.txbPDM.Size = new Size(258, 29);
      this.txbPDM.TabIndex = 23;
      this.txbPDM.Text = "D:\\Moban\\Sys.PDM";
      this.label7.AutoSize = true;
      this.label7.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
      this.label7.ForeColor = Color.FromArgb(64, 64, 64);
      this.label7.Location = new Point(7, 74);
      this.label7.Name = "label7";
      this.label7.Size = new Size(134, 22);
      this.label7.TabIndex = 22;
      this.label7.Text = "PowerDesigner";
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Font = new Font("微软雅黑", 12f);
      this.tabControl1.Location = new Point(35, 61);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(570, 374);
      this.tabControl1.TabIndex = 15;
      this.label11.AutoSize = true;
      this.label11.Font = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label11.ForeColor = Color.FromArgb(64, 64, 64);
      this.label11.Location = new Point(52, 9);
      this.label11.Name = "label11";
      this.label11.Size = new Size(88, 16);
      this.label11.TabIndex = 29;
      this.label11.Text = "连接数据源";
      this.pictureBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox2.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox2.BackgroundImage");
      this.pictureBox2.ErrorImage = (Image) componentResourceManager.GetObject("pictureBox2.ErrorImage");
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.InitialImage = (Image) componentResourceManager.GetObject("pictureBox2.InitialImage");
      this.pictureBox2.Location = new Point(-5, 42);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(650, 3);
      this.pictureBox2.TabIndex = 28;
      this.pictureBox2.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.tanchu17;
      this.pictureBox1.Location = new Point(12, 1);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(40, 35);
      this.pictureBox1.TabIndex = 27;
      this.pictureBox1.TabStop = false;
      this.close.ErrorImage = (Image) Resources.mousedown;
      this.close.Image = (Image) Resources.mouseup;
      this.close.InitialImage = (Image) Resources.mouseup;
      this.close.Location = new Point(579, 9);
      this.close.Name = "close";
      this.close.Size = new Size(50, 21);
      this.close.TabIndex = 30;
      this.close.TabStop = false;
      this.close.MouseClick += new MouseEventHandler(this.close_MouseClick);
      this.close.MouseLeave += new EventHandler(this.close_MouseLeave);
      this.close.MouseHover += new EventHandler(this.close_MouseHover);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(247, 247, 247);
      this.ClientSize = new Size(639, 455);
      this.Controls.Add((Control) this.close);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.tabControl1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmData";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "连接数据源";
      this.tabPage2.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.close).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
