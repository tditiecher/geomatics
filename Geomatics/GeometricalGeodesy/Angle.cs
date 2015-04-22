using System;

namespace Geomatics.GeometricalGeodesy
{
    public struct Angle
    {
        public readonly double Degrees;

        public Angle(double degrees)
        {
            Degrees = degrees;
        }

        public double Sin
        {
            get { return Math.Sin(Math.PI*Degrees/180.0); }
        }

        public double Cos
        {
            get { return Math.Cos(Math.PI*Degrees/180.0); }
        }
    }
}
