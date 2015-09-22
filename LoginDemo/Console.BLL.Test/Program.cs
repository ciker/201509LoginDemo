using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using LoginDemo.BLL;
using LoginDemo.BLL.UserAccount;
using LoginDemo.Commom;
using LoginDemo.DAL;
using LoginDemo.DAL.UserAccount;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
using Console = System.Console;

namespace Login.BLL.Test
{
    class Program
    {
        #region properties
        private static readonly UserBLL UserBll = new UserBLL(new UserDAL());
        private static readonly UserAccountBLL UserinfoBll = new UserAccountBLL(new UserAccountDAL());
        private const int ParallelNum = 8000;//parallel num
        private const int ParallelMaxThreadNum = 180;//parallel max thread num
        private const int NormalMaxThreadNum = 185;//normal max thread num 
        private const int NormalTreadNum = 185;//normal thread num
        private const int AsyncNormalTreadNum = 165;//async normal thread num
        //private static readonly TestTimer TeTimer = new TestTimer();//自定义定时器
        private static readonly Mutex MuxConsole = new Mutex();//线程同步基元
        //private static readonly object _locker = new object();//锁
        //private static int _poolFlag = 0;//线程标识
        #endregion
        static void Main(string[] args)
        {
            #region aTimer
            ////aTimer = new System.Timers.Timer(10000);
            //////注册计时器的事件
            ////aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //////设置时间间隔为2秒（2000毫秒），覆盖构造函数设置的间隔
            ////aTimer.Interval = 2000;
            //////设置是执行一次（false）还是一直执行(true)，默认为true
            ////aTimer.AutoReset = true;
            ////////开始计时
            //////aTimer.Enabled = true;
            #endregion


            //UserParallelTest();// register parallel test 
            UserInfoParallelTest();
            read();

            //Msg();
            //asynctest();
        }

        #region Test

        /// <summary>
        /// total  
        /// </summary>
        private static void UserParallelTest()
        {
            CodeTimer.Initialize();
            #region register
            #region thread
            //CodeTimer.Time("register thread with Mutex", 210, () =>
            //{
            //    new Thread(() =>
            //    {
            //        MuxConsole.WaitOne();//挂起
            //        Register();
            //        MuxConsole.ReleaseMutex();//释放
            //    }).Start();
            //});
            //CodeTimer.Time("register thread without Mutex", 210, () =>
            //{
            //    new Thread(Register).Start();
            //});
            //1081ms  100
            #endregion
            //CodeTimer.Time("Register Parallel", 1000000, 15, Register);
            #endregion

            #region login
            //CodeTimer.Time("login thread", 100, () =>
            //{
            //    new Thread(() =>
            //    {
            //        MuxConsole.WaitOne();
            //        Login();
            //        MuxConsole.ReleaseMutex();
            //    }).Start();
            //});
            //1400ms  100
            //CodeTimer.Time("login Parallel", 1000000, 15, Login);

            //CodeTimer.Time("RandomNum", 100, 15, RandomNum);

            #endregion

            #region SelectUserList

            //CodeTimer.Time("GenerateCondition Parallel", 1000000, 15, GenerateCondition);
            //CodeTimer.Time("SelectUserList Parallel", 1000000, 15, SelectUserList);

            CodeTimer.Time("SelectUserList Parallel", 100000, 15, SelectUserList);
            #endregion

            #region TimeDelegate
            //CodeTimer.TimeDelegate("register parallel TimeDelegate", 1, () =>
            //{
            //    Parallel.For(0, 150, a => { Register(); });
            //});
            //CodeTimer.TimeDelegate("Login parallel TimeDelegate", 1, () =>
            //{
            //    Parallel.For(0, 150, a => { Login(); });
            //});

            //CodeTimer.TimeDelegate("Register TimeDelegate", 1, () =>
            //{
            //    Parallel.For(0, 150, a => { Register(); });
            //});
            #endregion

            #region old
            ////Write("========test register start============" + "\r\t");
            ////var sw = new Stopwatch();
            ////sw.Start();

            ////var wait = new AutoResetEvent(false);
            ////for (var i = 0; i < ParallelMaxThreadNum; i++)
            ////{
            ////    new Thread(() =>
            ////    {
            ////        //lock (locker)
            ////        //{
            ////        //wait.WaitOne();1
            ////        MuxConsole.WaitOne();
            ////Parallel.For(0, ParallelNum, a =>
            ////{
            ////    #region ///lock IL 为 Monitor
            ////    //lock (_locker)
            ////    //{
            ////    //    Register();
            ////    //}
            ////    #endregion
            ////    try
            ////    {
            ////        //Monitor.Enter(_locker);
            ////        Register();
            ////    }
            ////    finally
            ////    {
            ////        //Monitor.Exit(_locker);
            ////    }

            ////});

            ////    MuxConsole.ReleaseMutex();
            ////    //}
            ////}).Start();
            ////Interlocked.Decrement(ref _poolFlag);
            ////}

            //// }
            //#region trhead
            ////wait.Set();
            ////for (var i = 0; i < 10; i++)
            ////{
            ////    var thread = new Thread(Register);
            ////    //Register();
            ////    thread.Start();
            ////    Write("thread_ID:" + thread.ManagedThreadId + "\r\t");
            ////    Thread.Sleep(200);
            ////}
            ////Write("thread_ID:" + thread.ManagedThreadId + "  耗时: " + sw.ElapsedMilliseconds + "\r\t");
            //#endregion
            ////sw.Stop();
            ////Write("time:" + sw.ElapsedMilliseconds.ToString());
            ////Write("============test register end===========" + "\r\t");
            #endregion
        }

