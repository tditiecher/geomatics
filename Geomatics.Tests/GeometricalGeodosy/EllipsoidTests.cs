using System;

using NUnit.Framework;

using Geomatics.GeometricalGeodesy;

namespace Geomatics.Tests.GeometricalGeodosy
{
    [TestFixture]
    public class EllipsoidTests
    {
        private const double AllowableDelta = 1e-10;

        [Test]
        public void CreationUsingFlattening()
        {
            Assert.AreEqual(1000, Ellipsoid.CreateUsingFlattening(1000, 0).SemiMinor, AllowableDelta);
            Assert.AreEqual(900, Ellipsoid.CreateUsingFlattening(1000, 0.1).SemiMinor, AllowableDelta);
        }

        [Test]
        public void Flattening()
        {
            Assert.AreEqual(0, new Ellipsoid(1000, 1000).Flattening, AllowableDelta);
            Assert.AreEqual(0.1, new Ellipsoid(1000, 900).Flattening, AllowableDelta);
            Assert.AreEqual(1 / 298.26, new Ellipsoid(6378135, 6356750.52).Flattening, AllowableDelta);
        }

        [Test]
        public void Eccentricity()
        {
            Assert.AreEqual(0, new Ellipsoid(1000, 1000).Eccentricity, AllowableDelta);
            Assert.AreEqual(0.08181881066d, new Ellipsoid(6378135, 6356750.52).Eccentricity, AllowableDelta);
        }

        [Test]
        public void DistanceToCenter()
        {
            Assert.AreEqual(1000, new Ellipsoid(1000, 900).DistanceToCenter(new Angle(0)), AllowableDelta);
            Assert.AreEqual(900, new Ellipsoid(1000, 900).DistanceToCenter(new Angle(90)), AllowableDelta);
            Assert.AreEqual(946.05899620974d, new Ellipsoid(1000, 900).DistanceToCenter(new Angle(135)), AllowableDelta);
        }

        [Test]
        public void RadiusOfCurvature()
        {
            // Meridian
            Assert.AreEqual(6364009, Math.Round(ReferenceEllipsoids.GRS80.RadiusOfCurvature(new Angle(41.98097), new Angle(0))));
            
            // PrimeVertical
            Assert.AreEqual(6387710, Math.Round(ReferenceEllipsoids.GRS80.RadiusOfCurvature(new Angle(41.98097), new Angle(90))));

            // Normal Section
            Assert.AreEqual(6375838, Math.Round(ReferenceEllipsoids.GRS80.RadiusOfCurvature(new Angle(41.98097), new Angle(45))));
        }
    }
}
