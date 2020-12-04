using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Four
{
    public class PassportFactory
    {
        private readonly string filePath;

        public PassportFactory(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Passport> Create()
        {
            List<Passport> passports = new List<Passport>();

            string line;
            string passportInput = "";

            StreamReader file = new StreamReader(filePath);

            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                // Passport can span multiple lines, so no easy way to split up the file with easy rules
                // We need to combine lines until there is a blank one
                if (line == "")
                {
                    passports.Add(new Passport(passportInput));
                    passportInput = "";
                }
                else
                {
                    passportInput = $"{passportInput}{line} ";
                }
            }
            file.Close();

            // Get the final line if valid
            if (passportInput != "")
                passports.Add(new Passport(passportInput));

            return passports;
        }
    }
}