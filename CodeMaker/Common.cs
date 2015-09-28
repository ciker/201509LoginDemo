// Decompiled with JetBrains decompiler
// Type: CodeMaker.Common
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;

namespace CodeMaker
{
  public class Common
  {
    public static string[] dateType = new string[5]
    {
      "DATE",
      "DATETIME",
      "DATETIME2",
      "SMALLDATETIME",
      "DATETIMEOFFSET"
    };
    public static string[] shortType = new string[1]
    {
      "SMALLINT"
    };
    public static string[] longType = new string[1]
    {
      "BIGINT"
    };
    public static string[] intType = new string[6]
    {
      "INT",
      "BIGINT",
      "INTEGER",
      "NUMBER",
      "TINYINT",
      "SMALLINT"
    };
    public static string[] stringType = new string[6]
    {
      "VARCHAR",
      "NVARCHAR",
      "CHAR",
      "NCHAR",
      "NTEXT",
      "TEXT"
    };
    public static string[] decimalType = new string[3]
    {
      "REAL",
      "FLOAT",
      "DECIMAL"
    };
    public static string[] moneyType = new string[2]
    {
      "MONEY",
      "SMALLMONEY"
    };
    public static string[] stampType = new string[1]
    {
      "TIMESTAMP"
    };
    public static string[] uniqueidentifierType = new string[1]
    {
      "UNIQUEIDENTIFIER"
    };
    public static string[] bitType = new string[1]
    {
      "BIT"
    };
    public static string[] time = new string[2]
    {
      "CREATETIME",
      "UPDATETIME"
    };
    public static string[] person = new string[2]
    {
      "CREATEPERSON",
      "UPDATEPERSON"
    };
    public static string m_Line = string.Empty;

    public static string GetNullPrimaryKeyType(string primaryKeyType)
    {
      string str = string.Empty;
      return !(primaryKeyType == "int") ? (!(primaryKeyType == "Guid") ? (!(primaryKeyType == "long") ? (!(primaryKeyType == "short") ? "string" : primaryKeyType + "?") : primaryKeyType + "?") : primaryKeyType + "?") : primaryKeyType + "?";
    }

    public static string GetMacAddresses()
    {
      string str = "sb123";
      try
      {
        foreach (ManagementObject managementObject in new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances())
        {
          if ((bool) managementObject["IPEnabled"])
          {
            str = managementObject["MacAddress"].ToString();
            if (!string.IsNullOrWhiteSpace(str))
              return str;
          }
        }
      }
      catch
      {
        return Common.GetCUPID();
      }
      if (string.IsNullOrWhiteSpace(str))
        str = Common.GetCUPID();
      return str;
    }

    public static string GetCUPID()
    {
      string str = string.Empty;
      try
      {
        foreach (ManagementBaseObject managementBaseObject in new ManagementClass("Win32_Processor").GetInstances())
        {
          str = managementBaseObject.Properties["ProcessorId"].Value.ToString();
          if (!string.IsNullOrWhiteSpace(str))
            return str;
        }
        return str;
      }
      catch
      {
        return str;
      }
    }

    public static string GetID()
    {
      return Common.GetMacAddresses();
    }

    public static List<Table> ConvertT(List<TableData> dataSourse)
    {
      List<Table> list = new List<Table>();
      if (dataSourse != null)
      {
        foreach (TableData tableData in dataSourse)
        {
          Table table = new Table();
          table.Code = tableData.Code;
          table.Columns = tableData.Columns;
          table.Id = tableData.Id;
          table.Name = tableData.Name;
          table.PrimaryKey = tableData.PrimaryKey;
          list.Add(table);
        }
      }
      return list;
    }

    public static List<View> ConvertT(List<ViewData> dataSourse)
    {
      List<View> list = new List<View>();
      if (dataSourse != null)
      {
        foreach (ViewData viewData in dataSourse)
        {
          View view = new View();
          view.Code = viewData.Code;
          view.Columns = viewData.Columns;
          view.Id = viewData.Id;
          view.Name = viewData.Name;
          view.SQLQuery = viewData.SQLQuery;
          list.Add(view);
        }
      }
      return list;
    }

