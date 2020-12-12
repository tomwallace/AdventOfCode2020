using AdventOfCode2020.Eleven;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayElevenTests
    {
        [Fact]
        public void SeatingArea()
        {
            string filePath = @"Eleven\DayElevenTestInputA.txt";
            var sut = new SeatingArea(filePath);
            sut.Run(false);
            var result = sut.CountOccupiedSeats();

            Assert.Equal(37, result);
        }

        [Fact]
        public void SeatingArea_VisibleSeats()
        {
            string filePath = @"Eleven\DayElevenTestInputA.txt";
            var sut = new SeatingArea(filePath);
            sut.Run(true);
            var result = sut.CountOccupiedSeats();

            Assert.Equal(26, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEleven();
            var result = sut.PartA();

            Assert.Equal("2424", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEleven();
            var result = sut.PartB();

            Assert.Equal("2208", result);
        }
    }
}