using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.One
{
    public class DayOne : IAdventProblemSet
    {
        public string Description()
        {
            return "Report Repair";
        }

        public int SortOrder()
        {
            return 1;
        }

        public string PartA()
        {
            string filePath = @"One\DayOneInput.txt";
            List<int> entries = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
            int sum = FindReportSumTwo(entries);

            return sum.ToString();
        }

        public string PartB()
        {
            string filePath = @"One\DayOneInput.txt";
            List<int> entries = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
            int sum = FindReportSumThree(entries);

            return sum.ToString();
        }

        public int FindReportSumTwo(List<int> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = 0; j < entries.Count; j++)
                {
                    if (i == j)
                        break;

                    if (entries[i] + entries[j] == 2020)
                        return entries[i] * entries[j];
                }
            }

            throw new Exception("Should never reach here");
        }

        public int FindReportSumThree(List<int> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = 0; j < entries.Count; j++)
                {
                    for (int k = 0; k < entries.Count; k++)
                    {
                        if (i == j && j == k)
                            break;

                        if (entries[i] + entries[j] + entries[k] == 2020)
                            return entries[i] * entries[j] * entries[k];
                    }
                }
            }

            throw new Exception("Should never reach here");
        }
    }
}