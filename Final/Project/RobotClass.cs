using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Collections;

namespace final_Project
{
	public class RobotClass
	{

		public int[] current_Co	= new int[2];
		public int[] objective_Co	= new int[2];
		int[] charge_Co		= new int[2];

		public StorageLoad load;

		public int task;
		public int id;

		int bLeft, bTop, bRight, bBottom, charge;

		const int left		= 0;
		const int top		= 1;
		const int right		= 2;
		const int bottom	= 3;

		public static double maxLoad = FullTest.maxLoad;

		public static double fullCharge = 100.0;
		public double chargingLevel = 30.00;
		public double consumption = 0.5;
		public double replenishment = 2.0;

		public double batteries = 100.0;
		public Mutex battery_Mutex;
		


		public RobotClass(int num, int[] cu, int[] ob, int t, int[] boundaries, Mutex layoutReadWrite)
        {
			battery_Mutex = new Mutex();

			this.load = new StorageLoad(0, 0, 0, 0, 0);

			this.current_Co[0]		= cu[0];
			this.current_Co[1]		= cu[1];

			this.charge_Co[0]		= cu[0] - 1;
			this.charge_Co[1]		= cu[1];

			this.objective_Co[0]	= ob[0];
			this.objective_Co[1]	= ob[1];

			this.task				= t;

			this.id					= num;

			this.bLeft		= boundaries[0];
			this.bTop		= boundaries[1];
			this.bRight		= boundaries[2];
			this.bBottom	= boundaries[3];

			this.charge		= boundaries[4];

			layoutReadWrite.WaitOne();
			FullTest.layout[cu[0], cu[1]] = 'x';
			layoutReadWrite.ReleaseMutex();
			// rob1.ObjectiveStep(r1StartObj, 0, layoutReadWrite);
			ObjectiveStep(this.objective_Co, 0, layoutReadWrite);

		}


		public bool BatteryCheck ()
        {
			battery_Mutex.WaitOne();
			double b = this.batteries;
			battery_Mutex.ReleaseMutex();

			if (b < this.chargingLevel)
            {
				return true;
            }
			else
            {
				return false;
            }
			
        }

		public double BatteryStatus()
        {
			battery_Mutex.WaitOne();
			double status = this.batteries;
			battery_Mutex.ReleaseMutex();

			return status;
		}

		public void Charge ()
        {
			battery_Mutex.WaitOne();
			int time = (int) ((RobotClass.fullCharge - batteries) / replenishment);
			battery_Mutex.ReleaseMutex();
			Thread.Sleep(time);

			battery_Mutex.WaitOne();
			this.batteries = RobotClass.fullCharge;
			battery_Mutex.ReleaseMutex();
		}

