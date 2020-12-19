using AdventOfCode2020.Seventeen;
using System.Linq;
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
            var sut = new Dimension(filePath, false);
            var point = new Point(x, y, z, 0);
            var result = sut.GetNeighbors(point);

            Assert.Equal(26, result.Count);
            Assert.Equal(expected, result.Count(p => p.Value));
        }

        [Theory]
        [InlineData(0, 0, 0, 1)]
        [InlineData(1, 0, 0, 1)]
        [InlineData(1, 2, 0, 3)]
        [InlineData(2, 1, 0, 3)]
        public void Dimension_GetNeighbors_WithW(int x, int y, int z, int expected)
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath, true);
            var point = new Point(x, y, z, 0);
            var result = sut.GetNeighbors(point);

            Assert.Equal(80, result.Count);
            Assert.Equal(expected, result.Count(p => p.Value));
        }

        [Theory]
        [InlineData(1, 0, 0, false)]
        [InlineData(1, 2, 0, true)]
        [InlineData(2, 1, 0, true)]
        public void Dimension_ShouldActivate(int x, int y, int z, bool expected)
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath, false);
            var point = new Point(x, y, z, 0);
            var result = sut.ShouldActivate(point);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 0, 0, false)]
        [InlineData(1, 2, 0, true)]
        [InlineData(2, 1, 0, true)]
        public void Dimension_ShouldActivate_WithW(int x, int y, int z, bool expected)
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath, true);
            var point = new Point(x, y, z, 0);
            var result = sut.ShouldActivate(point);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Dimension_Run()
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath, false);
            sut.Run(6);
            var result = sut.CountActiveCubes();

            Assert.Equal(112, result);
        }

        // Takes 45 sec, so commenting out
        /*
        [Fact]
        public void Dimension_Run_WithW()
        {
            string filePath = @"Seventeen\DaySeventeenTestInputA.txt";
            var sut = new Dimension(filePath, true);
            sut.Run(6);
            var result = sut.CountActiveCubes();

            Assert.Equal(848, result);
        }
        */

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartA();

            Assert.Equal("273", result);
        }

        // Takes minutes, so commenting out
        /*
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartB();

            Assert.Equal("1504", result);
        }
        */
    }
}