using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Collections;
using System.Globalization;



namespace final_Project
{
	public class FullTest
	{

		public static bool	programSwitch		= false;
		public static Mutex programSwitch_Mutex = new Mutex();









		public const int numberOfRobots = 4;

		public static double maxLoad = 5000.0;      // Maximum load of both a cubbard and also the robot.
		public static Mutex maxLoad_Mutex = new Mutex();

		public static double maxTruckLoad = 15000.0;
		public static Mutex maxTruckLoad_Mutex = new Mutex();

		const int travel = 0;
		const int extract = 1;
		const int unload = 2;


		const int width = 10;
		const int length = 5;

		public const int chargingLane = 0;
		public const int boundaryLeft = 1;
		public const int boundaryTop = 0;
		public const int boundaryRight = width - 1;
		public const int boundaryBottom = length - 1;
		public static int[] boundaries = new int[] { boundaryLeft, boundaryTop, boundaryRight, boundaryBottom, chargingLane };

		public const int columns = 7;
		public const int subColumns = 3;
		public const int side = 2;
		public const int cubbard = 6;








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

				for (int b = 0; b < w; b++)
				{

					Console.Write("{0}", layout[b, a]);

					if ( (b < w - 1) && (b > 1) )
                    {
						Console.Write('|');
                    }
					else
                    {
						Console.Write(' ');
                    }

				}

				Console.WriteLine();

			}

