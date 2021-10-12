using System;
namespace CarSimulator
{

    public class Physics1D
    {
        // Implement the methods
        public static double compute_position(double x0, double v, double dt) {
            return x0 + v * dt;
        }
        public static double compute_velocity(double v0, double a, double dt) {
            return v0 + a * dt;
        }
        public static double compute_velocity(double x0, double t0, double x1, double t1) {
            return (x1 - x0) / (t1 - t0);
        }
        public static double compute_acceleration(double v0, double t0, double v1, double t1) {
            return (v1 - v0) / (t1 - t0);
        }
        public static double compute_acceleration(double f, double m) {
            return f / m;
        }
        public static double compute_dragforce(double air_density, double drag_area_coeff, double velocity)
        {
            return 0.5 * air_density * drag_area_coeff * Math.Pow(velocity, 2);
        }
        
    }
}
