using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Twenty
{
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

        public Grid(Grid[,] grids)
        {
            Id = -1;

            _points = new List<char[]>();
            for (int gridRow = 0; gridRow < grids.GetLength(0); gridRow++)
            {
                for (int indRows = 0; indRows < grids[0, 0].Points.Count; indRows++)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int gridCol = 0; gridCol < grids.GetLength(1); gridCol++)
                    {
                        builder.Append(grids[gridRow, gridCol].Points[indRows]).ToString();
                    }
                    _points.Add(builder.ToString().ToCharArray());
                }
            }

            Output();
        }

        public long Id { get; }

        public List<char[]> Points => _points;

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

        public void TrimEdges()
        {
            List<char[]> newPoints = new List<char[]>();

            for (int row = 1; row < _points.Count - 1; row++)
            {
                StringBuilder builder = new StringBuilder();
                for (int col = 1; col < _points[0].Length - 1; col++)
                {
                    builder.Append(_points[row][col]);
                }

                newPoints.Add(builder.ToString().ToCharArray());
            }

            _points = newPoints;
        }

        public long CountPixels()
        {
            return _points.Sum(l => l.Count(c => c == '#'));
        }

        public int ContainsNumberOfSeaMonsters()
        {
            int numberFound = 0;
            for (int row = 0; row < _points.Count - 2; row++)
            {
                for (int col = 18; col < _points[0].Length - 1; col++)
                {
                    bool found = _points[row][col] == '#'
                                 && _points[row + 1][col - 18] == '#'
                                 && _points[row + 1][col - 13] == '#'
                                 && _points[row + 1][col - 12] == '#'
                                 && _points[row + 1][col - 7] == '#'
                                 && _points[row + 1][col - 6] == '#'
                                 && _points[row + 1][col - 1] == '#'
                                 && _points[row + 1][col] == '#'
                                 && _points[row + 1][col + 1] == '#'
                                 && _points[row + 2][col - 17] == '#'
                                 && _points[row + 2][col - 14] == '#'
                                 && _points[row + 2][col - 11] == '#'
                                 && _points[row + 2][col - 8] == '#'
                                 && _points[row + 2][col - 5] == '#'
                                 && _points[row + 2][col - 2] == '#';

                    if (found)
                        numberFound++;
                }
            }

            return numberFound;
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