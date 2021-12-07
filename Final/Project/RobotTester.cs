using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Collections;


namespace final_Project
{
	public class RobotTester
	{
		const int numberOfRobots = 2;
		static double maxLoad = 100.0;		// Maximum load of both a cubbard and also the robot.

		const int travel = 0;
		const int extract = 1;
		const int unload = 2;


		const int width = 8;
		const int length = 5;


		public const int boundaryLeft		= 0;
		public const int boundaryTop		= 0;
		public const int boundaryRight		= width - 1;
		public const int boundaryBottom		= length - 1;
		public static int[] boundaries = new int[] { boundaryLeft, boundaryTop, boundaryRight, boundaryBottom };


		public static char[,] layout = new char[width, length];
		public static Mutex layoutReadWrite = new Mutex();

		static public void LayoutPrint(Mutex lrw)
        {
			Console.WriteLine();
			lrw.WaitOne();
			int l = layout.GetLength(1);
			int w = layout.GetLength(0);

			for (int a = boundaryTop; a < l; a++)
            {
				for (int b = boundaryLeft; b < w; b++)
                {
					Console.Write("{0} ", layout[b,a]);
				}

				Console.WriteLine();
            }
			lrw.ReleaseMutex();
			Console.WriteLine();
        }

		static void Main(string[] args)
		{

			for (int a = 0; a < length; a++)
            {

				for (int b = 0; b < width; b++)
                {
					layout[b, a] = 'o';
                }

            }

			//for (int i = 0; i < numberOfRobots; i++)
			//         {
			//	layout[0, i] = 'x';
			//         }


			Mutex[] robotManager = new Mutex[numberOfRobots];

			for (int i = 0; i < numberOfRobots; i++)
            {
				robotManager[i] = new Mutex();
            }






			LayoutPrint(layoutReadWrite);

			int[] r1Start		= new int[] { 0, 1};
			int[] r1StartObj	= new int[] { 0, 0};

			RobotClass rob1 = new RobotClass(1, r1Start, r1StartObj, 0, boundaries, layoutReadWrite);
			rob1.ObjectiveStep(r1StartObj, 0, layoutReadWrite);


			int[] fullLoopTest = new int[] { 0, 2 };
			rob1.ObjectiveStep(fullLoopTest, 0, layoutReadWrite);

			fullLoopTest = new int[] { 0, 0 };
			rob1.ObjectiveStep(fullLoopTest, 0, layoutReadWrite);









			Task rob1Task = Task.Run(() =>
			{
				for (int i = boundaryLeft + 1; i < (boundaryRight + 1); i++)
				{
					int[] fullLoopTest1 = new int[] { i, ((i % 3) + 1) };
					Task robot1 = Task.Run(() => rob1.ObjectiveStep(fullLoopTest1, 0, layoutReadWrite));
					robot1.Wait();

					fullLoopTest1 = new int[] { i, 4 };
					robot1 = Task.Run(() => rob1.ObjectiveStep(fullLoopTest1, 0, layoutReadWrite));
					robot1.Wait();

					fullLoopTest1 = new int[] { 0, 0 };
					robot1 = Task.Run(() => rob1.ObjectiveStep(fullLoopTest1, 0, layoutReadWrite));
					robot1.Wait();
				}

				
			});


			Task rob2Task = Task.Run(() =>
			{

				int[] r2Start = new int[] { 0, 1 };
				int[] r2StartObj = new int[] { 0, 0 };
				RobotClass rob2 = new RobotClass(2, r2Start, r2StartObj, 0, boundaries, layoutReadWrite);
				rob2.ObjectiveStep(r2StartObj, 0, layoutReadWrite);

				for (int k = boundaryLeft + 1; k < (boundaryRight + 1); k++)
				{
					int[] fullLoopTest2 = new int[] { k, ((k % 3) + 1) };
					Task robot2 = Task.Run(() => rob2.ObjectiveStep(fullLoopTest2, 0, layoutReadWrite));
					robot2.Wait();

					fullLoopTest2 = new int[] { k, 4 };
					robot2 = Task.Run(() => rob2.ObjectiveStep(fullLoopTest2, 0, layoutReadWrite));
					robot2.Wait();

					if (k != 1)
					{
						fullLoopTest2 = new int[] { 0, 0 };
						robot2 = Task.Run(() => rob2.ObjectiveStep(fullLoopTest2, 0, layoutReadWrite));
						robot2.Wait();
					}
                    else
                    {
						fullLoopTest2 = new int[] { 0, 1 };
						robot2 = Task.Run(() => rob2.ObjectiveStep(fullLoopTest2, 0, layoutReadWrite));
						robot2.Wait();
					}
				}
			});

			while ((!rob2Task.IsCompleted) || (!rob1Task.IsCompleted))
			{
				if (rob1Task.IsCompleted)
				{
					int[] fullLoopTest1 = new int[] { 0, 1 };
					Task robot1 = Task.Run(() => rob1.ObjectiveStep(fullLoopTest1, 0, layoutReadWrite));
					robot1.Wait();

					if (!rob2Task.IsCompleted)
					{
						fullLoopTest1 = new int[] { 0, 0 };
						robot1 = Task.Run(() => rob1.ObjectiveStep(fullLoopTest1, 0, layoutReadWrite));
						robot1.Wait();
					}
				}
			}


			rob2Task.Wait();
			rob1Task.Wait();

			Console.WriteLine("\nFinished.");
			LayoutPrint(layoutReadWrite);
			Console.WriteLine("\n\n\n\n\n");







			



		}



	}

}