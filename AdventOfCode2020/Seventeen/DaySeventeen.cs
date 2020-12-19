using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AdventOfCode2020.Utility;

namespace AdventOfCode2020.Seventeen
{
    public class DaySeventeen : IAdventProblemSet
    {
        public string Description()
        {
            return "Conway Cubes";
        }

        public int SortOrder()
        {
            return 17;
        }

        public string PartA()
        {
            string filePath = @"Seventeen\DaySeventeenInput.txt";
            Dimension dimension = new Dimension(filePath);
            
            dimension.Run(6);
            int activeCubes = dimension.CountActiveCubes();

            return activeCubes.ToString();
        }

        public string PartB()
        {
            string filePath = @"Seventeen\DaySeventeenInput.txt";
            return "";
        }
    }



    public class Dimension
    {
        private Dictionary<Point, bool> _cubes;

        public Dimension(string filePath)
        {
            _cubes = new Dictionary<Point, bool>();
            List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
            for (int y = 0; y < lines.Count; y++)
            {
                char[] chars = lines[y].ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                {
                    _cubes.Add(new Point(x,y,0), chars[x] == '#');
                }
            }
        }

        public void Run()
        {
            Dictionary<Point, bool> newCubes = new Dictionary<Point, bool>();
            foreach (Point cube in _cubes.Keys)
            {
                if (ShouldActivate(cube) && !newCubes.ContainsKey(cube))
                    newCubes.Add(new Point(cube.X, cube.Y, cube.Z), true);

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
        public Dictionary<Point,bool> GetNeighbors(Point point)
        {
            Dictionary<Point, bool> neighbors = new Dictionary<Point, bool>();
            
            for (int x = point.X - 1; x <= point.X + 1; x++)
            {
                for (int y = point.Y - 1; y <= point.Y + 1; y++)
                {
                    for (int z = point.Z - 1; z <= point.Z + 1; z++)
                    {
                        Point current = new Point(x, y, z);
                        if (point.Equals(current))
                            continue;


                        var found = _cubes.ContainsKey(current);
                        if (found)
                        {
                            neighbors.Add(current, _cubes[current]);
                        }
                        else
                        {
                            neighbors.Add(current, false);
                        }
                    }
                }
            }

            return neighbors;
        }

        //public List<Point> CalculateNewCubes(List<Point> newCubes)

        // Mutates newCubes dictionary too
        /*
        private int CountActiveNeighbors(Point point, Dictionary<Point, char> newCubes)
        {
            int neighbors = 0;
            
            for (int x = point.X - 1; x <= point.X + 1; x++)
            {
                for (int y = point.Y - 1; y <= point.Y + 1; y++)
                {
                    for (int z = point.Z - 1; z <= point.Z + 1; z++)
                    {
                        Point current = new Point(x,y,z);
                        if (point.Equals(current))
                            continue;

                        if (!_cubes.ContainsKey(current))
                        {
                            // Add it to new collection, but it is automatically a zero
                            if (!newCubes.ContainsKey(current))
                                newCubes.Add(current, '.');
                        }
                        else
                        {
                            if (_cubes[current] == '#')
                                neighbors++;
                        }
                    }
                }
            }

            return neighbors;
        }
        */

        public void Visualize()
        {
            int minX = _cubes.Min(c => c.Key.X);
            int maxX = _cubes.Max(c => c.Key.X);
            int minY = _cubes.Min(c => c.Key.Y);
            int maxY = _cubes.Max(c => c.Key.Y);
            int minZ = _cubes.Min(c => c.Key.Z);
            int maxZ = _cubes.Max(c => c.Key.Z);

            for (int z = minZ; z <= maxZ; z++)
            {
                Debug.WriteLine($"Z = {z}");

                for (int y = minY; y <= maxY; y++)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int x = minX; x <= maxX; x++)
                    {
                        Point point = new Point(x,y,z);
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

    public class Cube
    {
        public bool Active { get; set; }

        public Point Location { get; set; }
    }

    public class Point
    {
        public Point(int x, int y, int z, bool active = false)
        {
            X = x;
            Y = y;
            Z = z;
            //Active = active;
        }
        
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        //public bool Active { get; set; }

        public override string ToString()
        {
            return $"{X}|{Y}|{Z}";
        }

        public override bool Equals(object obj)
        {
            PointComparer comparer = new PointComparer();
            return comparer.Equals(this, (Point)obj);
        }

        public override int GetHashCode()
        {
            PointComparer comparer = new PointComparer();
            return comparer.GetHashCode(this);
        }
    }

    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point c1, Point c2)
        {
            if (c2 == null && c1 == null)
                return true;
            else if (c1 == null | c2 == null)
                return false;
            else if (c1.ToString() == c2.ToString())
                return true;
            else
                return false;
        }

        public int GetHashCode(Point c)
        {
            return $"{c}".GetHashCode();
        }
    }
}