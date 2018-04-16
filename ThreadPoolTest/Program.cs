using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    class Program
    {
        static List<int> threadIdList = new List<int>();
        static object lockList = new object();

        static void AddBag(int threadId)
        {
            //Console.WriteLine("Thread IDs : {0}", threadIdList.Count);
                if (!threadIdList.Contains(threadId))
                    threadIdList.Add(threadId);
            
        }

        static void Print()
        {
            Console.WriteLine("Thread Count : {0}, Thread IDs :", threadIdList.Count);
        }

        static void Main(string[] args)
        {
            //ThreadPool.se
            ThreadPool.SetMinThreads(900, 900);
            ThreadPool.SetMaxThreads(900, 900);
            List<Task> taskList = new List<Task>();
            while (threadIdList.Count < 900)
            {
               
                lock (lockList)
                {
                    Task t = new Task(() =>
                    {
                        AddBag(Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(30);
                    });

                    taskList.Add(t);
                    t.Start();

                }
            }
            taskList.ForEach(x => x.Wait());
            Print();
        }
        /*
        static void Main(string[] args)
        {
           int minWorkerThreads = 0;
            int minCompletionPortThreads = 0;
            ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads); 
  
            int maxWorkerThreads = 0;
            int maxCompletionPortThreads = 0;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads); 
  
            Console.WriteLine("Min Worker Threads : {0}, max Worker Threads : {1}", minWorkerThreads, maxWorkerThreads);
            Console.WriteLine("Min CompletionPort Threads : {0}, max CompletionPort Threads : {1}", minCompletionPortThreads, maxCompletionPortThreads);
        }*/

    }
}
