﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Wrox.ProCSharp.Threading
{

    class Program
    {
        private static void ThreadingTimer()
        {
            using (var t1 = new System.Threading.Timer(
               TimeAction, null, TimeSpan.FromSeconds(2),
               TimeSpan.FromSeconds(3)))
            {

                Thread.Sleep(15000);
            }
        }

        static void TimeAction(object o)
        {
            Console.WriteLine("System.Threading.Timer {0:T}", DateTime.Now);
        }

        private static void TimersTimer()
        {
            var t1 = new System.Timers.Timer(1000);
            t1.AutoReset = true;
            t1.Elapsed += TimeAction;
            t1.Start();
            Thread.Sleep(10000);
            t1.Stop();

            t1.Dispose();
        }

        static void TimeAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("System.Timers.Timer {0:T}", e.SignalTime);
        }


        static void Main(string[] args)
        {
            // ThreadingTimer();
            TimersTimer();
        }
    }
}
