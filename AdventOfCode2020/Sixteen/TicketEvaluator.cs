using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Sixteen
{
    public class TicketEvaluator
    {
        private List<TicketRule> _rules;

        public TicketEvaluator(string filePath)
        {
            _rules = FileUtility.ParseFileToList(filePath, line => new TicketRule(line));
            for (int i = 0; i < _rules.Count; i++)
            {
                _rules[i].Id = i;
            }
        }

        public int SumInvalidFields(Ticket ticket)
        {
            return ticket.GetFields().Sum(f => _rules.Any(r => r.IsValidAnyRule(f)) ? 0 : f);
        }

        public bool IsValid(Ticket ticket)
        {
            return ticket.GetFields().All(f => _rules.Any(r => r.IsValidAnyRule(f)));
        }

        public Dictionary<int, HashSet<int>> GetValidFieldPositionsForTickets(List<Ticket> tickets)
        {
            Dictionary<int, HashSet<int>> dictionary = new Dictionary<int, HashSet<int>>();

            foreach (TicketRule rule in _rules)
            {
                for (int i = 0; i < tickets.First().GetFields().Count; i++)
                {
                    if (tickets.All(t => rule.IsValidAnyRule(t.GetFields()[i])))
                    {
                        if (dictionary.ContainsKey(rule.Id))
                        {
                            if (dictionary[rule.Id] == null)
                                dictionary[rule.Id] = new HashSet<int>();

                            dictionary[rule.Id].Add(i);
                        }
                        else
                        {
                            dictionary.Add(rule.Id, new HashSet<int>() { i });
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}