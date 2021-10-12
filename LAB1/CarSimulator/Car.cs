using System;
namespace CarSimulator
{
    public class Car
    {
        protected double mass;
        protected string model;
        protected double dragArea;
        protected double engineForce;
        protected double drag_force = 0;
        protected double rho = 1.225;
        protected bool drive_enable;

        public State myCarState;

        
        /// implement constructor and methods
        public Car(string mod, double m,double engForce, double drag_area)
        {
            model = mod;
            mass = m;
            engineForce = engForce;
            dragArea = drag_area;
            this.myCarState = new State(0, 0, 0, 0);
        }

        public virtual bool accelerate(bool on)
        {
            if (on)
                drive_enable = true;
            else
                drive_enable = false;
            return drive_enable;
        }

        public virtual void drive(double dt)
        {
            if (drive_enable)
            {
                myCarState.acceleration = CarSimulator.Physics1D.compute_acceleration(engineForce - drag_force, mass);
                myCarState.velocity = CarSimulator.Physics1D.compute_velocity(myCarState.velocity, myCarState.acceleration, dt);
                myCarState.position = CarSimulator.Physics1D.compute_position(myCarState.position, myCarState.velocity, dt);
                drag_force = CarSimulator.Physics1D.compute_dragforce(rho, dragArea, myCarState.velocity);
                myCarState.set(myCarState.position, myCarState.velocity, myCarState.acceleration, dt);
            }
            else
            {
                myCarState.acceleration = 0;
                myCarState.velocity = CarSimulator.Physics1D.compute_velocity(myCarState.velocity, myCarState.acceleration, dt);
                myCarState.position = CarSimulator.Physics1D.compute_position(myCarState.position, myCarState.velocity, dt);
                drag_force = CarSimulator.Physics1D.compute_dragforce(rho, dragArea, myCarState.velocity);
                myCarState.set(myCarState.position, myCarState.velocity, myCarState.acceleration, dt);
            }
        }

        public static double getMass(double mass)
        {
            return mass;
        }

        public static string getModel(string model)
        {
            return model;
        }

        public State getState()
        {
            return myCarState;
        }

        //implement inheritence

        
    }
    public class Prius : Car
    {
        public Prius() : base("Prius", 1000, 750, 0.43)
        {

        }
        public Prius(string model, double mass, double engineForce, double dragArea) : base(model, mass, engineForce, dragArea)
        {

        }
    }
    public class Mazda : Car
    {
        public Mazda() : base("Mazda 3", 1200, 600, 0.58)
        {

        }
        public Mazda(string model, double mass, double engineForce, double dragArea) : base(model, mass, engineForce, dragArea)
        {

        }
    }
    public class Tesla : Car
    {
        public Tesla() : base("Tesla", 1500, 1000, 0.51)
        {

        }
        public Tesla(string model, double mass, double engineForce, double dragArea) : base(model, mass, engineForce, dragArea)
        {

        }
    }
    public class Herbie : Car
    {
        public Herbie() : base("Herbie", 10000, 1250, 0.99)
        {

        }
        public Herbie(string model, double mass, double engineForce, double dragArea) : base(model, mass, engineForce, dragArea)
        {

        }
        public override bool accelerate(bool on)
        {
            if (on)
                drive_enable = true;
            else
                drive_enable = false;
            return drive_enable;
        }
        public override void drive(double dt)
        {
            if (drive_enable)
            {
                myCarState.acceleration = CarSimulator.Physics1D.compute_acceleration(engineForce - drag_force, mass);
                myCarState.velocity = CarSimulator.Physics1D.compute_velocity(myCarState.velocity, myCarState.acceleration, dt);
                myCarState.position = CarSimulator.Physics1D.compute_position(myCarState.position, myCarState.velocity, dt);
                drag_force = CarSimulator.Physics1D.compute_dragforce(rho, dragArea, myCarState.velocity);
                myCarState.set(myCarState.position, myCarState.velocity, myCarState.acceleration, dt);
            }
            else
            {
                myCarState.acceleration = 0;
                myCarState.velocity = CarSimulator.Physics1D.compute_velocity(myCarState.velocity, myCarState.acceleration, dt);
                myCarState.position = CarSimulator.Physics1D.compute_position(myCarState.position, myCarState.velocity, dt);
                drag_force = CarSimulator.Physics1D.compute_dragforce(rho, dragArea, myCarState.velocity);
                myCarState.set(myCarState.position, myCarState.velocity, myCarState.acceleration, dt);
            }
        }
    }
}
