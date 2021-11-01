using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab3Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            // map and mutex for thread safety
            Mutex mutex_single = new Mutex();
            Mutex mutex_multi = new Mutex();

            Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();
            Dictionary<string, int> wcountMultiThread = new Dictionary<string, int>();

            List<Tuple<int, string>> single_list = new List<Tuple<int, string>>();
            List<Tuple<int, string>> multi_list = new List<Tuple<int, string>>();

            TimeSpan ts_single = new TimeSpan();
            TimeSpan ts_multi = new TimeSpan();

            Stopwatch sw_single = new Stopwatch();
            Stopwatch sw_multi = new Stopwatch();

            var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
            };

            
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================

            Console.WriteLine("SingleThread start!");
            sw_single.Start();
            for (int i = 0; i < filenames.Count; i++)
            {
                HelperFunctions.CountCharacterWords(filenames[i], mutex_single, wcountsSingleThread);
            }
            single_list = HelperFunctions.SortCharactersByWordcount(wcountsSingleThread);

            sw_single.Stop();
            ts_single = sw_single.Elapsed;

            Console.WriteLine("SingleThread is Done!");

            Console.WriteLine("Printing Tuple List: ");
            HelperFunctions.PrintListofTuples(single_list);

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================
            Console.WriteLine("=====================================");
            Console.WriteLine("MultiThread start!");
            sw_multi.Start();
            var threads = new List<Thread>(filenames.Count);
            for (int i = 0; i < filenames.Count; i++)
            {
                var j = i;
                Thread thread = new Thread(() => HelperFunctions.CountCharacterWords(filenames[j], mutex_multi, wcountMultiThread));
                thread.Start();
                threads.Add(thread);
            }

            for (int i = 0; i < filenames.Count; i++)
            {
                threads[i].Join();
            }

            multi_list = HelperFunctions.SortCharactersByWordcount(wcountMultiThread);

            sw_multi.Stop();
            ts_multi = sw_multi.Elapsed;

            Console.WriteLine("MultiThread is Done!");

            Console.WriteLine("Printing Tuple List: ");
            HelperFunctions.PrintListofTuples(multi_list);

            Console.WriteLine("=====================================");
            Console.WriteLine(String.Format("Single thread duration: {0:00}hrs:{1:00}min:{2:00}s:{3:000000}ms \n ",
                             ts_single.Hours,
                             ts_single.Minutes,
                             ts_single.Seconds,
                             ts_single.Milliseconds));
            Console.WriteLine(String.Format("Multi thread duration: {0:00}hrs:{1:00}min:{2:00}s:{3:000000}ms \n ",
                              ts_multi.Hours,
                              ts_multi.Minutes,
                              ts_multi.Seconds,
                              ts_multi.Milliseconds));

            Console.WriteLine("Number of threads used: {0}", threads.Count);

            // 10000 ticks in 1 ms
            Console.WriteLine("Speed up factor: {0}", Math.Round(Convert.ToDouble(ts_single.Ticks / 10000) / (ts_multi.Ticks / 10000), 3));
        }
    }
}
