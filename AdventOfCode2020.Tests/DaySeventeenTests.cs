using System.Linq;
using AdventOfCode2020.Seventeen;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DaySeventeenTests
    {
        [Theory]
        [InlineData(0, 0, 0, 1)]
        [InlineData(1, 0, 0, 1)]
        [InlineData(1, 2, 0, 3)]
        [InlineData(2, 1, 0, 3)]
        public void Dimension_GetNeighbors(int x, int y, int z, int expected)
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath);
            var point = new Point(x,y,z);
            var result = sut.GetNeighbors(point);

            Assert.Equal(26, result.Count);
            Assert.Equal(expected, result.Count(p => p.Value));
        }

        [Theory]
        [InlineData(1, 0, 0, true, false)]
        [InlineData(1, 2, 0, true, true)]
        [InlineData(2, 1, 0, true, true)]
        public void Dimension_ShouldActivate(int x, int y, int z, bool startingState, bool expected)
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath);
            var point = new Point(x, y, z, startingState);
            var result = sut.ShouldActivate(point);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Dimension_Run()
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath);
            sut.Run(6);
            var result = sut.CountActiveCubes();

            Assert.Equal(112, result);
        }
        
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartA();

            Assert.Equal("273", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}