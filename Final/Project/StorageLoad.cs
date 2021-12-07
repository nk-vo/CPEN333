using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Collections;


namespace final_Project
{
	public class StorageLoad
	{


		public Item1 item1Load;
		public Item2 item2Load;
		public Item3 item3Load;
		public Item4 item4Load;
		public Item5 item5Load;

		public double storageMass;

		public int currentHoldNum;



		public StorageLoad(int i1, int i2, int i3, int i4, int i5)
		{

			this.item1Load = new Item1(i1);
			this.item2Load = new Item2(i2);
			this.item3Load = new Item3(i3);
			this.item4Load = new Item4(i4);
			this.item5Load = new Item5(i5);

			this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;

		}

		public double[] Removal ()
        {
			double[] mass = new double[5] { item1Load.totalMass, item2Load.totalMass, item3Load.totalMass, item4Load.totalMass, item5Load.totalMass };

			item1Load.Clear();
			item2Load.Clear();
			item3Load.Clear();
			item4Load.Clear();
			item5Load.Clear();

			return mass.ToArray();
		}

		public void SingleLoad (int itm, int u)
        {

			//this.item1Load.UpdateRecord(0);
			//this.item2Load.UpdateRecord(0);
			//this.item3Load.UpdateRecord(0);
			//this.item4Load.UpdateRecord(0);
			//this.item5Load.UpdateRecord(0);

			this.currentHoldNum = itm;

			if (itm == 0)
			{
				this.item1Load.UpdateRecord(u);
				this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;
			}
			else if (itm == 1)
			{

				this.item2Load.UpdateRecord(u);
				this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;

			}
			else if (itm == 2)
			{

				this.item3Load.UpdateRecord(u);
				this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;
			}
			else if (itm == 3)
			{

				this.item4Load.UpdateRecord(u);
				this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;
			}
			else if (itm == 4)
			{

				this.item5Load.UpdateRecord(u);
				this.storageMass = this.item1Load.totalMass + this.item2Load.totalMass + this.item3Load.totalMass + this.item4Load.totalMass + this.item5Load.totalMass;
			}
		}


	}
}
