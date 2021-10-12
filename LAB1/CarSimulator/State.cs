using System;
namespace CarSimulator
{
    public class State
    {
        public double position;
        public double velocity;
        public double acceleration;
        public double time;

        //implement methods like set, constructors (if applicable)

        // initialize everything to zero
        public State()
        {
            this.position = 0;
            this.velocity = 0;
            this.acceleration = 0;
            this.time = 0;
        }

        // overloaded constructor
        public State(double position, double velocity, double acceleration, double time)
        {
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.time = time;
        }

        // creating instance of State
        public void set(double position, double velocity, double acceleration, double time)
        {
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.time = time;
        }

        // print all the values

    }
}