    public static string PrimaryKeyType(Table table)
    {
      if (table != null && table.PrimaryKey != null)
      {
        IEnumerable<Column> source1 = Enumerable.Where<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => Enumerable.Contains<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef, f.Id))), (Func<Column, bool>) (f => Common.IsLongType(f.DataType)));
        if (source1 != null && Enumerable.Count<Column>(source1) > 0)
          return "long";
        IEnumerable<Column> source2 = Enumerable.Where<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => Enumerable.Contains<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef, f.Id))), (Func<Column, bool>) (f => Common.IsShotType(f.DataType)));
        if (source2 != null && Enumerable.Count<Column>(source2) > 0)
          return "short";
        IEnumerable<Column> source3 = Enumerable.Where<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => Enumerable.Contains<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef, f.Id))), (Func<Column, bool>) (f => Common.IsIntType(f.DataType)));
        if (source3 != null && Enumerable.Count<Column>(source3) > 0)
          return "int";
        IEnumerable<Column> source4 = Enumerable.Where<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => Enumerable.Contains<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef, f.Id))), (Func<Column, bool>) (f => Common.IsUniqueidentifierType(f.DataType)));
        if (source4 != null && Enumerable.Count<Column>(source4) > 0)
          return "Guid";
      }
      return "string";
    }

    public static bool IsUniqueidentifierType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.uniqueidentifierType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsShotType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.longType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsLongType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.longType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsIntType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.intType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsStringType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.stringType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsBitType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.bitType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsDecimalType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.decimalType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsStampType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.stampType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsDateType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.dateType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsMoneyType(string dataType)
    {
      dataType = Common.GetDataType(dataType).ToUpper();
      return Enumerable.Any<string>((IEnumerable<string>) Common.moneyType, (Func<string, bool>) (a => dataType == a));
    }

    public static bool IsSort(string comment)
    {
      return !string.IsNullOrWhiteSpace(comment) && comment.ToUpper().Contains("SORT");
    }

    public static bool IsWorkFlow(string comment)
    {
      return !string.IsNullOrWhiteSpace(comment) && comment.ToUpper().Contains("WORKFLOW");
    }

    public static bool IsNotDisplay(string comment)
    {
      return !string.IsNullOrWhiteSpace(comment) && comment.ToUpper().Contains("NOTDISPLAY");
    }

    public static bool IsRadioButton(string comment)
    {
      return !string.IsNullOrWhiteSpace(comment) && comment.ToUpper().Contains("RADIOBUTTON");
    }

    public static bool IsCreatePerson(Column column)
    {
      string dataType = Common.GetDataType(column.DataType).ToUpper();
      return column.Code.ToUpper() == "CREATEPERSON" && Common.IsStringType(dataType);
    }

    public static bool IsUpdatePerson(Column column)
    {
      string dataType = Common.GetDataType(column.DataType).ToUpper();
      return column.Code.ToUpper() == "UPDATEPERSON" && Common.IsStringType(dataType);
    }

    public static bool IsCreateTime(Column column)
    {
      string dataType = Common.GetDataType(column.DataType).ToUpper();
      return column.Code.ToUpper() == "CREATETIME" && Common.IsDateType(dataType);
    }

    public static bool IsUpdateTime(Column column)
    {
      string dataType = Common.GetDataType(column.DataType).ToUpper();
      return column.Code.ToUpper() == "UPDATETIME" && Common.IsDateType(dataType);
    }

    public static string GetDataType(string dataType)
    {
      if (string.IsNullOrWhiteSpace(dataType))
        return string.Empty;
      if (dataType.Contains("(") && dataType.Contains(")"))
        return dataType.Substring(0, dataType.IndexOf("("));
      return dataType;
    }

    public static bool IsPrimaryKey(Table table, string id)
    {
      return !string.IsNullOrWhiteSpace(id) && table.PrimaryKey != null && table.PrimaryKey.ColumnRef != null && Enumerable.Contains<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef, id);
    }

    public static string GetShowColumnCode(TableView table)
    {
      if (table != null && Enumerable.Count<Column>((IEnumerable<Column>) table.Columns) > 1)
        return table.Columns[1].Code;
      return (string) null;
    }

    public static RefIdName GetForeignKey(Table table, Column column)
    {
      if (table.refId != null && Enumerable.Count<RefIdName>((IEnumerable<RefIdName>) table.refId) > 0)
        return Enumerable.FirstOrDefault<RefIdName>(Enumerable.Where<RefIdName>((IEnumerable<RefIdName>) table.refId, (Func<RefIdName, bool>) (f => column.Code == f.Ref)));
      return (RefIdName) null;
    }

    public static Column GetColumnByKey(Table table, string id)
    {
      if (table != null && table.Columns != null && Enumerable.Count<Column>((IEnumerable<Column>) table.Columns) > 0)
        return Enumerable.SingleOrDefault<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => id == f.Id)));
      return (Column) null;
    }

    public static bool ExitsPrimaryKey(Table table)
    {
      return table != null && table.PrimaryKey != null && table.PrimaryKey.ColumnRef != null;
    }

    public static Column GetFirstPrimaryKey(Table table)
    {
      if (table != null && table.PrimaryKey != null && table.PrimaryKey.ColumnRef != null)
        return Enumerable.FirstOrDefault<Column>(Enumerable.Where<Column>((IEnumerable<Column>) table.Columns, (Func<Column, bool>) (f => f.Id == Enumerable.FirstOrDefault<string>((IEnumerable<string>) table.PrimaryKey.ColumnRef))));
      return (Column) null;
    }

    public static string GetFirstPrimaryKeyCode(Table table)
    {
      if (Common.GetFirstPrimaryKey(table) != null)
        return Common.GetFirstPrimaryKey(table).Code;
      return string.Empty;
    }

    public static string GetFirstPrimaryKeyName(Table table)
    {
      if (Common.GetFirstPrimaryKey(table) != null)
        return Common.GetFirstPrimaryKey(table).Name;
      return string.Empty;
    }

    public static string GetPath(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return string.Empty;
      if (path.Substring(path.Length - 1) != "/" && path.Substring(path.Length - 1) != "\\")
        path += "\\";
      return path;
    }

    public static string GetNameSpace(string m_SystemConfig)
    {
      string str = Common.Read(m_SystemConfig);
      if (!string.IsNullOrWhiteSpace(str))
      {
        string[] strArray = str.Split(',');
        if (strArray.Length >= 2 && !string.IsNullOrWhiteSpace(strArray[2]))
          return strArray[2].Trim() + ".";
      }
      return string.Empty;
    }

    public static bool GetJiChengQuanXian(string m_SystemConfig)
    {
      string str = Common.Read(m_SystemConfig);
      if (!string.IsNullOrWhiteSpace(str))
      {
        string[] strArray = str.Split(',');
        if (strArray.Length >= 2 && !string.IsNullOrWhiteSpace(strArray[2]) && strArray[0].Trim() == "n")
          return false;
      }
      return true;
    }

    public static List<string> GetFileControllers(string directory, string value)
    {
      List<string> list = new List<string>();
      if (Directory.Exists(directory))
      {
        foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles())
          list.Add(fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf("\\" + value) + 1));
      }
      return list;
    }

    public static List<string> GetFileViews(string directory, string value)
    {
      List<string> list = new List<string>();
      if (Directory.Exists(directory))
      {
        foreach (DirectoryInfo directoryInfo in new DirectoryInfo(directory).GetDirectories())
        {
          foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            list.Add(fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf("\\" + value) + 1));
        }
      }
      return list;
    }

    public static void Zip(string unzipPath, Stream sr)
    {
      if (string.IsNullOrWhiteSpace(unzipPath))
        return;
      if (!Directory.Exists(unzipPath))
        Directory.CreateDirectory(unzipPath);
      using (ZipInputStream zipInputStream = new ZipInputStream(sr))
      {
        string str = string.Empty;
        ZipEntry nextEntry;
        while ((nextEntry = zipInputStream.GetNextEntry()) != null)
        {
          string fileName = Path.GetFileName(nextEntry.Name);
          string path = unzipPath + nextEntry.Name;
          if (fileName != string.Empty)
          {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            byte[] buffer = new byte[2048];
            while (true)
            {
              int count = zipInputStream.Read(buffer, 0, buffer.Length);
              if (count > 0)
                fileStream.Write(buffer, 0, count);
              else
                break;
            }
            fileStream.Close();
          }
          else if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        }
      }
    }

    public static string Read(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return string.Empty;
      using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
      {
        StringBuilder stringBuilder = new StringBuilder();
        while ((Common.m_Line = streamReader.ReadLine()) != null)
          stringBuilder.Append(Common.m_Line).Append("\r\n");
        streamReader.Close();
        return stringBuilder.ToString();
      }
    }

    public static void Write(string path, string content)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8))
      {
        streamWriter.WriteLine(content);
        streamWriter.Close();
      }
    }
  }
}
