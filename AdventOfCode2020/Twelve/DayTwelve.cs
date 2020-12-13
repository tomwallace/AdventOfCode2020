using AdventOfCode2020.Utility;
using System.Collections.Generic;

namespace AdventOfCode2020.Twelve
{
    public class DayTwelve : IAdventProblemSet
    {
        public string Description()
        {
            return "Rain Risk";
        }

        public int SortOrder()
        {
            return 12;
        }

        public string PartA()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            List<string> instructions = FileUtility.ParseFileToList(filePath, line => line);

            Ship ship = new Ship(null);
            ship.Move(instructions);
            int distance = ship.GetManhattanDistanceFromOrigin();

            return distance.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            List<string> instructions = FileUtility.ParseFileToList(filePath, line => line);

            Location waypoint = new Location() { X = 10, Y = -1 };
            Ship ship = new Ship(waypoint);
            ship.Move(instructions);
            int distance = ship.GetManhattanDistanceFromOrigin();

            return distance.ToString();
        }
    }
}