using AdventOfCode2020.Twelve;
using AdventOfCode2020.Utility;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwelveTests
    {
        [Fact]
        public void ShipMove()
        {
            string filePath = @"Twelve\DayTwelveTestInputA.txt";
            List<string> instructions = FileUtility.ParseFileToList(filePath, line => line);

            var sut = new Ship(null);
            sut.Move(instructions);
            var result = sut.GetManhattanDistanceFromOrigin();

            Assert.Equal(25, result);
        }

        [Fact]
        public void ShipMove_With_Waypoint()
        {
            string filePath = @"Twelve\DayTwelveTestInputA.txt";
            List<string> instructions = FileUtility.ParseFileToList(filePath, line => line);

            var waypoint = new Location() { X = 10, Y = -1 };
            var sut = new Ship(waypoint);
            sut.Move(instructions);
            var result = sut.GetManhattanDistanceFromOrigin();

            Assert.Equal(286, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwelve();
            var result = sut.PartA();

            Assert.Equal("364", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwelve();
            var result = sut.PartB();

            Assert.Equal("39518", result);
        }
    }
}