using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Seventeen
{
    public class Dimension
    {
        private Dictionary<Point, bool> _cubes;
        private bool _useW;

        public Dimension(string filePath, bool useW)
        {
            _cubes = new Dictionary<Point, bool>();
            List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
            for (int y = 0; y < lines.Count; y++)
            {
                char[] chars = lines[y].ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                {
                    _cubes.Add(new Point(x, y, 0, 0), chars[x] == '#');
                }
            }

            _useW = useW;
        }

        public void Run()
        {
            Dictionary<Point, bool> newCubes = new Dictionary<Point, bool>();
            foreach (Point cube in _cubes.Keys)
            {
                if (ShouldActivate(cube) && !newCubes.ContainsKey(cube))
                    newCubes.Add(new Point(cube.X, cube.Y, cube.Z, cube.W), true);

                foreach (Point neighbor in GetNeighbors(cube).Keys)
                {
                    if (ShouldActivate(neighbor) && !newCubes.ContainsKey(neighbor))
                    {
                        newCubes.Add(neighbor, true);
                    }
                }
            }

            _cubes = newCubes;
        }

        public void Run(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Visualize();
                Run();
            }
        }

        public int CountActiveCubes()
        {
            return _cubes.Count(c => c.Value);
        }

        // Public to aid in unit tests
        public bool ShouldActivate(Point point)
        {
            Dictionary<Point, bool> neighbors = GetNeighbors(point);
            int activeNeighbors = neighbors.Count(n => n.Value);
            if (_cubes.ContainsKey(point) && _cubes[point])
            {
                return (activeNeighbors == 2 || activeNeighbors == 3);
            }

            return activeNeighbors == 3;
        }

        // Public to aid in unit tests
        public Dictionary<Point, bool> GetNeighbors(Point point)
        {
            Dictionary<Point, bool> neighbors = new Dictionary<Point, bool>();

            for (int x = point.X - 1; x <= point.X + 1; x++)
            {
                for (int y = point.Y - 1; y <= point.Y + 1; y++)
                {
                    for (int z = point.Z - 1; z <= point.Z + 1; z++)
                    {
                        if (_useW)
                        {
                            for (int w = point.W - 1; w <= point.W + 1; w++)
                            {
                                Point current = new Point(x, y, z, w);
                                if (point.Equals(current))
                                    continue;

                                var found = _cubes.ContainsKey(current);
                                if (found)
                                    neighbors.Add(current, _cubes[current]);
                                else
                                    neighbors.Add(current, false);
                            }
                        }
                        else
                        {
                            Point current = new Point(x, y, z, 0);
                            if (point.Equals(current))
                                continue;

                            var found = _cubes.ContainsKey(current);
                            if (found)
                                neighbors.Add(current, _cubes[current]);
                            else
                                neighbors.Add(current, false);
                        }
                    }
                }
            }

            return neighbors;
        }

        public void Visualize()
        {
            int minX = _cubes.Min(c => c.Key.X);
            int maxX = _cubes.Max(c => c.Key.X);
            int minY = _cubes.Min(c => c.Key.Y);
            int maxY = _cubes.Max(c => c.Key.Y);
            int minZ = _cubes.Min(c => c.Key.Z);
            int maxZ = _cubes.Max(c => c.Key.Z);

            if (_useW)
            {
                int minW = _cubes.Min(c => c.Key.W);
                int maxW = _cubes.Max(c => c.Key.W);

                for (int w = minW; w <= maxW; w++)
                {
                    Debug.WriteLine($"W = {w}");

                    for (int z = minZ; z <= maxZ; z++)
                    {
                        Debug.WriteLine($"Z = {z}");

                        for (int y = minY; y <= maxY; y++)
                        {
                            StringBuilder builder = new StringBuilder();
                            for (int x = minX; x <= maxX; x++)
                            {
                                Point point = new Point(x, y, z, w);
                                if (_cubes.ContainsKey(point))
                                    builder.Append(_cubes[point] ? '#' : '.');
                                else
                                    builder.Append('.');
                            }
                            Debug.WriteLine(builder.ToString());
                        }
                    }
                    Debug.WriteLine("");
                    Debug.WriteLine("============================================");
                    Debug.WriteLine("");
                }
            }
            else
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    Debug.WriteLine($"Z = {z}");

                    for (int y = minY; y <= maxY; y++)
                    {
                        StringBuilder builder = new StringBuilder();
                        for (int x = minX; x <= maxX; x++)
                        {
                            Point point = new Point(x, y, z, 0);
                            if (_cubes.ContainsKey(point))
                                builder.Append(_cubes[point] ? '#' : '.');
                            else
                                builder.Append('.');
                        }
                        Debug.WriteLine(builder.ToString());
                    }

                    Debug.WriteLine("");
                    Debug.WriteLine("============================================");
                    Debug.WriteLine("");
                }
            }
        }
    }
}