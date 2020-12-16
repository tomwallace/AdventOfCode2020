using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Thirteen
{
    public class DayThirteen : IAdventProblemSet
    {
        private readonly long _timestamp = 1002394;
        private readonly string _buses = "13,x,x,41,x,x,x,37,x,x,x,x,x,419,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,23,x,x,x,x,x,29,x,421,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,17";
        
        public string Description()
        {
            return "Shuttle Search";
        }

        public int SortOrder()
        {
            return 13;
        }

        public string PartA()
        {
            BusRoutes busRoutes = new BusRoutes(_timestamp, _buses);
            long bestTime = busRoutes.FindBestBusWaitTime();

            return bestTime.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            return "";
        }
    }

    public class BusRoutes
    {
        private readonly long timestamp;
        private readonly List<long> buses;
        private readonly Dictionary<int, long> importantBuses;

        public BusRoutes(long timestamp, string buses)
        {
            this.timestamp = timestamp;
            string[] busesArray = buses.Split(',');
            this.buses = busesArray.Where(b => b != "x")
                .Select(b => long.Parse(b))
                .ToList();

            importantBuses = new Dictionary<int, long>();
            for (int i = 0; i < busesArray.Length; i++)
            {
                if (busesArray[i] != "x")
                {
                    importantBuses.Add(i, long.Parse(busesArray[i]));
                }
            }
        }

        public long FindBestBusWaitTime()
        {
            Dictionary<long, long> waitTimes = new Dictionary<long, long>();
            
            // TODO: Change into a select statement
            foreach (long bus in buses)
            {
                double divisor = (double)timestamp/bus;
                long roundedUp = (long) Math.Ceiling(divisor);
                long nearestTime = roundedUp * bus;
                waitTimes.Add(bus, nearestTime - timestamp);
            }

            var sortedList = waitTimes.OrderBy(wt => wt.Value);
            return sortedList.First().Value * sortedList.First().Key;
        }

        public long FindEarliestTimeStampAllMatch()
        {
            // TODO: Refactor to split massive values once prove this works
            for (long i = 0; i < long.MaxValue; i++)
            {
                if (IsValidTimestamp(i))
                    return i;
            }

            throw new Exception("should never get here");
        }

        public bool IsValidTimestamp(long ts)
        {
            return importantBuses.All(ib => (ts + ib.Key) % ib.Value == 0);
        }

        // TODO: Break this down
        // Need to do some variant of LCM, modified for the offset
        static int GCD(int[] numbers)
        {
            return numbers.Aggregate(GCD);
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        static int gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static int lcm(int a, int b)
        {
            return (a / gcf(a, b)) * b;
        }

    }
}