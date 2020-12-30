using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Sixteen
{
    public class Ticket
    {
        private List<int> _fields;

        public Ticket(string input)
        {
            _fields = input.Split(',').Select(i => int.Parse(i)).ToList();
        }

        public List<int> GetFields()
        {
            return _fields;
        }
    }
}