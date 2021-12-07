using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Collections;



namespace final_Project
{
	public class OrderClass
	{

		public Item1 item1;
		public Item2 item2;
		public Item3 item3;
		public Item4 item4;
		public Item5 item5;

		public double totalMass;

		public int orderTime;

		public OrderClass (int i1, int i2, int i3, int i4, int i5, int t)
        {

			this.item1		= new Item1(i1);
			this.item2		= new Item2(i2);
			this.item3		= new Item3(i3);
			this.item4		= new Item4(i4);
			this.item5		= new Item5(i5);

			this.totalMass	= this.item1.totalMass + this.item2.totalMass + this.item3.totalMass + this.item4.totalMass + this.item5.totalMass;

			this.orderTime = t;

		}


	}







	public class Item1
	{

		public int			units;
		public double		totalMass;

		public static int	id;

		public Item1 (int u)
        {
			units = 0;
			totalMass = 0.0;

			id = 0;

			this.units		+= u;
			this.totalMass	+= ((double) u) * massPerUnit;
        }

		public static double massPerUnit = 10.0;

		public static int maxUnitsPerStorage = (int) (FullTest.maxLoad / massPerUnit);

		public void UpdateRecord(int u)
        {
			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
        }

		public void Clear()
        {
			this.units = 0;
			this.totalMass = 0.0;
		}

	}

	public class Item2
	{

		public int units;
		public double totalMass;

		public static int id;

		public Item2(int u)
		{
			units = 0;
			totalMass = 0.0;

			id = 0;

			this.units += u;
			this.totalMass += ((double) u) * massPerUnit;
		}

		public static double massPerUnit = 5.0;

		public static int maxUnitsPerStorage = (int)(FullTest.maxLoad / massPerUnit);

		public void UpdateRecord(int u)
		{
			this.units += u;
			this.totalMass += ((double) u) * massPerUnit;
		}

		public void Clear()
		{
			this.units = 0;
			this.totalMass = 0.0;
		}

	}

	public class Item3
	{

		public int units;
		public double totalMass;

		public static int id;

		public Item3(int u)
		{
			units = 0;
			totalMass = 0.0;

			id = 0;

			this.units += u;
			this.totalMass += ((double) u) * massPerUnit;
		}

		public static double massPerUnit = 25.0;

		public static int maxUnitsPerStorage = (int)(FullTest.maxLoad / massPerUnit);

		public void UpdateRecord(int u)
		{
			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
		}
		public void Clear()
		{
			this.units = 0;
			this.totalMass = 0.0;
		}

	}

	public class Item4
	{

		public int units;
		public double totalMass;

		public static int id;

		public Item4(int u)
		{
			units = 0;
			totalMass = 0.0;

			id = 0;

			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
		}

		public static double massPerUnit = 2.0;

		public static int maxUnitsPerStorage = (int)(FullTest.maxLoad / massPerUnit);

		public void UpdateRecord(int u)
		{
			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
		}

		public void Clear()
		{
			this.units = 0;
			this.totalMass = 0.0;
		}

	}

	public class Item5
	{

		public int units;
		public double totalMass;

		public static int id;

		public Item5(int u)
		{
			units = 0;
			totalMass = 0.0;

			id = 0;

			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
		}

		public static double massPerUnit = 20.0;

		public static int maxUnitsPerStorage = (int)(FullTest.maxLoad / massPerUnit);

		public void UpdateRecord(int u)
		{
			this.units += u;
			this.totalMass += ((double)u) * massPerUnit;
		}

		public void Clear()
		{
			this.units = 0;
			this.totalMass = 0.0;
		}

	}





}