using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Two
{
    public class DayTwo : IAdventProblemSet
    {
        public string Description()
        {
            return "Password Philosophy";
        }

        public int SortOrder()
        {
            return 2;
        }

        public string PartA()
        {
            string filePath = @"Two\DayTwoInput.txt";
            List<PasswordData> passwordData = FileUtility.ParseFileToList(filePath, line => new PasswordData(line.Trim()));
            int validPasswords = passwordData.Count(pd => pd.IsValidSledRentalRules());

            return validPasswords.ToString();
        }

        public string PartB()
        {
            string filePath = @"Two\DayTwoInput.txt";
            List<PasswordData> passwordData = FileUtility.ParseFileToList(filePath, line => new PasswordData(line.Trim()));
            int validPasswords = passwordData.Count(pd => pd.IsValidTobogganRentalRules());

            return validPasswords.ToString();
        }
    }
}