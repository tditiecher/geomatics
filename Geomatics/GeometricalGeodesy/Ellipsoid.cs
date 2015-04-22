using System;

namespace Geomatics.GeometricalGeodesy
{
    public class Ellipsoid
    {
        public readonly double SemiMajor;
        public readonly double SemiMinor;

        public double Flattening
        {
            get { return 1 - (SemiMinor/SemiMajor); }
        }

        public double EccentricitySqr
        {
            get { return (SemiMajor*SemiMajor - SemiMinor*SemiMinor)/(SemiMajor*SemiMajor); }
        }

        public double Eccentricity
        {
            get { return Math.Sqrt(EccentricitySqr); }
        }

        public Ellipsoid(double semiMajor, double semiMinor)
        {
            SemiMajor = semiMajor;
            SemiMinor = semiMinor;
        }

        public static Ellipsoid CreateUsingFlattening(double semiMajor, double flattening)
        {
            double semiMinor = semiMajor*(1 - flattening);
            return new Ellipsoid(semiMajor, semiMinor);
        }

        /// <summary>
        /// Calculates the distance from the ellipse to its center at the given geocentric latitude.
        /// </summary>
        /// <param name="psi">
        /// Geocentric latitude (psi) is the angle between a line segment connecting a reference ellipsoid's center to a location of
        /// interest, and the projection of that segment into the equatorial plane.
        /// </param>
        /// <returns>Distance in meters.</returns>
        public double DistanceToCenter(Angle psi)
        {
            return SemiMinor/Math.Sqrt(1 - (EccentricitySqr*psi.Cos*psi.Cos));
        }

        /// <summary>
        /// Calculates the radius of curvature at the given point of interest on the ellipsoid.
        /// </summary>
        /// <param name="phi">Geodetic latitude (phi).</param>
        /// <param name="azimuth">Azimuth of the sectioning plane at the point of interest.</param>
        /// <returns>Radius in meters.</returns>
        public double RadiusOfCurvature(Angle phi, Angle azimuth)
        {
            double radiusInMeridian = (SemiMajor*(1 - EccentricitySqr))/Math.Pow(1 - (EccentricitySqr*phi.Sin*phi.Sin), 3/2d);
            double radiusInPrimeVertical = SemiMajor/Math.Pow(1 - (EccentricitySqr*phi.Sin*phi.Sin), 1/2d);
            // Using Euler's Theorem.
            return 1/((azimuth.Cos*azimuth.Cos/radiusInMeridian) + (azimuth.Sin*azimuth.Sin/radiusInPrimeVertical));
        }
    }
}
