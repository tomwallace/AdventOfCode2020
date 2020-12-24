using System.Collections.Generic;

namespace AdventOfCode2020.TwentyFour
{
    public class HexComparer : IEqualityComparer<Hex>
    {
        public bool Equals(Hex c1, Hex c2)
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

        public int GetHashCode(Hex c)
        {
            return $"{c}".GetHashCode();
        }
    }
}