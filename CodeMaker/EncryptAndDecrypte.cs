// Decompiled with JetBrains decompiler
// Type: CodeMaker.EncryptAndDecrypte
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CodeMaker
{
  public class EncryptAndDecrypte
  {
    private static string strKey = "fdbc4y6hdhKlf4M3mjgGrMC3PbryXrxw";
    private static string strIV = "RfnMfrpec48=";

    public static byte[] EncryptString(string ToEncryptString, byte[] byKey, byte[] byIV)
    {
      if (string.IsNullOrWhiteSpace(ToEncryptString))
        return (byte[]) null;
      MemoryStream memoryStream = new MemoryStream();
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
      byte[] bytes = Encoding.Default.GetBytes(ToEncryptString);
      cryptoStream.Write(bytes, 0, bytes.Length);
      cryptoStream.FlushFinalBlock();
      cryptoStream.Close();
      return memoryStream.ToArray();
    }

    public static string DecrypteString(byte[] byIn, byte[] byKey, byte[] byIV)
    {
      if (byIn == null || byIn.Length == 0)
        return string.Empty;
      CryptoStream cryptoStream = new CryptoStream((Stream) new MemoryStream(byIn), new TripleDESCryptoServiceProvider().CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
      byte[] numArray = new byte[byIn.Length];
      cryptoStream.Read(numArray, 0, numArray.Length);
      cryptoStream.Close();
      return Encoding.Default.GetString(numArray);
    }

    private static byte[] GetBytes(int Len)
    {
      int Seed = 0;
      foreach (byte num in Guid.NewGuid().ToByteArray())
        Seed += (int) num;
      byte[] buffer = new byte[Len];
      new Random(Seed).NextBytes(buffer);
      return buffer;
    }

    public static void TryGetKeyAndIV(out byte[] Key, out byte[] IV)
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      for (int Len = 200; Len > 0; --Len)
      {
        try
        {
          Key = EncryptAndDecrypte.GetBytes(Len);
          IV = EncryptAndDecrypte.GetBytes(Len);
          cryptoServiceProvider.CreateDecryptor(Key, IV);
          return;
        }
        catch
        {
        }
      }
      Key = (byte[]) null;
      IV = (byte[]) null;
    }

    public static string EncryptString(string ConnString)
    {
      if (string.IsNullOrWhiteSpace(ConnString))
        return ConnString;
      return Convert.ToBase64String(EncryptAndDecrypte.EncryptString(ConnString, Convert.FromBase64String(EncryptAndDecrypte.strKey), Convert.FromBase64String(EncryptAndDecrypte.strIV)));
    }

    public static string DecrypteString(string EncryptedConnectionString)
    {
      if (string.IsNullOrWhiteSpace(EncryptedConnectionString))
        return EncryptedConnectionString;
      return EncryptAndDecrypte.DecrypteString(Convert.FromBase64String(EncryptedConnectionString), Convert.FromBase64String(EncryptAndDecrypte.strKey), Convert.FromBase64String(EncryptAndDecrypte.strIV)).TrimEnd(new char[1]);
    }
  }
}
