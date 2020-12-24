namespace AdventOfCode2020.TwentyFour
{
    public class DayTwentyFour : IAdventProblemSet
    {
        public string Description()
        {
            return "Lobby Layout";
        }

        public int SortOrder()
        {
            return 24;
        }

        public string PartA()
        {
            string filePath = @"TwentyFour\DayTwentyFourInput.txt";
            Lobby lobby = new Lobby();

            lobby.TileFloor(filePath);

            int blackTiles = lobby.CountBlackTiles();
            return blackTiles.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyFour\DayTwentyFourInput.txt";
            Lobby lobby = new Lobby();

            lobby.TileFloor(filePath);
            lobby.DayPasses(100);

            int blackTiles = lobby.CountBlackTiles();
            return blackTiles.ToString();
        }
    }
}