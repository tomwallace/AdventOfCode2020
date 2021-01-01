using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.TwentyOne
{
    public class Food
    {
        public Food(string input)
        {
            string[] splits = input.Split(new[] { "(" }, StringSplitOptions.RemoveEmptyEntries);
            Ingredients = splits[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToList();

            string allergens = splits[1].Split(new[] { "contains", ")" }, StringSplitOptions.RemoveEmptyEntries)
                .First();
            Allergens = allergens.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToList();
        }

        public List<string> Ingredients { get; set; }

        public List<string> Allergens { get; set; }
    }
}