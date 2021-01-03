using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Thirteen
{
    public class BusRoutes
    {
        private long timestamp;
        private readonly List<Bus> buses;
        private readonly Dictionary<long, Bus> busDict;

        public BusRoutes(long timestamp, string buses)
        {
            this.timestamp = timestamp;
            this.buses = new List<Bus>();
            busDict = new Dictionary<long, Bus>();
            string[] busesArray = buses.Split(',');
            for (int i = 0; i < busesArray.Length; i++)
            {
                if (busesArray[i] != "x")
                {
                    Bus bus = new Bus()
                    {
                        BusNumber = long.Parse(busesArray[i]),
                        PositionOffset = i
                    };
                    this.buses.Add(bus);
                    busDict.Add(bus.BusNumber, bus);
                }
            }
        }

        public IOrderedEnumerable<KeyValuePair<long, long>> FindBestBusWaitTime()
        {
            Dictionary<long, long> waitTimes = new Dictionary<long, long>();

            foreach (Bus bus in buses)
            {
                double divisor = (double)timestamp / bus.BusNumber;
                long roundedUp = (long)Math.Ceiling(divisor);
                long nearestTime = roundedUp * bus.BusNumber;
                waitTimes.Add(bus.BusNumber, nearestTime - timestamp);
            }

            var sortedList = waitTimes.OrderBy(wt => wt.Value);
            return sortedList;
        }

        public bool IsValidTimestamp(long ts)
        {
            return busDict.All(ib => (ts + ib.Key) % ib.Value.BusNumber == 0);
        }

        // I had reddit help for solving Part B - https://pastebin.com/jHpRYhzc
        // Solution approach
        //-------------------
        // For any two elements a and b that each have their own periodicty, when treated as a group they have periodicity a*b.
        // EG: one planet does a loop in 50 days, another in 70 days, if they line up at day=0 they'll line up again at 50*70 = 3500 days.
        // So, this approach is to find the match with the next bus, then change the periodicity by multiplying by that bus's loop time
        //   Then, move on to add another bus to the match and repeat.
        public long FindWhenAllBusesMatch()
        {
            timestamp = GetEqualOrGreaterDepartureTime(timestamp, buses[0].BusNumber);
            long incrementAmount = buses[0].BusNumber;   //As each periodicity pairing is found, increase the increment.
            int lockedIn = 0;  //Keeps track of which buses have been matched to a periodicity with the group.

            for (long testTime = timestamp; true; testTime += incrementAmount)
            {
                int nextBusToLookFor = lockedIn + 1;

                long requiredDepartureTime = testTime + buses[nextBusToLookFor].PositionOffset;
                long nearestDepartureTime = GetEqualOrGreaterDepartureTime(requiredDepartureTime, buses[nextBusToLookFor].BusNumber);

                if (requiredDepartureTime == nearestDepartureTime)
                {
                    incrementAmount *= buses[nextBusToLookFor].BusNumber;
                    lockedIn = nextBusToLookFor;

                    if (lockedIn == buses.Count - 1) //They're all lined up!
                    {
                        return testTime;
                    }
                }
            }
        }

        private long GetEqualOrGreaterDepartureTime(long targetDepartureTime, long busNum)
        {
            //Busses get around the loop in the time equal to their bus number.
            //All buses depart at t=0.
            //Find the time the bus is at the station greater than or equal to the target departure.
            //To find it:
            //  1) Find the number of cycles the bus runs in the target departure time.
            //  2) If there's a remainder, round up to the next highest number of cycles.
            //  3) Multiply the number of cycles by the bus' loop time to get the arrival that is >= the desired departure time.
            //
            // An earlier solution used Mathf.CeilToInt() but that approach failed when using long values (it would wrap to -ve numbers)
            // So this more manual solution was needed.

            long quotient = targetDepartureTime / busNum;    // (1)
            long remainder = targetDepartureTime % busNum;
            if (remainder > 0)
            {
                quotient++;                                  // (2)
            }
            long earliestDepartureTime = quotient * busNum;  // (3)

            return earliestDepartureTime;
        }
    }
}