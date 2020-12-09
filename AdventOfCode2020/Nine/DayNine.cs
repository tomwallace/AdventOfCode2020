using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Nine
{
    public class DayNine : IAdventProblemSet
    {
        public string Description()
        {
            return "Encoding Error";
        }

        public int SortOrder()
        {
            return 9;
        }

        public string PartA()
        {
            string filePath = @"Nine\DayNineInput.txt";
            long number = FindNumberWithWeakness(filePath, 25);

            return number.ToString();
        }

        public string PartB()
        {
            string filePath = @"Nine\DayNineInput.txt";
            long sum = FindSumOfSmallestAndLargestEqualingTarget(filePath, 14144619);

            return sum.ToString();
        }

        public long FindNumberWithWeakness(string filePath, int preamble)
        {
            long[] xmasData = FileUtility.ParseFileToList(filePath, line => long.Parse(line)).ToArray();
            int pointer = preamble;

            do
            {
                long targetValue = xmasData[pointer];
                bool foundMatch = false;

                // Loop over previous preamble worth of values to see if any combination adds up to target value
                for (int i = pointer - preamble; i < pointer; i++)
                {
                    for (int j = pointer - preamble; j < pointer; j++)
                    {
                        // Weakness is defined as the sum of two numbers that equal the target
                        if (i != j && xmasData[i] + xmasData[j] == targetValue)
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                }

                if (!foundMatch)
                    return targetValue;

                // Did not find it, so advance
                pointer++;
            } while (pointer < xmasData.Length);

            // Should not get here
            throw new Exception($"Moved past end of xmasData with pointer: {pointer}");
        }

        public long FindSumOfSmallestAndLargestEqualingTarget(string filePath, long target)
        {
            long[] xmasData = FileUtility.ParseFileToList(filePath, line => long.Parse(line)).ToArray();
            int smallestPointer = 0;

            do
            {
                // Reset variables
                int largestPointer = smallestPointer + 1;
                List<long> runningList = new List<long>() { xmasData[smallestPointer] };

                do
                {
                    runningList.Add(xmasData[largestPointer]);

                    // Match condition, so exit
                    if (runningList.Sum() == target)
                        return runningList.Min() + runningList.Max();

                    // If not, then ramp largestPointer
                    largestPointer++;
                } while (runningList.Sum() < target);

                // Step forward one
                smallestPointer++;
            } while (smallestPointer < xmasData.Length);

            // Should not get here
            throw new Exception($"Moved past end of xmasData with pointer: {smallestPointer}");
        }
    }
}