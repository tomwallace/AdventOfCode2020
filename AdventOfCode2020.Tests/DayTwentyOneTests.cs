using AdventOfCode2020.TwentyOne;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class DayTwentyOneTests
    {
        [Fact]
        public void CountIngredientsWithoutAllergens()
        {
            string filePath = @"TwentyOne\DayTwentyOneTestInputA.txt";
            var sut = new DayTwentyOne();
            var result = sut.CountIngredientsWithoutAllergens(filePath);

            Assert.Equal(5, result);
        }

        [Fact]
        public void GetDangerousIngredientList()
        {
            string filePath = @"TwentyOne\DayTwentyOneTestInputA.txt";
            var sut = new DayTwentyOne();
            var result = sut.GetDangerousIngredientList(filePath);

            Assert.Equal("mxmxvkd,sqjhc,fvjkl", result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyOne();
            var result = sut.PartA();

            Assert.Equal("2324", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyOne();
            var result = sut.PartB();

            Assert.Equal("bxjvzk,hqgqj,sp,spl,hsksz,qzzzf,fmpgn,tpnnkc", result);
        }
    }
}