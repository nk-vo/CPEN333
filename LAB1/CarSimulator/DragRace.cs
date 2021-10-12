using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator
{
    public class DragRace
    {
        static void Main(string[] args)
        {
            
            Car myTesla = new Car("Tesla", 1500, 1000, 0.51);
            Car myPrius = new Car("Prius", 1000, 750, 0.43);

        
            // drive for 60 seconds with delta time of 1s
            double dt = 1;

            // Start
            myTesla.accelerate(true);
            myPrius.accelerate(true);
            for (double t = 0; t < 60; t += dt)
            {
                double finish_line = 402.3;
                // print the time and current state
                // print who is in lead
                myTesla.drive(dt);
                myPrius.drive(dt);

                State myTesla_state = myTesla.getState();
                State myPrius_state = myPrius.getState();

                Console.WriteLine("The time is: {0}s", t);
                Console.WriteLine("Tesla\nPosition: {0}m Veolocity: {1}m/s Acceleration: {2}m/s^2", Math.Round(myTesla_state.position, 1), Math.Round(myTesla_state.velocity, 1), Math.Round(myTesla_state.acceleration, 2));
                Console.WriteLine("Prius\nPosition: {0}m Veolocity: {1}m/s Acceleration: {2}m/s^2", Math.Round(myPrius_state.position, 1), Math.Round(myPrius_state.velocity, 1), Math.Round(myPrius_state.acceleration, 2));


                if (myTesla_state.position > myPrius_state.position)
                    Console.WriteLine("Tesla is leading!");
                else if (myTesla_state.position < myPrius_state.position)
                    Console.WriteLine("Prius is leading!");
                else
                    Console.WriteLine("Both cars are at the same position!");
                if (myTesla_state.position >= finish_line | myPrius_state.position >= finish_line)
                {
                    Console.WriteLine("Race Finished!");
                    if (myTesla_state.position > myPrius_state.position)
                        Console.WriteLine("Tesla won!");
                    else if (myTesla_state.position < myPrius_state.position)
                        Console.WriteLine("Prius won!");
                    else
                        Console.WriteLine("It's a draw!");
                    break;
                }
            }
        }
    }
}
