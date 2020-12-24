namespace AdventOfCode2020.TwentyFour
{
    public class Hex
    {
        public Hex(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Hex(Hex original)
        {
            X = original.X;
            Y = original.Y;
            Z = original.Z;
        }

        public Hex Add(Hex other)
        {
            return new Hex(X + other.X, Y + other.Y, Z + other.Z);
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }

        public override bool Equals(object obj)
        {
            HexComparer comparer = new HexComparer();
            return comparer.Equals(this, (Hex)obj);
        }

        public override int GetHashCode()
        {
            HexComparer comparer = new HexComparer();
            return comparer.GetHashCode(this);
        }
    }
}