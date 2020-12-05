using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Five
{
    public class DayFive : IAdventProblemSet
    {
        public string Description()
        {
            return "Binary Boarding";
        }

        public int SortOrder()
        {
            return 5;
        }

        public string PartA()
        {
            string filePath = @"Five\DayFiveInput.txt";
            List<AirplaneSeat> airplaneSeats = FileUtility.ParseFileToList(filePath, line => new AirplaneSeat(line));
            int highestSeatId = airplaneSeats.Max(a => a.GetSeatId());

            return highestSeatId.ToString();
        }

        public string PartB()
        {
            string filePath = @"Five\DayFiveInput.txt";
            int missingSeatId = FindMissingSeatId(filePath);

            return missingSeatId.ToString();
        }

        public int FindMissingSeatId(string filePath)
        {
            List<AirplaneSeat> airplaneSeats = FileUtility.ParseFileToList(filePath, line => new AirplaneSeat(line));
            List<int> seatIds = airplaneSeats.Select(a => a.GetSeatId()).OrderBy(a => a).ToList();

            // Find first one in the list missing, but staring with the first item actually in the list
            for (int i = seatIds.First(); i < int.MaxValue; i++)
            {
                int found = seatIds.FirstOrDefault(a => a == i);

                if (found == 0)
                    return i;
            }

            throw new Exception("Should never get here, as the seat should be missing from the collection");
        }
    }
}