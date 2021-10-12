using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadPi
{
    class MainClass
    {
        static int numberOfCores = Environment.ProcessorCount;
        static long numberOfSamples = 100000000;
        static void Main(string[] args)
        {
            //long numberOfSamples = 100000000;
            long hits = 0;

            Console.WriteLine("Number of cores: " + numberOfCores);
            Console.WriteLine("Max threads: " + numberOfCores * 2);
            
            var sw = new Stopwatch();
            TimeSpan ts;
            sw.Start();
            double pi = EstimatePI(numberOfSamples, ref hits);
            sw.Stop();
            ts = sw.Elapsed;
            string elapsedTimeSingle = String.Format("{0:00}.{1:0000}", ts.Seconds, ts.Milliseconds);
       
            Console.WriteLine("Pi single thread: " + pi);
            Console.WriteLine("Runtime: " + elapsedTimeSingle + "s");

            int num_threads = numberOfCores * 1;
            Thread[] sub_threads = new Thread[num_threads];
            double[] pi_multi = new double[num_threads];
            double pi_approx = 0;
            sw.Restart();
            hits = 0;
            for (int i = 0; i < num_threads; i++)
            {
                int j = i;
                sub_threads[j] = new Thread(() => PiEst(num_threads,ref pi_multi[j], ref hits));
                sub_threads[j].Start();
            }
            for (int i = 0; i < num_threads; i++)
            {
                sub_threads[i].Join();
                pi_approx += pi_multi[i];
            }
            /*for (int i = 0; i < num_threads; i++)
            {
                pi_approx += pi_multi[i];
            }*/
            pi_approx /= num_threads;
            sw.Stop();

            ts = sw.Elapsed;
            string elapsedTimeMulti = String.Format("{0:00}.{1:0000}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Pi multi thread: " + pi_approx);
            Console.WriteLine("Runtime: " + elapsedTimeMulti + "s");

            static double PiEst(int num_threads ,ref double pi_approx, ref long hits)
            {
                pi_approx = EstimatePI(numberOfSamples/num_threads, ref hits);
                hits = 0;
                return pi_approx;
            }
            static double EstimatePI(long numberOfSamples, ref long hits)
            {
                //implement
                long total = 0;
                double pi = 0;
                double[,] cor = GenerateSamples(numberOfSamples);
                double x, y;
                while (total < numberOfSamples)
                {
                    x = cor[total, 0];
                    y = cor[total, 1];
                    if (Math.Sqrt(x * x + y * y) <= 1)
                        Interlocked.Increment(ref hits);
                    total++;
                    pi = 4 * ((double)hits / (double)total);
                }
                return pi;
            }

            static double[,] GenerateSamples(long numberOfSamples)
            {
                // Implement  
                var rand = new Random();
                double[,] cor = new double[numberOfSamples ,2];

                for (int i = 0; i < numberOfSamples; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        cor[i, j] = (rand.NextDouble() * 2) - 1;
                    }
                }
                return cor;
            }
            
        }
    }
}
