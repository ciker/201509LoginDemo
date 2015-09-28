// Decompiled with JetBrains decompiler
// Type: CodeMaker.FrmStart
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.Properties;
using CodeMaker.ServiceReference1;
using Crownwood.Magic.Common;
using Crownwood.Magic.Docking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeMaker
{
  public class FrmStart : BaseMain
  {
    public string m_Pdm = Application.StartupPath + "\\Pdm.ini";
    public string m_SystemConfig = Application.StartupPath + "\\SystemConfig.ini";
    private WebBrowser webBrowser1 = new WebBrowser();
    private Models models = new Models();
    private Repository repository = new Repository();
    private BLL bll = new BLL();
    private Controllers controllers = new Controllers();
    private Index index = new Index();
    private Create create = new Create();
    private Details details = new Details();
    private Edit edit = new Edit();
    private IBLL irepositoty = new IBLL();
    private Wcf wcf = new Wcf();
    private Workflow wf = new Workflow();
    private IContainer components = (IContainer) null;
    private Form m_childFrm;
    public DockingManager DBdockManager;
    private Content DbViewContent;
    private FrmTree m_FrmTree;
    private NotifyIcon m_notifyIcon1;
    private ContextMenu m_trayMenu;
    private MenuItem m_trayColse;
    private MenuItem m_trayOpen;
    private MenuItem m_trayLangben;
    private Panel panel1;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel tLabPath;
    public ToolStrip toolStrip1;
    private ToolStripButton tbtnConnection;
    private ToolStripButton btnOutput;
    private ToolStripButton tbtnRefresh;
    private ToolStripButton tbtnGenerate;
    private Panel panel3;
    private FolderBrowserDialog folderBrowserDialog1shengcheng;
    private ToolStripButton toolStripButton1;
    private ImageList leftViewImgsa;
    private ToolStripLabel toolStripLabel1;
    private ToolStripLabel toolStripLabel2;
    private PictureBox pictureBox1;
    private Panel panel2;
    private Label banben;
    private LinkLabel jishuboke;
    private LinkLabel guanfangwangzhan;
    private LinkLabel jiaoliuluntan;
    private Label kongjian;
    private Label weizhi;
    private Label lujing;
    private Label shengchengweizhi;
    private Label mingmingkongjian;
    private Label shujuyuancunfangweizhi;
    private PictureBox min;
    private PictureBox close;
    private PictureBox pictureBox2;
    public Crownwood.Magic.Controls.TabControl tabControlMain;
    private Panel panelTab;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripButton toolStripButton2;

    public DataType MyDataType
    {
      get
      {
        return this.GetPdmDataType();
      }
      set
      {
      }
    }

    public FrmStart()
    {
      this.InitializeComponent();
    }

    private void FrmStart_Load(object sender, EventArgs e)
    {
      BaseBusiness.Culture = BaseBusiness.ValidateCulture(Application.CurrentCulture.Name.ToLower());
      this.banben.Text = string.Format(BaseBusiness.GetResourceValue("nihao"), (object) " 6.1");
      string id = Common.GetID();
      string str1 = string.Empty;
      try
      {
        using (VersionClient versionClient = new VersionClient())
        {
          string str2 = versionClient.DoWork(id);
          versionClient.Close();
          if ("3" != str2.Substring(0, 1))
          {
            int num = (int) this.MessageBoxShow(BaseBusiness.GetResourceValue("LatestVersion"), MessageBoxIcon.Hand, MessageBoxButtons.OK);
            this.tLabPath_Click(sender, e);
          }
        }
      }
      catch
      {
      }
      if (BaseBusiness.IsNotDefault())
        this.LoadUIText();
      if (!File.Exists(this.m_Pdm))
      {
        this.tbtnConnection.ToolTipText = "第一次运行，请先选择需要生成代码的数据源";
        this.toolStripButton1.Enabled = false;
        this.tbtnGenerate.Enabled = false;
        this.tbtnRefresh.Enabled = false;
        this.btnOutput.Enabled = false;
      }
      else
      {
        this.GetPath();
        this.LeftPanle();
      }
      this.AddNewTabPage((Control) this.panel2, BaseBusiness.GetResourceValue("wodegongzuotai"));
      this.webBrowser1.Location = new Point(12, 367);
      this.webBrowser1.MinimumSize = new Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new Size(785, 270);
      this.webBrowser1.TabIndex = 13;
      this.webBrowser1.Url = new Uri("http://www.langben.com", UriKind.Absolute);
      this.AddTabPage((Control) this.webBrowser1, BaseBusiness.GetResourceValue("shiyongshuoming"));
      this.ThisTray();
    }

    private void GetPath()
    {
      string pdmConn = this.GetPdmConn();
      if (string.IsNullOrWhiteSpace(pdmConn) || pdmConn.Length > 25)
        this.lujing.Text = pdmConn.Substring(0, 25) + "...";
      else
        this.lujing.Text = pdmConn;
      this.weizhi.Text = this.GetSystemConfigMuDiDi();
      this.kongjian.Text = Common.GetNameSpace(this.m_SystemConfig);
    }

    private void WindowClose()
    {
      this.m_trayColse.Click -= new EventHandler(this.MenuItem1_Click);
      this.m_trayOpen.Click -= new EventHandler(this.MenuItem2_Click);
      this.m_notifyIcon1.DoubleClick -= new EventHandler(this.NotifyIcon1_DoubleClick);
      this.m_notifyIcon1.Visible = false;
      Application.ExitThread();
    }

    public void AddTabPage(Control ctrForm, string pageTitle)
    {
      if (!this.tabControlMain.Visible)
        this.tabControlMain.Visible = true;
      this.tabControlMain.TabPages.Add(new Crownwood.Magic.Controls.TabPage()
      {
        Title = pageTitle,
        Control = ctrForm
      });
    }

    public void AddSinglePage(Control control, string Title)
    {
      if (!this.tabControlMain.Visible)
        this.tabControlMain.Visible = true;
      bool flag = false;
      Crownwood.Magic.Controls.TabPage tabPage1 = (Crownwood.Magic.Controls.TabPage) null;
      foreach (Crownwood.Magic.Controls.TabPage tabPage2 in (CollectionBase) this.tabControlMain.TabPages)
      {
        if (tabPage2.Control.Name == control.Name)
        {
          flag = true;
          tabPage1 = tabPage2;
        }
      }
      if (!flag)
        this.AddNewTabPage(control, Title);
      else
        this.tabControlMain.SelectedTab = tabPage1;
    }

    public void AddNewTabPage(Control control, string Title)
    {
      if (this.tabControlMain.InvokeRequired)
      {
        this.Invoke((Delegate) new FrmStart.AddNewTabPageCallback(this.AddNewTabPage), (object) control, (object) Title);
      }
      else
      {
        Crownwood.Magic.Controls.TabPage tabPage = new Crownwood.Magic.Controls.TabPage();
        tabPage.Title = Title;
        tabPage.Control = control;
        this.tabControlMain.TabPages.Add(tabPage);
        this.tabControlMain.SelectedTab = tabPage;
      }
    }

    public string GetPdmConn()
    {
      if (File.Exists(this.m_Pdm))
      {
        string str = Common.Read(this.m_Pdm);
        if (!string.IsNullOrWhiteSpace(str))
        {
          string[] strArray = str.Split(',');
          if (strArray.Length >= 2)
            return strArray[0];
        }
      }
      return string.Empty;
    }

    private DataType GetPdmDataType()
    {
      if (File.Exists(this.m_Pdm))
      {
        string str = Common.Read(this.m_Pdm);
        if (!string.IsNullOrWhiteSpace(str))
        {
          string[] strArray = str.Split(',');
          if (strArray.Length >= 2)
          {
            switch (strArray[1].Trim())
            {
              case "PowerDesigner":
                BaseClass.m_Pdm = strArray[0];
                return DataType.PowerDesigner;
              case "MSSQLSRV2008":
                return DataType.MSSQLSRV2008;
              case "MSSQLSRV2005":
                return DataType.MSSQLSRV2005;
            }
          }
        }
      }
      return DataType.None;
    }

    public string GetSystemConfigMuDiDi()
    {
      string str = Common.Read(this.m_SystemConfig);
      if (!string.IsNullOrWhiteSpace(str))
      {
        string[] strArray = str.Split(',');
        if (strArray != null && strArray.Length >= 2)
          return strArray[1];
      }
      return (string) null;
    }

    public void WriteSystemConfig(string content)
    {
      if (string.IsNullOrWhiteSpace(content))
        return;
      Common.Write(this.m_SystemConfig, content);
    }

    public void WritePdm(string content)
    {
      if (string.IsNullOrWhiteSpace(content))
        return;
      Common.Write(this.m_Pdm, content);
    }

    public void LeftPanle()
    {
      if (this.DBdockManager != null && this.DBdockManager.Contents != null && this.DBdockManager.Contents.Count > 0)
        this.DBdockManager.HideAllContents();
      this.DBdockManager = new DockingManager((ContainerControl) this, VisualStyle.IDE);
      this.DBdockManager.BackColor = Color.DimGray;
      this.DBdockManager.OuterControl = (Control) this.panelTab;
      this.DBdockManager.InnerControl = (Control) this.panelTab;
      this.DbViewContent = new Content(this.DBdockManager);
      this.m_FrmTree = new FrmTree((Form) this);
      this.DbViewContent.Control = (Control) this.m_FrmTree;
      Size size = this.DbViewContent.Control.Size;
      this.DbViewContent.Title = string.Format(BaseBusiness.GetResourceValue("shujuyuan"), (object) this.MyDataType);
      this.DbViewContent.FullTitle = string.Format(BaseBusiness.GetResourceValue("shujuyuan"), (object) this.MyDataType);
      this.DbViewContent.AutoHideSize = size;
      this.DbViewContent.DisplaySize = size;
      this.DbViewContent.ImageList = this.leftViewImgsa;
      this.DbViewContent.ImageIndex = 0;
      this.DbViewContent.HideButton = false;
      this.DBdockManager.Contents.Add(this.DbViewContent);
      this.DBdockManager.AddContentWithState(this.DbViewContent, Crownwood.Magic.Docking.State.DockLeft);
      this.DBdockManager.BackColor = Color.FromArgb(166, 166, 166);
      this.DBdockManager.CaptionFont = new Font("微软雅黑", 11f, FontStyle.Regular);
    }

    private void ShowFrom(Form frm)
    {
      if (null != this.m_childFrm)
        this.m_childFrm.Close();
      this.m_childFrm = frm;
      this.m_childFrm.StartPosition = FormStartPosition.CenterScreen;
      int num = (int) this.m_childFrm.ShowDialog();
    }

    private void ShowFromTray(Form frm)
    {
      this.ShowInTaskbar = true;
      this.WindowState = FormWindowState.Normal;
      this.Opacity = 100.0;
      this.Activate();
    }

    private void tbtnConnection_Click(object sender, EventArgs e)
    {
      FrmData frmData = new FrmData(this);
      if (null != this.m_childFrm)
        this.m_childFrm.Close();
      this.m_childFrm = (Form) frmData;
      this.m_childFrm.StartPosition = FormStartPosition.CenterScreen;
      int num = (int) this.m_childFrm.ShowDialog();
      if (this.DialogResult != DialogResult.OK)
        return;
      this.LeftPanle();
      this.GetPath();
      this.toolStripButton1.Enabled = true;
      this.tbtnGenerate.Enabled = true;
      this.tbtnRefresh.Enabled = true;
      this.btnOutput.Enabled = true;
    }

    private void tbtnGenerate_Click(object sender, EventArgs e)
    {
      this.ShowFrom((Form) new FrmSystemSet(this));
      this.GetPath();
    }

    private void tbtnRefresh_Click(object sender, EventArgs e)
    {
      this.LeftPanle();
      this.GetPath();
    }

    private void btnOutput_Click(object sender, EventArgs e)
    {
      string str1 = string.Empty;
      string systemConfigMuDiDi = this.GetSystemConfigMuDiDi();
      if (!string.IsNullOrWhiteSpace(systemConfigMuDiDi) && Directory.Exists(systemConfigMuDiDi))
      {
        string str2 = Common.GetPath(systemConfigMuDiDi) + DateTime.Now.ToString("yyyyMMddHHmmssff");
        BaseClass.m_RootDirectory = str2 + "\\Solution\\";
        if (this.MyDataType == DataType.PowerDesigner)
        {
          string pdmConn = this.GetPdmConn();
          if (!string.IsNullOrWhiteSpace(pdmConn) && File.Exists(pdmConn.Trim()))
          {
            BaseClass.m_Pdm = pdmConn;
          }
          else
          {
            int num = (int) this.MessageBoxShow("PowerDesigner " + BaseBusiness.GetResourceValue("wenjianbucunzai"), MessageBoxIcon.Hand, MessageBoxButtons.OK);
            return;
          }
        }
        if (this.MessageBoxShow(string.Format(BaseBusiness.GetResourceValue("shengchengdao"), (object) BaseClass.m_RootDirectory) + "\r\n " + BaseBusiness.GetResourceValue("zhidingshengchengweizhi") + "\r\n进行智能分析，请耐心等待大约3分钟...", MessageBoxIcon.Question, MessageBoxButtons.OKCancel) != DialogResult.OK)
          return;
        DataSourse data = new DataFactory().CreateDataSource(this.MyDataType).GetData(this.GetPdmConn());
        if (!string.IsNullOrWhiteSpace(data.m_ErrorMessage))
        {
          int num1 = (int) MessageBox.Show(data.m_ErrorMessage);
        }
        else
        {
          List<Table> listTable = Common.ConvertT(data.ListTable);
          List<Reference> listReference = data.ListReference;
          List<View> list1 = Common.ConvertT(data.ListView);
          List<string> list2 = new List<string>();
          List<string> list3 = new List<string>();
          if (this.m_FrmTree != null && this.m_FrmTree.treeView1 != null && (this.m_FrmTree.treeView1.Nodes != null && this.m_FrmTree.treeView1.Nodes[0] != null) && (this.m_FrmTree.treeView1.Nodes[0].Nodes != null && this.m_FrmTree.treeView1.Nodes[0].Nodes[0] != null && this.m_FrmTree.treeView1.Nodes[0].Nodes[0].Nodes != null) && this.m_FrmTree.treeView1.Nodes[0].Nodes[0].Nodes.Count > 0)
          {
            foreach (TreeNode treeNode in this.m_FrmTree.treeView1.Nodes[0].Nodes[0].Nodes)
            {
              if (!treeNode.Checked)
                list2.Add(treeNode.Tag.ToString());
            }
          }
          if (this.m_FrmTree != null && this.m_FrmTree.treeView1 != null && (this.m_FrmTree.treeView1.Nodes != null && this.m_FrmTree.treeView1.Nodes[0] != null) && (this.m_FrmTree.treeView1.Nodes[0].Nodes != null && this.m_FrmTree.treeView1.Nodes[0].Nodes[0] != null && this.m_FrmTree.treeView1.Nodes[0].Nodes[1].Nodes != null) && this.m_FrmTree.treeView1.Nodes[0].Nodes[1].Nodes.Count > 0)
          {
            foreach (TreeNode treeNode in this.m_FrmTree.treeView1.Nodes[0].Nodes[1].Nodes)
            {
              if (!treeNode.Checked)
                list3.Add(treeNode.Tag.ToString());
            }
          }
          List<Reference> list4 = Enumerable.ToList<Reference>(Enumerable.Distinct<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) listReference, (Func<Reference, bool>) (l => l.ParentTable == l.ChildTable))));
          Enumerable.Select<IGrouping<string, Reference>, string>(Enumerable.Where<IGrouping<string, Reference>>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, bool>) (g => Enumerable.Count<Reference>((IEnumerable<Reference>) g) == 2)), (Func<IGrouping<string, Reference>, string>) (g => g.Key));
          Enumerable.Select<IGrouping<string, Reference>, string>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, string>) (g => g.Key));
          Enumerable.Select<IGrouping<string, Reference>, IGrouping<string, Reference>>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, IGrouping<string, Reference>>) (g => g));
          Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (f => f.Columns.Count == 2)), (Func<Table, string>) (f => f.Id));
          List<Reference> refNotIdClassId = Enumerable.ToList<Reference>(Enumerable.Distinct<Reference>(Enumerable.Where<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) listReference, (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<IGrouping<string, Reference>, string>(Enumerable.Where<IGrouping<string, Reference>>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, bool>) (g => Enumerable.Count<Reference>((IEnumerable<Reference>) g) == 2)), (Func<IGrouping<string, Reference>, string>) (g => g.Key)), l.ChildTable))), (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (f => f.Columns.Count == 2)), (Func<Table, string>) (f => f.Id)), l.ChildTable)))));
          List<Reference> list5 = Enumerable.ToList<Reference>(Enumerable.Distinct<Reference>(Enumerable.Where<Reference>(Enumerable.Where<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) listReference, (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<IGrouping<string, Reference>, string>(Enumerable.Where<IGrouping<string, Reference>>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, bool>) (g => Enumerable.Count<Reference>((IEnumerable<Reference>) g) >= 1)), (Func<IGrouping<string, Reference>, string>) (g => g.Key)), l.ChildTable))), (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (f => f.Columns.Count > 2)), (Func<Table, string>) (f => f.Id)), l.ChildTable))), (Func<Reference, bool>) (l => l.ParentTable != l.ChildTable))));
          List<string> list6 = Enumerable.ToList<string>(Enumerable.Distinct<string>(Enumerable.Select<Reference, string>(Enumerable.Where<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) listReference, (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<IGrouping<string, Reference>, string>(Enumerable.Where<IGrouping<string, Reference>>(Enumerable.GroupBy<Reference, string>((IEnumerable<Reference>) listReference, (Func<Reference, string>) (f => f.ChildTable)), (Func<IGrouping<string, Reference>, bool>) (g => Enumerable.Count<Reference>((IEnumerable<Reference>) g) == 2)), (Func<IGrouping<string, Reference>, string>) (g => g.Key)), l.ChildTable))), (Func<Reference, bool>) (l => Enumerable.Contains<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (f => f.Columns.Count == 2)), (Func<Table, string>) (f => f.Id)), l.ChildTable))), (Func<Reference, string>) (l => l.ChildTable))));
          List<string> fileName1 = new List<string>();
          List<string> fileName2 = new List<string>();
          List<string> fileName3 = new List<string>();
          List<string> fileName4 = new List<string>();
          List<string> fileName5 = new List<string>();
          List<string> fileName6 = new List<string>();
          List<string> fileName7 = new List<string>();
          List<string> fileName8 = new List<string>();
          List<string> fileName9 = new List<string>();
          List<string> fileName10 = new List<string>();
          string nameSpace = Common.GetNameSpace(this.m_SystemConfig);
          bool jiChengQuanXian = Common.GetJiChengQuanXian(this.m_SystemConfig);
          if (jiChengQuanXian)
            Common.Zip(str2.Trim() + "/", (Stream) new MemoryStream(Resources.Solution622));
          else
            Common.Zip(str2.Trim() + "/", (Stream) new MemoryStream(Resources.Solution));
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append(" <div data-options=@iconCls:'tu2011'@ title=@所有表@>\r\n                <div class=@easyui-panel@ fit=@true@ border=@false@>\r\n                    <ul class=@easyui-tree@>".Replace('@', '"'));
          foreach (Table table in listTable)
          {
            Table item = table;
            if (!list6.Contains(item.Id) && ((list2 == null || list2.Count <= 0 || !Enumerable.Any<string>((IEnumerable<string>) list2, (Func<string, bool>) (a => a == item.Id))) && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => a.ToLower() == item.Code.ToLower()))))
            {
              if (!Common.ExitsPrimaryKey(item))
              {
                int num2 = (int) this.MessageBoxShow(string.Format(BaseBusiness.GetResourceValue("biaomeiyouzhujian"), (object) item.Code, (object) item.Name), MessageBoxIcon.Hand, MessageBoxButtons.OK);
                return;
              }
              item.NameSpace = nameSpace;
              item.PrimaryKeyType = Common.PrimaryKeyType(item);
              item.childTableColumnRef = Enumerable.SingleOrDefault<string>(Enumerable.Select<Reference, string>(Enumerable.Where<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) list4, (Func<Reference, bool>) (w => w.ChildTable == item.Id)), (Func<Reference, bool>) (w => w.ParentTable == item.Id)), (Func<Reference, string>) (s => s.ChildTableColumnRef)));
              IEnumerable<Reference> enumerable = Enumerable.Where<Reference>((IEnumerable<Reference>) list5, (Func<Reference, bool>) (w => w.ChildTable == item.Id));
              List<RefIdName> list7 = new List<RefIdName>();
              foreach (Reference reference in enumerable)
              {
                Reference it = reference;
                list7.Add(new RefIdName()
                {
                  RefTableCode = Enumerable.FirstOrDefault<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (i => i.Id == it.ParentTable)), (Func<Table, string>) (i => i.Code))),
                  Ref = Enumerable.FirstOrDefault<string>(Enumerable.Select<Column, string>(Enumerable.Where<Column>((IEnumerable<Column>) item.Columns, (Func<Column, bool>) (i => i.Id == it.ChildTableColumnRef)), (Func<Column, string>) (i => i.Code))),
                  RefName = Enumerable.FirstOrDefault<string>(Enumerable.Select<Column, string>(Enumerable.Where<Column>((IEnumerable<Column>) item.Columns, (Func<Column, bool>) (i => i.Id == it.ChildTableColumnRef)), (Func<Column, string>) (i => i.Name))),
                  Id = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => param0.a.Code)),
                  Name = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => param0.i.Columns[1].Code)),
                  RefTableName = Enumerable.FirstOrDefault<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (i => i.Id == it.ParentTable)), (Func<Table, string>) (i => i.Name))),
                  RefType = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => Common.PrimaryKeyType(param0.i))),
                  IsRefSelf = Enumerable.Any<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) list4, (Func<Reference, bool>) (w => w.ParentTable == it.ParentTable)))
                });
              }
              if (list7 != null && list7.Count > 0)
                item.refId = list7;
              List<RefIdName> list8 = new List<RefIdName>();
              foreach (Reference reference in Enumerable.Select(Enumerable.Where(Enumerable.Where(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Reference>) refNotIdClassId, (Func<Reference, IEnumerable<Reference>>) (n => (IEnumerable<Reference>) refNotIdClassId), (n, o) =>
              {
                var fAnonymousType1 = new
                {
                  n = n,
                  o = o
                };
                return fAnonymousType1;
              }), param0 => param0.n.ParentTable != param0.o.ParentTable), param0 => param0.n.ChildTable == param0.o.ChildTable), param0 => param0.n.ParentTable == item.Id), param0 => param0.o))
              {
                Reference it = reference;
                list8.Add(new RefIdName()
                {
                  RefTableName = Enumerable.FirstOrDefault<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (i => i.Id == it.ParentTable)), (Func<Table, string>) (i => i.Name))),
                  RefTableCode = Enumerable.FirstOrDefault<string>(Enumerable.Select<Table, string>(Enumerable.Where<Table>((IEnumerable<Table>) listTable, (Func<Table, bool>) (i => i.Id == it.ParentTable)), (Func<Table, string>) (i => i.Code))),
                  Id = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => param0.a.Code)),
                  Name = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => param0.i.Columns[1].Code)),
                  RefType = Enumerable.FirstOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany((IEnumerable<Table>) listTable, (Func<Table, IEnumerable<Column>>) (i => (IEnumerable<Column>) i.Columns), (i, a) =>
                  {
                    var fAnonymousType0 = new
                    {
                      i = i,
                      a = a
                    };
                    return fAnonymousType0;
                  }), param0 => param0.a.Id == it.ParentTableColumnRef), param0 => Common.PrimaryKeyType(param0.i))),
                  IsRefSelf = Enumerable.Any<Reference>(Enumerable.Where<Reference>((IEnumerable<Reference>) list4, (Func<Reference, bool>) (w => w.ParentTable == it.ParentTable)))
                });
              }
              if (list8 != null && list8.Count > 0)
                item.refNotId = list8;
              this.create.DoCreate(item, ref fileName4);
              this.wf.DoWorkflow(item, ref fileName4);
              string message = this.bll.DoBLL(item, ref fileName2);
              if (!string.IsNullOrWhiteSpace(message))
              {
                int num2 = (int) this.MessageBoxShow(message, MessageBoxIcon.Hand, MessageBoxButtons.OK);
                return;
              }
              this.controllers.DoControllers(item, ref fileName3);
              this.index.DoIndex(item, ref fileName5);
              this.details.DoDetails(item, ref fileName6);
              this.edit.DoEdit(item, ref fileName7);
              this.models.DoModels(item, ref fileName8);
              this.irepositoty.DoIRepository(item, ref fileName9);
              this.repository.DoRepository(item, ref fileName1);
              this.wcf.DoWcf(item, ref fileName10);
              if (!jiChengQuanXian && item.Code == "SysField")
              {
                string content1 = Common.Read(BaseClass.m_DempDirectory + "/SysFieldModels.cs").Replace(BaseClass.m_BLLnamespace, item.NameSpace + "BLL").Replace(BaseClass.m_IBLLnamespace, item.NameSpace + "IBLL");
                Common.Write(BaseClass.m_RootDirectory + "/" + this.baseClass.m_App + "/Codes/SysFieldModels.cs", content1);
                string content2 = Common.Read(BaseClass.m_DempDirectory + "/SysFieldHander.cs").Replace(BaseClass.m_DALnamespace, item.NameSpace + "DAL").Replace(BaseClass.m_BLLnamespace, item.NameSpace + "BLL").Replace(BaseClass.m_IBLLnamespace, item.NameSpace + "IBLL");
                Common.Write(BaseClass.m_RootDirectory + "/" + this.baseClass.m_BLL + "/Framework/SysFieldHander.cs", content2);
                string content3 = Common.Read(BaseClass.m_DempDirectory + "/ISysFieldHander.cs").Replace(BaseClass.m_DALnamespace, item.NameSpace + "DAL").Replace(BaseClass.m_IBLLnamespace, item.NameSpace + "IBLL");
                Common.Write(BaseClass.m_RootDirectory + "/" + this.baseClass.m_IBLL + "/Framework/ISysFieldHander.cs", content3);
              }
              stringBuilder.Append(string.Format("\r\n<li data-options=@iconCls:'tu0202'@><a href=@#@ icon=@tu0202@ rel=@{0}@ >{1}</a></li>\r\n".Replace('@', '"'), (object) item.Code, (object) item.Name));
            }
          }
          stringBuilder.Append("   </ul>\r\n                </div>\r\n            </div>\r\n<div data-options=@iconCls:'tu2012'@ title=@所有视图@>\r\n                <div class=@easyui-panel@ fit=@true@ border=@false@>\r\n                    <ul class=@easyui-tree@>".Replace('@', '"'));
          foreach (View view in list1)
          {
            View item = view;
            if ((list3 == null || list3.Count <= 0 || !Enumerable.Any<string>((IEnumerable<string>) list3, (Func<string, bool>) (a => a == item.Id))) && !Enumerable.Any<string>((IEnumerable<string>) this.WorkflowView, (Func<string, bool>) (a => a == item.Code)))
            {
              item.NameSpace = nameSpace;
              this.irepositoty.DoIRepository(item, ref fileName9);
              this.repository.DoRepository(item, ref fileName1);
              this.bll.DoBLL(item, ref fileName2);
              this.controllers.DoControllers(item, ref fileName3);
              this.index.DoIndex(item, ref fileName5);
              this.models.DoModels(item, ref fileName8);
              stringBuilder.Append(string.Format("\r\n<li data-options=@iconCls:'tu0202'@><a href=@#@ icon=@tu0202@ rel=@{0}@ >{1}</a></li>\r\n".Replace('@', '"'), (object) item.Code, (object) item.Name));
            }
          }
          stringBuilder.Append("   </ul>\r\n                </div>\r\n            </div>\r\n");
          string content4 = Common.Read(BaseClass.m_RootDirectory + "/" + this.baseClass.m_App + "/Views/Home/Index.aspx").Replace(BaseClass.m_BLLnamespace, stringBuilder.ToString());
          Common.Write(BaseClass.m_RootDirectory + "/" + this.baseClass.m_App + "/Views/Home/Index.aspx", content4);
          string str3;
          if (!string.IsNullOrWhiteSpace(nameSpace))
          {
            string str4 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_DAL + "/";
            string path1 = str4 + "Model1.Designer.cs";
            if (!File.Exists(path1))
            {
              int num2 = (int) MessageBox.Show("Entity Framework文件不存在,即后缀名为edmx的文件", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return;
            }
            str3 = string.Empty;
            if (jiChengQuanXian)
            {
              using (StreamReader streamReader = new StreamReader(path1, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("Langben.DAL", nameSpace.Trim() + "DAL");
                streamReader.Close();
                Common.Write(path1, content1);
                str3 = string.Empty;
              }
            }
            else
            {
              using (StreamReader streamReader = new StreamReader(path1, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("DAL", nameSpace.Trim() + "DAL");
                streamReader.Close();
                Common.Write(path1, content1);
                str3 = string.Empty;
              }
            }
            foreach (string path2 in Directory.GetFiles(str4 + "Framework"))
            {
              using (StreamReader streamReader = new StreamReader(path2, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("namespace DAL", "namespace " + nameSpace.Trim() + "DAL");
                streamReader.Close();
                Common.Write(path2, content1);
                str3 = string.Empty;
              }
            }
            foreach (string path2 in Directory.GetFiles(BaseClass.m_RootDirectory + "/BLL/Framework"))
            {
              using (StreamReader streamReader = new StreamReader(path2, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("namespace BLL", "namespace " + nameSpace.Trim() + "BLL").Replace("using DAL", "using " + nameSpace.Trim() + "DAL");
                streamReader.Close();
                Common.Write(path2, content1);
                str3 = string.Empty;
              }
            }
            foreach (string path2 in Directory.GetFiles(BaseClass.m_RootDirectory + "/App/Models"))
            {
              using (StreamReader streamReader = new StreamReader(path2, Encoding.Default))
              {
                string content1 = !path2.Contains("SysFieldModels.cs") ? streamReader.ReadToEnd().Replace("using BLL", "using " + nameSpace.Trim() + "BLL").Replace("using DAL", "using " + nameSpace.Trim() + "DAL").Replace("namespace App.Models", "namespace " + nameSpace.Trim() + "App.Models").Replace("namespace Langben.App.Models", "namespace " + nameSpace.Trim() + "App.Models") : streamReader.ReadToEnd().Replace("using BLL", "using " + nameSpace.Trim() + "BLL").Replace("using DAL", "using " + nameSpace.Trim() + "DAL");
                streamReader.Close();
                Common.Write(path2, content1);
                str3 = string.Empty;
              }
            }
            foreach (string path2 in Directory.GetFiles(BaseClass.m_RootDirectory + "/App/Controllers"))
            {
              using (StreamReader streamReader = new StreamReader(path2, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("using BLL", "using " + nameSpace.Trim() + "BLL").Replace("using DAL", "using " + nameSpace.Trim() + "DAL").Replace("using App", "using " + nameSpace.Trim() + "App").Replace("namespace App.", "namespace " + nameSpace.Trim() + "App.");
                streamReader.Close();
                Common.Write(path2, content1);
                str3 = string.Empty;
              }
            }
            string path3 = BaseClass.m_RootDirectory + "/App/Global.asax.cs";
            using (StreamReader streamReader = new StreamReader(path3, Encoding.Default))
            {
              string content1 = streamReader.ReadToEnd().Replace("using BLL", "using " + nameSpace.Trim() + "BLL");
              streamReader.Close();
              Common.Write(path3, content1);
              str3 = string.Empty;
            }
            string path4 = BaseClass.m_RootDirectory + "/App/Res/jquery.uploadify-v2.1.4/FileUploader.ashx";
            using (StreamReader streamReader = new StreamReader(path4, Encoding.Default))
            {
              string content1 = streamReader.ReadToEnd().Replace("App", nameSpace.Trim() + "App");
              streamReader.Close();
              Common.Write(path4, content1);
              str3 = string.Empty;
            }
          }
          switch (this.MyDataType)
          {
            case DataType.PowerDesigner:
              if (data != null && data.DataBaseInfor != null && data.DataBaseInfor.Code == "MSSQLSRV2005")
              {
                string path = BaseClass.m_RootDirectory + "/" + this.baseClass.m_DAL + "/" + "Model1.edmx";
                if (!File.Exists(path))
                {
                  int num2 = (int) MessageBox.Show("Entity Framework文件不存在,即后缀名为edmx的文件", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  return;
                }
                str3 = string.Empty;
                using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
                {
                  string content1 = streamReader.ReadToEnd().Replace("ProviderManifestToken=@2008@".Replace('@', '"'), "ProviderManifestToken=@2005@").Replace('@', '"');
                  streamReader.Close();
                  Common.Write(path, content1);
                  str3 = string.Empty;
                  break;
                }
              }
              else
                break;
            case DataType.MSSQLSRV2008:
              string path5 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_App + "/Web.config";
              string str5 = string.Empty;
              using (StreamReader streamReader = new StreamReader(path5, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("Data Source=.;Initial Catalog=Sys;Persist Security Info=True;User ID=sa;Password=sa", this.GetPdmConn());
                streamReader.Close();
                Common.Write(path5, content1);
                str5 = string.Empty;
              }
              string path6 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_DAL + "/App.config";
              string str6 = string.Empty;
              using (StreamReader streamReader = new StreamReader(path6, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("Data Source=.;Initial Catalog=Sys;Persist Security Info=True;User ID=sa;Password=sa", this.GetPdmConn());
                streamReader.Close();
                Common.Write(path6, content1);
                str6 = string.Empty;
                break;
              }
            case DataType.MSSQLSRV2005:
              string path7 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_DAL + "/" + "Model1.edmx";
              string str7 = string.Empty;
              using (StreamReader streamReader = new StreamReader(path7, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("ProviderManifestToken=@2008@".Replace('@', '"'), "ProviderManifestToken=@2005@").Replace('@', '"');
                streamReader.Close();
                Common.Write(path7, content1);
                str7 = string.Empty;
              }
              string path8 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_App + "/Web.config";
              str7 = string.Empty;
              using (StreamReader streamReader = new StreamReader(path8, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("Data Source=.;Initial Catalog=Sys;Persist Security Info=True;User ID=sa;Password=sa;", this.GetPdmConn());
                streamReader.Close();
                Common.Write(path8, content1);
                str7 = string.Empty;
              }
              string path9 = BaseClass.m_RootDirectory + "/" + this.baseClass.m_DAL + "/App.config";
              string str8 = string.Empty;
              using (StreamReader streamReader = new StreamReader(path9, Encoding.Default))
              {
                string content1 = streamReader.ReadToEnd().Replace("Data Source=.;Initial Catalog=Sys;Persist Security Info=True;User ID=sa;Password=sa", this.GetPdmConn());
                streamReader.Close();
                Common.Write(path9, content1);
                str8 = string.Empty;
                break;
              }
          }
          new AddSolution().DoAddSolution(fileName1, fileName2, fileName9, fileName10, nameSpace);
          if (this.MessageBoxShow(BaseBusiness.GetResourceValue("jiejuefangan"), MessageBoxIcon.Asterisk, MessageBoxButtons.OK) == DialogResult.OK)
            Process.Start("Explorer", "/select," + BaseClass.m_RootDirectory + "Solution.sln");
          this.WindowClose();
        }
      }
      else
      {
        int num3 = (int) this.MessageBoxShow(BaseBusiness.GetResourceValue("lujingbucunzai"), MessageBoxIcon.Hand, MessageBoxButtons.OK);
      }
    }

    public DialogResult MessageBoxShow(string message, MessageBoxIcon icon = MessageBoxIcon.Hand, MessageBoxButtons box = MessageBoxButtons.OK)
    {
      return MessageBox.Show(message, BaseBusiness.GetResourceValue("caozuotishi"), box, icon);
    }

    private void FrmStart_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
      this.ShowInTaskbar = false;
      this.Opacity = 0.0;
      e.Cancel = true;
    }

    public void ThisTray()
    {
      this.components = (IContainer) new Container();
      this.m_trayMenu = new ContextMenu();
      this.m_trayColse = new MenuItem();
      this.m_trayOpen = new MenuItem();
      this.m_trayLangben = new MenuItem();
      this.m_trayMenu.MenuItems.AddRange(new MenuItem[3]
      {
        this.m_trayColse,
        this.m_trayOpen,
        this.m_trayLangben
      });
      this.m_trayColse.Index = 1;
      this.m_trayColse.Text = BaseBusiness.GetResourceValue("tuichu");
      this.m_trayColse.Click += new EventHandler(this.MenuItem1_Click);
      this.m_trayOpen.Index = 0;
      this.m_trayOpen.Text = BaseBusiness.GetResourceValue("dakai");
      this.m_trayOpen.Click += new EventHandler(this.MenuItem2_Click);
      this.m_trayLangben.Text = BaseBusiness.GetResourceValue("guanfangwangzhan");
      this.m_trayLangben.Click += new EventHandler(this.tLabPath_Click);
      this.m_notifyIcon1 = new NotifyIcon(this.components);
      this.m_notifyIcon1.Icon = this.Icon;
      this.m_notifyIcon1.ContextMenu = this.m_trayMenu;
      this.m_notifyIcon1.Text = BaseBusiness.GetResourceValue("Title");
      this.m_notifyIcon1.Visible = true;
      this.m_notifyIcon1.DoubleClick += new EventHandler(this.NotifyIcon1_DoubleClick);
    }

    private void NotifyIcon1_DoubleClick(object Sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.ShowInTaskbar = true;
        this.WindowState = FormWindowState.Normal;
        this.Opacity = 100.0;
      }
      else
      {
        this.ShowInTaskbar = false;
        this.Opacity = 0.0;
        this.WindowState = FormWindowState.Minimized;
      }
      this.Activate();
    }

    private void MenuItem1_Click(object Sender, EventArgs e)
    {
      this.WindowClose();
    }

    private void MenuItem2_Click(object Sender, EventArgs e)
    {
      this.ShowFromTray((Form) this);
    }

    private void tabControlMain_ClosePressed(object sender, EventArgs e)
    {
      if (this.tabControlMain.TabPages.Count <= 0)
        return;
      this.tabControlMain.SelectedTab.Control.Dispose();
      this.tabControlMain.TabPages.Remove(this.tabControlMain.SelectedTab);
      if (this.tabControlMain.TabPages.Count == 0)
        this.tabControlMain.Visible = false;
    }

    private void tLabPath_Click(object sender, EventArgs e)
    {
      new Process()
      {
        StartInfo = new ProcessStartInfo("IExplore.exe")
        {
          Arguments = "http://www.langben.com",
          WindowStyle = ProcessWindowStyle.Maximized
        }
      }.Start();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      this.ShowFrom((Form) new FrmLogin(this));
    }

    private bool FirstRun()
    {
      string str1 = string.Empty;
      this.folderBrowserDialog1shengcheng.Description = BaseBusiness.GetResourceValue("qingxuanzelujing");
      if (this.folderBrowserDialog1shengcheng.ShowDialog() != DialogResult.OK)
        return false;
      string path = Common.GetPath(this.folderBrowserDialog1shengcheng.SelectedPath);
      if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
      {
        int num = (int) this.MessageBoxShow(BaseBusiness.GetResourceValue("lujingbucunzai"), MessageBoxIcon.Hand, MessageBoxButtons.OK);
        return false;
      }
      string str2 = path + "Moban\\Sys.PDM";
      Common.Write(this.m_Pdm, str2 + (object) "," + (string) (object) DataType.PowerDesigner);
      Process.Start("Explorer", "/select," + str2);
      return true;
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      if (!this.FirstRun())
        return;
      this.tbtnRefresh_Click(sender, e);
      this.tbtnConnection.Enabled = true;
      this.toolStripButton1.Enabled = true;
      this.tbtnGenerate.Enabled = true;
      this.tbtnRefresh.Enabled = true;
      this.btnOutput.Enabled = true;
    }

    private void FrmStart_Paint(object sender, PaintEventArgs e)
    {
      RoundFormPainter.Paint(sender, e);
    }

    private void panel1_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.mouseOff = new Point(-e.X, -e.Y);
      this.leftFlag = true;
    }

    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.leftFlag)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseOff.X, this.mouseOff.Y);
      this.Location = mousePosition;
    }

    private void panel1_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.leftFlag)
        return;
      this.leftFlag = false;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
      RoundFormPainter.Paint(sender, e);
    }

    private void toolStrip1_Paint(object sender, PaintEventArgs e)
    {
      if ((sender as ToolStrip).RenderMode != ToolStripRenderMode.System)
        return;
      Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width - 2, this.toolStrip1.Height - 2);
      e.Graphics.SetClip(rect);
    }

    private void jiaoliuluntan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://rights.langben.com");
    }

    private void jishuboke_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://www.cnblogs.com/angben");
    }

    private void guanfangwangzhan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://www.langben.com");
    }

    private void min_MouseClick(object sender, MouseEventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void min_MouseDown(object sender, MouseEventArgs e)
    {
      this.min.Image = this.min.ErrorImage;
    }

    private void min_MouseLeave(object sender, EventArgs e)
    {
      this.min.Image = this.min.BackgroundImage;
    }

    private void min_MouseHover(object sender, EventArgs e)
    {
      this.min.Image = this.min.InitialImage;
    }

    private void close_MouseClick(object sender, MouseEventArgs e)
    {
      this.WindowClose();
    }

    private void close_MouseDown(object sender, MouseEventArgs e)
    {
      this.close.Image = this.close.ErrorImage;
    }

    private void close_MouseHover(object sender, EventArgs e)
    {
      this.close.Image = this.close.InitialImage;
    }

    private void close_MouseLeave(object sender, EventArgs e)
    {
      this.close.Image = this.close.BackgroundImage;
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
      if (BaseBusiness.IsNotDefault())
      {
        this.pictureBox2.Image = this.pictureBox2.ErrorImage;
        this.panel1.BackgroundImage = this.pictureBox1.InitialImage;
        BaseBusiness.Culture = "ZH-CN";
        this.tabControlMain.TabPages[0].Title = BaseBusiness.GetResourceValue("wodegongzuotai");
        this.tabControlMain.TabPages[1].Title = BaseBusiness.GetResourceValue("shiyongshuoming");
      }
      else
      {
        this.panel1.BackgroundImage = this.pictureBox1.ErrorImage;
        this.pictureBox2.Image = this.pictureBox2.InitialImage;
        BaseBusiness.Culture = "EN-US";
        this.tabControlMain.TabPages[0].Title = BaseBusiness.GetResourceValue("wodegongzuotai");
        this.tabControlMain.TabPages[1].Title = BaseBusiness.GetResourceValue("shiyongshuoming");
      }
      this.LoadUIText();
      this.LeftPanle();
      this.m_trayColse.Click -= new EventHandler(this.MenuItem1_Click);
      this.m_trayOpen.Click -= new EventHandler(this.MenuItem2_Click);
      this.m_notifyIcon1.DoubleClick -= new EventHandler(this.NotifyIcon1_DoubleClick);
      this.m_notifyIcon1.Visible = false;
      this.ThisTray();
    }

    private void LoadUIText()
    {
      this.Text = BaseBusiness.GetResourceValue("Title");
      this.tbtnConnection.Text = BaseBusiness.GetResourceValue("lianjieshujuyuan");
      this.toolStripButton2.Text = BaseBusiness.GetResourceValue("houqushujuyuan");
      this.toolStripButton1.Text = BaseBusiness.GetResourceValue("xitongdenglu");
      this.tbtnRefresh.Text = BaseBusiness.GetResourceValue("shuaxinxiangmu");
      this.btnOutput.Text = BaseBusiness.GetResourceValue("shengchengxiangmu");
      this.tbtnGenerate.Text = BaseBusiness.GetResourceValue("xitongshezhi");
      this.toolStripStatusLabel1.Text = BaseBusiness.GetResourceValue("ruyouyiwen");
      this.banben.Text = string.Format(BaseBusiness.GetResourceValue("nihao"), (object) "6.1");
      this.shujuyuancunfangweizhi.Text = BaseBusiness.GetResourceValue("shujuyuancunfanglujing");
      this.shengchengweizhi.Text = BaseBusiness.GetResourceValue("shengchengweizhi");
      this.mingmingkongjian.Text = BaseBusiness.GetResourceValue("mingmingkongjian");
      this.jiaoliuluntan.Text = BaseBusiness.GetResourceValue("jiaoliuluntan");
      this.jishuboke.Text = BaseBusiness.GetResourceValue("jishuboke");
      this.guanfangwangzhan.Text = BaseBusiness.GetResourceValue("guanfangwangzhan");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmStart));
      this.panel1 = new Panel();
      this.pictureBox2 = new PictureBox();
      this.close = new PictureBox();
      this.min = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.toolStrip1 = new ToolStrip();
      this.tbtnConnection = new ToolStripButton();
      this.toolStripButton2 = new ToolStripButton();
      this.toolStripLabel1 = new ToolStripLabel();
      this.toolStripButton1 = new ToolStripButton();
      this.tbtnGenerate = new ToolStripButton();
      this.toolStripLabel2 = new ToolStripLabel();
      this.tbtnRefresh = new ToolStripButton();
      this.btnOutput = new ToolStripButton();
      this.tabControlMain = new Crownwood.Magic.Controls.TabControl();
      this.panel2 = new Panel();
      this.banben = new Label();
      this.jishuboke = new LinkLabel();
      this.guanfangwangzhan = new LinkLabel();
      this.jiaoliuluntan = new LinkLabel();
      this.kongjian = new Label();
      this.weizhi = new Label();
      this.lujing = new Label();
      this.shengchengweizhi = new Label();
      this.mingmingkongjian = new Label();
      this.shujuyuancunfangweizhi = new Label();
      this.statusStrip1 = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.tLabPath = new ToolStripStatusLabel();
      this.panel3 = new Panel();
      this.panelTab = new Panel();
      this.folderBrowserDialog1shengcheng = new FolderBrowserDialog();
      this.leftViewImgsa = new ImageList(this.components);
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.close).BeginInit();
      ((ISupportInitialize) this.min).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.toolStrip1.SuspendLayout();
      this.tabControlMain.SuspendLayout();
      this.panel2.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      this.panel1.BackColor = Color.FromArgb(221, 221, 221);
      this.panel1.BackgroundImage = (Image) componentResourceManager.GetObject("panel1.BackgroundImage");
      this.panel1.BackgroundImageLayout = ImageLayout.None;
      this.panel1.Controls.Add((Control) this.pictureBox2);
      this.panel1.Controls.Add((Control) this.close);
      this.panel1.Controls.Add((Control) this.min);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Controls.Add((Control) this.toolStrip1);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Margin = new Padding(3, 4, 3, 4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(1024, 139);
      this.panel1.TabIndex = 3;
      this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
      this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
      this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
      this.panel1.MouseUp += new MouseEventHandler(this.panel1_MouseUp);
      this.pictureBox2.ErrorImage = (Image) componentResourceManager.GetObject("pictureBox2.ErrorImage");
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.InitialImage = (Image) componentResourceManager.GetObject("pictureBox2.InitialImage");
      this.pictureBox2.Location = new Point(916, 74);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(99, 22);
      this.pictureBox2.TabIndex = 14;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
      this.close.BackgroundImage = (Image) componentResourceManager.GetObject("close.BackgroundImage");
      this.close.ErrorImage = (Image) componentResourceManager.GetObject("close.ErrorImage");
      this.close.Image = (Image) componentResourceManager.GetObject("close.Image");
      this.close.InitialImage = (Image) componentResourceManager.GetObject("close.InitialImage");
      this.close.Location = new Point(973, 6);
      this.close.Name = "close";
      this.close.Size = new Size(42, 18);
      this.close.TabIndex = 13;
      this.close.TabStop = false;
      this.close.MouseClick += new MouseEventHandler(this.close_MouseClick);
      this.close.MouseDown += new MouseEventHandler(this.close_MouseDown);
      this.close.MouseLeave += new EventHandler(this.close_MouseLeave);
      this.close.MouseHover += new EventHandler(this.close_MouseHover);
      this.min.BackgroundImage = (Image) componentResourceManager.GetObject("min.BackgroundImage");
      this.min.ErrorImage = (Image) componentResourceManager.GetObject("min.ErrorImage");
      this.min.Image = (Image) componentResourceManager.GetObject("min.Image");
      this.min.InitialImage = (Image) componentResourceManager.GetObject("min.InitialImage");
      this.min.Location = new Point(948, 6);
      this.min.Name = "min";
      this.min.Size = new Size(26, 18);
      this.min.TabIndex = 11;
      this.min.TabStop = false;
      this.min.MouseClick += new MouseEventHandler(this.min_MouseClick);
      this.min.MouseDown += new MouseEventHandler(this.min_MouseDown);
      this.min.MouseLeave += new EventHandler(this.min_MouseLeave);
      this.min.MouseHover += new EventHandler(this.min_MouseHover);
      this.pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox1.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox1.BackgroundImage");
      this.pictureBox1.ErrorImage = (Image) componentResourceManager.GetObject("pictureBox1.ErrorImage");
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.InitialImage = (Image) componentResourceManager.GetObject("pictureBox1.InitialImage");
      this.pictureBox1.Location = new Point(0, 60);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(1911, 3);
      this.pictureBox1.TabIndex = 9;
      this.pictureBox1.TabStop = false;
      this.toolStrip1.AutoSize = false;
      this.toolStrip1.BackColor = Color.FromArgb(221, 221, 221);
      this.toolStrip1.BackgroundImageLayout = ImageLayout.None;
      this.toolStrip1.Dock = DockStyle.None;
      this.toolStrip1.Font = new Font("微软雅黑", 13f);
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.ImageScalingSize = new Size(118, 71);
      this.toolStrip1.Items.AddRange(new ToolStripItem[8]
      {
        (ToolStripItem) this.tbtnConnection,
        (ToolStripItem) this.toolStripButton2,
        (ToolStripItem) this.toolStripLabel1,
        (ToolStripItem) this.toolStripButton1,
        (ToolStripItem) this.tbtnGenerate,
        (ToolStripItem) this.toolStripLabel2,
        (ToolStripItem) this.tbtnRefresh,
        (ToolStripItem) this.btnOutput
      });
      this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new Point(4, 60);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new Padding(0);
      this.toolStrip1.RenderMode = ToolStripRenderMode.System;
      this.toolStrip1.Size = new Size(633, 80);
      this.toolStrip1.TabIndex = 7;
      this.toolStrip1.Paint += new PaintEventHandler(this.toolStrip1_Paint);
      this.tbtnConnection.AutoSize = false;
      this.tbtnConnection.Font = new Font("微软雅黑", 12f);
      this.tbtnConnection.Image = (Image) Resources.touming_03;
      this.tbtnConnection.ImageScaling = ToolStripItemImageScaling.None;
      this.tbtnConnection.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.tbtnConnection.Name = "tbtnConnection";
      this.tbtnConnection.Size = new Size(100, 67);
      this.tbtnConnection.Text = "连接数据源";
      this.tbtnConnection.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tbtnConnection.Click += new EventHandler(this.tbtnConnection_Click);
      this.toolStripButton2.AutoSize = false;
      this.toolStripButton2.Font = new Font("微软雅黑", 12f);
      this.toolStripButton2.Image = (Image) Resources.touming_06;
      this.toolStripButton2.ImageScaling = ToolStripItemImageScaling.None;
      this.toolStripButton2.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new Size(100, 67);
      this.toolStripButton2.Text = "获取数据源";
      this.toolStripButton2.TextImageRelation = TextImageRelation.ImageAboveText;
      this.toolStripButton2.ToolTipText = "第一次运行的时候，获取默认的数据源";
      this.toolStripButton2.Visible = false;
      this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
      this.toolStripLabel1.ActiveLinkColor = Color.FromArgb(221, 221, 221);
      this.toolStripLabel1.AutoSize = false;
      this.toolStripLabel1.BackgroundImage = (Image) componentResourceManager.GetObject("toolStripLabel1.BackgroundImage");
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new Size(2, 57);
      this.toolStripButton1.AutoSize = false;
      this.toolStripButton1.Font = new Font("微软雅黑", 12f);
      this.toolStripButton1.Image = (Image) Resources.touming_08;
      this.toolStripButton1.ImageScaling = ToolStripItemImageScaling.None;
      this.toolStripButton1.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new Size(100, 67);
      this.toolStripButton1.Text = "系统登录";
      this.toolStripButton1.TextImageRelation = TextImageRelation.ImageAboveText;
      this.toolStripButton1.Visible = false;
      this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
      this.tbtnGenerate.AutoSize = false;
      this.tbtnGenerate.Font = new Font("微软雅黑", 12f);
      this.tbtnGenerate.Image = (Image) Resources.touming_10;
      this.tbtnGenerate.ImageScaling = ToolStripItemImageScaling.None;
      this.tbtnGenerate.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.tbtnGenerate.Name = "tbtnGenerate";
      this.tbtnGenerate.Size = new Size(100, 67);
      this.tbtnGenerate.Text = "系统设置";
      this.tbtnGenerate.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tbtnGenerate.Click += new EventHandler(this.tbtnGenerate_Click);
      this.toolStripLabel2.ActiveLinkColor = Color.FromArgb(221, 221, 221);
      this.toolStripLabel2.AutoSize = false;
      this.toolStripLabel2.BackgroundImage = (Image) componentResourceManager.GetObject("toolStripLabel2.BackgroundImage");
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new Size(2, 57);
      this.tbtnRefresh.AutoSize = false;
      this.tbtnRefresh.Font = new Font("微软雅黑", 12f);
      this.tbtnRefresh.Image = (Image) Resources.touming_12;
      this.tbtnRefresh.ImageScaling = ToolStripItemImageScaling.None;
      this.tbtnRefresh.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.tbtnRefresh.Name = "tbtnRefresh";
      this.tbtnRefresh.Size = new Size(100, 67);
      this.tbtnRefresh.Text = "刷新项目";
      this.tbtnRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tbtnRefresh.Click += new EventHandler(this.tbtnRefresh_Click);
      this.btnOutput.AutoSize = false;
      this.btnOutput.BackgroundImageLayout = ImageLayout.None;
      this.btnOutput.Font = new Font("微软雅黑", 12f);
      this.btnOutput.Image = (Image) Resources.touming_15;
      this.btnOutput.ImageScaling = ToolStripItemImageScaling.None;
      this.btnOutput.ImageTransparentColor = Color.FromArgb(221, 221, 221);
      this.btnOutput.Name = "btnOutput";
      this.btnOutput.Size = new Size(100, 67);
      this.btnOutput.Text = "生成项目";
      this.btnOutput.TextImageRelation = TextImageRelation.ImageAboveText;
      this.btnOutput.Click += new EventHandler(this.btnOutput_Click);
      this.tabControlMain.BackColor = Color.FromArgb(188, 188, 188);
      this.tabControlMain.BoldSelectedPage = true;
      this.tabControlMain.ButtonActiveColor = Color.Black;
      this.tabControlMain.ButtonInactiveColor = Color.Black;
      this.tabControlMain.Controls.Add((Control) this.panel2);
      this.tabControlMain.Dock = DockStyle.Fill;
      this.tabControlMain.Font = new Font("微软雅黑", 11f);
      this.tabControlMain.ForeColor = Color.Black;
      this.tabControlMain.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
      this.tabControlMain.HotTextColor = Color.Transparent;
      this.tabControlMain.HoverSelect = true;
      this.tabControlMain.Location = new Point(0, 0);
      this.tabControlMain.Name = "tabControlMain";
      this.tabControlMain.PositionTop = true;
      this.tabControlMain.Size = new Size(1024, 540);
      this.tabControlMain.TabIndex = 13;
      this.tabControlMain.TextColor = Color.Black;
      this.tabControlMain.TextInactiveColor = Color.Black;
      this.panel2.BackgroundImage = (Image) componentResourceManager.GetObject("panel2.BackgroundImage");
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
      this.panel2.Location = new Point(150, 41);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(998, 541);
      this.panel2.TabIndex = 10;
      this.banben.AutoSize = true;
      this.banben.BackColor = Color.Transparent;
      this.banben.Font = new Font("微软雅黑", 12f);
      this.banben.ForeColor = Color.White;
      this.banben.Location = new Point(103, 67);
      this.banben.Name = "banben";
      this.banben.Size = new Size(171, 21);
      this.banben.TabIndex = 10;
      this.banben.Text = "您好，当前版本是V6.1";
      this.jishuboke.AutoSize = true;
      this.jishuboke.BackColor = Color.Transparent;
      this.jishuboke.Font = new Font("微软雅黑", 12f);
      this.jishuboke.ForeColor = Color.Black;
      this.jishuboke.LinkBehavior = LinkBehavior.NeverUnderline;
      this.jishuboke.LinkColor = Color.Black;
      this.jishuboke.Location = new Point(494, 122);
      this.jishuboke.Name = "jishuboke";
      this.jishuboke.Size = new Size(74, 21);
      this.jishuboke.TabIndex = 8;
      this.jishuboke.TabStop = true;
      this.jishuboke.Text = "技术博客";
      this.jishuboke.LinkClicked += new LinkLabelLinkClickedEventHandler(this.jishuboke_LinkClicked);
      this.guanfangwangzhan.AutoSize = true;
      this.guanfangwangzhan.BackColor = Color.Transparent;
      this.guanfangwangzhan.Font = new Font("微软雅黑", 12f);
      this.guanfangwangzhan.ForeColor = Color.Black;
      this.guanfangwangzhan.LinkBehavior = LinkBehavior.NeverUnderline;
      this.guanfangwangzhan.LinkColor = Color.Black;
      this.guanfangwangzhan.Location = new Point(494, 188);
      this.guanfangwangzhan.Name = "guanfangwangzhan";
      this.guanfangwangzhan.Size = new Size(74, 21);
      this.guanfangwangzhan.TabIndex = 7;
      this.guanfangwangzhan.TabStop = true;
      this.guanfangwangzhan.Text = "官方网站";
      this.guanfangwangzhan.LinkClicked += new LinkLabelLinkClickedEventHandler(this.guanfangwangzhan_LinkClicked);
      this.jiaoliuluntan.AutoSize = true;
      this.jiaoliuluntan.BackColor = Color.Transparent;
      this.jiaoliuluntan.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.jiaoliuluntan.ForeColor = Color.Black;
      this.jiaoliuluntan.LinkBehavior = LinkBehavior.NeverUnderline;
      this.jiaoliuluntan.LinkColor = Color.Black;
      this.jiaoliuluntan.Location = new Point(494, 153);
      this.jiaoliuluntan.Name = "jiaoliuluntan";
      this.jiaoliuluntan.Size = new Size(106, 21);
      this.jiaoliuluntan.TabIndex = 6;
      this.jiaoliuluntan.TabStop = true;
      this.jiaoliuluntan.Text = "权限管理系统";
      this.jiaoliuluntan.LinkClicked += new LinkLabelLinkClickedEventHandler(this.jiaoliuluntan_LinkClicked);
      this.kongjian.AutoSize = true;
      this.kongjian.BackColor = Color.Transparent;
      this.kongjian.Font = new Font("微软雅黑", 12f);
      this.kongjian.ForeColor = Color.FromArgb(96, 96, 96);
      this.kongjian.Location = new Point(144, 247);
      this.kongjian.Name = "kongjian";
      this.kongjian.Size = new Size(0, 21);
      this.kongjian.TabIndex = 5;
      this.weizhi.AutoSize = true;
      this.weizhi.BackColor = Color.Transparent;
      this.weizhi.Font = new Font("微软雅黑", 12f);
      this.weizhi.ForeColor = Color.FromArgb(96, 96, 96);
      this.weizhi.Location = new Point(144, 198);
      this.weizhi.Name = "weizhi";
      this.weizhi.Size = new Size(0, 21);
      this.weizhi.TabIndex = 4;
      this.lujing.AutoSize = true;
      this.lujing.BackColor = Color.Transparent;
      this.lujing.Font = new Font("微软雅黑", 12f);
      this.lujing.ForeColor = Color.FromArgb(96, 96, 96);
      this.lujing.Location = new Point(144, 145);
      this.lujing.Name = "lujing";
      this.lujing.Size = new Size(0, 21);
      this.lujing.TabIndex = 3;
      this.shengchengweizhi.AutoSize = true;
      this.shengchengweizhi.BackColor = Color.Transparent;
      this.shengchengweizhi.Font = new Font("微软雅黑", 12f);
      this.shengchengweizhi.Location = new Point(142, 172);
      this.shengchengweizhi.Name = "shengchengweizhi";
      this.shengchengweizhi.Size = new Size(74, 21);
      this.shengchengweizhi.TabIndex = 2;
      this.shengchengweizhi.Text = "生成位置";
      this.mingmingkongjian.AutoSize = true;
      this.mingmingkongjian.BackColor = Color.Transparent;
      this.mingmingkongjian.Font = new Font("微软雅黑", 12f);
      this.mingmingkongjian.Location = new Point(144, 223);
      this.mingmingkongjian.Name = "mingmingkongjian";
      this.mingmingkongjian.Size = new Size(74, 21);
      this.mingmingkongjian.TabIndex = 1;
      this.mingmingkongjian.Text = "命名空间";
      this.shujuyuancunfangweizhi.AutoSize = true;
      this.shujuyuancunfangweizhi.BackColor = Color.Transparent;
      this.shujuyuancunfangweizhi.Font = new Font("微软雅黑", 12f);
      this.shujuyuancunfangweizhi.Location = new Point(144, 119);
      this.shujuyuancunfangweizhi.Name = "shujuyuancunfangweizhi";
      this.shujuyuancunfangweizhi.Size = new Size(122, 21);
      this.shujuyuancunfangweizhi.TabIndex = 0;
      this.shujuyuancunfangweizhi.Text = "数据源存放路径";
      this.statusStrip1.BackColor = Color.FromArgb(221, 221, 221);
      this.statusStrip1.Font = new Font("微软雅黑", 12f);
      this.statusStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripStatusLabel1
      });
      this.statusStrip1.Location = new Point(0, 679);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(1024, 26);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      this.toolStripStatusLabel1.ForeColor = Color.DimGray;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(320, 21);
      this.toolStripStatusLabel1.Text = "如有疑问，请发邮件到 ben@langben.com";
      this.tLabPath.ActiveLinkColor = Color.Transparent;
      this.tLabPath.BackColor = Color.Transparent;
      this.tLabPath.Font = new Font("微软雅黑", 12f);
      this.tLabPath.ForeColor = Color.FromArgb(96, 96, 96);
      this.tLabPath.IsLink = true;
      this.tLabPath.LinkBehavior = LinkBehavior.NeverUnderline;
      this.tLabPath.LinkColor = Color.FromArgb(96, 96, 96);
      this.tLabPath.Name = "tLabPath";
      this.tLabPath.Size = new Size(320, 21);
      this.tLabPath.Text = "如有疑问，请发邮件到 ben@langben.com";
      this.tLabPath.Click += new EventHandler(this.tLabPath_Click);
      this.panel3.BackColor = Color.FromArgb(188, 188, 188);
      this.panel3.Controls.Add((Control) this.tabControlMain);
      this.panel3.Dock = DockStyle.Fill;
      this.panel3.Location = new Point(0, 139);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(1024, 540);
      this.panel3.TabIndex = 11;
      this.panelTab.BackColor = Color.FromArgb(221, 221, 221);
      this.panelTab.Dock = DockStyle.Left;
      this.panelTab.Location = new Point(0, 139);
      this.panelTab.Name = "panelTab";
      this.panelTab.Size = new Size(0, 540);
      this.panelTab.TabIndex = 15;
      this.leftViewImgsa.ColorDepth = ColorDepth.Depth8Bit;
      this.leftViewImgsa.ImageSize = new Size(16, 16);
      this.leftViewImgsa.TransparentColor = Color.Transparent;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(221, 221, 221);
      this.ClientSize = new Size(1024, 705);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panelTab);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.panel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.Name = "FrmStart";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "狼奔代码生成器";
      this.FormClosing += new FormClosingEventHandler(this.FrmStart_FormClosing);
      this.Load += new EventHandler(this.FrmStart_Load);
      this.Paint += new PaintEventHandler(this.FrmStart_Paint);
      this.panel1.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.close).EndInit();
      ((ISupportInitialize) this.min).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.tabControlMain.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void AddNewTabPageCallback(Control control, string Title);
  }
}
