using AdventOfCode2020.Eight;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayEightTests
    {
        [Fact]
        public void ProgramRun()
        {
            string filePath = @"Eight\DayEightTestInputA.txt";
            var sut = new Program(filePath);
            sut.RunUntilInstructionRepeated();
            var result = sut.GetAccumulator();

            Assert.Equal(5, result);
        }

        [Fact]
        public void TryProgramUntilReachBottomAndReturnAccumulator()
        {
            string filePath = @"Eight\DayEightTestInputA.txt";
            var sut = new DayEight();
            var result = sut.TryProgramUntilReachBottomAndReturnAccumulator(filePath);

            Assert.Equal(8, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEight();
            var result = sut.PartA();

            Assert.Equal("1941", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEight();
            var result = sut.PartB();

            Assert.Equal("2096", result);
        }
    }
}