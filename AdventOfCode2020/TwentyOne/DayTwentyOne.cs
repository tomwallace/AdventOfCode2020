using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.TwentyOne
{
    public class DayTwentyOne : IAdventProblemSet
    {
        public string Description()
        {
            return "Allergen Assessment [HARD]";
        }

        public int SortOrder()
        {
            return 21;
        }

        public string PartA()
        {
            string filePath = @"TwentyOne\DayTwentyOneInput.txt";
            return "";
        }

        public string PartB()
        {
            string filePath = @"TwentyOne\DayTwentyOneInput.txt";
            return "";
        }

        public int CountIngredientsWithoutAllergens(string filePath)
        {
            List<Food> foods = FileUtility.ParseFileToList(filePath, line => new Food(line));
            Dictionary<string, HashSet<string>> allergenIndex = new Dictionary<string, HashSet<string>>();

            foreach (Food food in foods)
            {
                foreach (string allergen in food.Allergens)
                {
                    if (!allergenIndex.ContainsKey(allergen))
                        allergenIndex.Add(allergen, new HashSet<string>());

                    foreach (string ingredient in food.Ingredients)
                    {
                        allergenIndex[allergen].Add(ingredient);
                    }
                }
            }

            Dictionary<string, string> allergenCausers = new Dictionary<string, string>();
            foreach (string key in allergenIndex.Keys)
            {
                allergenCausers.Add(key, null);
            }

            // TODO: Figure out what I am doing
            do
            {
                var current = allergenCausers.First(a => a.Value == null);
            } while (allergenCausers.Any(a => a.Value == null));

            return 0;
        }
    }

    public class Food
    {
        public Food(string input)
        {
            // TODO: Replace with a RegEx expression
            string[] splits = input.Split(new[] { "(" }, StringSplitOptions.RemoveEmptyEntries);
            Ingredients = splits[0].Split(' ').ToList();
            string allergens = splits[1].Split(new[] { "contains", ")" }, StringSplitOptions.RemoveEmptyEntries).First();
            Allergens = allergens.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public List<string> Ingredients { get; set; }

        public List<string> Allergens { get; set; }
    }

    public class Ingredient
    {
    }

    public class Allergen
    {
    }
}