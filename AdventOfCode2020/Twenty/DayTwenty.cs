using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            return "";
        }

        public long FindCornerProduct(string filePath)
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
                    // Remove the grid we found, so we don't alter it - this made me stumble for quite a while
                    grids.Remove(found);
                    overallMap[row, col] = found;
                }
            }

            long cornerProduct = overallMap[0, 0].Id * overallMap[0, size - 1].Id * overallMap[size - 1, 0].Id *
                                 overallMap[size - 1, size - 1].Id;

            return cornerProduct;
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
                            grid.Output();
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
                            grid.Output();
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

    public class Grid
    {
        public static string Separator => "|";

        private List<char[]> _points;

        public Grid(string lineGroup)
        {
            List<string> lines = lineGroup.Split('|').ToList();

            var idString = lines.First(l => l.Contains("Tile")).Split(new string[] { "Tile ", ":" }, StringSplitOptions.None);
            Id = long.Parse(idString[1]);
            _points = lines.Where(l => !l.Contains("Tile"))
                .Select(l => l.ToCharArray())
                .ToList();
        }

        public long Id { get; }

        public string TopEdge()
        {
            return string.Join("", _points.First());
        }

        public string BottomEdge()
        {
            return string.Join("", _points.Last());
        }

        public string LeftEdge()
        {
            StringBuilder builder = new StringBuilder();
            foreach (char[] row in _points)
            {
                builder.Append(row.First());
            }

            return builder.ToString();
        }

        public string RightEdge()
        {
            StringBuilder builder = new StringBuilder();
            foreach (char[] row in _points)
            {
                builder.Append(row.Last());
            }

            return builder.ToString();
        }

        public void Rotate()
        {
            List<char[]> newPoints = new List<char[]>();

            for (int col = 0; col < _points[0].Length; col++)
            {
                StringBuilder builder = new StringBuilder();
                for (int row = _points.Count - 1; row >= 0; row--)
                {
                    builder.Append(_points[row][col]);
                }
                newPoints.Add(builder.ToString().ToCharArray());
            }

            _points = newPoints;
        }

        public void FlipVertical()
        {
            List<char[]> newPoints = new List<char[]>();

            for (int row = _points.Count - 1; row >= 0; row--)
            {
                StringBuilder builder = new StringBuilder();
                for (int col = 0; col < _points[0].Length; col++)
                {
                    builder.Append(_points[row][col]);
                }

                newPoints.Add(builder.ToString().ToCharArray());
            }

            _points = newPoints;
        }

        public void Output()
        {
            Debug.WriteLine($"Id = {Id}");
            for (int row = 0; row < _points.Count; row++)
            {
                StringBuilder rowBuilder = new StringBuilder();
                for (int col = 0; col < _points[0].Length; col++)
                {
                    rowBuilder.Append(_points[row][col]);
                }

                Debug.WriteLine(rowBuilder.ToString());
            }

            Debug.WriteLine("");
            Debug.WriteLine("===========");
            Debug.WriteLine("");
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}