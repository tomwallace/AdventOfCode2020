using AdventOfCode2020.TwentyFive;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyFiveTests
    {
        [Fact]
        public void EncryptionBreaker_TransformNumber()
        {
            long doorPublic = 17807724;
            long cardPublic = 5764801;
            var sut = new EncryptionBreaker(doorPublic, cardPublic);

            var cardResult = sut.TransformNumber(8, 7);
            Assert.Equal(cardPublic, cardResult);

            var doorResult = sut.TransformNumber(11, 7);
            Assert.Equal(doorPublic, doorResult);
        }

        [Fact]
        public void EncryptionBreaker_FindLoopResults()
        {
            long doorPublic = 17807724;
            long cardPublic = 5764801;
            var sut = new EncryptionBreaker(doorPublic, cardPublic);
            var result = sut.FindLoopResults();

            Assert.Equal(8, result.CardLoopSize);
            Assert.Equal(11, result.DoorLoopSize);
        }

        [Fact]
        public void EncryptionBreaker_CrackEncryptionKey()
        {
            long doorPublic = 17807724;
            long cardPublic = 5764801;
            var sut = new EncryptionBreaker(doorPublic, cardPublic);
            var result = sut.CrackEncryptionKey();

            Assert.Equal(14897079, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyFive();
            var result = sut.PartA();

            Assert.Equal("18862163", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyFive();
            var result = sut.PartB();

            Assert.Equal("Day25PartB", result);
        }
    }
}