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
            return "Shuttle Search [HARD]";
        }

        public int SortOrder()
        {
            return 13;
        }

        public string PartA()
        {
            BusRoutes busRoutes = new BusRoutes(_timestamp, _buses);
            var sortedList = busRoutes.FindBestBusWaitTime();
            long bestTime = sortedList.First().Key * sortedList.First().Value;

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
        private long timestamp;
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

        public IOrderedEnumerable<KeyValuePair<long, long>> FindBestBusWaitTime()
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

            // TODO: Consider creating a bus class?
            var sortedList = waitTimes.OrderBy(wt => wt.Value);
            return sortedList;
            //return sortedList.First().Value * sortedList.First().Key;
        }

        public long FindEarliestTimeStampAllMatch()
        {
            /*
             var (baseBusId, _) = buses[0];
             var (time, period) = (baseBusId, baseBusId);
             
             foreach (var (schedule, offset) in buses.Skip(1))
            {
              while ((time + offset) % schedule != 0) time += period;

              period *= schedule;
            }
             */
            /*
            var sortedList = FindBestBusWaitTime();


            long time = sortedList.First().Key;
            long period = time;
            foreach (var bus in sortedList.Skip(1))
            {
                while ((time + bus.Value) % bus.Key != 0)
                {
                    time += period;
                }

                period *= bus.Key;
            }
            */
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

        // TODO: Still not working
        // https://pastebin.com/jHpRYhzc
        // Solution approach
        //-------------------
        // For any two elements a and b that each have their own periodicty, when treated as a group they have periodicity a*b.
        // EG: one planet does a loop in 50 days, another in 70 days, if they line up at day=0 they'll line up again at 50*70 = 3500 days.
        // So, this approach is to find the match with the next bus, then change the periodicity by multiplying by that bus's loop time
        //   Then, move on to add another bus to the match and repeat.
        public long FindWhenAllBusesMatch()
        {
            var busWaitTimes = FindBestBusWaitTime();

            timestamp = busWaitTimes.First(b => b.Key == buses[0]).Value;
            long incrementAmount = buses[0];   //As each periodicity pairing is found, increase the increment.
            int lockedIn = 0;  //Keeps track of which buses have been matched to a periodicity with the group.

            for (long testTime = timestamp; true; testTime += incrementAmount)
            {
                int nextBusToLookFor = lockedIn + 1;
                timestamp = testTime + buses[nextBusToLookFor];

                var localBusWaitTimes = FindBestBusWaitTime();
                long nearestDepartureTime = localBusWaitTimes.First(b => b.Key == buses[nextBusToLookFor]).Value;  //GetEqualOrGreaterDepartureTime(requiredDepartureTime, busDepartureInfo[nextBusToLookFor].busNum);

                if (timestamp == nearestDepartureTime)
                {
                    incrementAmount *= nextBusToLookFor;
                    lockedIn = nextBusToLookFor;

                    if (lockedIn == buses.Count - 1) //They're all lined up!
                    {
                        return testTime;
                    }
                }
            }
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