			lrw.ReleaseMutex();
			Console.WriteLine();

		}







		public static Item1[,,,] item1Tracker = new Item1[columns, subColumns, side, cubbard];
		public static Item2[,,,] item2Tracker = new Item2[columns, subColumns, side, cubbard];
		public static Item3[,,,] item3Tracker = new Item3[columns, subColumns, side, cubbard];
		public static Item4[,,,] item4Tracker = new Item4[columns, subColumns, side, cubbard];
		public static Item5[,,,] item5Tracker = new Item5[columns, subColumns, side, cubbard];

		public static Mutex item1_Mutex = new Mutex();
		public static Mutex item2_Mutex = new Mutex();
		public static Mutex item3_Mutex = new Mutex();
		public static Mutex item4_Mutex = new Mutex();
		public static Mutex item5_Mutex = new Mutex();

		public static double[,,,] shelfMass = new double[columns, subColumns, side, cubbard];
		public static Mutex shelf_Mutex = new Mutex();

		static public double totalMassOfCategory(int itm)
		{
			double totalMass = 0.0;

			shelf_Mutex.WaitOne();
			int shelf = shelfMass.GetLength(0);
			int row = shelfMass.GetLength(1);
			int side = shelfMass.GetLength(2);
			int cubbard = shelfMass.GetLength(3);
			shelf_Mutex.ReleaseMutex();

			if (itm == 0)
			{


				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item1_Mutex.WaitOne();
								totalMass += item1Tracker[s, r, sd, c].totalMass;
								item1_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;


			}
			else if (itm == 1)
			{
				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item2_Mutex.WaitOne();
								totalMass += item2Tracker[s, r, sd, c].totalMass;
								item2_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;
			}
			else if (itm == 2)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item3_Mutex.WaitOne();
								totalMass += item3Tracker[s, r, sd, c].totalMass;
								item3_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;

			}
			else if (itm == 3)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item4_Mutex.WaitOne();
								totalMass += item4Tracker[s, r, sd, c].totalMass;
								item4_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;

			}
			else if (itm == 4)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item5_Mutex.WaitOne();
								totalMass += item5Tracker[s, r, sd, c].totalMass;
								item5_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;

			}
			else if (itm == 5)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								shelf_Mutex.WaitOne();
								totalMass += shelfMass[s, r, sd, c];
								shelf_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalMass;

			}

			return 0.0;
		}

		static public int totalUnitsOfCategory(int itm)
		{

			shelf_Mutex.WaitOne();
			int shelf = shelfMass.GetLength(0);
			int row = shelfMass.GetLength(1);
			int side = shelfMass.GetLength(2);
			int cubbard = shelfMass.GetLength(3);
			shelf_Mutex.ReleaseMutex();

			int totalUnits = 0;
			if (itm == 0)
			{


				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item1_Mutex.WaitOne();
								totalUnits += item1Tracker[s, r, sd, c].units;
								item1_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalUnits;


			}
			else if (itm == 1)
			{
				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item2_Mutex.WaitOne();
								totalUnits += item2Tracker[s, r, sd, c].units;
								item2_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalUnits;
			}
			else if (itm == 2)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item3_Mutex.WaitOne();
								totalUnits += item3Tracker[s, r, sd, c].units;
								item3_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalUnits;

			}
			else if (itm == 3)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item4_Mutex.WaitOne();
								totalUnits += item4Tracker[s, r, sd, c].units;
								item4_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalUnits;

			}
			else if (itm == 4)
			{

				for (int s = 0; s < shelf; s++)
				{
					for (int r = 0; r < row; r++)
					{
						for (int sd = 0; sd < side; sd++)
						{
							for (int c = 0; c < cubbard; c++)
							{
								item5_Mutex.WaitOne();
								totalUnits += item5Tracker[s, r, sd, c].units;
								item5_Mutex.ReleaseMutex();
							}
						}
					}
				}

				return totalUnits;

			}
			else if (itm == 5)
			{

				int iteration1 = totalUnitsOfCategory(0);
				int iteration2 = totalUnitsOfCategory(1);
				int iteration3 = totalUnitsOfCategory(2);
				int iteration4 = totalUnitsOfCategory(3);
				int iteration5 = totalUnitsOfCategory(4);

				totalUnits = iteration1 + iteration2 + iteration3 + iteration4 + iteration5;

				return totalUnits;

			}

			return 0;
		}

		static public double[] checkStorage()   // Check for any shortages and returns the amount of each item that needs to be replenished.
		{

			shelf_Mutex.WaitOne();
			int shelf = shelfMass.GetLength(0);
			int row = shelfMass.GetLength(1);
			int side = shelfMass.GetLength(2);
			int cubbard = shelfMass.GetLength(3);
			shelf_Mutex.ReleaseMutex();

			maxLoad_Mutex.WaitOne();
			double mLoad = maxLoad;
			maxLoad_Mutex.ReleaseMutex();




			double storagePartition = (mLoad * ((double)(shelf * row * side * cubbard))) / 5.0;

			double[] shortage = new double[5];

			for (int i = 0; i < 5; i++)
			{
				double MassCollection = totalMassOfCategory(i);
				if (MassCollection < storagePartition)
				{
					shortage[i] = storagePartition - MassCollection;
				}
				else
				{
					shortage[i] = 0.0;
				}
			}

			return shortage.ToArray();

		}

		static public bool shortageBool()
		{

			double[] shortage = new double[5];

			shortage = checkStorage().ToArray();

			for (int i = 0; i < 5; i++)
			{
				if (shortage[i] > 0.0)
				{
					return true;
				}
			}

			return false;

		}

		public static Mutex[] robot_Mutex = new Mutex[4];




		static public Tuple<double, int[]> maxStorageSimple1()
		{



			item1_Mutex.WaitOne();
			int shelf		= item1Tracker.GetLength(0);
			int row			= item1Tracker.GetLength(1);
			int side		= item1Tracker.GetLength(2);
			int cubbard		= item1Tracker.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{


							if (item1Tracker[s, r, sd, c].totalMass > floor)
							{
								if ((maxLoad - item1Tracker[s, r, sd, c].totalMass) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									item1_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = item1Tracker[s, r, sd, c].totalMass;
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			item1_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification1(int a)
		{

			Tuple<double, int[]> e = maxStorageSimple1();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			item1_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < item1Tracker.GetLength(0); s++)
				{
					for (int r = 0; r < item1Tracker.GetLength(1); r++)
					{
						for (int sd = 0; sd < item1Tracker.GetLength(2); sd++)
						{
							for (int c = 0; c < item1Tracker.GetLength(3); c++)
							{



								if (item1Tracker[s, r, sd, c].totalMass >= floor)  // New potential value.
								{

									if (item1Tracker[s, r, sd, c].totalMass <= ceiling[w])
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = item1Tracker[s, r, sd, c].totalMass;
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			item1_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}


		static public Tuple<double, int[]> maxStorageSimple2()
		{



			item2_Mutex.WaitOne();
			int shelf = item2Tracker.GetLength(0);
			int row = item2Tracker.GetLength(1);
			int side = item2Tracker.GetLength(2);
			int cubbard = item2Tracker.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{


							if (item2Tracker[s, r, sd, c].totalMass > floor)
							{
								if ((maxLoad - item2Tracker[s, r, sd, c].totalMass) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									item2_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = item2Tracker[s, r, sd, c].totalMass;
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			item2_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification2(int a)
		{

			Tuple<double, int[]> e = maxStorageSimple2();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			item2_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < item2Tracker.GetLength(0); s++)
				{
					for (int r = 0; r < item2Tracker.GetLength(1); r++)
					{
						for (int sd = 0; sd < item2Tracker.GetLength(2); sd++)
						{
							for (int c = 0; c < item2Tracker.GetLength(3); c++)
							{



								if (item2Tracker[s, r, sd, c].totalMass >= floor)  // New potential value.
								{

									if (item2Tracker[s, r, sd, c].totalMass <= ceiling[w])
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = item2Tracker[s, r, sd, c].totalMass;
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			item2_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}


		static public Tuple<double, int[]> maxStorageSimple3()
		{



			item3_Mutex.WaitOne();
			int shelf = item3Tracker.GetLength(0);
			int row = item3Tracker.GetLength(1);
			int side = item3Tracker.GetLength(2);
			int cubbard = item3Tracker.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{


							if (item3Tracker[s, r, sd, c].totalMass > floor)
							{
								if ((maxLoad - item3Tracker[s, r, sd, c].totalMass) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									item3_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = item3Tracker[s, r, sd, c].totalMass;
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			item3_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification3(int a)
		{

			Tuple<double, int[]> e = maxStorageSimple3();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			item3_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < item3Tracker.GetLength(0); s++)
				{
					for (int r = 0; r < item3Tracker.GetLength(1); r++)
					{
						for (int sd = 0; sd < item3Tracker.GetLength(2); sd++)
						{
							for (int c = 0; c < item3Tracker.GetLength(3); c++)
							{



								if (item3Tracker[s, r, sd, c].totalMass >= floor)  // New potential value.
								{

									if (item3Tracker[s, r, sd, c].totalMass <= ceiling[w])
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = item3Tracker[s, r, sd, c].totalMass;
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			item3_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}


		static public Tuple<double, int[]> maxStorageSimple4()
		{



			item4_Mutex.WaitOne();
			int shelf = item4Tracker.GetLength(0);
			int row = item4Tracker.GetLength(1);
			int side = item4Tracker.GetLength(2);
			int cubbard = item4Tracker.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{


							if (item4Tracker[s, r, sd, c].totalMass > floor)
							{
								if ((maxLoad - item3Tracker[s, r, sd, c].totalMass) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									item4_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = item4Tracker[s, r, sd, c].totalMass;
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			item4_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification4(int a)
		{

			Tuple<double, int[]> e = maxStorageSimple4();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			item4_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < item4Tracker.GetLength(0); s++)
				{
					for (int r = 0; r < item4Tracker.GetLength(1); r++)
					{
						for (int sd = 0; sd < item4Tracker.GetLength(2); sd++)
						{
							for (int c = 0; c < item4Tracker.GetLength(3); c++)
							{



								if (item4Tracker[s, r, sd, c].totalMass >= floor)  // New potential value.
								{

									if (item4Tracker[s, r, sd, c].totalMass <= ceiling[w])
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = item4Tracker[s, r, sd, c].totalMass;
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			item4_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}


		static public Tuple<double, int[]> maxStorageSimple5()
		{



			item5_Mutex.WaitOne();
			int shelf = item5Tracker.GetLength(0);
			int row = item5Tracker.GetLength(1);
			int side = item5Tracker.GetLength(2);
			int cubbard = item5Tracker.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{


							if (item5Tracker[s, r, sd, c].totalMass > floor)
							{
								if ((maxLoad - item5Tracker[s, r, sd, c].totalMass) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									item5_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = item5Tracker[s, r, sd, c].totalMass;
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			item5_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification5(int a)
		{

			Tuple<double, int[]> e = maxStorageSimple5();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			item5_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < item5Tracker.GetLength(0); s++)
				{
					for (int r = 0; r < item5Tracker.GetLength(1); r++)
					{
						for (int sd = 0; sd < item5Tracker.GetLength(2); sd++)
						{
							for (int c = 0; c < item5Tracker.GetLength(3); c++)
							{



								if (item5Tracker[s, r, sd, c].totalMass >= floor)  // New potential value.
								{

									if (item5Tracker[s, r, sd, c].totalMass <= ceiling[w])
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = item5Tracker[s, r, sd, c].totalMass;
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			item5_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}







		static public Tuple<double, int[]> maxStorageSimple()
		{

			//if (itemNum == 0)
			//{
			//	item1_Mutex.WaitOne();
			//	var item = item1Tracker;
			//	item1_Mutex.ReleaseMutex();
			//}
			//else if (itemNum == 1)
			//{
			//	item2_Mutex.WaitOne();
			//	var item = item2Tracker;
			//	item2_Mutex.ReleaseMutex();
			//}
			//else if (itemNum == 2)
			//{
			//	item2_Mutex.WaitOne();
			//	var item = item2Tracker;
			//	item2_Mutex.ReleaseMutex();
			//}
			//else if (itemNum == 3)
			//{
			//	item4_Mutex.WaitOne();
			//	var item = item4Tracker;
			//	item4_Mutex.ReleaseMutex();
			//}
			//else if (itemNum == 4)
			//{
			//	item5_Mutex.WaitOne();
			//	var item = item5Tracker;
			//	item5_Mutex.ReleaseMutex();
			//}



			shelf_Mutex.WaitOne();
			int shelf = shelfMass.GetLength(0);
			int row = shelfMass.GetLength(1);
			int side = shelfMass.GetLength(2);
			int cubbard = shelfMass.GetLength(3);

			double floor = 0.0;
			int[] co = new int[4] { 0, 0, 0, 0 };

			double ceiling = maxLoad;

			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{
							
							
							if (shelfMass[s, r, sd, c] > floor)
							{
								if ((maxLoad - shelfMass[s, r, sd, c]) < 0.00001) // Value is so small it can be considered to be '0'.
								{
									shelf_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(maxLoad, new int[] { s, r, sd, c });
								}
								else
								{
									floor = shelfMass[s, r, sd, c];
									co = new int[] { s, r, sd, c };
								}
							}


						}
					}
				}
			}

			shelf_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(floor, new int[4] { co[0], co[1], co[2], co[3] });


		}

		static public Tuple<double[], int[][]> maxStorageIdentification(int a)
        {

			Tuple<double, int[]> e = maxStorageSimple();

			double[] ceiling = new double[a];
			ceiling[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			shelf_Mutex.WaitOne();
			double floor = 0.0;

			bool z;

			for (int w = 0; w < (a - 1); w++)
			{
				floor = 0.0;


				for (int s = 0; s < shelfMass.GetLength(0); s++)
				{
					for (int r = 0; r < shelfMass.GetLength(1); r++)
					{
						for (int sd = 0; sd < shelfMass.GetLength(2); sd++)
						{
							for (int c = 0; c < shelfMass.GetLength(3); c++)
							{



								if (shelfMass[s, r, sd, c] >= floor)  // New potential value.
								{

									if ((shelfMass[s, r, sd, c] <= ceiling[w]))
									{
										z = true;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = false;
												break;
											}
										}

										if (z)
										{
											floor = shelfMass[s, r, sd, c];
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				ceiling[w + 1] = floor;

			}

			shelf_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(ceiling.ToArray(), co.ToArray());

		}





		static public Tuple<double, int[]> minStorageSimple() // Returns lowest storage weight.
		{

			shelf_Mutex.WaitOne();
			int shelf = shelfMass.GetLength(0);
			int row = shelfMass.GetLength(1);
			int side = shelfMass.GetLength(2);
			int cubbard = shelfMass.GetLength(3);

			double min = maxLoad;
			int[] co = new int[4] { 0, 0, 0, 0 };


			for (int s = 0; s < shelf; s++)
			{
				for (int r = 0; r < row; r++)
				{
					for (int sd = 0; sd < side; sd++)
					{
						for (int c = 0; c < cubbard; c++)
						{

							if (shelfMass[s, r, sd, c] < min)
							{

								if (shelfMass[s, r, sd, c] < 0.00001) // Value is so small it can be considered to be '0'.
								{
									shelf_Mutex.ReleaseMutex();
									return new Tuple<double, int[]>(0.0, new int[] { s, r, sd, c });
								}
								else
								{
									min = shelfMass[s, r, sd, c];
									co = new int[] { s, r, sd, c };
								}

							}
						}
					}
				}
			}

			shelf_Mutex.ReleaseMutex();
			return new Tuple<double, int[]>(min, co.ToArray());

		}

		static public Tuple<double[], int[][]> minStorageIdentification(int a) // Returns the 'a' lowest storage weights.
		{
			Tuple<double, int[]> e = minStorageSimple();

			double[] floor = new double[a];
			floor[0] = e.Item1;

			int[][] co = new int[a][];
			co[0] = e.Item2.ToArray();

			shelf_Mutex.WaitOne();
			double ceiling = maxLoad;

			int z;

			for (int w = 0; w < (a - 1); w++)
			{
				ceiling = maxLoad;


				for (int s = 0; s < shelfMass.GetLength(0); s++)
				{
					for (int r = 0; r < shelfMass.GetLength(1); r++)
					{
						for (int sd = 0; sd < shelfMass.GetLength(2); sd++)
						{
							for (int c = 0; c < shelfMass.GetLength(3); c++)
							{



								if (shelfMass[s, r, sd, c] <= ceiling)  // New potential value.
								{

									if ((shelfMass[s, r, sd, c] >= floor[w]))
									{
										z = w + 1;
										for (int b = 0; b < (w + 1); b++)
										{
											if (((co[b][0] == s) && (co[b][1] == r) && (co[b][2] == sd) && (co[b][3] == c)))
											{
												z = b;
												break;
											}
										}

										if (z == (w + 1))
										{
											ceiling = shelfMass[s, r, sd, c];
											co[w + 1] = new int[4] { s, r, sd, c };
										}
									}

								}



							}
						}
					}
				}

				floor[w + 1] = ceiling;

			}

			shelf_Mutex.ReleaseMutex();
			return new Tuple<double[], int[][]>(floor.ToArray(), co.ToArray());

		}








		public static Queue<Tuple<double, int, int[], int[]>> unloadRobotOrders_Queue = new Queue<Tuple<double, int, int[], int[]>>();
		public static double queueMass = 0.0;
		public static Mutex unloadRobotOrders_Mutex = new Mutex();

		public static double ultimateCapacity = columns * subColumns * side * cubbard * maxLoad;

		public static int[][] unloadingPads		= new int[2][];
		public static int[][] loadingPads		= new int[2][];







		public const int numberOfItemTypes = 5;
		public static Queue<OrderClass> orders			= new Queue<OrderClass>();
		public static Mutex				orders_Mutex	= new Mutex();

		public static Queue<Tuple<double, int, int[], int[], int>> loadingRobotOrders_Queue = new Queue<Tuple<double, int, int[], int[], int>>();
		public static Mutex loadingRobotOrder_Mutex = new Mutex();

		public static Queue<OrderClass> underway_Queue	= new Queue<OrderClass>();
		public static Mutex				underway_Mutex	= new Mutex();

		public static Mutex status_Mutex = new Mutex();
		public static List<string> status = new List<string>(); 

		public static bool				truckBool		= false;
		public static Mutex				truckBool_Mutex = new Mutex();
		public static int				truckDelay		= 5000; 




		// Not in use
		public static void unloaderManager ()
        {
			programSwitch_Mutex.WaitOne();
			bool programContinuity = programSwitch;
			programSwitch_Mutex.ReleaseMutex();

			double[] requirements = new double[5];
			Random timeNum = new Random();

			maxLoad_Mutex.WaitOne();
			double maxMass = maxLoad;
			maxLoad_Mutex.ReleaseMutex();


			int[] equivalentUnits = new int[5] { 0, 0, 0, 0, 0 };
			double requirements_Mass = 0.0;
			Tuple<double[], int[][]> tupleStorage;
			Tuple<double, int, int[], int[]> instruction_Place; // 1 - mass;	2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate
			int n = 0;

			double gatheredMass = 0.0;



			int p = 0;

			// while (!programContinuity)
			while (p < 10)
			{
				p++;
				if (shortageBool() && (queueMass <= ultimateCapacity)) // When resources need to be re-stocked.
				{

					requirements = checkStorage().ToArray();

					for (int i = 0; i < 5; i++)
					{
						requirements_Mass += requirements[i];
					}

					for (int i = 1; i <= 252; i++) // Allocate the right cubbards for each available product.
					{
						gatheredMass = 0.0;
						tupleStorage = minStorageIdentification(i);

						for (int a = 0; a < i; a++)
						{
							gatheredMass += (maxLoad - tupleStorage.Item1[a]);
						}

						if (gatheredMass >= requirements_Mass)
						{
							n = i;
							break;
						}
					}

					int k = 0; // Key indices for the next while loop.
					int t = 0;
					tupleStorage = minStorageIdentification(n);

					int x, y;



					while ((k < 5) && (queueMass <= ultimateCapacity))
					{
						if (requirements[k] < 0.01)
						{
							k++;
							continue;
						}
						else if (requirements[k] > (maxMass - tupleStorage.Item1[t]))
						{       // The need for one type of item exceeds the first available storage space.
							if (requirements[k] < 0.01)
							{
								continue;
							}

							x = boundaryLeft + 1 + tupleStorage.Item2[t][0] + tupleStorage.Item2[t][2];
							y = boundaryTop + 1 + tupleStorage.Item2[t][1];
							instruction_Place = new Tuple<double, int, int[], int[]>((maxMass - tupleStorage.Item1[t]), k, new int[2] { x, y }, tupleStorage.Item2[t].ToArray());
							requirements[k] -= (maxMass - tupleStorage.Item1[t]);

							unloadRobotOrders_Mutex.WaitOne();
							Thread.Sleep(timeNum.Next(10, 500)); // Truck delivery delay.	
							unloadRobotOrders_Queue.Enqueue(instruction_Place);
							queueMass += (maxMass - tupleStorage.Item1[t]);
							unloadRobotOrders_Mutex.ReleaseMutex();

							t++;

						}
						else if ((maxMass - tupleStorage.Item1[t]) >= requirements[k])
						{       // Need for one type of item is below the available capacity for one cubbard.

							while ((k < 5) && ((maxMass - tupleStorage.Item1[t]) >= requirements[k]))
							{
								if (requirements[k] < 0.01)
								{
									break;
								}

								x = boundaryLeft + 1 + tupleStorage.Item2[t][0] + tupleStorage.Item2[t][2];
								y = boundaryTop + 1 + tupleStorage.Item2[t][1];
								instruction_Place = new Tuple<double, int, int[], int[]>((requirements[k]), k, new int[2] { x, y }, tupleStorage.Item2[t].ToArray());

								unloadRobotOrders_Mutex.WaitOne();
								Thread.Sleep(timeNum.Next(10, 500));                    // Truck delivery delay.	
								unloadRobotOrders_Queue.Enqueue(instruction_Place);
								queueMass += (requirements[k]);
								unloadRobotOrders_Mutex.ReleaseMutex();

								requirements[k] = 0.0;

								k++;

							}

						}
					}


					Thread.Sleep(5000);


				}
				else                    // When the warehouse is full, tell robots to recharge. TO DO
				{

					Thread.Sleep(5000);
					// Nothing necessary so far.

				}

				programSwitch_Mutex.WaitOne();
				programContinuity = programSwitch;
				programSwitch_Mutex.ReleaseMutex();

			}
		}




		// Not in use
		public static void robotUnloader ()
        {
			programSwitch_Mutex.WaitOne();
			bool programContinuity = programSwitch;
			programSwitch_Mutex.ReleaseMutex();

			RobotClass robot3 = new RobotClass(3, new int[2] { chargingLane, 2 }, new int[2] { chargingLane, 2 }, 0, boundaries, layoutReadWrite);
			RobotClass robot4 = new RobotClass(4, new int[2] { chargingLane, 3 }, new int[2] { chargingLane, 3 }, 0, boundaries, layoutReadWrite);

			robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);
			robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);

			Tuple<double, int, int[], int[]> instruction_Place3; // 1 - mass;		2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate
			Tuple<double, int, int[], int[]> instruction_Place4;


			int y = 0;

			// while (!programContinuity)
			while (y < 10)
			{
				y++;
				Console.WriteLine("Program Iteration: Robots");
				while (unloadRobotOrders_Queue.Count > 0)
				{
					if ((robot3.BatteryCheck()) || (robot4.BatteryCheck()))
					{ // At least one of the robots needs to re-charge.

						if ((robot3.BatteryCheck()) && (robot4.BatteryCheck()))
						{
							Console.WriteLine("Both charging.\n\n");


							Task rob3_Charge = Task.Run(() =>
							{
								robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);
								robot3.MovementLeft(layoutReadWrite);
								robot3.Charge();
								robot3.MovementRight(layoutReadWrite);
							});

							Task rob4_Charge = Task.Run(() =>
							{
								robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
								robot4.MovementLeft(layoutReadWrite);
								robot4.Charge();
								robot4.MovementRight(layoutReadWrite);
							});

							rob3_Charge.Wait();
							rob4_Charge.Wait();

						}
						else if ((robot3.BatteryCheck()))
						{
							Console.WriteLine("Robot 3 Charging.\n");

							bool charging = true;
							Mutex c = new Mutex();
							bool engaged = false;
							bool check = true;

							while (check)
							{
								if (!engaged)
								{
									Task rob3_Charge = Task.Run(() =>
									{
										robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);
										robot3.MovementLeft(layoutReadWrite);
										robot3.Charge();
										robot3.MovementRight(layoutReadWrite);
										c.WaitOne();
										charging = false;
										c.ReleaseMutex();
									});
									engaged = true;
								}

								Task rob4Unloading = Task.Run(() =>
								{
									robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);

									unloadRobotOrders_Mutex.WaitOne();
									instruction_Place4 = unloadRobotOrders_Queue.Dequeue();
									unloadRobotOrders_Mutex.ReleaseMutex();

									double m4 = instruction_Place4.Item1;
									int itm4 = instruction_Place4.Item2;
									int[] objDes4 = instruction_Place4.Item3.ToArray();
									int[] shelf_Co4 = instruction_Place4.Item4.ToArray();


									Task rob4 = Task.Run(() =>
									{
										robot4.ObjectiveStep(loadingPads[1], 1, layoutReadWrite);
										robot4.Load(itm4, m4);
									});

									rob4.Wait();


									rob4 = Task.Run(() =>
									{
										robot4.ObjectiveStep(objDes4, 1, layoutReadWrite);
										robot4.Unload(objDes4);
									});

									rob4.Wait();


									rob4 = Task.Run(() =>
									{
										robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
									});

									rob4.Wait();

								});

								c.WaitOne();
								check = charging;
								c.ReleaseMutex();
							}

						}
						else if ((robot4.BatteryCheck()))
						{
							Console.WriteLine("Robot 4 Charging.\n");

							bool charging = true;
							Mutex c = new Mutex();
							bool engaged = false;
							bool check = true;

							while (check)
							{
								if (!engaged)
								{
									Task rob4_Charge = Task.Run(() =>
									{
										robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
										robot4.MovementLeft(layoutReadWrite);
										robot4.Charge();
										robot4.MovementRight(layoutReadWrite);
										c.WaitOne();
										charging = false;
										c.ReleaseMutex();
									});
									engaged = true;
								}

								Task rob3Unloading = Task.Run(() =>
								{
									robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);

									unloadRobotOrders_Mutex.WaitOne();
									instruction_Place3 = unloadRobotOrders_Queue.Dequeue();
									unloadRobotOrders_Mutex.ReleaseMutex();

									double m3 = instruction_Place3.Item1;
									int itm3 = instruction_Place3.Item2;
									int[] objDes3 = instruction_Place3.Item3.ToArray();
									int[] shelf_Co3 = instruction_Place3.Item4.ToArray();

									Task rob3 = Task.Run(() =>
									{
										robot3.ObjectiveStep(loadingPads[0], 1, layoutReadWrite);
										robot3.Load(itm3, m3);
									});

									rob3.Wait();

									rob3 = Task.Run(() =>
									{
										robot3.ObjectiveStep(objDes3, 1, layoutReadWrite);
										robot3.Unload(objDes3);
									});

									rob3.Wait();

									rob3 = Task.Run(() =>
									{
										robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);
									});

									rob3.Wait();

								});

								c.WaitOne();
								check = charging;
								c.ReleaseMutex();
							}

						}

					}
					else
					{   // No charging.

						Console.WriteLine("Moving.");

						unloadRobotOrders_Mutex.WaitOne();
						instruction_Place3 = unloadRobotOrders_Queue.Dequeue();
						instruction_Place4 = unloadRobotOrders_Queue.Dequeue();
						unloadRobotOrders_Mutex.ReleaseMutex();



						double m3 = instruction_Place3.Item1;
						int itm3 = instruction_Place3.Item2;
						int[] objDes3 = instruction_Place3.Item3.ToArray();
						int[] shelf_Co3 = instruction_Place3.Item4.ToArray();

						double m4 = instruction_Place4.Item1;
						int itm4 = instruction_Place4.Item2;
						int[] objDes4 = instruction_Place4.Item3.ToArray();
						int[] shelf_Co4 = instruction_Place4.Item4.ToArray();


						Thread threadR3 = new Thread(() =>
						{
							robot3.ObjectiveStep(loadingPads[0], 1, layoutReadWrite);
							robot3.Load(itm3, m3);
						});


						Thread threadR4 = new Thread(() =>
						{
							robot4.ObjectiveStep(loadingPads[1], 1, layoutReadWrite);
							robot4.Load(itm4, m4);
						});

						threadR3.Start();
						threadR4.Start();
						threadR3.Join();
						threadR4.Join();


						threadR3 = new Thread(() =>
						{
							robot3.ObjectiveStep(objDes3, 1, layoutReadWrite);
							robot3.Unload(objDes3);
						});


						threadR4 = new Thread(() =>
						{
							robot4.ObjectiveStep(objDes4, 1, layoutReadWrite);
							robot4.Unload(objDes4);
						});

						threadR3.Start();
						threadR4.Start();
						threadR3.Join();
						threadR4.Join();




						threadR3 = new Thread(() =>
						{
							robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);

						});


						threadR4 = new Thread(() =>
						{

							robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
						});

						threadR3.Start();
						threadR4.Start();
						threadR3.Join();
						threadR4.Join();



						


						Console.WriteLine("Robot 3: Unloaded {0} grams of item {1}", instruction_Place3.Item1, instruction_Place3.Item2);
						Console.WriteLine("Robot 4: Unloaded {0} grams of item {1}\n", instruction_Place4.Item1, instruction_Place4.Item2);
						LayoutPrint(layoutReadWrite);
					}

				}


				programSwitch_Mutex.WaitOne();
				programContinuity = programSwitch;
				programSwitch_Mutex.ReleaseMutex();

			}

		}






		public static bool programButton	= true;
		public static string command;

		static void Main(string[] args)
		{

			for (int i = 0; i < 4; i++)
            {
				robot_Mutex[i] = new Mutex();
            }

			for (int a = 0; a < length; a++)
			{

				for (int b = 0; b < width; b++)
				{
					layout[b, a] = 'o';
				}

			}

			unloadingPads[0]	= new int[2] { 6, 4 };
			unloadingPads[1]	= new int[2] { 5, 4 };

			loadingPads[0]		= new int[2] { 4, 4 };
			loadingPads[1]		= new int[2] { 3, 4 };

			OrderClass[] testOrders = new OrderClass[] 
			{

				new OrderClass(	1,	3,	8,	0,	0, 5000		), 
				new OrderClass(	0,	0,	2,	15,	1, 9000		),
				new OrderClass(	0,	6,	12,	3,	5, 10000	),
				new OrderClass(	5,	2,	0,	0,	0, 1000		),
				new OrderClass(	0,	0,	8,	0,	1, 8000		)

			};

			for (int i = 0; i < testOrders.Length; i++)
            {
				orders.Enqueue(testOrders[i]);
            }

			int o = orders.Count;

			Random rNum = new Random();

			for (int s = 0; s < shelfMass.GetLength(0); s++)
            {
				for (int r = 0; r < shelfMass.GetLength(1); r++)
				{
					for (int sd = 0; sd < shelfMass.GetLength(2); sd++)
					{
						for (int c = 0; c < shelfMass.GetLength(3); c++)
						{

							shelfMass[s, r, sd, c] = 0.0;

							item1Tracker[s, r, sd, c] = new Item1(0);
							item2Tracker[s, r, sd, c] = new Item2(0);
							item3Tracker[s, r, sd, c] = new Item3(0);
							item4Tracker[s, r, sd, c] = new Item4(0);
							item5Tracker[s, r, sd, c] = new Item5(0);

						}
					}
				}
			}

			shelfMass[2, 1, 0, 3] = 5000.0;
			item1Tracker[2, 1, 0, 3].UpdateRecord(500);
			shelfMass[2, 0, 0, 3] = 5000.0;
			item2Tracker[2, 0, 0, 3].UpdateRecord(1000);
			shelfMass[3, 1, 0, 3] = 5000.0;
			item3Tracker[3, 1, 0, 3].UpdateRecord(200);
			shelfMass[1, 0, 0, 3] = 5000.0;
			item4Tracker[1, 0, 0, 3].UpdateRecord(2500);
			shelfMass[1, 1, 0, 3] = 5000.0;
			item5Tracker[1, 1, 0, 3].UpdateRecord(250);


			//TruckClass[] truckFleet = new TruckClass[numberOfTrucks];

			//for (int i = 0; i < numberOfTrucks; i++)
			//         {

			//	int[] t = new int[5];

			//	for (int k = 0; k < 5; k++)
			//             {
			//		if (((i + k) % 5) == 0)
			//		{
			//			t[k] = 1;
			//		}
			//		else
			//                 {
			//			t[k] = 0;
			//                 }
			//             }

			//	truckFleet[i] = new TruckClass(i, new StorageLoad( 
			//			  ( (int) (TruckClass.truckMaxLoad / Item1.massPerUnit)) *  t[0]
			//			, ( (int) (TruckClass.truckMaxLoad / Item2.massPerUnit)) *  t[1]
			//			, ( (int) (TruckClass.truckMaxLoad / Item3.massPerUnit)) *  t[2]
			//			, ( (int) (TruckClass.truckMaxLoad / Item4.massPerUnit)) *  t[3]
			//			, ( (int) (TruckClass.truckMaxLoad / Item5.massPerUnit)) *  t[4]
			//			));

			//	Console.WriteLine("{0}, {1}, {2}, {3}, {4}\n\n", truckFleet[i].currentLoad.item1Load.units, truckFleet[i].currentLoad.item2Load.units, truckFleet[i].currentLoad.item3Load.units, truckFleet[i].currentLoad.item4Load.units, truckFleet[i].currentLoad.item5Load.units);

			//         }

			//
			//FullTest02 ftest02 = new FullTest02();
			////


			//// --- Thread threadQueue = new Thread(() => unloaderManager());
			//Thread threadQueue = new Thread(() => ftest02.unloaderManager());
			//threadQueue.Start();

			//// --- Thread robotUnload = new Thread(() => robotUnloader());
			//Thread robotUnload = new Thread(() => ftest02.robotUnloader());
			//robotUnload.Start();

			//threadQueue.Join();
			//robotUnload.Join();

			/*
			 * Save for later
			 */

			//RobotClass[]	robots		= new RobotClass[4];
			//Task[]			robotSetup	= new Task[numberOfRobots];
			//for (int i = 0; i < robots.Length; i++)
			//         {
			//	robotSetup[i] = Task.Run(() =>
			//	{
			//	   robots[i] = new RobotClass(i, new int[] { boundaryLeft, i }, new int[] { 0, 0 }, 0, boundaries, layoutReadWrite);
			//	});
			//	// REMEMBER TO USE Wait() method!!!
			//         }

			//shelfMass[3, 1, 1, 3] = 50.0;
			//shelfMass[3, 2, 1, 5] = 99.0;

			//Tuple<double[], int[][]> test = minStorageIdentification(6);
			//for (int i = 0; i < 6; i++)
			//{
			//	Console.Write("{0}\t", test.Item1[i]);
			//}
			//Console.WriteLine();
			//for (int i = 0; i < 6; i++)
			//         {
			//	Console.Write("{0} {1} {2} {3}\t\t", test.Item2[i][0], test.Item2[i][1], test.Item2[i][2], test.Item2[i][3]);
			//         }



			/*
			 * This next part manages the re-stocking queue for the robots to follow.
			 * 
			 */

			//for (int i = 0; i < 6; i++)
			//         {
			//	LayoutPrint(layoutReadWrite);
			//	Thread.Sleep(15000);
			//         }





			//Thread.Sleep(18000);

			//shelf_Mutex.WaitOne();
			//shelfMass[3, 1, 0, 5] = 300.0;
			//shelf_Mutex.ReleaseMutex();

			//for (int i = 1; i <= 8; i++)
			//         {
			//	Console.WriteLine();
			//	Tuple<double[], int[][]> placeholder = maxStorageIdentification(i);

			//	for (int k = 0; k < i; k++)
			//             {
			//		Console.Write("{0} {1} {2} {3} {4}\t",placeholder.Item1[k], placeholder.Item2[k][0], placeholder.Item2[k][1], placeholder.Item2[k][2], placeholder.Item2[k][3]);
			//             }
			//	Console.WriteLine();

			//         }



			Task unloadManager = Task.Run(() =>
			{
				//Console.WriteLine("Inside Thread");
				//programSwitch_Mutex.WaitOne();
				//bool programContinuity = programSwitch;
				//programSwitch_Mutex.ReleaseMutex();

				double[] requirements = new double[5];
				Random timeNum = new Random();

				maxLoad_Mutex.WaitOne();
				double maxMass = maxLoad;
				maxLoad_Mutex.ReleaseMutex();


				int[] equivalentUnits = new int[5] { 0, 0, 0, 0, 0 };
				double requirements_Mass = 0.0;
				Tuple<double[], int[][]> tupleStorage;
				Tuple<double, int, int[], int[]> instruction_Place; // 1 - mass;	2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate
				int n = 0;

				double gatheredMass = 0.0;

				//int p = 0;

				//// while (!programContinuity)
				//while (p < 10)
				//{
				//	p++;
				if (shortageBool() && (queueMass <= ultimateCapacity)) // When resources need to be re-stocked.
				{

					requirements = checkStorage().ToArray();

					for (int i = 0; i < 5; i++)
					{
						requirements_Mass += requirements[i];
					}

					for (int i = 1; i <= 252; i++) // Allocate the right cubbards for each available product.
					{

						gatheredMass = 0.0;
						tupleStorage = minStorageIdentification(i);

						for (int a = 0; a < i; a++)
						{
							gatheredMass += (maxLoad - tupleStorage.Item1[a]);
						}

						if (i == 252)
                        {
							Thread.Sleep(10);
                        }

						if (gatheredMass >= requirements_Mass)
						{
							n = i;
							break;
						}

					}

					int k = 0; // Key indices for the next while loop.
					int t = 0;
					tupleStorage = minStorageIdentification(n);

					int x, y;


					// && (queueMass <= ultimateCapacity)
					while ((k < 5) && (t <= n))
					{
						if (requirements[k] < 0.01)
						{
							k++;
							continue;
						}
						else if (requirements[k] > (maxMass - tupleStorage.Item1[t]))
						{       // The need for one type of item exceeds the first available storage space.
							if (requirements[k] < 0.01)
							{
								continue;
							}

							x = boundaryLeft + 1 + tupleStorage.Item2[t][0] + tupleStorage.Item2[t][2];
							y = boundaryTop + 1 + tupleStorage.Item2[t][1];
							instruction_Place = new Tuple<double, int, int[], int[]>((maxMass - tupleStorage.Item1[t]), k, new int[2] { x, y }, tupleStorage.Item2[t].ToArray());
							requirements[k] -= (maxMass - tupleStorage.Item1[t]);

							unloadRobotOrders_Mutex.WaitOne();
							Thread.Sleep(timeNum.Next(10, 500)); // Truck delivery delay.	
							unloadRobotOrders_Queue.Enqueue(instruction_Place);
							queueMass += (maxMass - tupleStorage.Item1[t]);
							unloadRobotOrders_Mutex.ReleaseMutex();

							t++;

						}
						else if ((maxMass - tupleStorage.Item1[t]) >= requirements[k])
						{       // Need for one type of item is below the available capacity for one cubbard.

							if (requirements[k] < 0.01)
							{
								k++;
								continue;
							}

							x = boundaryLeft + 1 + tupleStorage.Item2[t][0] + tupleStorage.Item2[t][2];
							y = boundaryTop + 1 + tupleStorage.Item2[t][1];
							instruction_Place = new Tuple<double, int, int[], int[]>((requirements[k]), k, new int[2] { x, y }, tupleStorage.Item2[t].ToArray());



							unloadRobotOrders_Mutex.WaitOne();
							Thread.Sleep(timeNum.Next(10, 500));                    // Truck delivery delay.	
							unloadRobotOrders_Queue.Enqueue(instruction_Place);
							queueMass += (requirements[k]);
							unloadRobotOrders_Mutex.ReleaseMutex();

							requirements[k] = 0.0;

							k++;



						}
					}


					Thread.Sleep(5000);


				}
				else                    // When the warehouse is full, tell robots to recharge. TO DO
				{

					Thread.Sleep(5000);
					// Nothing necessary so far.

				}

				//programSwitch_Mutex.WaitOne();
				//programContinuity = programSwitch;
				//programSwitch_Mutex.ReleaseMutex();

				//}

			});

			Task robot3Unload = Task.Run(() =>
			{
				//Console.WriteLine("Inside Thread");
				//programSwitch_Mutex.WaitOne();
				//bool programContinuity = programSwitch;
				//programSwitch_Mutex.ReleaseMutex();

				robot_Mutex[2].WaitOne();
				RobotClass robot3 = new RobotClass(2, new int[2] { boundaryLeft, 2 }, new int[2] { boundaryLeft, 2 }, 0, boundaries, layoutReadWrite);
				

				robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite	);
				robot_Mutex[2].ReleaseMutex();

				Tuple<double, int, int[], int[]> instruction_Place3; // 1 - mass;		2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate


				//int y = 0;

				//// while (!programContinuity)
				//while (y < 10)
				//{
				//	y++;
				unloadRobotOrders_Mutex.WaitOne();
				int restockCount = unloadRobotOrders_Queue.Count;
				unloadRobotOrders_Mutex.ReleaseMutex();

				//Console.WriteLine("Program Iteration: Robots");
				//Console.WriteLine("Restock Count: {0}", restockCount);
				

				while( restockCount >= 0 )
				{

					//Console.WriteLine("Inside \" while\" loop.\nRestock Count {0}", restockCount);
					robot_Mutex[2].WaitOne();
					 if ( robot3.BatteryCheck() )
					{ // At least one of the robots needs to re-charge.

						// Console.WriteLine("Robot 3 charging.\n\n");
						

						robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 5, layoutReadWrite);
						robot3.MovementLeft(layoutReadWrite);
						robot3.Charge();
						robot3.MovementRight(layoutReadWrite);

					}
					else if (restockCount == 0)
					{
						robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 5, layoutReadWrite);
						
						robot3.MovementLeft(layoutReadWrite);
						Thread.Sleep(5000);
						robot3.MovementRight(layoutReadWrite);
					}
					else
					{   // No charging.

						//Console.WriteLine("Unloading movement.");

						robot3.ObjectiveStep(new int[2] { boundaryLeft, robot3.id }, 0, layoutReadWrite);

						unloadRobotOrders_Mutex.WaitOne();
						instruction_Place3 = unloadRobotOrders_Queue.Dequeue();
						unloadRobotOrders_Mutex.ReleaseMutex();



						double m3 = instruction_Place3.Item1;
						int itm3 = instruction_Place3.Item2;
						int[] objDes3 = instruction_Place3.Item3.ToArray();
						int[] shelf_Co3 = instruction_Place3.Item4.ToArray();


						robot3.ObjectiveStep(new int[] { loadingPads[0][0], loadingPads[0][1] }, 1, layoutReadWrite);
						robot3.Load(itm3, m3);


						robot3.ObjectiveStep(new int[2] { objDes3[0], objDes3[1]}, 2, layoutReadWrite);
						robot3.Unload(shelf_Co3.ToArray());


						// Console.WriteLine("Robot 3: Unloaded {0} grams of item {1}", instruction_Place3.Item1, instruction_Place3.Item2);
						
						// LayoutPrint(layoutReadWrite);

					}
					robot_Mutex[2].ReleaseMutex();

					unloadRobotOrders_Mutex.WaitOne();
					restockCount = unloadRobotOrders_Queue.Count;
					unloadRobotOrders_Mutex.ReleaseMutex();

				}

				// Console.WriteLine("Out of loop");

				//	programSwitch_Mutex.WaitOne();
				//	programContinuity = programSwitch;
				//	programSwitch_Mutex.ReleaseMutex();

				//}

            });

            Task robot4Unload = Task.Run(() =>
            {

				robot_Mutex[3].WaitOne();
				RobotClass robot4 = new RobotClass(3, new int[2] { boundaryLeft, 3 }, new int[2] { boundaryLeft, 3 }, 0, boundaries, layoutReadWrite);

                robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
				robot_Mutex[3].ReleaseMutex();

				Tuple<double, int, int[], int[]> instruction_Place4;

                //int y = 0;

                //// while (!programContinuity)
                //while (y < 10)
                //{
                //y++;

                //Console.WriteLine("Program Iteration: Robots");
                unloadRobotOrders_Mutex.WaitOne();
                int restockCount = unloadRobotOrders_Queue.Count;
                unloadRobotOrders_Mutex.ReleaseMutex();

				while (restockCount >= 0)
				{

					robot_Mutex[3].WaitOne();
					//Console.WriteLine("Inside \" while\" loop.\nRestock Count {0}", restockCount);
					if(robot4.BatteryCheck())
                    { // At least one of the robots needs to re-charge.

                        //Console.WriteLine("Robot 4 charging.\n\n");


                        robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 5, layoutReadWrite);
                        robot4.MovementLeft(layoutReadWrite);
                        robot4.Charge();
                        robot4.MovementRight(layoutReadWrite);

                    }
					else if (restockCount == 0)
					{
						robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);
						robot4.MovementLeft(layoutReadWrite);
						Thread.Sleep(5000);
						robot4.MovementRight(layoutReadWrite);
					}
					else
                    {   // No charging.

						//Console.WriteLine("Unloading movement.");

						robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);

						unloadRobotOrders_Mutex.WaitOne();
                        instruction_Place4 = unloadRobotOrders_Queue.Dequeue();
                        unloadRobotOrders_Mutex.ReleaseMutex();


                        double m4 = instruction_Place4.Item1;
                        int itm4 = instruction_Place4.Item2;
                        int[] objDes4 = instruction_Place4.Item3.ToArray();
                        int[] shelf_Co4 = instruction_Place4.Item4.ToArray();

                        robot4.ObjectiveStep( new int[] { loadingPads[1][0], loadingPads[1][1] }, 1, layoutReadWrite);
                        robot4.Load(itm4, m4);


                        robot4.ObjectiveStep(new int[] { objDes4[0], objDes4[1] }, 2, layoutReadWrite);
                        robot4.Unload(shelf_Co4.ToArray());



                        //Console.WriteLine("Robot 4: Unloaded {0} grams of item {1}\n", instruction_Place4.Item1, instruction_Place4.Item2);
                        //LayoutPrint(layoutReadWrite);

						robot4.ObjectiveStep(new int[2] { boundaryLeft, robot4.id }, 0, layoutReadWrite);

					}

					robot_Mutex[3].ReleaseMutex();


					unloadRobotOrders_Mutex.WaitOne();
                    restockCount = unloadRobotOrders_Queue.Count;
                    unloadRobotOrders_Mutex.ReleaseMutex();

                }


                //	programSwitch_Mutex.WaitOne();
                //	programContinuity = programSwitch;
                //	programSwitch_Mutex.ReleaseMutex();

                //}
            });




			Task loadingManagement = Task.Run(() =>
			{

				double[] itemMass = new double[5];
				int[] itemNumber = new int[5];
				Tuple<double[], int[][]>[] tupleStorage = new Tuple<double[], int[][]>[5];
				Tuple<double, int, int[], int[], int> instructionPlace;
				double requiredMass = 0.0;
				double accumulatedMass = 0.0;
				double deliveryMass = 0.0;

				orders_Mutex.WaitOne();
				int numOrders = orders.Count;
				orders_Mutex.ReleaseMutex();

				while (numOrders >= 0)
				{
					if (numOrders > 0)
					{
						orders_Mutex.WaitOne();
						numOrders = orders.Count;
						orders_Mutex.ReleaseMutex();

						// Send the truck away.
						if ((numOrders == 0) || ((deliveryMass + orders.Peek().totalMass) > maxTruckLoad))
						{
							if (deliveryMass < 0.01)
                            {
								Thread.Sleep(2500);
								continue;
                            }

							truckBool_Mutex.WaitOne();
							truckBool = true;
							truckBool_Mutex.ReleaseMutex();

							underway_Mutex.WaitOne();

							//foreach (string output in status)
							//{
							//	Console.WriteLine(output);
							//}

							deliveryMass = 0.0;

							underway_Mutex.ReleaseMutex();

							Thread.Sleep(truckDelay);

							truckBool_Mutex.WaitOne();
							truckBool = false;
							truckBool_Mutex.ReleaseMutex();
						}

						orders_Mutex.WaitOne();
						OrderClass placeholderOrder = orders.Peek();
						orders_Mutex.ReleaseMutex();

						requiredMass = 0.0;
						accumulatedMass = 0.0;
						deliveryMass += placeholderOrder.totalMass;

						for (int i = 0; i < 5; i++)
						{

							if (i == 0)
							{
								itemMass[i] = placeholderOrder.item1.totalMass;
								requiredMass += itemMass[i];
								itemNumber[i] = placeholderOrder.item1.units;
							}
							else if (i == 1)
							{
								itemMass[i] = placeholderOrder.item2.totalMass;
								requiredMass += itemMass[i];
								itemNumber[i] = placeholderOrder.item2.units;
							}
							else if (i == 2)
							{
								itemMass[i] = placeholderOrder.item3.totalMass;
								requiredMass += itemMass[i];
								itemNumber[i] = placeholderOrder.item3.units;
							}
							else if (i == 3)
							{
								itemMass[i] = placeholderOrder.item4.totalMass;
								requiredMass += itemMass[i];
								itemNumber[i] = placeholderOrder.item4.units;
							}
							else if (i == 4)
							{
								itemMass[i] = placeholderOrder.item5.totalMass;
								requiredMass += itemMass[i];
								itemNumber[i] = placeholderOrder.item5.units;
							}

						}


						int k = 0;
						int t = 0;
						int n = 0;

						bool emptyFlag = false;


						for (int l = 0; l < 5; l++)
						{
							for (int i = 1; i <= 252; i++) // Allocate the right cubbards for each available product.
							{

								tupleStorage[0] = maxStorageIdentification1(i);
								tupleStorage[1] = maxStorageIdentification2(i);
								tupleStorage[2] = maxStorageIdentification3(i);
								tupleStorage[3] = maxStorageIdentification4(i);
								tupleStorage[4] = maxStorageIdentification5(i);

								for (int a = 0; a < i; a++)
								{
									accumulatedMass += tupleStorage[0].Item1[a] + tupleStorage[1].Item1[a] + tupleStorage[2].Item1[a] + tupleStorage[3].Item1[a] + tupleStorage[4].Item1[a];
								}

								if (accumulatedMass >= requiredMass)
								{
									n = i;
									break;
								}
								else if (i == 252)
								{
									emptyFlag = true;
								}

							}
						}

						if (emptyFlag == true)
                        {
							continue;
                        }

						tupleStorage[0] = maxStorageIdentification1(n);
						tupleStorage[1] = maxStorageIdentification2(n);
						tupleStorage[2] = maxStorageIdentification3(n);
						tupleStorage[3] = maxStorageIdentification4(n);
						tupleStorage[4] = maxStorageIdentification5(n);

						double insertMass;
						int itemNum;
						int[] robotCo = new int[2];
						int[] shelfCo = new int[4];
						int x, y;
						int time = (int) ((double) placeholderOrder.orderTime / (double) n);

						//Console.WriteLine("Processing.");
						while ((k < 5) && (t < n))
						{

							if (itemMass[k] < 0.01)
							{
								k++;
							}
							else if ( itemMass[k] > tupleStorage[k].Item1[t] )
							{
								x = boundaryLeft + 1 + tupleStorage[k].Item2[t][0] + tupleStorage[k].Item2[t][2];
								y = boundaryTop + 1 + tupleStorage[k].Item2[t][1];

								insertMass = tupleStorage[k].Item1[t];
								itemNum = k;
								robotCo = new int[2] { x, y };
								shelfCo = new int[4] { tupleStorage[k].Item2[t][0], tupleStorage[k].Item2[t][1], tupleStorage[k].Item2[t][2], tupleStorage[k].Item2[t][3] };
							

								instructionPlace = new Tuple<double, int, int[], int[], int>(insertMass, itemNum, new int[2] { robotCo[0], robotCo[1] }, new int[4]{shelfCo[0], shelfCo[1], shelfCo[2], shelfCo[3]}, time );

								loadingRobotOrder_Mutex.WaitOne();
								loadingRobotOrders_Queue.Enqueue(instructionPlace);
								loadingRobotOrder_Mutex.ReleaseMutex();


								t++;

							}
							else if (itemMass[k] <= tupleStorage[k].Item1[t])
							{

								x = boundaryLeft + 1 + tupleStorage[k].Item2[t][0] + tupleStorage[k].Item2[t][2];
								y = boundaryTop + 1 + tupleStorage[k].Item2[t][1];

								insertMass = itemMass[k];
								itemNum = k;
								robotCo = new int[2] { x, y };
								shelfCo = new int[4] { tupleStorage[k].Item2[t][0], tupleStorage[k].Item2[t][1], tupleStorage[k].Item2[t][2], tupleStorage[k].Item2[t][3] };

								instructionPlace = new Tuple<double, int, int[], int[], int>(insertMass, itemNum, new int[2] { robotCo[0], robotCo[1] }, new int[4] { shelfCo[0], shelfCo[1], shelfCo[2], shelfCo[3] }, time);

								loadingRobotOrder_Mutex.WaitOne();
								loadingRobotOrders_Queue.Enqueue(instructionPlace);
								loadingRobotOrder_Mutex.ReleaseMutex();

								k++;

							}

						
						}


						// Send the truck away.
						if ((orders.Count == 0) || ((deliveryMass + orders.Peek().totalMass) > maxTruckLoad))
						{
							truckBool_Mutex.WaitOne();
							truckBool = true;
							truckBool_Mutex.ReleaseMutex();

							underway_Mutex.WaitOne();

							//foreach (string output in status)
							//{
							//	Console.WriteLine(output);
							//}

							deliveryMass = 0.0;

							underway_Mutex.ReleaseMutex();

							Thread.Sleep(truckDelay);

							truckBool_Mutex.WaitOne();
							truckBool = false;
							truckBool_Mutex.ReleaseMutex();
						}

						DateTime timeStamp = new DateTime();

						orders_Mutex.WaitOne();
						timeStamp = DateTime.Now;
						status.Add("Total Mass:" + orders.Peek().orderTime + "\t\tDelivery Time: " + orders.Peek().totalMass + "\t\tTime Stamp of Completion: " + timeStamp.ToString());
						orders.Dequeue();
						orders_Mutex.ReleaseMutex();


						loadingRobotOrder_Mutex.WaitOne();
						int oNum = loadingRobotOrders_Queue.Count;
						loadingRobotOrder_Mutex.ReleaseMutex();

						while (oNum > 0)
						{
							Thread.Sleep(7500);
							loadingRobotOrder_Mutex.WaitOne();
							oNum = loadingRobotOrders_Queue.Count;
							loadingRobotOrder_Mutex.ReleaseMutex();
						}
					}
					else
                    {
						// Send the truck away
						if ( orders.Count == 0 )
						{
							truckBool_Mutex.WaitOne();
							truckBool = true;
							truckBool_Mutex.ReleaseMutex();

							underway_Mutex.WaitOne();

							//foreach (string output in status)
							//{
							//	Console.WriteLine(output);
							//}

							deliveryMass = 0.0;

							underway_Mutex.ReleaseMutex();

							Thread.Sleep(truckDelay);

							truckBool_Mutex.WaitOne();
							truckBool = false;
							truckBool_Mutex.ReleaseMutex();
						}

						Thread.Sleep(7500);
					}

					orders_Mutex.WaitOne();
					numOrders = orders.Count;
					orders_Mutex.ReleaseMutex();
				}


            });

			Task robot1Load = Task.Run(() =>
			{

				int oNum;

				robot_Mutex[0].WaitOne();
				RobotClass robot1 = new RobotClass(0, new int[2] {boundaryLeft, 0}, new int[2] { boundaryLeft, 0 }, 0, boundaries, layoutReadWrite);
				

				Tuple<double, int, int[], int[], int> instruction_Place1;       // 1 - mass;		2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate

				
				robot1.ObjectiveStep(new int[2] { boundaryLeft, robot1.id }, 0, layoutReadWrite);
				robot_Mutex[0].ReleaseMutex();

				loadingRobotOrder_Mutex.WaitOne();
				oNum = loadingRobotOrders_Queue.Count;
				loadingRobotOrder_Mutex.ReleaseMutex();

				while (oNum >= 0)
				{

					robot_Mutex[0].WaitOne();

					if (truckBool)
					{
						robot1.ObjectiveStep(new int[2] { boundaryLeft, robot1.id }, 5, layoutReadWrite);
						Thread.Sleep(truckDelay);
                    }
					else if (oNum == 0)
					{
						robot1.ObjectiveStep(new int[2] { boundaryLeft, robot1.id }, 5, layoutReadWrite);
						robot1.MovementLeft(layoutReadWrite);
						Thread.Sleep(5000);
						robot1.MovementRight(layoutReadWrite);
						
					}
					else if (robot1.BatteryCheck())
					{ // At least one of the robots needs to re-charge.

						//Console.WriteLine("Robot 1 charging.\n\n");

						
						robot1.ObjectiveStep(new int[2] { boundaryLeft, robot1.id }, 5, layoutReadWrite);
						robot1.MovementLeft(layoutReadWrite);
						robot1.Charge();
						robot1.MovementRight(layoutReadWrite);

					}
                    else 
					{

						//Console.WriteLine("Loading movement.");


						
						instruction_Place1 = loadingRobotOrders_Queue.Dequeue();



						double		m1			= instruction_Place1.Item1;
						int			itm1		= instruction_Place1.Item2;
						int[]		objDes1		= instruction_Place1.Item3.ToArray();
						int[]		shelf_Co1	= instruction_Place1.Item4.ToArray();
						int			time1		= instruction_Place1.Item5;

						
						robot1.ObjectiveStep(new int[2] { objDes1[0], objDes1[1] }, 2, layoutReadWrite);
						robot1.Remove(new int[] { shelf_Co1[0], shelf_Co1[1], shelf_Co1[2], shelf_Co1[3] }, m1, itm1);
						//Console.WriteLine("Robot 1: At {2}, {3}, {4}, {5}\nRemoved {0} grams of item {1}", instruction_Place1.Item1, instruction_Place1.Item2, shelf_Co1[0], shelf_Co1[1], shelf_Co1[2], shelf_Co1[3]);

						robot1.ObjectiveStep(new int[] { loadingPads[0][0], loadingPads[0][1] }, 1, layoutReadWrite);
						robot1.Deliver(time1);

						//Console.WriteLine("Robot 1: Delivered {0} grams of item {1}", instruction_Place1.Item1, instruction_Place1.Item2);

						//LayoutPrint(layoutReadWrite);

						robot1.ObjectiveStep(new int[2] { boundaryLeft, robot1.id }, 0, layoutReadWrite);

					}

					robot_Mutex[0].ReleaseMutex();


					loadingRobotOrder_Mutex.WaitOne();
					oNum = loadingRobotOrders_Queue.Count;
					loadingRobotOrder_Mutex.ReleaseMutex();

				}

			});

			Task robot2Load = Task.Run(() =>
			{

				int oNum;

				robot_Mutex[1].WaitOne();
				RobotClass robot2 = new RobotClass(1, new int[2] { boundaryLeft, 1 }, new int[2] { boundaryLeft, 1 }, 0, boundaries, layoutReadWrite);


				Tuple<double, int, int[], int[], int> instruction_Place2;       // 1 - mass;		2 - item category;		3 - x,y co-ordinate;	4 - shelf co-ordinate


				robot2.ObjectiveStep(new int[2] { boundaryLeft, robot2.id }, 0, layoutReadWrite);
				robot_Mutex[1].ReleaseMutex();

				loadingRobotOrder_Mutex.WaitOne();
				oNum = loadingRobotOrders_Queue.Count;
				loadingRobotOrder_Mutex.ReleaseMutex();

				while (oNum >= 0)
				{

					robot_Mutex[1].WaitOne();
					if (truckBool)
					{
						robot2.ObjectiveStep(new int[2] { boundaryLeft, robot2.id }, 5, layoutReadWrite);
						Thread.Sleep(truckDelay);
					}
					else if (oNum == 0)
					{
						robot2.ObjectiveStep(new int[2] { boundaryLeft, robot2.id }, 5, layoutReadWrite);

						robot2.MovementLeft(layoutReadWrite);
						Thread.Sleep(5000);
						robot2.MovementRight(layoutReadWrite);

					}
					else if (robot2.BatteryCheck())
					{ // At least one of the robots needs to re-charge.

						//Console.WriteLine("Robot 2 charging.\n\n");


						robot2.ObjectiveStep(new int[2] { boundaryLeft, robot2.id }, 5, layoutReadWrite);
						robot2.MovementLeft(layoutReadWrite);
						robot2.Charge();
						robot2.MovementRight(layoutReadWrite);

					}
					else
					{

						//Console.WriteLine("Loading movement.");


						loadingRobotOrder_Mutex.WaitOne();
						instruction_Place2 = loadingRobotOrders_Queue.Dequeue();
						loadingRobotOrder_Mutex.ReleaseMutex();



						double m2 = instruction_Place2.Item1;
						int itm2 = instruction_Place2.Item2;
						int[] objDes2 = instruction_Place2.Item3.ToArray();
						int[] shelf_Co2 = instruction_Place2.Item4.ToArray();
						int time2 = instruction_Place2.Item5;

						robot2.ObjectiveStep(new int[2] { objDes2[0], objDes2[1] }, 2, layoutReadWrite);
						robot2.Remove(new int[] { shelf_Co2[0], shelf_Co2[1], shelf_Co2[2], shelf_Co2[3] }, m2, itm2);
						//Console.WriteLine("Robot 2: At {2}, {3}, {4}, {5}\nRemoved {0} grams of item {1}", instruction_Place2.Item1, instruction_Place2.Item2, shelf_Co2[0], shelf_Co2[1], shelf_Co2[2], shelf_Co2[3]);

						robot2.ObjectiveStep(new int[] { loadingPads[0][0], loadingPads[0][1] }, 1, layoutReadWrite);
						robot2.Deliver(time2);

						//Console.WriteLine("Robot 2: Delivered {0} grams of item {1}", instruction_Place2.Item1, instruction_Place2.Item2);

						//LayoutPrint(layoutReadWrite);

						robot2.ObjectiveStep(new int[2] { boundaryLeft, robot2.id }, 0, layoutReadWrite);

					}
					robot_Mutex[1].ReleaseMutex();


					loadingRobotOrder_Mutex.WaitOne();
					oNum = loadingRobotOrders_Queue.Count;
					loadingRobotOrder_Mutex.ReleaseMutex();

				}

			});


			programSwitch_Mutex.WaitOne();
			programSwitch = true;
			programButton = programSwitch;
			programSwitch_Mutex.ReleaseMutex();
			
			while (programButton)
            {
				Console.WriteLine("Insert Command");
				Console.WriteLine("Queued Orders: \"O\"\tWarehouse Layout: \"L\"\tWarehouse Stock: \"S\"\tInsert New Order: \"N\"\tSent Orders: \"D\"\nExit Program: \"E\"");
				command = Console.ReadLine();

				Console.WriteLine("\n\n");

				char c = command.ToUpper().ToArray()[0];

				if (c == 'O')
                {
					foreach (OrderClass sent in orders)
                    {
						Console.WriteLine("Order Mass: {0}\nItem 0: {1}\t\tItem 1: {2}\t\tItem 2: {3}\t\tItem 3: {4}\t\tItem 4: {5}\nOrder Time: {6}\n\n", sent.totalMass, sent.item1.units, sent.item2.units, sent.item3.units, sent.item4.units, sent.item5.units, sent.orderTime);
                    }

					Console.WriteLine("\n\n");
				}
				else if (c == 'L')
                {
					LayoutPrint(layoutReadWrite);
					Console.WriteLine("\n\n");
                }
				else if (c == 'S')
                {

					Console.WriteLine("Enter the item type you want to track: ");
					int itemNumberRequest = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);
					Console.WriteLine();
					int b;

					if (itemNumberRequest == 0)
                    {

						Tuple<double[], int[][]> printPlaceholder = maxStorageIdentification1(1);

						for (int i = 1; i <= 252; i++)
                        {
							printPlaceholder = maxStorageIdentification1(i);

							b = printPlaceholder.Item1.Length;
							if (printPlaceholder.Item1[(b - 1)] < 0.01)
                            {
								if (i > 1)
								{
									printPlaceholder = maxStorageIdentification1((i - 1));
									break;
								}
								else
                                {
									printPlaceholder = maxStorageIdentification1(1);
									break;
                                }
                            }
                        }

						for (int i = 0; i < printPlaceholder.Item1.Length; i++)
                        {
							Console.WriteLine("Quantity of Item {0}: {1} grams\n\n", itemNumberRequest, printPlaceholder.Item1[i]);
                        }


                    }
					else if (itemNumberRequest == 1)
                    {
						Tuple<double[], int[][]> printPlaceholder = maxStorageIdentification2(1);

						for (int i = 1; i <= 252; i++)
						{
							printPlaceholder = maxStorageIdentification2(i);

							b = printPlaceholder.Item1.Length;
							if (printPlaceholder.Item1[(b - 1)] < 0.01)
							{
								if (i > 1)
								{
									printPlaceholder = maxStorageIdentification2((i - 1));
									break;
								}
								else
								{
									printPlaceholder = maxStorageIdentification2(1);
									break;
								}
							}
						}

						for (int i = 0; i < printPlaceholder.Item1.Length; i++)
						{
							Console.WriteLine("Quantity of Item {0}: {1} grams\n\n", itemNumberRequest, printPlaceholder.Item1[i]);
						}

					}
					else if (itemNumberRequest == 2)
                    {

						Tuple<double[], int[][]> printPlaceholder = maxStorageIdentification3(1);

						for (int i = 1; i <= 252; i++)
						{
							printPlaceholder = maxStorageIdentification3(i);

							b = printPlaceholder.Item1.Length;
							if (printPlaceholder.Item1[(b - 1)] < 0.01)
							{
								if (i > 1)
								{
									printPlaceholder = maxStorageIdentification3((i - 1));
									break;
								}
								else
								{
									printPlaceholder = maxStorageIdentification3(1);
									break;
								}
							}
						}

						for (int i = 0; i < printPlaceholder.Item1.Length; i++)
						{
							Console.WriteLine("Quantity of Item {0}: {1} grams\n\n", itemNumberRequest, printPlaceholder.Item1[i]);
						}

					}
					else if (itemNumberRequest == 3)
                    {
						Tuple<double[], int[][]> printPlaceholder = maxStorageIdentification4(1);

						for (int i = 1; i <= 252; i++)
						{
							printPlaceholder = maxStorageIdentification4(i);

							b = printPlaceholder.Item1.Length;
							if (printPlaceholder.Item1[(b - 1)] < 0.01)
							{
								if (i > 1)
								{
									printPlaceholder = maxStorageIdentification4((i - 1));
									break;
								}
								else
								{
									printPlaceholder = maxStorageIdentification4(1);
									break;
								}
							}
						}

						for (int i = 0; i < printPlaceholder.Item1.Length; i++)
						{
							Console.WriteLine("Quantity of Item {0}: {1} grams\n\n", itemNumberRequest, printPlaceholder.Item1[i]);
						}

					}
					else if (itemNumberRequest == 4)
                    {
						Tuple<double[], int[][]> printPlaceholder = maxStorageIdentification5(1);

						for (int i = 1; i <= 252; i++)
						{
							printPlaceholder = maxStorageIdentification5(i);

							b = printPlaceholder.Item1.Length;
							if (printPlaceholder.Item1[(b - 1)] < 0.01)
							{
								if (i > 1)
								{
									printPlaceholder = maxStorageIdentification5((i - 1));
									break;
								}
								else
								{
									printPlaceholder = maxStorageIdentification5(1);
									break;
								}
							}
						}

						for (int i = 0; i < printPlaceholder.Item1.Length; i++)
						{
							Console.WriteLine("Quantity of Item {0}: {1} grams\n\n", itemNumberRequest, printPlaceholder.Item1[i]);
						}

					}

                }	
				else if (c == 'N')
                {

					int item1Read, item2Read, item3Read, item4Read, item5Read;
					Random rt = new Random();

					Console.WriteLine("Item 1 Input: ");
					item1Read = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);

					Console.WriteLine("Item 2 Input: ");
					item2Read = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);

					Console.WriteLine("Item 3 Input: ");
					item3Read = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);

					Console.WriteLine("Item 4 Input: ");
					item4Read = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);

					Console.WriteLine("Item 5 Input: ");
					item5Read = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);

					orders_Mutex.WaitOne();
					OrderClass orderSample = new OrderClass(item1Read, item2Read, item3Read, item4Read, item5Read, rt.Next(1000, 12000));
					orders.Enqueue(orderSample);

					Console.WriteLine("\nOrder inserted.\nTotal Weight: {0, -15}\t\tOrder Time: {1, -5}\n\n", orderSample.totalMass, orderSample.orderTime);
					orders_Mutex.ReleaseMutex();

				}
				else if (c == 'D')
                {

					foreach(string completions in status)
                    {
						Console.WriteLine(completions);
					}

					Console.WriteLine("\n\n");

				}
				//else if (c == 'R')
    //            {
				//	Console.WriteLine("Robot ID Input:");
				//	int RobotID = int.Parse((Console.ReadLine()), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.Any);
				//	Console.WriteLine("\n\n");

				//	if(RobotID == 0)
				//	{
				//		robot_Mutex[RobotID].WaitOne();
				//		Console.WriteLine("Robot ID: {0}\nRobot Position: {1}, {2}\nRobot Load: {3}\nRobot Battery {4}", );
				//		robot_Mutex[RobotID].ReleaseMutex();

				//	}
				//	else if (RobotID == 1)
				//	{
				//		robot_Mutex[RobotID].WaitOne();

				//		robot_Mutex[RobotID].ReleaseMutex();

				//	}
				//	else if (RobotID == 2)
				//	{
				//		robot_Mutex[RobotID].WaitOne();

				//		robot_Mutex[RobotID].ReleaseMutex();

				//	}
				//	else if (RobotID == 3)
				//	{
				//		robot_Mutex[RobotID].WaitOne();

				//		robot_Mutex[RobotID].ReleaseMutex();

				//	}
				//	else
    //                {
				//		Console.WriteLine("Invalid Input.\n\n");
    //                }


				//}
				else if (c == 'E')
                {
					programSwitch_Mutex.WaitOne();
					programSwitch = false;
					programButton = programSwitch;
					programSwitch_Mutex.ReleaseMutex();

					Console.WriteLine("Ending Program.\n\n");
				}
				else
                {
					Console.WriteLine("\n\n\nInvalid Input\n\n");
					continue;
                }

			}



			//unloadManager.Wait();
			//loadingManagement.Wait();
			//         robot3Unload.Wait();
			//         robot4Unload.Wait();
			//robot1Load.Wait();
			//robot2Load.Wait();

			Console.WriteLine("Program Ended.");


		}

    }
}