		public void ObjectiveStep (int[] obj, int tsk, Mutex layoutReadWrite)
        {
			bool complete = false;

			this.objective_Co[0] = obj[0];
			this.objective_Co[1] = obj[1];

			while (!complete)
            {

                if ( !((current_Co[0] == objective_Co[0]) && (current_Co[1] == objective_Co[1])) ) // Neither x or y are equal to destination.
                {

					if (		(current_Co[0] == bLeft) )					// Robot is on the left-most movement column (not the charging station)
                    {
						if (current_Co[1] > bTop)							// y-coordinate not touching the top-left corner (0, 0).
						{
							MovementUp(layoutReadWrite);                                // Helper function for upward movement.

							battery_Mutex.WaitOne();
							this.batteries -= this.consumption;
							battery_Mutex.ReleaseMutex();

							continue;
                        }
                        else
                        {
							MovementRight(layoutReadWrite);                             // Start moving rightward to cycle through the warehouse.

							battery_Mutex.WaitOne();
							this.batteries -= this.consumption;
							battery_Mutex.ReleaseMutex();

						}
                    }
					else if (	( current_Co[1] == bTop ) || ( current_Co[1] == bBottom )	)	// At the top or bottom edges of the warehouse.
                    {
						if (current_Co[1] == bTop)					// For positions at the top corridor.
                        {
							if ( current_Co[0] > objective_Co[0] )	// If the objective position is to the left, but the robot can't reach because
                            {                                       // it had overshot its position.

								MovementDown(layoutReadWrite);

								battery_Mutex.WaitOne();
								this.batteries -= this.consumption;
								battery_Mutex.ReleaseMutex();

							}
							else if ( current_Co[0] < objective_Co[0] )		// Robot is moving clockwise but is reaching the objective to the right.
                            {
								MovementRight(layoutReadWrite);

								battery_Mutex.WaitOne();
								this.batteries -= this.consumption;
								battery_Mutex.ReleaseMutex();
							}
							else if (current_Co[0] == objective_Co[0] )		// At the target column, the robot needs to move down.
                            {
								MovementDown(layoutReadWrite);

								battery_Mutex.WaitOne();
								this.batteries -= this.consumption;
								battery_Mutex.ReleaseMutex();
							}

						}
						else												// For positions at the bottom corridor.
						{
							while ( (current_Co[0] > bLeft) && !((current_Co[0] == objective_Co[0]) && (current_Co[1] == objective_Co[1])) )
							{                                               // Reach the line-up lane on the very left unless the objective is along the path.
								
								MovementLeft(layoutReadWrite);

								battery_Mutex.WaitOne();
								this.batteries -= this.consumption;
								battery_Mutex.ReleaseMutex();
							}

							if (current_Co[0] == 0)
							{
								MovementRight(layoutReadWrite);
							}

							continue;

                        }
                    } 
					else if ( ((current_Co[1] != bTop) && (current_Co[1] != bBottom)) )		// Within any aisle
                    {

						while ( (current_Co[1] != bBottom) && (current_Co[1] != bTop) && !((current_Co[0] == objective_Co[0]) && (current_Co[1] == objective_Co[1])) )
						{													// Reach the bottom lane unless the objective is along the path.

							MovementDown(layoutReadWrite);

							battery_Mutex.WaitOne();
							this.batteries -= this.consumption;
							battery_Mutex.ReleaseMutex();
						}

						continue;

					}
                }
                else
                {
					//layoutReadWrite.WaitOne();
					//Console.WriteLine("\nRobot {2} Destination [{0},{1}] Reached", current_Co[0], current_Co[1], this.id);
					//layoutReadWrite.ReleaseMutex();
					//FullTest.LayoutPrint(layoutReadWrite);

					/*
					 *	Program the tasks that the robot can perform when it reaches the destination.
					 *	The robot must be capable of loading and unloading.
					 */
					complete = true;
                }

            }
        }

		public void Remove( int[] cubbard_Co, double mass, int itm) // Take resources from a shelf
		{

			int column		= cubbard_Co[0];
			int subColumn	= cubbard_Co[1];
			int side		= cubbard_Co[2];
			int cubbard		= cubbard_Co[3];


			if (itm == 0)
			{

				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] -= mass;
				//Console.WriteLine("Mass on shelf: {0}", FullTest.shelfMass[column, subColumn, side, cubbard]);
				//Console.WriteLine("Co-Ordinates: {0}, {1}, {2}, {3}", column, subColumn, side, cubbard);
				FullTest.shelf_Mutex.ReleaseMutex();


				int Units = (int)(mass / Item1.massPerUnit);
				FullTest.item1_Mutex.WaitOne();
				FullTest.item1Tracker[column, subColumn, side, cubbard].units -= Units;
				FullTest.item1_Mutex.ReleaseMutex();

				load.SingleLoad(itm, Units);
			}
			else if (itm == 1)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] -= mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				int Units = (int)(mass / Item2.massPerUnit);
				FullTest.item2_Mutex.WaitOne();
				FullTest.item2Tracker[column, subColumn, side, cubbard].units -= Units;
				FullTest.item2_Mutex.ReleaseMutex();

