using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2020.Twelve
{
    public class Ship
    {
        public Ship(Location waypoint)
        {
            Current = new Location() { X = 0, Y = 0 };
            Heading = 1;
            // Note the waypoint is RELATIVE to the ship, not absolute distance
            Waypoint = waypoint;
        }

        // 0 = North, 1 = East, 2 = South, 3 = West
        public int Heading { get; set; }

        public Location Current { get; set; }

        public Location Waypoint { get; set; }

        public int GetManhattanDistanceFromOrigin()
        {
            return Math.Abs(Current.X) + Math.Abs(Current.Y);
        }

        public void Move(List<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                Move(instruction);
                Debug.WriteLine($"Ship: {Current}. Waypoint: {(Waypoint != null ? Waypoint.ToString() : string.Empty)}");
                int x = 0;
            }
        }

        public void Move(string instruction)
        {
            string cmd = instruction.Substring(0, 1);
            int mod = int.Parse(instruction.Substring(1));

            switch (cmd)
            {
                case "N":
                    if (Waypoint == null)
                        Current.Y -= mod;
                    else
                        Waypoint.Y -= mod;
                    break;

                case "S":
                    if (Waypoint == null)
                        Current.Y += mod;
                    else
                        Waypoint.Y += mod;
                    break;

                case "E":
                    if (Waypoint == null)
                        Current.X += mod;
                    else
                        Waypoint.X += mod;
                    break;

                case "W":
                    if (Waypoint == null)
                        Current.X -= mod;
                    else
                        Waypoint.X -= mod;
                    break;

                case "L":
                    ChangeHeading(-1 * (mod / 90));
                    break;

                case "R":
                    ChangeHeading(1 * (mod / 90));
                    break;

                case "F":
                    MoveForward(mod);
                    break;

                default:
                    throw new Exception($"Unrecognized command: {cmd}");
            }
        }

        private void ChangeHeading(int mod)
        {
            // I had to look up how to rotate a point clockwise or counter clockwise
            if (Waypoint != null)
            {
                bool clockwise = mod > 0;
                int remainder = Math.Abs(mod % 4);
                for (int i = 0; i < remainder; i++)
                {
                    if (clockwise)
                    {
                        int x = Waypoint.X;
                        int y = Waypoint.Y;
                        Waypoint.X = y * -1;
                        Waypoint.Y = x;
                    }
                    else
                    {
                        int x = Waypoint.X;
                        int y = Waypoint.Y;
                        Waypoint.X = y;
                        Waypoint.Y = x * -1;
                    }
                }
            }
            else
            {
                int modifier = mod > 0 ? 1 : -1;
                int remainder = Math.Abs(mod % 4);
                for (int i = 0; i < remainder; i++)
                {
                    Heading += modifier;
                    if (Heading == 4)
                        Heading = 0;
                    else if (Heading == -1)
                        Heading = 3;
                }
            }
        }

        private void MoveForward(int mod)
        {
            if (Waypoint != null)
            {
                Current.X += Waypoint.X * mod;
                Current.Y += Waypoint.Y * mod;
                return;
            }

            switch (Heading)
            {
                case 0:
                    Current.Y -= mod;
                    break;

                case 2:
                    Current.Y += mod;
                    break;

                case 1:
                    Current.X += mod;
                    break;

                case 3:
                    Current.X -= mod;
                    break;

                default:
                    throw new Exception($"Unrecognized heading {Heading}");
            }
        }
    }
}