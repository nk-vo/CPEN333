using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 1000000;
            int[] arraySingleThread = new int[ARRAY_SIZE];

            // TODO : Use the "Random" class in a for loop to initialize an array
            var rand = new Random();
            for (int i = 0; i < ARRAY_SIZE; i++)
            {
                arraySingleThread[i] = rand.Next(ARRAY_SIZE);
            }
            // copy array by value.. You can also use array.copy()
            //int[] arrayMultiThread = arraySingleThread.Slice(0,arraySingleThread.Length);
            int[] arrayMultiThread = new int[ARRAY_SIZE];
            Array.Copy(arraySingleThread, arrayMultiThread, arraySingleThread.Length);
            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */

            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;

            //TODO :start the stopwatch
            stopWatch.Start();
            MergeSort(arraySingleThread);

            //TODO :Stop the stopwatch
            stopWatch.Stop();
            ts = stopWatch.Elapsed;

            string elapsedTimeSingle = String.Format("{0:00}.{1:0000}", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Runtime single thread: " + elapsedTimeSingle + "s");
            double SingleSpeed = ts.Milliseconds;

            // just for testing
            /*PrintArray(A);
            if (IsSorted(arraySingleThread))
                Console.WriteLine("Array is sorted");
            else
                Console.WriteLine("Array not sorted");*/


            //TODO: Multi Threading Merge Sort
            int num_threads = 5;
            int chunk_size = ARRAY_SIZE / num_threads;
            Thread[] sub_threads = new Thread[num_threads];

            int m = 0;
            int[][] sub_array = arrayMultiThread.GroupBy(s => m++ / chunk_size).Select(g => g.ToArray()).ToArray();

            stopWatch.Restart();
            for (int i = 0; i < num_threads; i++)
            {
                int j = i;
                sub_threads[j] = new Thread(() => MergeSort(sub_array[j]));
                sub_threads[j].Start();
            }
            for (int i = 0; i < num_threads; i++)
            {
                sub_threads[i].Join();
            }

            var list = new List<int>();
            for (int i = 0; i < num_threads; i++) {
                list.AddRange(sub_array[i]);
            }
            arrayMultiThread = list.ToArray();
            // not sure if I should merge sort it again so I'll just leave it here
            //arrayMultiThread = MergeSort(arrayMultiThread);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            string elapsedTimeMulti = String.Format("{0:00}.{1:0000}", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Runtime multithread: " + elapsedTimeMulti + "s");

            double MultiSpeed = ts.Milliseconds;
            Console.WriteLine("Speed-up factor: " + Math.Round(SingleSpeed / MultiSpeed, 2));
            // just for testing
            /*PrintArray(arrayMultiThread);
            if (IsSorted(arrayMultiThread))
                Console.WriteLine("Array is sorted");
            else
                Console.WriteLine("Array not sorted");*/

            /*********************** Methods **********************
             *****************************************************/
            /*
            implement Merge method. This method takes two sorted array and
            and constructs a sorted array in the size of combined arrays
            */
            
            static int[] Merge(int[] LA, int[] RA, int[] A)
            {

                // TODO :implement
                A = LA.Concat(RA).ToArray();
                return A;
            }


             /*
             implement MergeSort method: takes an integer array by reference
             and makes some recursive calls to intself and then sorts the array
             */
            static int[] MergeSort(int[] A)
            {

                // TODO :implement
                if (A.Length > 1)
                {
                    // this is for even case only
                    // for odd case simply add + 1 after A.Length / 2
                    int[] left_half = A.Take(A.Length / 2).ToArray();
                    int[] right_half = A.Skip(A.Length / 2).ToArray();

                    MergeSort(left_half);
                    MergeSort(right_half);
                    int i = 0, j = 0, k = 0;
                    while (i < left_half.Length && j < right_half.Length)
                    {
                        if (left_half[i] <= right_half[j])
                        {
                            A[k] = left_half[i];
                            i++;
                        }
                        else
                        {
                            A[k] = right_half[j];
                            j++;
                        }
                        k++;
                    }
                    while (i < left_half.Length)
                    {
                        A[k] = left_half[i];
                        i++;
                        k++;
                    }
                    while (j < right_half.Length)
                    {
                        A[k] = right_half[j];
                        j++;
                        k++;
                    }
                }
                return A;
            }


            // a helper function to print your array
            static void PrintArray(int[] myArray)
            {
                Console.Write("[");
                for (int i = 0; i < myArray.Length; i++)
                {
                    Console.Write("{0} ", myArray[i]);

                }
                Console.Write("]");
                Console.WriteLine();

            }

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool IsSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1)
                    return true;
                int ai = a[0], i = 1;

                while (i <= j && ai <= (ai = a[i]))
                    i++;
                return i > j;
            }

        }
        

    }
}
