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
        public void PartA_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartA();

            Assert.Equal("25961", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}