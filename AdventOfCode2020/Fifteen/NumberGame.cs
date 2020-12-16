using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Fifteen
{
    public class NumberGame
    {
        private readonly Dictionary<int, Stack<int>> _saids;
        private readonly List<int> _startingNumbers;

        public NumberGame(List<int> startingNumbers)
        {
            _startingNumbers = startingNumbers;
            _saids = new Dictionary<int, Stack<int>>();
            for (int i = 0; i < _startingNumbers.Count; i++)
            {
                Stack<int> stack = new Stack<int>();
                stack.Push(i + 1);
                _saids.Add(_startingNumbers[i], stack);
            }
        }

        public int Play(int targetTurn)
        {
            if (targetTurn <= _startingNumbers.Count)
                throw new Exception($"You cannot play a game shorter than the startingNumbers");

            // Note the turns are 1-based (not 0-based)
            //int currentTurn = _startingNumbers.Count;
            int lastSaid = _startingNumbers.Last();

            for (int currentTurn = _startingNumbers.Count - 1; currentTurn < targetTurn - 1; currentTurn++)
            {
                // If we have the number already, add the new value to the stack
                if (_saids.TryGetValue(lastSaid, out var result))
                {
                    lastSaid = (currentTurn + 1) - result.Peek();
                    result.Push(currentTurn + 1);
                }
                // Otherwise, it will be a zero returned, but after we add a new entry
                else
                {
                    Stack<int> stack = new Stack<int>();
                    stack.Push(currentTurn + 1);
                    _saids.Add(lastSaid, stack);

                    lastSaid = 0;
                }
            }

            return lastSaid;
        }
    }
}