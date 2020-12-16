using System.Collections.Generic;

namespace AdventOfCode2020.Fifteen
{
    public class DayFifteen : IAdventProblemSet
    {
        private readonly List<int> _input = new List<int>() { 11, 18, 0, 20, 1, 7, 16 };

        public string Description()
        {
            return "Rambunctious Recitation";
        }

        public int SortOrder()
        {
            return 15;
        }

        public string PartA()
        {
            NumberGame numberGame = new NumberGame(_input);
            int finalNumber = numberGame.Play(2020);
            return finalNumber.ToString();
        }

        public string PartB()
        {
            NumberGame numberGame = new NumberGame(_input);
            int finalNumber = numberGame.Play(30000000);
            return finalNumber.ToString();
        }
    }
}