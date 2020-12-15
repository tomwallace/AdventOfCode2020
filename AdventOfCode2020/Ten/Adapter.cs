namespace AdventOfCode2020.Ten
{
    public class Adapter
    {
        public Adapter(int joltageRating, int flexibility)
        {
            JoltageRating = joltageRating;
            Flexibility = flexibility;
        }

        public int JoltageRating { get; set; }

        public int Flexibility { get; set; }
    }
}