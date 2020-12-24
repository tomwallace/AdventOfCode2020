using AdventOfCode2020.TwentyFour;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyFourTests
    {
        [Fact]
        public void Lobby_DeriveDirections()
        {
            var instruction = "nwwswee";
            var sut = new Lobby();
            var result = sut.DeriveDirections(instruction);

            var expected = new List<HexDirection>()
            {
                HexDirection.NW,
                HexDirection.W,
                HexDirection.SW,
                HexDirection.E,
                HexDirection.E
            };
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("nwwswee", 0, 0, 0)]
        [InlineData("esenee", 3, -3, 0)]
        public void Lobby_FollowDirections(string instruction, int x, int y, int z)
        {
            var origin = new Hex(0, 0, 0);
            var sut = new Lobby();
            var directions = sut.DeriveDirections(instruction);
            var result = sut.FollowDirections(origin, directions);

            Assert.Equal(x, result.X);
            Assert.Equal(y, result.Y);
            Assert.Equal(z, result.Z);
        }

        [Fact]
        public void Lobby_TileFloor()
        {
            string filePath = @"TwentyFour\DayTwentyFourTestInputA.txt";
            var sut = new Lobby();
            sut.TileFloor(filePath);
            var result = sut.CountBlackTiles();

            Assert.Equal(10, result);
        }

        [Theory]
        [InlineData(0, 0, 0, 1)]
        [InlineData(-10, -10, -10, 0)]
        [InlineData(-1, 1, 0, 5)]
        [InlineData(0, 1, -1, 2)]
        [InlineData(1, -1, 0, 2)]
        public void Lobby_CountSurroundingBlackTiles(int x, int y, int z, int expected)
        {
            string filePath = @"TwentyFour\DayTwentyFourTestInputA.txt";
            var sut = new Lobby();
            sut.TileFloor(filePath);

            var center = new Hex(x, y, z);
            var result = sut.CountSurroundingBlackTiles(center);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Lobby_DayPasses()
        {
            string filePath = @"TwentyFour\DayTwentyFourTestInputA.txt";
            var sut = new Lobby();
            sut.TileFloor(filePath);
            var result = sut.CountBlackTiles();
            Assert.Equal(10, result);

            sut.DayPasses();
            Assert.Equal(15, sut.CountBlackTiles());

            sut.DayPasses();
            Assert.Equal(12, sut.CountBlackTiles());
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyFour();
            var result = sut.PartA();

            Assert.Equal("351", result);
        }

        // Takes 8 min to run, so commenting out
        /*
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyFour();
            var result = sut.PartB();

            Assert.Equal("3869", result);
        }
        */
    }
}