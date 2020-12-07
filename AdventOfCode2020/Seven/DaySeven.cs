using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Seven
{
    public class DaySeven : IAdventProblemSet
    {
        private readonly string _targetBagName = "shiny gold";

        public string Description()
        {
            return "Handy Haversacks";
        }

        public int SortOrder()
        {
            return 7;
        }

        public string PartA()
        {
            string filePath = @"Seven\DaySevenInput.txt";
            int numberOfBags = GetNumberOfBagsThatContainShinyGold(filePath);

            return numberOfBags.ToString();
        }

        public string PartB()
        {
            string filePath = @"Seven\DaySevenInput.txt";
            int numberOfBags = GetTotalNumberOfBagsContainedByShinyGold(filePath);

            return numberOfBags.ToString();
        }

        public int GetNumberOfBagsThatContainShinyGold(string filePath)
        {
            List<Bag> bags = FileUtility.ParseFileToList(filePath, line => new Bag(line));
            Dictionary<string, Bag> bagReference = bags.ToDictionary(b => b.Name, b => b);

            // Link the bags
            foreach (Bag bag in bags)
            {
                bag.Link(bagReference);
            }

            int totalBags = 0;
            foreach (Bag bag in bags)
            {
                totalBags += BagQueueEvaluation(bag);
            }

            return totalBags;
        }

        public int GetTotalNumberOfBagsContainedByShinyGold(string filePath)
        {
            List<Bag> bags = FileUtility.ParseFileToList(filePath, line => new Bag(line));
            Dictionary<string, Bag> bagReference = bags.ToDictionary(b => b.Name, b => b);

            // Link the bags
            foreach (Bag bag in bags)
            {
                bag.Link(bagReference);
            }

            Bag shinyGoldStart = bagReference[_targetBagName];

            // Recurse to find the bags, but don't count the original
            return BagRecurseForTotal(shinyGoldStart) - 1;
        }

        // Used a queue because we only want to count the root bag
        private int BagQueueEvaluation(Bag bag)
        {
            // Escape if bag is a shiny gold bag itself
            if (bag.Name == _targetBagName)
                return 0;

            Queue<Bag> queue = new Queue<Bag>();
            queue.Enqueue(bag);
            while (queue.Any())
            {
                Bag current = queue.Dequeue();

                // Found it - count the original bag
                if (current.ContainedBags.Any(bc => bc.Bag.Name == _targetBagName))
                    return 1;

                // Otherwise add any child bags
                foreach (Bag containedBag in current.ContainedBags.Select(cb => cb.Bag))
                {
                    queue.Enqueue(containedBag);
                }
            };

            // We made it to the bottom, so not found
            return 0;
        }

        // Recursion works great for getting the total
        private int BagRecurseForTotal(Bag bag)
        {
            // Look at each of its bags, multiplying by the number of that bag
            return 1 + bag.ContainedBags.Sum(cb => cb.Number * BagRecurseForTotal(cb.Bag));
        }
    }
}