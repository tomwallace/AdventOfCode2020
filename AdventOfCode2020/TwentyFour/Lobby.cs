using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2020.TwentyFour
{
    public class Lobby
    {
        // True = white tile face up
        private Dictionary<Hex, bool> _tiles;

        public Lobby()
        {
            _tiles = new Dictionary<Hex, bool>();
        }

        public void TileFloor(string filePath)
        {
            List<List<HexDirection>> instructions = FileUtility.ParseFileToList(filePath, line => DeriveDirections(line));
            Hex origin = new Hex(0, 0, 0);

            foreach (List<HexDirection> instruction in instructions)
            {
                Hex found = FollowDirections(origin, instruction);
                PlaceTile(found);
            }
        }

        public void DayPasses(int days)
        {
            for (int i = 0; i < days; i++)
            {
                Debug.WriteLine($"Iteration: {i}");
                DayPasses();
            }
        }

        public void DayPasses()
        {
            Dictionary<Hex, bool> newTiles = new Dictionary<Hex, bool>();

            int minX = _tiles.Keys.Min(k => k.X);
            int minY = _tiles.Keys.Min(k => k.Y);
            int minZ = _tiles.Keys.Min(k => k.Z);
            int maxX = _tiles.Keys.Max(k => k.X);
            int maxY = _tiles.Keys.Max(k => k.Y);
            int maxZ = _tiles.Keys.Max(k => k.Z);

            for (int x = minX - 2; x <= maxX + 2; x++)
            {
                for (int y = minY - 2; y <= maxY + 2; y++)
                {
                    for (int z = minZ - 2; z <= maxZ + 2; z++)
                    {
                        Hex center = new Hex(x, y, z);
                        int blackTiles = CountSurroundingBlackTiles(center);

                        // Handle if center is already white
                        if ((!_tiles.ContainsKey(center) || _tiles[center]) && blackTiles == 2)
                            SetTile(center, false, newTiles);

                        // Handle if center is already black
                        if (_tiles.ContainsKey(center) && !_tiles[center] && blackTiles <= 2 && blackTiles > 0)
                            SetTile(center, false, newTiles);
                    }
                }
            }

            _tiles = newTiles;
        }

        public int CountBlackTiles()
        {
            return _tiles.Count(t => !t.Value);
        }

        private void PlaceTile(Hex tile)
        {
            if (!_tiles.ContainsKey(tile))
            {
                _tiles.Add(tile, false);
                return;
            }

            // Otherwise, toggle
            _tiles[tile] = !_tiles[tile];
        }

        private void SetTile(Hex tile, bool setToWhite, Dictionary<Hex, bool> tiles)
        {
            if (!tiles.ContainsKey(tile))
            {
                tiles.Add(tile, setToWhite);
                return;
            }

            tiles[tile] = setToWhite;
        }

        // Thanks to this website, which also helped my in AoC in 2017 - https://www.redblobgames.com/grids/hexagons/#coordinates
        // Left public to allow unit testing
        public Hex FollowDirections(Hex origin, List<HexDirection> directions)
        {
            Hex current = new Hex(origin);
            foreach (HexDirection direction in directions)
            {
                switch (direction)
                {
                    case HexDirection.NW:
                        current.Y++;
                        current.Z--;
                        break;

                    case HexDirection.NE:
                        current.X++;
                        current.Z--;
                        break;

                    case HexDirection.E:
                        current.X++;
                        current.Y--;
                        break;

                    case HexDirection.SE:
                        current.Y--;
                        current.Z++;
                        break;

                    case HexDirection.SW:
                        current.X--;
                        current.Z++;
                        break;

                    case HexDirection.W:
                        current.X--;
                        current.Y++;
                        break;

                    default:
                        throw new Exception("Unrecognized HexDirection");
                }
            }

            return current;
        }

        // Left public to allow unit testing
        public List<HexDirection> DeriveDirections(string instruction)
        {
            List<HexDirection> directions = new List<HexDirection>();
            char[] instArray = instruction.ToCharArray();
            for (int i = 0; i < instArray.Length; i++)
            {
                switch (instArray[i])
                {
                    case 'e':
                        directions.Add(HexDirection.E);
                        break;

                    case 'w':
                        directions.Add(HexDirection.W);
                        break;

                    case 'n':
                        if (instArray[i + 1] == 'w')
                            directions.Add(HexDirection.NW);
                        else
                            directions.Add(HexDirection.NE);
                        i++;
                        break;

                    case 's':
                        if (instArray[i + 1] == 'w')
                            directions.Add(HexDirection.SW);
                        else
                            directions.Add(HexDirection.SE);
                        i++;
                        break;

                    default:
                        throw new Exception($"Unrecognized hex direction: {instArray[i]}");
                }
            }

            return directions;
        }

        // Made public for unit tests
        public int CountSurroundingBlackTiles(Hex center)
        {
            Hex[] modifiers = new[]
            {
                new Hex(0, 1, -1), new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1),
                new Hex(-1, 1, 0)
            };

            int count = modifiers.Select(m => m.Add(center)).Count(m => _tiles.ContainsKey(m) && _tiles[m] == false);
            return count;
        }
    }
}