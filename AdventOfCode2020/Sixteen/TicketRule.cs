using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Sixteen
{
    public class TicketRule
    {
        // class: 1-3 or 5-7
        public TicketRule(string input)
        {
            string[] splits = input.Split(':');
            Name = splits[0];
            Rules = new List<Rule>();
            foreach (string range in splits[1].Split(new string[] { " or " }, StringSplitOptions.None))
            {
                Rules.Add(new Rule(range));
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Rule> Rules { get; set; }

        public bool IsValidAnyRule(int field)
        {
            bool anyValid = Rules.Any(r => r.IsValid(field));
            return anyValid;
        }
    }
}