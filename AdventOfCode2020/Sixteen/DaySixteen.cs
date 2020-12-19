using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Sixteen
{
    public class DaySixteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Ticket Translation";
        }

        public int SortOrder()
        {
            return 16;
        }

        public string PartA()
        {
            string filePath = @"Sixteen\DaySixteenInput_Rules.txt";
            TicketEvaluator evaluator = new TicketEvaluator(filePath);

            filePath = @"Sixteen\DaySixteenInput_Tickets.txt";
            List<Ticket> tickets = FileUtility.ParseFileToList(filePath, line => new Ticket(line));

            int errorRate = FindTicketErrorRate(tickets, evaluator);
            return errorRate.ToString();
        }

        public string PartB()
        {
            string filePath = @"Sixteen\DaySixteenInput.txt";
            return "";
        }

        public int FindTicketErrorRate(List<Ticket> tickets, TicketEvaluator evaluator)
        {
            return tickets.Sum(t => evaluator.SumInvalidFields(t));
        }

        public int ProductOfMatchedFieldValues(List<Ticket> tickets, TicketEvaluator evaluator)
        {
            List<Ticket> validTickets = tickets.Where(t => evaluator.IsValid(t)).ToList();

            Dictionary<int, HashSet<int>> validRuleFields =  evaluator.GetValidFieldPositionsForTickets(validTickets);

            Queue<RuleStep> queue = new Queue<RuleStep>();
            foreach (int ruleId in validRuleFields.Keys)
            {
                RuleStep step = new RuleStep()
                {
                    RuleOrder = new List<int>() {ruleId},
                    RemainingRules = Enumerable.Range(0, validRuleFields.Keys.Count - 1).ToList()
                };
                step.RemainingRules.Remove(ruleId);

                queue.Enqueue(step);
            }

            while (queue.Any())
            {
                RuleStep current = queue.Dequeue();
                // TODO: Figure out how to cover all the allowable options
            }

            throw new Exception("Should never reach here");
        }
    }

    public class RuleStep
    {
        public List<int> RuleOrder { get; set; }
        public List<int> RemainingRules { get; set; }
    }

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

    public class TicketEvaluator
    {
        private List<TicketRule> _rules;

        public TicketEvaluator(string filePath)
        {
            _rules = FileUtility.ParseFileToList(filePath, line => new TicketRule(line));
            for (int i = 0; i < _rules.Count; i++)
            {
                _rules[0].Id = i;
            }
        }

        public int SumInvalidFields(Ticket ticket)
        {
            return ticket.GetFields().Sum(f => _rules.Any(r => r.IsValidAnyRule(f)) ? 0 : f);
        }

        public bool IsValid(Ticket ticket)
        {
            return ticket.GetFields().Any(f => _rules.Any(r => r.IsValidAnyRule(f)));
        }

        public Dictionary<int, HashSet<int>> GetValidFieldPositionsForTickets(List<Ticket> tickets)
        {
            Dictionary<int, HashSet<int>> dictionary = new Dictionary<int, HashSet<int>>();
            foreach (TicketRule rule in _rules)
            {
                foreach (Ticket ticket in tickets)
                {
                    foreach (int field in ticket.GetFields())
                    {
                        if (rule.IsValidAnyRule(field))
                        {
                            if (dictionary.ContainsKey(rule.Id))
                            {
                                if (dictionary[rule.Id] == null)
                                    dictionary[rule.Id] = new HashSet<int>();

                                dictionary[rule.Id].Add(field);
                            }
                            else
                            {
                                dictionary.Add(rule.Id, new HashSet<int>() { field });
                            }
                        }
                    }
                }
            }

            return dictionary;
        }
    }

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

    public class Rule
    {
        public Rule(string input)
        {
            string[] splits = input.Split('-');
            Lower = int.Parse(splits[0]);
            Upper = int.Parse(splits[1]);
        }

        public int Lower { get; set; }

        public int Upper { get; set; }

        public bool IsValid(int field)
        {
            bool isValid = field >= Lower && field <= Upper;
            return isValid;
        }
    }
}