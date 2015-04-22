using System;

namespace Geomatics.GeometricalGeodesy
{
    public static class ReferenceEllipsoids
    {
        public static readonly Ellipsoid GRS80 = Ellipsoid.CreateUsingFlattening(6378137, 1/298.257222101);
    }
}
