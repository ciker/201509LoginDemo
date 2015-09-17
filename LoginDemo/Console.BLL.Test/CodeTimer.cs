using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = System.Console;

namespace Login.BLL.Test
{
    public static class CodeTimer
    {
        public delegate void ActionDelegate();
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time("", 1, 1, () => { });
            TimeDelegate("", 1, () => { });
        }

        public static void Time(string name, int iteration, int MaxDegreeOfParallelism, Action action)
        {
            if (String.IsNullOrEmpty(name)) return;

            // 1.
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            Parallel.For(0, iteration, new ParallelOptions() { MaxDegreeOfParallelism = MaxDegreeOfParallelism }, i => action());
            // for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tCount:\t" + iteration);
            Console.WriteLine("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t" + count);
            }

            Console.WriteLine();
        }

        public static void TimeDelegate(string name, int iteration, ActionDelegate action)
        {
            if (String.IsNullOrEmpty(name))
            {
                return;
            }

            if (action == null)
            {
                return;
            }

            //1. Print name
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);


            // 2. Record the latest GC counts
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.Collect(GC.MaxGeneration);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3. Run action
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long ticksFst = GetCurrentThreadTimes(); //100 nanosecond one tick

            for (int i = 0; i < iteration; i++) action();
            long ticks = GetCurrentThreadTimes() - ticksFst;
            watch.Stop();

            // 4. Print CPU
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t\t" +
               watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tTime Elapsed (one time):" +
               (watch.ElapsedMilliseconds / iteration).ToString("N0") + "ms");

            Console.WriteLine("\tCPU time:\t\t" + (ticks * 100).ToString("N0")
               + "ns");
            Console.WriteLine("\tCPU time (one time):\t" + (ticks * 100 /
               iteration).ToString("N0") + "ns");

            // 5. Print GC
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t\t" + count);
            }

            Console.WriteLine();

        }

        public static void Time(string name, int iteration, IAction action)
        {
            if (String.IsNullOrEmpty(name))
            {
                return;
            }

            if (action == null)
            {
                return;
            }

            //1. Print name
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);


            // 2. Record the latest GC counts
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.Collect(GC.MaxGeneration);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3. Run action
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long ticksFst = GetCurrentThreadTimes(); //100 nanosecond one tick

            for (int i = 0; i < iteration; i++) action.Action();
            long ticks = GetCurrentThreadTimes() - ticksFst;
            watch.Stop();

            // 4. Print CPU
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t\t" +
               watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tTime Elapsed (one time):" +
               (watch.ElapsedMilliseconds / iteration).ToString("N0") + "ms");

            Console.WriteLine("\tCPU time:\t\t" + (ticks * 100).ToString("N0")
                + "ns");
            Console.WriteLine("\tCPU time (one time):\t" + (ticks * 100 /
                iteration).ToString("N0") + "ns");

            // 5. Print GC
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t\t" + count);
            }

            Console.WriteLine();

        }
        public static long GetCurrentThreadTimes()
        {
            long l;
            long kernelTime, userTimer;
            GetThreadTimes(GetCurrentThread(), out l, out l, out kernelTime,
               out userTimer);
            return kernelTime + userTimer;
        }
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetThreadTimes(IntPtr hThread, out long lpCreationTime,
           out long lpExitTime, out long lpKernelTime, out long lpUserTime);

    }
    public interface IAction
    {
        void Action();
    }
}
