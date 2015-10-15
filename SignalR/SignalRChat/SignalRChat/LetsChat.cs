using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using System.IO;
using System.Reflection;

namespace SignalRChat
{
    [HubName("myChatHub")]
    public class LetsChat : Hub
    {
        public void send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                Clients.All.addMessage("文件内容为空,请检查!!");
                return;
            }

            int fileCount = 0;
            

            if (message.Contains("|"))
                fileCount = message.Split('|').Length;
            else
                fileCount = 1;

            string[] fileCollection = new string[fileCount];
            if (fileCount > 1)
                fileCollection = message.Split('|');
            else
                fileCollection[0] = message;


            string uploadPath = AppDomain.CurrentDomain.BaseDirectory;
            int fileFlag = 0;

            foreach (string filename in fileCollection)
            {
                if (File.Exists(filename))
                {
                    string newName = Path.Combine(uploadPath,"Upload",FileWithOutExtension(filename));
                    if (File.Exists(newName))
                        try
                        {
                            File.Delete(newName);
                        }
                        catch (Exception ex)
                        {
                            Clients.All.addMessage(ex.Message);
                        }
                    parameterCollection p = new parameterCollection();
                    p.filename = filename;
                    p.newName = newName;
                    p.eachLoopSize = 2048;
                    p.fileFlag = fileFlag;

                    //Thread t = new Thread(new ParameterizedThreadStart(CopyFilesAsync));
                    //t.IsBackground = true;
                    //t.Start((object)p);
                    BeginCopy(p);
                    
                    fileFlag++;
                    
                }
            }
        }

        private void BeginCopy(object obj)
        {
            try
            {
                parameterCollection pCollection = (parameterCollection)obj;
                Clients.All.addMessage("Start to copy " + pCollection.filename+ " now...");
                Action<object> actionStart = new Action<object>(CopyFilesAsync);
                actionStart.BeginInvoke(obj, new AsyncCallback(iar =>
                {
                    Action<object> actionEnd = (Action<object>)iar.AsyncState;
                    actionEnd.EndInvoke(iar);
                    Clients.All.addMessage("Copied " + pCollection.filename + " ok...");
                }), actionStart);
            }
            catch (Exception ex)
            {
                Clients.All.addMessage(ex.Message);
            }
        }

        private struct parameterCollection
        {
            public string filename;
            public string newName;
            public int eachLoopSize;
            public int fileFlag;
        }

        private void CopyFilesAsync(object obj)
        {
            parameterCollection objConvert = (parameterCollection)obj;
            CopyFile(objConvert.filename, objConvert.newName, objConvert.eachLoopSize,objConvert.fileFlag);
        }

        

        ///<summary>
        ///复制文件
        ///</summary>
        ///<param name="fromFile">要复制的文件</param>
        ///<param name="toFile">要保存的位置</param>
        ///<param name="lengthEachTime">每次复制的长度</param>
        private void CopyFile(string fromFile, string toFile, int lengthEachTime,int fileFlag)
        {
            FileStream fileToCopy = null;
            try{fileToCopy = new FileStream(fromFile, FileMode.Open, FileAccess.Read);}
            catch (Exception ex) { Clients.All.addMessage(ex.Message); return; }

            FileStream copyToFile = null;
            try { copyToFile = new FileStream(toFile, FileMode.Append, FileAccess.Write); }
            catch (Exception ex) { Clients.All.addMessage(ex.Message); return; }

            string fileFlagStr = fileFlag.ToString();
            int lengthToCopy;
            int pauseCount=0; //主要是进行计数,然后调用Thead.sleep来是界面滑行更加流畅

            if (lengthEachTime < fileToCopy.Length)//如果分段拷贝，即每次拷贝内容小于文件总长度
            {
                byte[] buffer = new byte[lengthEachTime];
                int copied = 0;
                while (copied <= ((int)fileToCopy.Length - lengthEachTime))//拷贝主体部分
                {
                    lengthToCopy = fileToCopy.Read(buffer, 0, lengthEachTime);
                    fileToCopy.Flush();
                    copyToFile.Write(buffer, 0, lengthEachTime);
                    copyToFile.Flush();
                    copyToFile.Position = fileToCopy.Position;
                    copied += lengthToCopy;

                    //send to front UI
                    string sendSizeCurrent = ((double)copied / (double)fileToCopy.Length).ToString();
                    Clients.All.addMessage(fileFlagStr + "|" + sendSizeCurrent);
                    pauseCount++;
                    if (pauseCount % 3 == 0)
                        Thread.Sleep(1); //加上这个很重要,主要是让流能够有足够的事件写入,我们可以控制这里来让PrograssBar滑行的更流畅
                }
                int left = (int)fileToCopy.Length - copied;//拷贝剩余部分
                lengthToCopy = fileToCopy.Read(buffer, 0, left);
                fileToCopy.Flush();
                copyToFile.Write(buffer, 0, left);
                copyToFile.Flush();

                Clients.All.addMessage(fileFlagStr + "|" + 1);
            }
            else//如果整体拷贝，即每次拷贝内容大于文件总长度
            {
                byte[] buffer = new byte[fileToCopy.Length];
                fileToCopy.Read(buffer, 0, (int)fileToCopy.Length);
                fileToCopy.Flush();
                copyToFile.Write(buffer, 0, (int)fileToCopy.Length);
                copyToFile.Flush();

                Clients.All.addMessage(fileFlagStr + "|" + 1);
            }
            fileToCopy.Close();
            copyToFile.Close();
            Thread.Sleep(10);
        }


        private string FileWithOutExtension(string filePath)
        {
            if (filePath.Contains(@"\"))
                return filePath.Substring(filePath.LastIndexOf(@"\") + 1);

            if(filePath.Contains(@"/"))
                return filePath.Substring(filePath.LastIndexOf(@"/") + 1);
            return filePath;
        }
    }
}