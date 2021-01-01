using AdventOfCode2020.Utility;
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
            int count = CountIngredientsWithoutAllergens(filePath);
            return count.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyOne\DayTwentyOneInput.txt";
            string dangerousIngredients = GetDangerousIngredientList(filePath);
            return dangerousIngredients;
        }

        public int CountIngredientsWithoutAllergens(string filePath)
        {
            List<Food> foods = FileUtility.ParseFileToList(filePath, line => new Food(line));

            List<string> allergens = foods.SelectMany(f => f.Allergens).Distinct().ToList();
            do
            {
                foreach (var a in allergens.ToList())
                {
                    HashSet<string> candidates = foods.First(f => f.Allergens.Contains(a)).Ingredients.ToHashSet();

                    foreach (Food f in foods.Where(f => f.Allergens.Contains(a)).Skip(1))
                        candidates.IntersectWith(f.Ingredients);

                    if (candidates.Count == 1)
                    {
                        string i = candidates.Single();

                        foreach (Food f in foods)
                            f.Ingredients.Remove(i);

                        allergens.Remove(a);
                    }
                }
            } while (allergens.Count != 0);

            return foods.SelectMany(f => f.Ingredients).Count();
        }

        public string GetDangerousIngredientList(string filePath)
        {
            List<Food> foods = FileUtility.ParseFileToList(filePath, line => new Food(line));

            Dictionary<string, string> allergenMap = new Dictionary<string, string>();
            List<string> allergens = foods.SelectMany(f => f.Allergens).Distinct().ToList();
            do
            {
                foreach (var a in allergens.ToList())
                {
                    HashSet<string> candidates = foods.First(f => f.Allergens.Contains(a)).Ingredients.ToHashSet();

                    foreach (Food f in foods.Where(f => f.Allergens.Contains(a)).Skip(1))
                        candidates.IntersectWith(f.Ingredients);

                    if (candidates.Count == 1)
                    {
                        string i = allergenMap[a] = candidates.Single();

                        foreach (Food f in foods)
                            f.Ingredients.Remove(i);

                        allergens.Remove(a);
                    }
                }
            } while (allergens.Count != 0);

            return string.Join(",", allergenMap.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value));
        }
    }
}