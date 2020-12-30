using AdventOfCode2020.Sixteen;
using AdventOfCode2020.Utility;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DaySixteenTests
    {
        [Fact]
        public void FindTicketErrorRate()
        {
            string filePath = @"Sixteen\DaySixteenTestInputA_Rules.txt";
            var evaluator = new TicketEvaluator(filePath);

            filePath = @"Sixteen\DaySixteenTestInputA_Tickets.txt";
            List<Ticket> tickets = FileUtility.ParseFileToList(filePath, line => new Ticket(line));

            var sut = new DaySixteen();
            var result = sut.FindTicketErrorRate(tickets, evaluator);

            Assert.Equal(71, result);
        }

        [Fact]
        public void TicketEvaluator_GetValidFieldPositionsForTickets()
        {
            string filePath = @"Sixteen\DaySixteenTestInputB_Rules.txt";
            var evaluator = new TicketEvaluator(filePath);

            filePath = @"Sixteen\DaySixteenTestInputB_Tickets.txt";
            var tickets = FileUtility.ParseFileToList(filePath, line => new Ticket(line));

            var result = evaluator.GetValidFieldPositionsForTickets(tickets);

            Assert.Contains(1, result[0]);
            Assert.Contains(2, result[0]);
            Assert.Contains(0, result[1]);
            Assert.Contains(1, result[1]);
            Assert.Contains(2, result[1]);
            Assert.Contains(2, result[2]);
        }

        [Fact]
        public void DeterminedMatchedFieldOrder()
        {
            string filePath = @"Sixteen\DaySixteenTestInputB_Rules.txt";
            var evaluator = new TicketEvaluator(filePath);

            filePath = @"Sixteen\DaySixteenTestInputB_Tickets.txt";
            var tickets = FileUtility.ParseFileToList(filePath, line => new Ticket(line));

            var sut = new DaySixteen();
            var result = sut.DeterminedMatchedFieldOrder(tickets, evaluator);

            Assert.Equal(1, result[0]);
            Assert.Equal(0, result[1]);
            Assert.Equal(2, result[2]);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartA();

            Assert.Equal("25961", result);
        }

        // 2114402351 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartB();

            Assert.Equal("603409823791", result);
        }
    }
}