        /// <summary>
        /// thread 185  
        /// </summary>
        private static void RegisterNormalTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < NormalMaxThreadNum; i++)
            {
                new Thread(() =>
                {
                    MuxConsole.WaitOne();//挂起
                    #region ///lock IL Monitor

                    #endregion
                    try
                    {
                        //Monitor.Enter(_locker);

                        Register();
                    }
                    finally
                    {
                        //Monitor.Exit(_locker);
                    }

                    MuxConsole.ReleaseMutex();
                }).Start();
            }
            sw.Stop();
            Write("time:" + sw.ElapsedMilliseconds.ToString());
            Write("============test register end===========" + "\r\t");
        }

        /// <summary>
        ///  total  8000*180=1440000 =1440000
        /// </summary>
        private static void LoginParallelTest()
        {
            Write("========test login start============" + "\r\t");
            var sw = new Stopwatch();
            sw.Start();

            //var wait = new AutoResetEvent(false);
            for (var i = 0; i < ParallelMaxThreadNum; i++)
            {
                new Thread(() =>
                {
                    MuxConsole.WaitOne();
                    Parallel.For(0, ParallelNum, a =>
                    {
                        try
                        {
                            //Monitor.Enter(_locker);

                            Login();
                        }
                        finally
                        {
                            //Monitor.Exit(_locker);
                        }

                    });

                    MuxConsole.ReleaseMutex();
                }).Start();

            }
            sw.Stop();
            Write("time:" + sw.ElapsedMilliseconds.ToString());
            Write("============test login end===========" + "\r\t");
        }

        /// <summary>
        /// total 185
        /// </summary>
        private static void LoginNormalTest()
        {
            Write("========test login start============" + "\r\t");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < NormalTreadNum; i++)
            {
                new Thread(() =>
                {
                    MuxConsole.WaitOne();//挂起
                    try
                    {
                        //Monitor.Enter(_locker);
                        Login();
                    }
                    finally
                    {
                        //Monitor.Exit(_locker);
                    }
                    MuxConsole.ReleaseMutex();
                }).Start();

            }
            sw.Stop();
            Write("time:" + sw.ElapsedMilliseconds.ToString());
            Write("============test login end===========" + "\r\t");
        }

        /// <summary>
        /// total 165
        /// </summary>
        private static void LoginAsyncNormalTest()
        {
            Write("========test login start============" + "\r\t");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < AsyncNormalTreadNum; i++)
            {
                new Thread(() =>
                {
                    MuxConsole.WaitOne();//挂起
                    try
                    {
                        //Monitor.Enter(_locker);
                        LoginAsync();
                    }
                    finally
                    {
                        //Monitor.Exit(_locker);
                    }
                    MuxConsole.ReleaseMutex();
                }).Start();

            }
            sw.Stop();
            Write("time:" + sw.ElapsedMilliseconds.ToString());
            Write("============test login end===========" + "\r\t");
        }

        private static void UserInfoParallelTest()
        {
            //CodeTimer.Time("UserInfoRegister Parallel", 100000, 15, UserInfoRegister);


            //CodeTimer.Time("UserInfoLogin Parallel", 100000, 15, UserInfoLogin);
            //try
            //{
            CodeTimer.Time("SelectUserInfoList Parallel", 10000, 15, SelectUserInfoList);
            //}
            //catch (AggregateException ex)
            //{
            //    throw ex;
            //}
        }


        #endregion

        #region business

        private static void GenerateCondition()
        {
            string.Empty.GenerateCondition(new UserQueryParameter() { PageIndex = 1, PageSize = 1 });
        }
        private static void SelectUserList()
        {
            UserBll.GetUserListbyParameter(new UserQueryParameter() { PageIndex = 1, PageSize = 10 });
        }

        private static void SelectUserInfoList()
        {
            UserinfoBll.Query(new UserInfoQueryParameter() { PageIndex = 1, PageSize = 10 });
        }
        private static void Register()
        {
            var num = Guid.NewGuid().ToString();// GenerareRandomString();//  new Random().Next();// Guid.NewGuid().ToString();////DateTime.Now.ToString("ddHHmmssfffff");//new Random().Next(0, int.MaxValue);//CodeTimer.GetCurrentThreadTimes();
            var user = new User()
            {
                UserName = "1_Unit_Test" + num,
                UserPWD = ("1_Unit_Test" + num),
                Mobile = "13678900123",
                Email = "13678900123@qq.com"
            };

            UserBll.Register(user);
        }

        private static void UserInfoRegister()
        {
            var num = Guid.NewGuid().ToString();
            var userInfo = new UserInfoAndAccount()
            {
                Account = "1_Unit_Test" + num,
                Password = ("1_Unit_Test" + num),
                CompanyName = "东方航空集团航空公司",
                Address = "上海市长宁区绥宁路388号"
            };

            UserinfoBll.Register(userInfo);
        }

        private static void Login()
        {
            //var num = new Random().Next(0, int.MaxValue);
            UserBll.Login(new User()
           {
               UserName = "1_Unit_Test31983c30-66c5-4210-8695-61ae1eb25f9f",
               UserPWD = "1_Unit_Test31983c30-66c5-4210-8695-61ae1eb25f9f"
               //UserName = "1_Unit_Test" + num,
               //UserPWD = ("1_Unit_Test" + num),
           });
        }

        private static void UserInfoLogin()
        {
            UserBll.Login(new User()
          {
              UserName = "1_Unit_Test1afb9c02-076c-4c67-b06a-a68fac88e030",
              UserPWD = "1_Unit_Test1afb9c02-076c-4c67-b06a-a68fac88e030"
              //UserName = "1_Unit_Test" + num,
              //UserPWD = ("1_Unit_Test" + num),
          });
        }

        private static void RandomNum()
        {
            var num = new Random().Next();
            Write(num.ToString());
        }

        private static void RandomNumSeed()
        {
            var num = new Random().Next(int.MaxValue);
            Write(num.ToString());
        }

        private static void LoginAsync()
        {
            var num = new Random().Next(int.MinValue, int.MaxValue);
            UserBll.LoginAsync(new User()
            {
                UserName = "1_Unit_Test2" + num,
                UserPWD = ("1_Unit_Test" + num).ToBase64String(),
            });
        }

        #endregion

        #region common
        private static void Write(string msg)
        {
            System.Console.WriteLine(msg);
        }
        private static void read()
        {
            System.Console.ReadLine();
        }

        private static void Msg()
        {
            MessageBox(IntPtr.Zero, "您的操作已经到了极限，请停下来歇一歇，就可以并行处理了。", "系统提示", 0);
        }

        [DllImport("User32.dll")]
        internal static extern int MessageBox(IntPtr hwnd, string text, string caption, uint type);


        private async static void asynctest()
        {
            var i = 0;
            var task = Task.Run(() =>
            {
                Write("here shows ");
                i++;
                Thread.Sleep(1000);
                return "hello word ,time " + i + "\t\r";
            });
            var str = await task;
            Write(str);
        }

        private static string GenerareRandomString()
        {
            var str = string.Empty;
            var chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < 5; i++)
            {
                var seed = new Random().Next(0, 35);
                str += chars[seed];
            }
            return str;
        }
        #endregion
    }
}
