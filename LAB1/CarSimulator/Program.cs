using System;

namespace CarSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // read in car mass
            Console.WriteLine("Enter the mass of car (kg): ");
            double mass;
            mass = Convert.ToDouble(Console.ReadLine());

            // read in engine force
            Console.WriteLine("Enter the net force of the engine (N): ");
            double engine_force;
            engine_force = Convert.ToDouble(Console.ReadLine());

            // read in drag area coefficient
            Console.WriteLine("Enter the car's drag area (m^2): ");
            double drag_area;
            drag_area = Convert.ToDouble(Console.ReadLine());

            // read in time step
            Console.WriteLine("Enter the simulation time step (s): ");
            double dt;
            dt = Convert.ToDouble(Console.ReadLine());

            // read in total number of simulation steps
            Console.WriteLine("Enter the number of time steps (int): ");
            int N;
            N = Convert.ToInt32(Console.ReadLine());

            // initiate the car's state
            double x0 = 0;  // initial position
            double v = 0;   // initial velocity
            double t = 0;   // initial time
            double rho = 1.225; // air density at sea level
            double fd = 0, x1, a;   // drag force and secondary position and acceleration
            

            // run the simulation
            for (int i = 0; i < N; ++i)
            {
                // TODO: COMPUTE UPDATED STATE HERE
                
                a = CarSimulator.Physics1D.compute_acceleration(engine_force - fd, mass);
                v = CarSimulator.Physics1D.compute_velocity(v, a, dt);
                x1 = CarSimulator.Physics1D.compute_position(x0, v, dt);
                fd = CarSimulator.Physics1D.compute_dragforce(rho, drag_area, v);
                
                t += dt;    // increment time

                // print the time and current state
                Console.WriteLine("t:{0}, a:{1}, v:{2}, x1:{3}, fd:{4} ", t, a, v, x1, fd);
                
            }
        }
    }
}
