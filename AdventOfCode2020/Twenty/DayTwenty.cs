using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Twenty
{
    public class DayTwenty : IAdventProblemSet
    {
        public string Description()
        {
            return "Jurassic Jigsaw [VERY HARD]";
        }

        public int SortOrder()
        {
            return 20;
        }

        public string PartA()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            long product = FindCornerProduct(filePath);
            return product.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            long count = FindActivePixelsNotPartOfSeaMonster(filePath);
            return count.ToString();
        }

        public long FindCornerProduct(string filePath)
        {
            Grid[,] overallMap = MakeCompositeMap(filePath);

            long cornerProduct = overallMap[0, 0].Id
                                 * overallMap[0, overallMap.GetLength(0) - 1].Id
                                 * overallMap[overallMap.GetLength(1) - 1, 0].Id
                                 * overallMap[overallMap.GetLength(0) - 1, overallMap.GetLength(1) - 1].Id;

            return cornerProduct;
        }

        public long FindActivePixelsNotPartOfSeaMonster(string filePath)
        {
            Grid[,] overallMap = MakeCompositeMap(filePath);
            // Trim all
            for (int gridRow = 0; gridRow < overallMap.GetLength(0); gridRow++)
            {
                for (int gridCol = 0; gridCol < overallMap.GetLength(1); gridCol++)
                {
                    overallMap[gridRow, gridCol].TrimEdges();
                    overallMap[gridRow, gridCol].Output();
                }
            }
            Grid masterGrid = new Grid(overallMap);
            int mostMonsters = 0;
            // Go through all possibilities to find the most monsters
            for (int i = 0; i < 8; i++)
            {
                masterGrid.Output();

                int found = masterGrid.ContainsNumberOfSeaMonsters();
                if (found > mostMonsters)
                    mostMonsters = found;

                // Otherwise rotate/flip
                if (i == 3)
                    masterGrid.FlipVertical();

                masterGrid.Rotate();
            }

            long pixels = masterGrid.CountPixels();

            return pixels - (mostMonsters * 15);
        }

        private Grid[,] MakeCompositeMap(string filePath)
        {
            List<Grid> grids = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new Grid(lineGroup), Grid.Separator);
            Dictionary<string, List<Grid>> validEdges = new Dictionary<string, List<Grid>>();
            foreach (Grid grid in grids)
            {
                // Go through all possibilities to make our dictionary
                for (int i = 0; i < 8; i++)
                {
                    string edge = grid.TopEdge();
                    if (!validEdges.ContainsKey(edge))
                        validEdges.Add(edge, new List<Grid>());

                    validEdges[edge].Add(grid);

                    // Otherwise rotate/flip
                    if (i == 3)
                        grid.FlipVertical();

                    grid.Rotate();
                }
            }

            // Find height/width
            int size = (int)Math.Sqrt(grids.Count);
            Grid[,] overallMap = new Grid[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid above = row == 0 ? null : overallMap[row - 1, col];
                    Grid left = col == 0 ? null : overallMap[row, col - 1];
                    Grid found = FindTargetGrid(above, left, grids, validEdges);
                    //found.TrimEdges();
                    // Remove the grid we found, so we don't alter it - this made me stumble for quite a while
                    grids.Remove(found);
                    overallMap[row, col] = found;
                }
            }

            return overallMap;
        }

        private Grid FindTargetGrid(Grid above, Grid left, List<Grid> grids, Dictionary<string, List<Grid>> validEdges)
        {
            foreach (Grid grid in grids)
            {
                // Rotate the grid to see if it matches
                for (int i = 0; i < 8; i++)
                {
                    // Looking for top left
                    if (above == null && left == null)
                    {
                        if (IsEdge(grid.TopEdge(), validEdges) && IsEdge(grid.LeftEdge(), validEdges))
                        {
                            return grid;
                        }

                        // Otherwise rotate/flip
                        if (i == 3)
                            grid.FlipVertical();

                        grid.Rotate();
                    }
                    else
                    {
                        bool matchesTop = above == null
                            ? IsEdge(grid.TopEdge(), validEdges)
                            : above.Id != grid.Id && above.BottomEdge() == grid.TopEdge();

                        bool matchesLeft = left == null
                            ? IsEdge(grid.LeftEdge(), validEdges)
                            : left.Id != grid.Id && left.RightEdge() == grid.LeftEdge();

                        if (matchesTop && matchesLeft)
                        {
                            return grid;
                        }
                    }

                    // Otherwise rotate/flip
                    if (i == 3)
                        grid.FlipVertical();

                    grid.Rotate();
                }
            }

            throw new Exception("Should never NOT find a grid");
        }

        private bool IsEdge(string edge, Dictionary<string, List<Grid>> validEdges)
        {
            return validEdges[edge].Count() == 1;
        }
    }
}