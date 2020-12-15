using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Ten
{
    public class DayTen : IAdventProblemSet
    {
        public string Description()
        {
            return "Adapter Array [HARD]";
        }

        public int SortOrder()
        {
            return 10;
        }

        public string PartA()
        {
            string filePath = @"Ten\DayTenInput.txt";
            int product = FindJoltDifferenceProduct(filePath);

            return product.ToString();
        }

        public string PartB()
        {
            string filePath = @"Ten\DayTenInput.txt";
            //return "";
            long count = FindNumberOfValidAdapterCombos(filePath);

            return count.ToString();
        }

        public int FindJoltDifferenceProduct(string filePath)
        {
            int flexibility = 3;
            List<Adapter> adapters = FileUtility.ParseFileToList(filePath, line => new Adapter(int.Parse(line), flexibility));

            HashSet<Adapter> usedAdapters = new HashSet<Adapter>();
            int currentJoltage = 0;
            int oneJoltDifferences = 0;
            int threeJoltDifferences = 0;

            do
            {
                Adapter lowest = adapters
                    .Where(a => a.JoltageRating >= currentJoltage && a.JoltageRating <= currentJoltage + flexibility && !usedAdapters.Contains(a))
                    .OrderBy(a => a.JoltageRating)
                    .First();

                usedAdapters.Add(lowest);

                switch (lowest.JoltageRating - currentJoltage)
                {
                    case 1:
                        oneJoltDifferences++;
                        break;

                    case 3:
                        threeJoltDifferences++;
                        break;

                    default:
                        throw new Exception($"Unanticipated difference of: {lowest.JoltageRating - currentJoltage}");
                }

                currentJoltage = lowest.JoltageRating;
            } while (usedAdapters.Count < adapters.Count);

            return oneJoltDifferences * (threeJoltDifferences + 1);
        }

        public long FindNumberOfValidAdapterCombos(string filePath)
        {
            List<int> adapters = FileUtility.ParseFileToList(filePath, line => int.Parse(line))
                .OrderBy(a => a).ToList();

            adapters.Add(adapters.Max() + 3);

            // I struggled with this one and got some help from Reddit.

            // Seed for first value - 0
            var routes = new Dictionary<int, long> { { 0, 1 } };

            foreach (var adapter in adapters)
            {
                var minusOne = routes.ContainsKey(adapter - 1) ? routes[adapter - 1] : 0;
                var minusTwo = routes.ContainsKey(adapter - 2) ? routes[adapter - 2] : 0;
                var minusThree = routes.ContainsKey(adapter - 3) ? routes[adapter - 3] : 0;

                routes[adapter] = minusOne + minusTwo + minusThree;
            }

            return routes[adapters.Max()];
        }
    }
}