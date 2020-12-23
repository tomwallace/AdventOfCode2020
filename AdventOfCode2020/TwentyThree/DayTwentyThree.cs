using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.TwentyThree
{
    public class DayTwentyThree : IAdventProblemSet
    {
        public static string Input = "123487596";

        public string Description()
        {
            return "Crab Cups";
        }

        public int SortOrder()
        {
            return 23;
        }

        public string PartA()
        {
            CupGame game = new CupGame(Input);
            game.Play(100);
            string cupOrder = game.ListCupOrder();
            return cupOrder;
        }

        public string PartB()
        {
            return "";
        }

    }

    public class CupGame
    {
        private readonly Dictionary<long, Cup> _cups;
        private readonly long _start;

        public CupGame(string input)
        {
            _cups = new Dictionary<long, Cup>();

            long[] ids = input.ToCharArray().Select(i => long.Parse(i.ToString())).ToArray();
            foreach (long id in ids)
            {
                _cups.Add(id, new Cup(id));
            }

            _start = ids.First();

            // Link them
            for (long i = 0; i < ids.Length; i++)
            {
                long leftId = ids[i - 1 >= 0 ? i - 1 : ids.Length - 1];
                long rightId = ids[i + 1 < ids.Length ? i + 1 : 0];
                Cup left = _cups[leftId];
                Cup current = _cups[ids[i]];
                Cup right = _cups[rightId];

                current.Left = left;
                left.Right = current;
                current.Right = right;
                right.Left = current;
            }
        }

        public CupGame(string input, long numberToPadTo)
        {
            _cups = new Dictionary<long, Cup>();

            List<long> idList = input.ToCharArray().Select(i => long.Parse(i.ToString())).ToList();

            _start = idList.First();

            // Pad up to the number
            for (long i = idList.Max() + 1; i <= numberToPadTo; i++)
            {
                idList.Add(i);
            }

            var ids = idList.ToArray();
            foreach (long id in ids)
            {
                _cups.Add(id, new Cup(id));
            }

            // Link them
            for (long i = 0; i < ids.Length; i++)
            {
                long leftId = ids[i - 1 >= 0 ? i - 1 : ids.Length - 1];
                long rightId = ids[i + 1 < ids.Length ? i + 1 : 0];
                Cup left = _cups[leftId];
                Cup current = _cups[ids[i]];
                Cup right = _cups[rightId];

                current.Left = left;
                left.Right = current;
                current.Right = right;
                right.Left = current;
            }
        }

        public Cup Move(Cup current)
        {
            // Remove the next 3 cups as a chain
            Cup removeLeft = current.Right;
            Cup removeMiddle = removeLeft.Right;
            Cup removeRight = removeLeft.Right.Right;
            Cup farRight = removeRight.Right;

            removeLeft.Left = null;
            removeRight.Right = null;
            current.Right = farRight;
            farRight.Left = current;

            // Destination cup is current ID - 1
            long destinationId = current.Id - 1;
            while (destinationId == removeLeft.Id || destinationId == removeMiddle.Id || destinationId == removeRight.Id)
            {
                destinationId--;
            }

            if (!_cups.ContainsKey(destinationId))
            {
                destinationId = _cups.Keys.Max();
                while (destinationId == removeLeft.Id || destinationId == removeMiddle.Id || destinationId == removeRight.Id)
                {
                    destinationId--;
                }
            }

            //Debug.WriteLine($"DestinationId: {destinationId}");
            Cup destination = _cups[destinationId];

            // Add cups clockwise to destination
            Cup insertRight = destination.Right;
            destination.Right = removeLeft;
            removeLeft.Left = destination;
            removeRight.Right = insertRight;
            insertRight.Left = removeRight;

            // Get the clockwise cup from current
            return current.Right;
        }

        public void Play(long moves)
        {
            Cup current = _cups[_start];
            Debug.WriteLine($"Move number: {0}");
            //Debug.WriteLine(ToString());
            //Debug.WriteLine("");

            for (long i = 1; i <= moves; i++)
            {
                Debug.WriteLine($"Move number: {i}");
                //Debug.WriteLine($"Current: {current.Id}");
                current = Move(current);
                //Debug.WriteLine(ToString());
                //Debug.WriteLine("");
            }
        }

        public string ListCupOrder()
        {
            StringBuilder builder = new StringBuilder();
            Cup current = _cups[1].Right;
            do
            {
                builder.Append(current.Id);
                current = current.Right;
            } while (current.Id != 1);

            return builder.ToString();
        }

        public Cup GetCup(long id)
        {
            return _cups[id];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            Cup current = _cups[_start];
            do
            {
                builder.Append(current.Id);
                current = current.Right;
            } while (current.Id != _start);

            return builder.ToString();
        }
    }

    public class Cup
    {
        public Cup(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public Cup Left { get; set; }

        public Cup Right { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}