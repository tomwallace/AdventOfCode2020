using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Four
{
    public class PassportValidator
    {
        private readonly Passport passport;

        public PassportValidator(Passport passport)
        {
            this.passport = passport;
        }

        // Part A
        public bool IsValidSimple()
        {
            return passport.BirthYear.HasValue
                   && passport.IssueYear.HasValue
                   && passport.ExpirationYear.HasValue
                   && passport.Height != null
                   && passport.HairColor != null
                   && passport.EyeColor != null
                   && passport.PassportId != null;
        }

        // Part B
        public bool IsValidRules()
        {
            return BirthYearValid()
                   && IssueYearValid()
                   && ExpirationYearValid()
                   && HeightValid()
                   && HairColorValid()
                   && EyeColorValid()
                   && PassportIdValid()
                   && CountryIdValid();
        }

        // byr (Birth Year) - four digits; at least 1920 and at most 2002.
        private bool BirthYearValid()
        {
            if (!passport.BirthYear.HasValue)
                return false;

            return passport.BirthYear >= 1920 && passport.BirthYear <= 2002;
        }

        // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
        private bool IssueYearValid()
        {
            if (!passport.IssueYear.HasValue)
                return false;

            return passport.IssueYear >= 2010 && passport.IssueYear <= 2020;
        }

        // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
        private bool ExpirationYearValid()
        {
            if (!passport.ExpirationYear.HasValue)
                return false;

            return passport.ExpirationYear >= 2020 && passport.ExpirationYear <= 2030;
        }

        // hgt (Height) - a number followed by either cm or in:
        //    If cm, the number must be at least 150 and at most 193.
        //    If in, the number must be at least 59 and at most 76.
        private bool HeightValid()
        {
            if (string.IsNullOrEmpty(passport.Height))
                return false;

            string unit = passport.Height.Substring((passport.Height.Length - 2), 2);
            if (unit == "in")
            {
                string number = passport.Height.Substring(0, passport.Height.Length - 2);
                bool tryParse = int.TryParse(number, out var numberValue);

                return tryParse && numberValue >= 59 && numberValue <= 76;
            }

            if (unit == "cm")
            {
                string number = passport.Height.Substring(0, passport.Height.Length - 2);
                bool tryParse = int.TryParse(number, out var numberValue);

                return tryParse && numberValue >= 150 && numberValue <= 193;
            }

            // Otherwise fail
            return false;
        }

        // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        private bool HairColorValid()
        {
            if (string.IsNullOrEmpty(passport.HairColor))
                return false;

            return Regex.Match(passport.HairColor, @"^#(?:[0-9a-fA-F]{3}){1,2}$").Success;
        }

        // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        private bool EyeColorValid()
        {
            if (string.IsNullOrEmpty(passport.EyeColor))
                return false;

            string[] validColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validColors.Contains(passport.EyeColor);
        }

        // pid (Passport ID) - a nine-digit number, including leading zeroes.
        private bool PassportIdValid()
        {
            if (string.IsNullOrEmpty(passport.PassportId))
                return false;

            return passport.PassportId.ToCharArray().Length == 9 && int.TryParse(passport.PassportId, out var numberValue);
        }

        // cid (Country ID) - ignored, missing or not.
        private bool CountryIdValid()
        {
            return true;
        }
    }
}