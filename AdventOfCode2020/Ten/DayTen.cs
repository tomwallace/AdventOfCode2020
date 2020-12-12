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
            List<int> adapters = FileUtility.ParseFileToList(filePath, line => int.Parse(line));
            var startingJoltage = 0;
            var max = adapters.Max();

            return FindValidAdaptersRecurse(startingJoltage, max, adapters);
        }

        public long FindValidAdaptersRecurse(int currentJoltage, int max, List<int> adapters)
        {
            int flexibility = 3;

            if (currentJoltage >= max)
                return 1;

            List<int> usableAdapters = adapters.Where(a => a >= currentJoltage && a <= currentJoltage + flexibility).ToList();
            if (!usableAdapters.Any())
                return 0;

            long recursiveReturnValue = 0;
            // Recurse over usableAdapters
            foreach (int usableAdapter in usableAdapters)
            {
                List<int> adjustedList = adapters.Where(a => a > usableAdapter).ToList();
                adjustedList.Remove(usableAdapter);
                int pathJoltage = usableAdapter;

                recursiveReturnValue += FindValidAdaptersRecurse(pathJoltage, max, adjustedList);
            }

            return recursiveReturnValue;
        }

        public long FindNumberOfValidAdapterCombosWithQueue(string filePath)
        {
            List<int> adapters = FileUtility.ParseFileToList(filePath, line => int.Parse(line));
            var startingJoltage = 0;
            int flexibility = 3;
            var max = adapters.Max();
            long total = 0;

            Queue<AdapterStep> queue = new Queue<AdapterStep>();
            queue.Enqueue(new AdapterStep(startingJoltage, adapters));

            do
            {
                AdapterStep current = queue.Dequeue();
                if (current.CurrentJoltage >= max)
                    total++;
                else
                {
                    List<int> usableAdapters = current.Adapters.Where(a =>
                        a >= current.CurrentJoltage && a <= current.CurrentJoltage + flexibility).ToList();

                    foreach (int usableAdapter in usableAdapters)
                    {
                        List<int> adjustedList = new List<int>(current.Adapters); //.Select(a => a).ToList());adapters.Select(a => a).ToList();
                        adjustedList.Remove(usableAdapter);
                        // int pathJoltage = current.CurrentJoltage + usableAdapter;

                        queue.Enqueue(new AdapterStep(usableAdapter, adjustedList));
                    }
                }
            } while (queue.Any());

            return total;
        }
    }

    public class Adapter
    {
        public Adapter(int joltageRating, int flexibility)
        {
            JoltageRating = joltageRating;
            Flexibility = flexibility;
        }

        public int JoltageRating { get; set; }

        public int Flexibility { get; set; }
    }

    public class AdapterStep
    {
        public AdapterStep(int currentJoltage, List<int> adapters)
        {
            CurrentJoltage = currentJoltage;
            Adapters = adapters;
        }

        public int CurrentJoltage { get; set; }

        public List<int> Adapters { get; set; }
    }
}