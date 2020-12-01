using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.SampleOne
{
    public class SampleDayOne : IAdventProblemSet
    {
        public string Description()
        {
            return "Sample From Last Year: The Tyranny of the Rocket Equation";
        }

        public int SortOrder()
        {
            return 99;
        }

        public string PartA()
        {
            string filePath = @"SampleOne\SampleDayOneInput.txt";
            int totalFuel = SumFuelRequired(filePath);

            return totalFuel.ToString();
        }

        public string PartB()
        {
            string filePath = @"SampleOne\SampleDayOneInput.txt";
            int totalFuel = SumAllFuelRequiredIncludingFuelForFuel(filePath);

            return totalFuel.ToString();
        }

        public int CalculateFuelRequired(int mass)
        {
            decimal dividedMass = mass / 3;
            decimal roundedMass = Math.Floor(dividedMass);
            return (int)(roundedMass - 2);
        }

        public int CalculateFuelAccountingForFuelWeight(int mass)
        {
            int fuelNeededForModule = CalculateFuelRequired(mass);
            int fuelForFuel = fuelNeededForModule;

            // We have to account for the mass of the fuel to determine the total mass of fuel needed
            // Loop over the fuel accounting for its needed additional fuel mass until the cost is zero or negative
            while (fuelForFuel > 0)
            {
                fuelForFuel = CalculateFuelRequired(fuelForFuel);
                fuelNeededForModule += fuelForFuel > 0 ? fuelForFuel : 0;
            }

            return fuelNeededForModule;
        }

        private int SumFuelRequired(string filePath)
        {
            List<int> masses = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
            int runningTotal = 0;

            foreach (int mass in masses)
            {
                int fuelNeeded = CalculateFuelRequired(mass);
                runningTotal += fuelNeeded;
            }

            return runningTotal;
        }

        private int SumAllFuelRequiredIncludingFuelForFuel(string filePath)
        {
            List<int> masses = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
            int runningTotal = 0;

            foreach (int mass in masses)
            {
                int fuelNeeded = CalculateFuelAccountingForFuelWeight(mass);
                runningTotal += fuelNeeded;
            }

            return runningTotal;
        }
    }
}