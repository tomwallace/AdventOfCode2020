using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Three
{
    public class DayThree : IAdventProblemSet
    {
        public string Description()
        {
            return "Toboggan Trajectory";
        }

        public int SortOrder()
        {
            return 3;
        }

        public string PartA()
        {
            string filePath = @"Three\DayThreeInput.txt";
            Slope slope = new Slope(1, 3);

            int treesHit = CalculateTreesHit(filePath, slope);

            return treesHit.ToString();
        }

        public string PartB()
        {
            string filePath = @"Three\DayThreeInput.txt";
            int product = ProductOfMultipleRuns(filePath);

            return product.ToString();
        }

        public int ProductOfMultipleRuns(string filePath)
        {
            List<Slope> slopes = new List<Slope>()
            {
                new Slope(1,1),
                new Slope(1,3),
                new Slope(1,5),
                new Slope(1,7),
                new Slope(2,1)
            };

            int product = 1;
            foreach (Slope slope in slopes)
            {
                product *= CalculateTreesHit(filePath, slope);
            }

            return product;
        }

        public int CalculateTreesHit(string filePath, Slope slope)
        {
            List<char[]> map = FileUtility.ParseFileToList(filePath, line => line.ToCharArray());
            int treesHit = 0;

            // Our current pointers
            int currentRow = 0;
            int currentCol = 0;
            int mapWidth = map.First().Length;

            // Loop through the map and determine if we hit a tree
            do
            {
                char location = map[currentRow][currentCol];

                if (location == '#')
                    treesHit++;

                // Move right, including wrap around
                currentCol += slope.Right;
                if (currentCol >= mapWidth)
                    currentCol = currentCol % mapWidth;

                // Move down
                currentRow += slope.Down;
            } while (currentRow < map.Count);

            return treesHit;
        }
    }
}