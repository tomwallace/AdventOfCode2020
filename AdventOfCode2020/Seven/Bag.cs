using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Seven
{
    public class Bag
    {
        // Reference string to contained bags we will work through once a collection of bags are available to connect with
        private readonly string _containingBags;

        public Bag(string input)
        {
            // Sample:  light red bags contain 1 bright white bag, 2 muted yellow bags.
            string[] splits = input.Split(new string[] { " contain " }, StringSplitOptions.None);
            Name = splits[0].Replace(" bags", "").Replace(" bag", "");

            _containingBags = splits[1].Replace(".", "");
            ContainedBags = new List<BagConnection>();
        }

        public string Name { get; set; }

        public List<BagConnection> ContainedBags { get; set; }

        // Link with other bags
        public void Link(Dictionary<string, Bag> bags)
        {
            if (_containingBags == "no other bags")
                return;

            List<string> splits = _containingBags.Split(',').ToList();
            foreach (string split in splits)
            {
                string sansBag = split.Replace(" bags", "").Replace(" bag", "");
                int number = int.Parse(sansBag.Trim().Split(' ')[0]);
                string numberRemoved = sansBag.Replace(number.ToString(), "").Trim();

                ContainedBags.Add(new BagConnection()
                {
                    Number = number,
                    Bag = bags[numberRemoved]
                });
            }
        }

        public override string ToString()
        {
            return $"Bag: {Name}";
        }
    }
}