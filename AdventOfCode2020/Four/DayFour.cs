using System.Collections.Generic;
using System.Linq;

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
            PassportFactory factory = new PassportFactory(filePath);
            List<Passport> passports = factory.Create();

            int validPassports = passports.Count(p => new PassportValidator(p).IsValidSimple());

            return validPassports;
        }

        public int FindNumberOfValidPassportsByRules(string filePath)
        {
            PassportFactory factory = new PassportFactory(filePath);
            List<Passport> passports = factory.Create();

            int validPassports = passports.Count(p => new PassportValidator(p).IsValidRules());

            return validPassports;
        }
    }
}