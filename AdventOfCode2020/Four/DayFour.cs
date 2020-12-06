using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Utility;

namespace AdventOfCode2020.Four
{
    public class DayFour : IAdventProblemSet
    {
        public string Description()
        {
            return "Passport Processing";
        }

        public int SortOrder()
        {
            return 4;
        }

        public string PartA()
        {
            string filePath = @"Four\DayFourInput.txt";
            int validPassports = FindNumberOfValidPassports(filePath);

            return validPassports.ToString();
        }

        public string PartB()
        {
            string filePath = @"Four\DayFourInput.txt";
            int validPassports = FindNumberOfValidPassportsByRules(filePath);

            return validPassports.ToString();
        }

        public int FindNumberOfValidPassports(string filePath)
        {
            List<Passport> passports = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new Passport(lineGroup), " ");

            int validPassports = passports.Count(p => new PassportValidator(p).IsValidSimple());

            return validPassports;
        }

        public int FindNumberOfValidPassportsByRules(string filePath)
        {
            List<Passport> passports = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new Passport(lineGroup), " ");

            int validPassports = passports.Count(p => new PassportValidator(p).IsValidRules());

            return validPassports;
        }
    }
}