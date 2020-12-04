using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Four
{
    public class Passport
    {
        public Passport(string input)
        {
            // Parse the information and set fields
            // Example: ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr: 1937 iyr: 2017 cid: 147 hgt: 183cm

            string[] split = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> inputValues =
                split.Select(item => item.Split(':')).ToDictionary(s => s[0], s => s[1]);

            if (inputValues.ContainsKey("byr"))
                BirthYear = int.Parse(inputValues["byr"]);

            if (inputValues.ContainsKey("iyr"))
                IssueYear = int.Parse(inputValues["iyr"]);

            if (inputValues.ContainsKey("eyr"))
                ExpirationYear = int.Parse(inputValues["eyr"]);

            Height = inputValues.ContainsKey("hgt") ? inputValues["hgt"] : null;
            HairColor = inputValues.ContainsKey("hcl") ? inputValues["hcl"] : null;
            EyeColor = inputValues.ContainsKey("ecl") ? inputValues["ecl"] : null;
            PassportId = inputValues.ContainsKey("pid") ? inputValues["pid"] : null;
            CountryId = inputValues.ContainsKey("cid") ? inputValues["cid"] : null;
        }

        // byr (Birth Year)
        public int? BirthYear { get; set; }

        // iyr (Issue Year)
        public int? IssueYear { get; set; }

        // eyr (Expiration Year)
        public int? ExpirationYear { get; set; }

        // hgt (Height)
        public string Height { get; set; }

        // hcl (Hair Color)
        public string HairColor { get; set; }

        // ecl (Eye Color)
        public string EyeColor { get; set; }

        // pid (Passport ID)
        public string PassportId { get; set; }

        // cid (Country ID)
        public string CountryId { get; set; }
    }
}