using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Fourteen
{
    public class DayFourteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Docking Data [HARD]";
        }

        public int SortOrder()
        {
            return 14;
        }

        public string PartA()
        {
            string filePath = @"Fourteen\DayFourteenInput.txt";
            long sum = RunProgramAndReturnSum(filePath, false);

            return sum.ToString();
        }

        public string PartB()
        {
            string filePath = @"Fourteen\DayFourteenInput.txt";
            long sum = RunProgramAndReturnSum(filePath, true);

            return sum.ToString();
        }

        public long RunProgramAndReturnSum(string filePath, bool isVersionTwoDecoder)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            string mask = String.Empty;

            List<string> inputLines = FileUtility.ParseFileToList(filePath, line => line);

            foreach (string input in inputLines)
            {
                if (input.Contains("mask"))
                {
                    string[] splits = input.Split(new string[] { " = " }, StringSplitOptions.None);
                    mask = splits[1];
                }
                else
                {
                    string[] splitsBracket = input.Split(new string[] { "[", "]" }, StringSplitOptions.None);
                    long location = long.Parse(splitsBracket[1]);
                    string[] splits = input.Split(new string[] { " = " }, StringSplitOptions.None);
                    long value = long.Parse(splits[1]);

                    // Convert to a string of base 2 with padded zeros
                    long valueToUse = isVersionTwoDecoder ? location : value;
                    string convertedValue = Convert.ToString(valueToUse, 2).PadLeft(mask.Length, '0');
                    string maskApplied = isVersionTwoDecoder
                        ? ApplyMaskVersionTwo(mask, convertedValue)
                        : ApplyMask(mask, convertedValue);

                    List<Address> addresses = isVersionTwoDecoder
                        ? GenerateCombinations(maskApplied)
                        : new List<Address> { new Address(location, maskApplied) };

                    foreach (Address address in addresses)
                    {
                        long addressValueToUse = isVersionTwoDecoder ? value : address.ValueToLong();

                        if (memory.ContainsKey(address.Location))
                            memory[address.Location] = addressValueToUse;
                        else
                            memory.Add(address.Location, addressValueToUse);
                    }
                }
            }

            // Sum of values converted from base 2
            return memory.Sum(m => m.Value);
        }

        private string ApplyMask(string mask, string value)
        {
            if (mask.Length != value.Length)
                throw new Exception("The mask and value to apply to it are not the same length");

            char[] maskArray = mask.ToCharArray();
            char[] valueArray = value.ToCharArray();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < mask.Length; i++)
            {
                switch (maskArray[i])
                {
                    case 'X':
                        builder.Append(valueArray[i]);
                        break;

                    case '1':
                        builder.Append("1");
                        break;

                    case '0':
                        builder.Append("0");
                        break;

                    default:
                        throw new Exception($"Unrecognized mask character: {maskArray[i]}");
                }
            }

            return builder.ToString();
        }

        private string ApplyMaskVersionTwo(string mask, string value)
        {
            if (mask.Length != value.Length)
                throw new Exception("Version 2 - The mask and value to apply to it are not the same length");

            char[] maskArray = mask.ToCharArray();
            char[] valueArray = value.ToCharArray();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < mask.Length; i++)
            {
                switch (maskArray[i])
                {
                    case 'X':
                        builder.Append("X");
                        break;

                    case '1':
                        builder.Append("1");
                        break;

                    case '0':
                        builder.Append(valueArray[i]);
                        break;

                    default:
                        throw new Exception($"Unrecognized mask character: {maskArray[i]}");
                }
            }

            return builder.ToString();
        }

        private List<Address> GenerateCombinations(string value)
        {
            if (!value.Any(c => c.Equals('X')))
            {
                return new List<Address> { new Address(value) };
            }
            else
            {
                var zeroMask = ReplaceFirstMatch(value, "X", "0");
                var oneMask = ReplaceFirstMatch(value, "X", "1");

                // Call recursively to get all combinations - I needed some help from Reddit to get this to work
                return GenerateCombinations(zeroMask).Concat(GenerateCombinations(oneMask)).ToList();
            }
        }

        private string ReplaceFirstMatch(string value, string mask, string replacement)
        {
            var firstMaskIndex = value.IndexOf(mask);
            if (firstMaskIndex < 0)
            {
                return value;
            }
            return value.Remove(firstMaskIndex, 1).Insert(firstMaskIndex, replacement);
        }
    }
}