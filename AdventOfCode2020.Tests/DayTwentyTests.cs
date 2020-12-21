using AdventOfCode2020.Twenty;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyTests
    {
        [Fact]
        public void Grid_CheckRotations()
        {
            string input = "Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###";
            var sut = new Grid(input);

            var top = "..##.#..#.";
            Assert.Equal(top, sut.TopEdge());

            sut.Rotate();
            var expected = ".#..#####.";
            Assert.Equal(expected, sut.TopEdge());

            sut.Rotate();
            expected = "###..###..";
            Assert.Equal(expected, sut.TopEdge());
        }

        [Fact]
        public void Grid_FlipVertical()
        {
            string input = "Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###";
            var sut = new Grid(input);

            sut.FlipVertical();
            var expected = "..###..###";
            Assert.Equal(expected, sut.TopEdge());
        }

        [Fact]
        public void Grid_TrimEdges()
        {
            string input = "Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###";
            var sut = new Grid(input);

            var top = "..##.#..#.";
            Assert.Equal(top, sut.TopEdge());

            sut.TrimEdges();
            var expected = "#..#....";
            Assert.Equal(expected, sut.TopEdge());
        }

        [Fact]
        public void FindCornerProduct()
        {
            string filePath = @"Twenty\DayTwentyTestInputA.txt";
            var sut = new DayTwenty();
            var result = sut.FindCornerProduct(filePath);

            Assert.Equal(20899048083289, result);
        }

        [Fact]
        public void FindActivePixelsNotPartOfSeaMonster()
        {
            string filePath = @"Twenty\DayTwentyTestInputA.txt";
            var sut = new DayTwenty();
            var result = sut.FindActivePixelsNotPartOfSeaMonster(filePath);

            Assert.Equal(273, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartA();

            Assert.Equal("66020135789767", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartB();

            Assert.Equal("1537", result);
        }
    }
}