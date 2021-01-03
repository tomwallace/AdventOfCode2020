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
            BusRoutes busRoutes = new BusRoutes(_timestamp, _buses);
            long timeStampWhenMatch = busRoutes.FindWhenAllBusesMatch();

            return timeStampWhenMatch.ToString();
        }
    }
}