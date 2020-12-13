namespace AdventOfCode2020.Eleven
{
    public class DayEleven : IAdventProblemSet
    {
        public string Description()
        {
            return "Seating System";
        }

        public int SortOrder()
        {
            return 11;
        }

        public string PartA()
        {
            string filePath = @"Eleven\DayElevenInput.txt";
            SeatingArea seatingArea = new SeatingArea(filePath);

            seatingArea.Run(false);
            int occupiedSeats = seatingArea.CountOccupiedSeats();

            return occupiedSeats.ToString();
        }

        public string PartB()
        {
            string filePath = @"Eleven\DayElevenInput.txt";
            SeatingArea seatingArea = new SeatingArea(filePath);

            seatingArea.Run(true);
            int occupiedSeats = seatingArea.CountOccupiedSeats();

            return occupiedSeats.ToString();
        }
    }
}