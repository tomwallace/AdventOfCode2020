using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Eighteen
{
    public class DayEighteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Operation Order";
        }

        public int SortOrder()
        {
            return 18;
        }

        public string PartA()
        {
            string filePath = @"Eighteen\DayEighteenInput.txt";
            List<string> equations = FileUtility.ParseFileToList(filePath, line => line);
            long total = equations.Sum(e => CalculateEquationLeftToRight(e));
            return total.ToString();
        }

        public string PartB()
        {
            string filePath = @"Eighteen\DayEighteenInput.txt";
            List<string> equations = FileUtility.ParseFileToList(filePath, line => line);
            long total = equations.Sum(e => CalculateEquationAdditionFirst(e));
            return total.ToString();
        }

        public long CalculateEquationLeftToRight(string equation)
        {
            // Work with nested parentheses
            MatchCollection innerMatches = Regex.Matches(equation, @"\([\d+* ]+?\)");
            if (innerMatches.Count > 0)
            {
                // Recursive case
                foreach (Match match in innerMatches)
                {
                    string inner = match.Value.Replace("(", "").Replace(")", "");
                    equation = equation.Replace(match.Value, CalculateEquationLeftToRight(inner).ToString());
                }

                return CalculateEquationLeftToRight(equation);
            }

            long total = 0;
            // Now work from left to right
            // Default is addition
            string operation = "+";

            // Split by spaces
            foreach (string split in Regex.Split(equation, @" "))
            {
                switch (split)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        operation = split;
                        break;

                    // it must be a number
                    default:
                        switch (operation)
                        {
                            case "+":
                                total += long.Parse(split);
                                break;

                            case "-":
                                total -= long.Parse(split);
                                break;

                            case "*":
                                total *= long.Parse(split);
                                break;

                            case "/":
                                total /= long.Parse(split);
                                break;

                            default:
                                throw new Exception($"Unrecognized operation: {operation}");
                        }

                        break;
                }
            }

            return total;
        }

        public long CalculateEquationAdditionFirst(string equation)
        {
            // Work with nested parentheses
            MatchCollection innerMatches = Regex.Matches(equation, @"\([\d+* ]+?\)");
            if (innerMatches.Count > 0)
            {
                // Recursive case
                foreach (Match match in innerMatches)
                {
                    string inner = match.Value.Replace("(", "").Replace(")", "");
                    equation = equation.Replace(match.Value, CalculateEquationAdditionFirst(inner).ToString());
                }

                return CalculateEquationAdditionFirst(equation);
            }

            // Work with addition first
            while (equation.Contains("+"))
            {
                // Find digits plus other digits
                Match addition = Regex.Match(equation, @"(\d+) \+ (\d+)");
                long numOne = long.Parse(addition.Groups[1].Value);
                long numTwo = long.Parse(addition.Groups[2].Value);
                long result = numOne + numTwo;
                // Now replace the whole thing by removing the capture groups from the pattern (for the first time it occurs)
                equation = new Regex(@"\d+ \+ \d+").Replace(equation, result.ToString(), 1);
            }

            // Now we can move left to right, but there is only multiplication remaining
            long total = 1;

            // Split by spaces
            foreach (string split in Regex.Split(equation, @" "))
            {
                if (long.TryParse(split, out long result))
                {
                    total *= result;
                }
            }

            return total;
        }
    }
}