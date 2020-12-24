using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.TwentyThree
{
    public class CupGame
    {
        private readonly long _start;
        private LinkedList<long> _cups;
        private readonly Dictionary<long, LinkedListNode<long>> _locations;
        private readonly Dictionary<long, bool> _isPresent;
        private long _max;

        public CupGame(string input)
        {
            long[] ids = input.ToCharArray().Select(i => long.Parse(i.ToString())).ToArray();

            _start = ids.First();
            _max = ids.Max();

            _cups = new LinkedList<long>();
            _locations = new Dictionary<long, LinkedListNode<long>>();
            _isPresent = new Dictionary<long, bool>();
            foreach (var id in ids)
            {
                _locations.Add(id, _cups.AddLast(id));
                _isPresent.Add(id, true);
            }
        }

        public CupGame(string input, long numberToPadTo)
        {
            List<long> idList = input.ToCharArray().Select(i => long.Parse(i.ToString())).ToList();

            _start = idList.First();
            _max = numberToPadTo;

            // Pad up to the number
            for (long i = idList.Max() + 1; i <= numberToPadTo; i++)
            {
                idList.Add(i);
            }

            var ids = idList.ToArray();

            _cups = new LinkedList<long>();
            _locations = new Dictionary<long, LinkedListNode<long>>();
            _isPresent = new Dictionary<long, bool>();
            foreach (var id in ids)
            {
                _locations.Add(id, _cups.AddLast(id));
                _isPresent.Add(id, true);
            }
        }

        public LinkedListNode<long> Move(LinkedListNode<long> current)
        {
            Stack<long> removed = new Stack<long>();

            for (int i = 0; i < 3; i++)
            {
                long num = (current.Next ?? _cups.First).Value;
                removed.Push(num);
                _isPresent[num] = false;
                _cups.Remove(current.Next ?? _cups.First);
            }

            // Destination cup is current ID - 1
            long destinationId = current.Value - 1;
            while (removed.Any(r => r == destinationId))
            {
                destinationId--;
            }

            if (!_isPresent.TryGetValue(destinationId, out bool res))
            {
                destinationId = _max;
                while (removed.Any(r => r == destinationId))
                {
                    destinationId--;
                }
            }

            var destination = _locations[destinationId];

            // Add cups clockwise to destination
            for (int i = 0; i < 3; i++)
            {
                long cup = removed.Pop();
                _locations[cup] = _cups.AddAfter(destination, cup);
                _isPresent[cup] = true;
            }

            // Get the clockwise cup from current
            return current.Next ?? _cups.First;
        }

        public void Play(long moves)
        {
            var current = _locations[_start];
            Debug.WriteLine($"Move number: {0}");

            for (long i = 1; i <= moves; i++)
            {
                if (i % 100000 == 0)
                    Debug.WriteLine($"Move number: {i}");

                current = Move(current);
            }
        }

        public string ListCupOrder()
        {
            StringBuilder builder = new StringBuilder();
            var current = _locations[1].Next;
            do
            {
                builder.Append(current.Value);
                current = current.Next ?? _cups.First;
            } while (current.Value != 1);

            return builder.ToString();
        }

        public LinkedListNode<long> GetCup(long id)
        {
            return _locations[id];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            var current = _locations[_start];
            do
            {
                builder.Append(current.Value);
                current = current.Next ?? _cups.First;
            } while (current.Value != _start);

            return builder.ToString();
        }
    }
}