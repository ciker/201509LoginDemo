// Decompiled with JetBrains decompiler
// Type: CodeMaker.FrmTree
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using CodeMaker.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CodeMaker
{
  public class FrmTree : BaseMain
  {
    private IContainer components = (IContainer) null;
    public FrmStart m_FrmStart;
    private ImageList treeImgs;
    public TreeView treeView1;
    private ContextMenuStrip DbTreeContextMenu;
    private ToolStripMenuItem ToolStripMenuItemCreate;

    public FrmTree(Form mdiParentForm)
    {
      this.m_FrmStart = (FrmStart) mdiParentForm;
      this.InitializeComponent();
      Control.CheckForIllegalCrossThreadCalls = false;
    }

    private void FrmTree_Load(object sender, EventArgs e)
    {
      this.LoadServer();
    }

    private void LoadServer()
    {
      this.treeView1.Nodes.Clear();
      TreeNode node1 = new TreeNode(BaseBusiness.GetResourceValue("shujuku"));
      node1.Tag = (object) "serverlist";
      node1.ImageIndex = 2;
      node1.SelectedImageIndex = 2;
      node1.Checked = true;
      this.treeView1.Nodes.Add(node1);
      TreeNode node2 = new TreeNode(BaseBusiness.GetResourceValue("biao"));
      node2.Tag = (object) "serverlist";
      node2.ImageIndex = 3;
      node2.SelectedImageIndex = 4;
      node2.Checked = true;
      node1.Nodes.Add(node2);
      TreeNode node3 = new TreeNode(BaseBusiness.GetResourceValue("shitu"));
      node3.Tag = (object) "serverlist";
      node3.ImageIndex = 3;
      node3.SelectedImageIndex = 4;
      node3.Checked = true;
      node1.Nodes.Add(node3);
      node1.ExpandAll();
      try
      {
        IData dataSource = new DataFactory().CreateDataSource(this.m_FrmStart.MyDataType);
        if (dataSource == null)
          return;
        DataSourse data = dataSource.GetData(this.m_FrmStart.GetPdmConn());
        if (!string.IsNullOrWhiteSpace(data.m_ErrorMessage))
        {
          int num = (int) this.m_FrmStart.MessageBoxShow(data.m_ErrorMessage, MessageBoxIcon.Asterisk, MessageBoxButtons.OK);
        }
        else
        {
          List<Table> list1 = Common.ConvertT(data.ListTable);
          List<View> list2 = Common.ConvertT(data.ListView);
          if (list1 != null)
          {
            foreach (Table table1 in list1)
            {
              Table table = table1;
              if (!Enumerable.Any<string>((IEnumerable<string>) this.WorkflowTableAndSys, (Func<string, bool>) (a => a.ToLower() == table.Code.ToLower())))
                node2.Nodes.Add(new TreeNode(table.Name)
                {
                  ImageIndex = 5,
                  SelectedImageIndex = 5,
                  Tag = (object) table.Id,
                  Checked = true
                });
            }
          }
          if (list2 != null)
          {
            foreach (View view1 in list2)
            {
              View view = view1;
              if (!Enumerable.Any<string>((IEnumerable<string>) this.WorkflowView, (Func<string, bool>) (a => a.ToLower() == view.Code.ToLower())))
                node3.Nodes.Add(new TreeNode(view.Name)
                {
                  ImageIndex = 6,
                  SelectedImageIndex = 6,
                  Tag = (object) view.Id,
                  Checked = true
                });
            }
          }
        }
      }
      catch (Exception ex)
      {
        try
        {
          using (VersionClient versionClient = new VersionClient())
          {
            versionClient.WriteExceptions(new SysException()
            {
              ComputerInfo = Common.GetID(),
              Message = ex.Message,
              StackTrace = ex.StackTrace
            });
            versionClient.Close();
            int num = (int) this.m_FrmStart.MessageBoxShow(ex.Message, MessageBoxIcon.Hand, MessageBoxButtons.OK);
          }
        }
        catch
        {
        }
      }
    }

    private void treeView1_AfterCheck_1(object sender, TreeViewEventArgs e)
    {
      if (e.Node.Checked)
      {
        foreach (TreeNode treeNode in e.Node.Nodes)
          treeNode.Checked = true;
      }
      else
      {
        foreach (TreeNode treeNode in e.Node.Nodes)
          treeNode.Checked = false;
      }
    }

    private void ToolStripMenuItemCreate_Click(object sender, EventArgs e)
    {
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmTree));
      TreeNode treeNode1 = new TreeNode("ID", 7, 7);
      TreeNode treeNode2 = new TreeNode("table1", 5, 5, new TreeNode[1]
      {
        treeNode1
      });
      TreeNode treeNode3 = new TreeNode("表", 3, 4, new TreeNode[1]
      {
        treeNode2
      });
      TreeNode treeNode4 = new TreeNode("节点4", 7, 7);
      TreeNode treeNode5 = new TreeNode("view1", 6, 6, new TreeNode[1]
      {
        treeNode4
      });
      TreeNode treeNode6 = new TreeNode("视图", 3, 4, new TreeNode[1]
      {
        treeNode5
      });
      TreeNode treeNode7 = new TreeNode("数据库", 2, 2, new TreeNode[2]
      {
        treeNode3,
        treeNode6
      });
      TreeNode treeNode8 = new TreeNode("服务器", 1, 1, new TreeNode[1]
      {
        treeNode7
      });
      this.treeImgs = new ImageList(this.components);
      this.DbTreeContextMenu = new ContextMenuStrip(this.components);
      this.ToolStripMenuItemCreate = new ToolStripMenuItem();
      this.treeView1 = new TreeView();
      this.DbTreeContextMenu.SuspendLayout();
      this.SuspendLayout();
      this.treeImgs.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("treeImgs.ImageStream");
      this.treeImgs.TransparentColor = Color.Transparent;
      this.treeImgs.Images.SetKeyName(0, "serverlist.gif");
      this.treeImgs.Images.SetKeyName(1, "server.ico");
      this.treeImgs.Images.SetKeyName(2, "db.gif");
      this.treeImgs.Images.SetKeyName(3, "logo2.png");
      this.treeImgs.Images.SetKeyName(4, "logo2.png");
      this.treeImgs.Images.SetKeyName(5, "tab2.gif");
      this.treeImgs.Images.SetKeyName(6, "view.gif");
      this.treeImgs.Images.SetKeyName(7, "fild2.gif");
      this.treeImgs.Images.SetKeyName(8, "sp.gif");
      this.treeImgs.Images.SetKeyName(9, "sp_p.gif");
      this.DbTreeContextMenu.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.ToolStripMenuItemCreate
      });
      this.DbTreeContextMenu.Name = "DbTreeContextMenu";
      this.DbTreeContextMenu.Size = new Size(95, 26);
      this.ToolStripMenuItemCreate.Name = "ToolStripMenuItemCreate";
      this.ToolStripMenuItemCreate.Size = new Size(94, 22);
      this.ToolStripMenuItemCreate.Text = "生成";
      this.ToolStripMenuItemCreate.Click += new EventHandler(this.ToolStripMenuItemCreate_Click);
      this.treeView1.BackColor = Color.FromArgb(221, 221, 221);
      this.treeView1.BorderStyle = BorderStyle.FixedSingle;
      this.treeView1.CheckBoxes = true;
      this.treeView1.Dock = DockStyle.Fill;
      this.treeView1.Font = new Font("微软雅黑", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.treeView1.ImageIndex = 0;
      this.treeView1.ImageList = this.treeImgs;
      this.treeView1.Location = new Point(0, 0);
      this.treeView1.Margin = new Padding(4);
      this.treeView1.Name = "treeView1";
      treeNode1.ImageIndex = 7;
      treeNode1.Name = "ID";
      treeNode1.SelectedImageIndex = 7;
      treeNode1.Text = "ID";
      treeNode2.ImageIndex = 5;
      treeNode2.Name = "table1";
      treeNode2.SelectedImageIndex = 5;
      treeNode2.Text = "table1";
      treeNode3.ImageIndex = 3;
      treeNode3.Name = "Table";
      treeNode3.SelectedImageIndex = 4;
      treeNode3.Text = "表";
      treeNode4.ImageIndex = 7;
      treeNode4.Name = "节点4";
      treeNode4.SelectedImageIndex = 7;
      treeNode4.Text = "节点4";
      treeNode5.ImageIndex = 6;
      treeNode5.Name = "view1";
      treeNode5.SelectedImageIndex = 6;
      treeNode5.Text = "view1";
      treeNode6.ImageIndex = 3;
      treeNode6.Name = "View";
      treeNode6.SelectedImageIndex = 4;
      treeNode6.Text = "视图";
      treeNode7.ImageIndex = 2;
      treeNode7.Name = "master";
      treeNode7.SelectedImageIndex = 2;
      treeNode7.Text = "数据库";
      treeNode8.ImageIndex = 1;
      treeNode8.Name = "";
      treeNode8.SelectedImageIndex = 1;
      treeNode8.Text = "服务器";
      this.treeView1.Nodes.AddRange(new TreeNode[1]
      {
        treeNode8
      });
      this.treeView1.SelectedImageIndex = 0;
      this.treeView1.ShowRootLines = false;
      this.treeView1.Size = new Size(249, 633);
      this.treeView1.TabIndex = 4;
      this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck_1);
      this.AutoScaleDimensions = new SizeF(9f, 17f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(249, 633);
      this.Controls.Add((Control) this.treeView1);
      this.Font = new Font("宋体", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.Margin = new Padding(4);
      this.Name = "FrmTree";
      this.Text = "FrmTree";
      this.Load += new EventHandler(this.FrmTree_Load);
      this.DbTreeContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
