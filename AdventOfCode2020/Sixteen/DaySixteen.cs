using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Sixteen
{
    public class DaySixteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Ticket Translation [HARD]";
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
            string filePath = @"Sixteen\DaySixteenInput_Rules.txt";
            TicketEvaluator evaluator = new TicketEvaluator(filePath);

            filePath = @"Sixteen\DaySixteenInput_Tickets.txt";
            List<Ticket> tickets = FileUtility.ParseFileToList(filePath, line => new Ticket(line));

            Dictionary<int, int> fieldOrder = DeterminedMatchedFieldOrder(tickets, evaluator);
            List<int> myTicket = new List<int>() { 53, 101, 83, 151, 127, 131, 103, 61, 73, 71, 97, 89, 113, 67, 149, 163, 139, 59, 79, 137 };
            List<int> interestedFields = new List<int>() { 0, 1, 2, 3, 4, 5 };

            // I got int.Max when I first did this, so entry is clearly a long
            long result = 1;
            for (int i = 0; i < interestedFields.Count; i++)
            {
                int index = fieldOrder[interestedFields[i]];
                result *= myTicket[index];
            }

            return result.ToString();
        }

        public int FindTicketErrorRate(List<Ticket> tickets, TicketEvaluator evaluator)
        {
            return tickets.Sum(t => evaluator.SumInvalidFields(t));
        }

        public Dictionary<int, int> DeterminedMatchedFieldOrder(List<Ticket> tickets, TicketEvaluator evaluator)
        {
            List<Ticket> validTickets = tickets.Where(t => evaluator.IsValid(t)).ToList();

            Dictionary<int, HashSet<int>> validRuleFields = evaluator.GetValidFieldPositionsForTickets(validTickets);

            Dictionary<int, int> finalFieldLocation = new Dictionary<int, int>();

            // As a performance improvement, work with the fields with the fewest options first
            // My original brute force method using a queue and all possibilities ran to system out of memorty
            var ordered = validRuleFields.OrderBy(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);

            // Rule locations already removed
            List<int> toRemove = new List<int>();
            foreach (var t in ordered)
            {
                if (t.Value.Count == 1)
                {
                    toRemove.AddRange(t.Value);
                    finalFieldLocation.Add(t.Key, t.Value.First());
                    continue;
                }

                List<int> s = t.Value.Except(toRemove).ToList();
                toRemove.Add(s.First());
                finalFieldLocation.Add(t.Key, s.First());
            }

            return finalFieldLocation;
        }
    }
}