namespace AdventOfCode2020.Seventeen
{
    public class DaySeventeen : IAdventProblemSet
    {
        public string Description()
        {
            return "Conway Cubes [HARD]";
        }

        public int SortOrder()
        {
            return 17;
        }

        public string PartA()
        {
            string filePath = @"Seventeen\DaySeventeenInput.txt";
            Dimension dimension = new Dimension(filePath, false);

            dimension.Run(6);
            int activeCubes = dimension.CountActiveCubes();

            return activeCubes.ToString();
        }

        public string PartB()
        {
            string filePath = @"Seventeen\DaySeventeenInput.txt";
            Dimension dimension = new Dimension(filePath, true);

            dimension.Run(6);
            int activeCubes = dimension.CountActiveCubes();

            return activeCubes.ToString();
        }
    }
}