				load.SingleLoad(itm, Units);
			}
			else if (itm == 2)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] -= mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				int Units = (int)(mass / Item3.massPerUnit);
				FullTest.item3_Mutex.WaitOne();
				FullTest.item3Tracker[column, subColumn, side, cubbard].units -= Units;
				FullTest.item3_Mutex.ReleaseMutex();

				load.SingleLoad(itm, Units);
			}
			else if (itm == 3)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] -= mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				int Units = (int)(mass / Item4.massPerUnit);
				FullTest.item4_Mutex.WaitOne();
				FullTest.item4Tracker[column, subColumn, side, cubbard].units -= Units;
				FullTest.item4_Mutex.ReleaseMutex();

				load.SingleLoad(itm, Units);
			}
			else if (itm == 4)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] -= mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				int Units = (int)(mass / Item5.massPerUnit);
				FullTest.item5_Mutex.WaitOne();
				FullTest.item5Tracker[column, subColumn, side, cubbard].units -= Units;
				FullTest.item5_Mutex.ReleaseMutex();

				load.SingleLoad(itm, Units);
			}

		}

		public void Deliver(int time)
        {

			int units = RobotClass.massToUnit(this.load.storageMass, this.load.currentHoldNum);
			double mass = this.load.storageMass;
			int itemNum = this.load.currentHoldNum;

			if (itemNum == 0)
			{
				FullTest.underway_Queue.Enqueue(new OrderClass(units, 0,0,0,0, time));
			}
			else if (itemNum == 1)
			{
				FullTest.underway_Queue.Enqueue(new OrderClass(0, units, 0, 0, 0, time));
			}
			else if (itemNum == 2)
			{
				FullTest.underway_Queue.Enqueue(new OrderClass(0, 0, units, 0, 0, time));
			}
			else if (itemNum == 3)
			{
				FullTest.underway_Queue.Enqueue(new OrderClass(0, 0, 0, units, 0, time));
			}
			else if (itemNum == 4)
			{
				FullTest.underway_Queue.Enqueue(new OrderClass(0, 0, 0, 0, units, time));
			}


			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;

		}

		public void Unload (int[] cubbard_Co)
        {

			int		units	= RobotClass.massToUnit(this.load.storageMass, this.load.currentHoldNum);
			double	mass	= this.load.storageMass;
			int		itemNum = this.load.currentHoldNum;

			int column		= cubbard_Co[0];
			int subColumn	= cubbard_Co[1];
			int side		= cubbard_Co[2];
			int cubbard		= cubbard_Co[3];

			if (itemNum == 0)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] += mass;
				//Console.WriteLine("Mass on shelf: {0}", FullTest.shelfMass[column, subColumn, side, cubbard]);
				//Console.WriteLine("Co-Ordinates: {0}, {1}, {2}, {3}", column, subColumn, side, cubbard);
				FullTest.shelf_Mutex.ReleaseMutex();


				FullTest.item1_Mutex.WaitOne();
				FullTest.item1Tracker[column, subColumn, side, cubbard].UpdateRecord(units);
				FullTest.item1_Mutex.ReleaseMutex();
			}
			else if (itemNum == 1)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] += mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				FullTest.item2_Mutex.WaitOne();
				FullTest.item2Tracker[column, subColumn, side, cubbard].UpdateRecord(units);
				FullTest.item2_Mutex.ReleaseMutex();
			}
			else if (itemNum == 2)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] += mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				FullTest.item3_Mutex.WaitOne();
				FullTest.item3Tracker[column, subColumn, side, cubbard].UpdateRecord(units);
				FullTest.item3_Mutex.ReleaseMutex();
			}
			else if (itemNum == 3)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] += mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				FullTest.item4_Mutex.WaitOne();
				FullTest.item4Tracker[column, subColumn, side, cubbard].UpdateRecord(units);
				FullTest.item4_Mutex.ReleaseMutex();
			}
			else if (itemNum == 4)
			{
				FullTest.shelf_Mutex.WaitOne();
				FullTest.shelfMass[column, subColumn, side, cubbard] += mass;
				FullTest.shelf_Mutex.ReleaseMutex();


				FullTest.item5_Mutex.WaitOne();
				FullTest.item5Tracker[column, subColumn, side, cubbard].UpdateRecord(units);
				FullTest.item5_Mutex.ReleaseMutex();
			}

			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;
			this.load.item1Load.totalMass = 0.0; this.load.item1Load.units = 0;

		}


		public void Load (int itm, double mass)
        {
			int u = massToUnit(mass, itm);

			load.SingleLoad(itm, u);

        }


		public static int massToUnit (double m, int itm)
        {
			double conversionUnit;

			if (itm == 0)
			{
				conversionUnit = Item1.massPerUnit;
			}
			else if (itm == 1)
			{
				conversionUnit = Item2.massPerUnit;
			}
			else if (itm == 2)
			{
				conversionUnit = Item3.massPerUnit;
			}
			else if (itm == 3)
			{
				conversionUnit = Item4.massPerUnit;
			}
			else if (itm == 4)
			{
				conversionUnit = Item5.massPerUnit;
			}
            else
            {
				return 0;
            }
			

			int units = (int)(m / conversionUnit);

			return units;
		}



		bool Collision( int dir , Mutex layoutReadWrite)			// False if no collision. True if there is collision.
        {
			if		( dir == left )
            {
				layoutReadWrite.WaitOne();
				if ((current_Co[0]) == 0)
                {
					Console.WriteLine("Objective: {0} {1}\nID:{2}\nTask: {3}", objective_Co[0], objective_Co[1], this.id, this.task);
                }
				if(FullTest.layout[ (current_Co[0] - 1), current_Co[1]] == 'o') // 'o' means vacant space. 'x' means occupied by another robot.
				{
					layoutReadWrite.ReleaseMutex();
					return false;
                }
                else
				{
					layoutReadWrite.ReleaseMutex();
					return true;
                }
            }
			else if ( dir == top )
            {
				layoutReadWrite.WaitOne();
				char test = FullTest.layout[(current_Co[0]), (current_Co[1] - 1)];
				if (FullTest.layout[ current_Co[0], (current_Co[1] - 1)] == 'o')
				{
					layoutReadWrite.ReleaseMutex();
					return false;
				}
				else
				{
					layoutReadWrite.ReleaseMutex();
					return true;
                }
			}
			else if ( dir == right )
            {
				layoutReadWrite.WaitOne();
				char test = FullTest.layout[(current_Co[0] + 1)	, current_Co[1]];
				if (		FullTest.layout[ (current_Co[0] + 1), current_Co[1]] == 'o')
				{
					layoutReadWrite.ReleaseMutex();
					return false;
				}
                else
				{
					layoutReadWrite.ReleaseMutex();
					return true;
                }
			}
			else if ( dir == bottom )
            {
				layoutReadWrite.WaitOne();
				if (FullTest.layout[ current_Co[0], (current_Co[1] + 1)] == 'o')
				{
					layoutReadWrite.ReleaseMutex();
					return false;
				}
                else
				{
					layoutReadWrite.ReleaseMutex();
					return true;
                }
			}

			return true;
        }



		void MovementUp (Mutex layoutReadWrite)						// Determines if the robot is qualified to move upwards.
        {
			if (!Collision(top, layoutReadWrite))   // Checks for collision.
			{

				current_Co[1]--;
				layoutReadWrite.WaitOne();
				FullTest.layout[	current_Co[0],		current_Co[1]		]		= 'x';          // Moves position on layout picture.
				FullTest.layout[	current_Co[0], (	current_Co[1] + 1)	]		= 'o';          // Replaces previous position with vacant symbol.
				layoutReadWrite.ReleaseMutex();
				Thread.Sleep(500);

			}
			else                        // Case for collision.
			{

				Thread.Sleep(50);      // Nothing happens. Robot waits for vacant position.

			}
		}



		public void MovementRight (Mutex layoutReadWrite)					// Determines if the robot is qualified to move right.
        {
			if ( !Collision(right, layoutReadWrite) )
            {

				current_Co[0]++;
				layoutReadWrite.WaitOne();
				FullTest.layout[		current_Co[0]		,	current_Co[1]	]	= 'x';          // Moves position on layout picture.
				FullTest.layout[	(	current_Co[0] - 1)	,	current_Co[1]	]	= 'o';          // Replaces previous position with vacant symbol.
				layoutReadWrite.ReleaseMutex();
				Thread.Sleep(50);

            }
            else
            {

				Thread.Sleep(50);

            }
        }			



		void MovementDown(Mutex layoutReadWrite)					// Determines if the robot is qualified to move downwards.
		{
			if (!Collision(bottom, layoutReadWrite))				// Checks for collision.
			{

				current_Co[1]++;
				layoutReadWrite.WaitOne();

				FullTest.layout[	current_Co[0],		current_Co[1]		]		= 'x';      // Moves position on layout picture.
				FullTest.layout[	current_Co[0], (	current_Co[1] - 1)	]		= 'o';      // Replaces previous position with vacant symbol.

				layoutReadWrite.ReleaseMutex();
				Thread.Sleep(50);

			}
			else                        // Case for collision.
			{

				Thread.Sleep(50);      // Nothing happens. Robot waits for vacant position.

			}
		}



		public void MovementLeft(Mutex layoutReadWrite)					// Determiens if the robot is qualified to move left.
		{
			if (!Collision(left, layoutReadWrite))
			{
				current_Co[0]--;
				layoutReadWrite.WaitOne();

				FullTest.layout[		current_Co[0]		,	current_Co[1]] = 'x';   // Moves position on layout picture.
				FullTest.layout[(		current_Co[0] + 1)	,	current_Co[1]] = 'o';   // Replaces previous position with vacant symbol.

				layoutReadWrite.ReleaseMutex();
				Thread.Sleep(50);
			}
			else
			{

				Thread.Sleep(50);

			}
		}




	}

}
