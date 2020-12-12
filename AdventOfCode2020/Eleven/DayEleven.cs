using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

    public class SeatingArea
    {
        private List<char[]> _seats;

        private readonly HashSet<string> _seenConfigurations;

        public SeatingArea(string filePath)
        {
            _seats = FileUtility.ParseFileToList(filePath, line => line.ToCharArray());

            _seenConfigurations = new HashSet<string>();
        }

        public void Run(bool useVisibleSeats)
        {
            do
            {
                Debug.WriteLine(ToString());

                // Start by adding current seating to seenConfigs
                _seenConfigurations.Add(ToString());

                // Iterate
                _seats = IterateAndReturnNewSeats(useVisibleSeats);
            } while (!_seenConfigurations.Contains(ToString()));
        }

        public int CountOccupiedSeats()
        {
            return ToString().ToCharArray().Count(c => c == '#');
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; row < _seats.Count; row++)
            {
                StringBuilder rowBuilder = new StringBuilder();
                for (int col = 0; col < _seats[0].Length; col++)
                {
                    rowBuilder.Append(_seats[row][col]);
                }

                builder.AppendLine(rowBuilder.ToString());
            }

            return builder.ToString();
        }

        private List<char[]> IterateAndReturnNewSeats(bool useVisibleSeats)
        {
            int targetOccupiedSeats = useVisibleSeats ? 5 : 4;
            List<char[]> newSeats = new List<char[]>();
            for (int row = 0; row < _seats.Count; row++)
            {
                StringBuilder rowBuilder = new StringBuilder();
                for (int col = 0; col < _seats[0].Length; col++)
                {
                    int occupiedSeats = GetOccupiedSeats(useVisibleSeats, row, col);
                    // If occupied and X or more adjacent chairs are occupied, then empty
                    if (_seats[row][col] == '#' && occupiedSeats >= targetOccupiedSeats)
                        rowBuilder.Append('L');
                    // If unoccupied and no occupied seats, fill
                    else if (_seats[row][col] == 'L' && occupiedSeats == 0)
                        rowBuilder.Append('#');
                    // Otherwise, remain the same
                    else
                        rowBuilder.Append(_seats[row][col]);
                }

                newSeats.Add(rowBuilder.ToString().ToCharArray());
            }

            return newSeats;
        }

        private int GetOccupiedSeats(bool useVisibleSeats, int currentRow, int currentCol)
        {
            List<Point> surroundingPoints = new List<Point>{new Point(-1,-1), new Point(-1,0), new Point(-1,1), new Point(0,-1),
                new Point(0,1), new Point(1,-1), new Point(1,0), new Point(1,1)};

            if (!useVisibleSeats)
                return surroundingPoints.Count(p => IsSeatOccupied(currentRow + p.Row, currentCol + p.Col));

            return surroundingPoints.Sum(p => EvaluateDirection(currentRow, p.Row, currentCol, p.Col));
        }

        // Looks at just the surrounding squares
        private bool IsSeatOccupied(int row, int col)
        {
            // Assumes seats outside of bounds are unoccupied
            if (row < 0 || col < 0 || row >= _seats.Count || col >= _seats[0].Length)
                return false;

            return _seats[row][col] == '#';
        }

        // Considers the entire tangent until a non-floor point is hit or the boundary is hit
        private int EvaluateDirection(int currentRow, int rowMod, int currentCol, int colMod)
        {
            int row = currentRow + rowMod;
            int col = currentCol + colMod;

            while (row >= 0 && col >= 0 && row < _seats.Count && col < _seats[0].Length)
            {
                if (_seats[row][col] == '#')
                    return 1;

                if (_seats[row][col] == 'L')
                    return 0;

                row += rowMod;
                col += colMod;
            };

            return 0;
        }
    }
}