using System.Linq;

namespace AdventOfCode2020.Two
{
    public class PasswordData
    {
        public PasswordData(string input)
        {
            // example: 1-3 a: abcde
            string[] split = input.Split(' ');
            Password = split[2];
            RequiredLetter = split[1].ToCharArray()[0];

            string[] limits = split[0].Split('-');
            LowerLimit = int.Parse(limits[0]);
            UpperLimit = int.Parse(limits[1]);
        }

        public string Password { get; set; }

        public char RequiredLetter { get; set; }

        public int LowerLimit { get; set; }

        public int UpperLimit { get; set; }

        // Is the password valid according to its password rule (Sled Rental Rules)
        public bool IsValidSledRentalRules()
        {
            int appearances = Password.ToCharArray().Count(c => c == RequiredLetter);
            return appearances >= LowerLimit && appearances <= UpperLimit;
        }

        // Is the password valid according to its password rule (Toboggan Rental Rules)
        public bool IsValidTobogganRentalRules()
        {
            char[] passwordCharArray = Password.ToCharArray();
            char firstChar = passwordCharArray[LowerLimit - 1];
            char secondChar = passwordCharArray[UpperLimit - 1];

            // To be correct, the ONLY ONE of the characters at the lowerLimit and upperLimit (base 1) can match
            bool atLeastOneCharacterMatches = firstChar == RequiredLetter || secondChar == RequiredLetter;
            bool bothCharactersMatch = firstChar == RequiredLetter && secondChar == RequiredLetter;

            return atLeastOneCharacterMatches && !bothCharactersMatch;
        }
    }
}