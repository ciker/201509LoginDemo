// Decompiled with JetBrains decompiler
// Type: CodeMaker.RoundFormPainter
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CodeMaker
{
  public static class RoundFormPainter
  {
    public static void Paint(this object sender, PaintEventArgs e)
    {
      Control control = (Control) sender;
      List<Point> list = new List<Point>();
      int width = control.Width;
      int height = control.Height;
      list.Add(new Point(0, 5));
      list.Add(new Point(1, 5));
      list.Add(new Point(1, 3));
      list.Add(new Point(2, 3));
      list.Add(new Point(2, 2));
      list.Add(new Point(3, 2));
      list.Add(new Point(3, 1));
      list.Add(new Point(5, 1));
      list.Add(new Point(5, 0));
      list.Add(new Point(width - 5, 0));
      list.Add(new Point(width - 5, 1));
      list.Add(new Point(width - 3, 1));
      list.Add(new Point(width - 3, 2));
      list.Add(new Point(width - 2, 2));
      list.Add(new Point(width - 2, 3));
      list.Add(new Point(width - 1, 3));
      list.Add(new Point(width - 1, 5));
      list.Add(new Point(width, 5));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(width, height));
      list.Add(new Point(5, height));
      list.Add(new Point(5, height));
      list.Add(new Point(3, height));
      list.Add(new Point(3, height));
      list.Add(new Point(2, height));
      list.Add(new Point(2, height));
      list.Add(new Point(1, height));
      list.Add(new Point(1, height));
      list.Add(new Point(0, height));
      Point[] points = list.ToArray();
      GraphicsPath path = new GraphicsPath();
      path.AddPolygon(points);
      control.Region = new Region(path);
    }
  }
}
