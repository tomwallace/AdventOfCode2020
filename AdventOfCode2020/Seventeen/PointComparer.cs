using System.Collections.Generic;

namespace AdventOfCode2020.Seventeen
{
    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point c1, Point c2)
        {
            if (c2 == null && c1 == null)
                return true;
            else if (c1 == null | c2 == null)
                return false;
            else if (c1.ToString() == c2.ToString())
                return true;
            else
                return false;
        }

        public int GetHashCode(Point c)
        {
            return $"{c}".GetHashCode();
        }
    }
}