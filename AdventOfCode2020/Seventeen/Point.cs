namespace AdventOfCode2020.Seventeen
{
    public class Point
    {
        public Point(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int W { get; set; }

        public override string ToString()
        {
            return $"{X}|{Y}|{Z}|{W}";
        }

        public override bool Equals(object obj)
        {
            PointComparer comparer = new PointComparer();
            return comparer.Equals(this, (Point)obj);
        }

        public override int GetHashCode()
        {
            PointComparer comparer = new PointComparer();
            return comparer.GetHashCode(this);
        }
    }
}