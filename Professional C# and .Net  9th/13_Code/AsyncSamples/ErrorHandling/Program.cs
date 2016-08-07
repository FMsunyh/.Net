﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorHandling
{
  class Program
  {
    static void Main(string[] args)
    {
      // DontHandle();
      // HandleOneError();
      //StartTwoTasks();
      // StartTwoTasksParallel();
      ShowAggregatedException();

      Console.ReadLine();
    }

    private static async void ShowAggregatedException()
    {
      Task taskResult = null;
      try
      {
        Task t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await (taskResult = Task.WhenAll(t1, t2));
      }
      catch (Exception ex)
      {
        // just display the exception information of the first task that is awaited within WhenAll
        Console.WriteLine("handled {0}", ex.Message);
        foreach (var ex1 in taskResult.Exception.InnerExceptions)
        {
          Console.WriteLine("inner exception {0} from task {1}", ex1.Message, ex1.Source);
        }
      }
    }

    private async static void StartTwoTasksParallel()
    {
      Task t1 = null;
      try
      {
        t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await Task.WhenAll(t1, t2);
      }
      catch (Exception ex)
      {
        // just display the exception information of the first task that is awaited within WhenAll
        Console.WriteLine("handled {0}", ex.Message);
      }
    }

    private static async void StartTwoTasks()
    {
      try
      {
        await ThrowAfter(2000, "first");
        await ThrowAfter(1000, "second"); // the second call is not invoked because the first method throws an exception
      }
      catch (Exception ex)
      {
        Console.WriteLine("handled {0}", ex.Message);
      }
    }

    private static void DontHandle()
    {
      try
      {
        ThrowAfter(200, "first");
        // exception is not caught because this method is finished before the exception is thrown
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    private static async void HandleOneError()
    {
      try
      {
        await ThrowAfter(2000, "first");
      }
      catch (Exception ex)
      {
        Console.WriteLine("handled {0}", ex.Message);
      }
    }

    static async Task ThrowAfter(int ms, string message)
    {
      await Task.Delay(ms);
      throw new Exception(message);
    }
  }
}
