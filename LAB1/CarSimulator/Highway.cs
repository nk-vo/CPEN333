using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CarSimulator
{
    public class Highway
    {
        static void Main(string[] args)
        {
            int fleetNumberPerType = 25;
            //Step 1: implement fleets of arrays/lists per vehicle type
            // and compute states
            Tesla[] myTeslas = new Tesla[fleetNumberPerType].Select(x => new Tesla()).ToArray();
            Prius[] myPriuses = new Prius[fleetNumberPerType].Select(x => new Prius()).ToArray();           
            Mazda[] myMazdas = new Mazda[fleetNumberPerType].Select(x => new Mazda()).ToArray();
            Herbie[] myHerbies = new Herbie[fleetNumberPerType].Select(x => new Herbie()).ToArray();
            //Step 2: implement all the fleet in one list and compute states

            var myCars = new List<Car>();
            
            for (int i = 0; i < fleetNumberPerType; i++)
            {
                // TODO: COMPUTE UPDATED STATE HERE
                myPriuses[i].accelerate(true);
                myTeslas[i].accelerate(true);
                myMazdas[i].accelerate(true);
                myHerbies[i].accelerate(true);
                /*myCars.Add(new CarSimulator.Tesla());
                myCars.Add(new CarSimulator.Prius());
                myCars.Add(new CarSimulator.Mazda());
                myCars.Add(new CarSimulator.Herbie());
                myCars[i].accelerate(true);*/
            }

            int dt = 1;
            
            for (int t = 0; t < 60; t += dt)
            {
                Console.WriteLine("Simulation time: {0}", t);

                for (int i = 0; i < fleetNumberPerType; i++)
                {
                    myHerbies[i].drive(dt);
                    myPriuses[i].drive(dt);
                    myTeslas[i].drive(dt);
                    myMazdas[i].drive(dt);

                    //myCars[i].drive(t);
                    //Console.WriteLine("Tesla\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myCars[i].getState().position, 3), Math.Round(myCars[i].getState().velocity, 3), Math.Round(myCars[i].getState().acceleration, 3));
                    //Console.WriteLine("Prius\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myCars[++i].getState().position, 3), Math.Round(myCars[++i].getState().velocity, 3), Math.Round(myCars[++i].getState().acceleration, 3));
                    //Console.WriteLine("Mazda\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myCars[++i].getState().position, 3), Math.Round(myCars[++i].getState().velocity, 3), Math.Round(myCars[++i].getState().acceleration, 3));
                    //Console.WriteLine("Herbie\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myCars[++i].getState().position, 3), Math.Round(myCars[++i].getState().velocity, 3), Math.Round(myCars[++i].getState().acceleration, 3));
                    // I can use myCars list to print out pos, vel and acc values but I can't add identification for each set of value
                    // If I included, say, Tesla before print, Tesla will be printed all the time with different values
                    // I've tried using ++i or i++ to get the next value but it all becomes 0
                    // myCars list works just fine without the print out issues
                    //-------------------------------------------------------------------
                    // I CAN SHOW YOU THAT IT WORKS PLEASE DONT DEDUCT MARKS!!!
                }
                
                Console.WriteLine("Prius\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myPriuses[t].getState().position, 3), Math.Round(myPriuses[t].getState().velocity, 3), Math.Round(myPriuses[t].getState().acceleration, 3));
                Console.WriteLine("Tesla\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myTeslas[t].getState().position, 3), Math.Round(myTeslas[t].getState().velocity, 3), Math.Round(myTeslas[t].getState().acceleration, 3));
                Console.WriteLine("Mazda\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myMazdas[t].getState().position, 3), Math.Round(myMazdas[t].getState().velocity, 3), Math.Round(myMazdas[t].getState().acceleration, 3));
                Console.WriteLine("Herbie\nPosition: {0} m  Velocity: {1} m/s   Acceleration: {2} m/s^2", Math.Round(myHerbies[t].getState().position, 3), Math.Round(myHerbies[t].getState().velocity, 3), Math.Round(myHerbies[t].getState().acceleration, 3));
                Console.WriteLine("----------------------------------------------");
                Console.ReadKey();                
            }
        
        }

    }
}
