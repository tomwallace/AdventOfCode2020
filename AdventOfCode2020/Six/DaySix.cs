using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Six
{
    public class DaySix : IAdventProblemSet
    {
        public string Description()
        {
            return "Custom Customs";
        }

        public int SortOrder()
        {
            return 6;
        }

        public string PartA()
        {
            string filePath = @"Six\DaySixInput.txt";
            int sumOfAnswers = SumCustomFormUniqueQuestionAnswers(filePath);

            return sumOfAnswers.ToString();
        }

        public string PartB()
        {
            string filePath = @"Six\DaySixInput.txt";
            int sumOfAnswers = SumCustomFormAllYesAnswers(filePath);

            return sumOfAnswers.ToString();
        }

        public int SumCustomFormUniqueQuestionAnswers(string filePath)
        {
            List<GroupResponse> groupResponses = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new GroupResponse(lineGroup), GroupResponse.Separator.ToString());
            return groupResponses.Sum(gr => gr.DistinctResponses.Count);
        }

        public int SumCustomFormAllYesAnswers(string filePath)
        {
            // Collect list of group responses, where individual respondants are separated by |s
            List<GroupResponse> groupResponses = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new GroupResponse(lineGroup), GroupResponse.Separator.ToString());
            return groupResponses.Sum(gr => gr.NumberOfCommonResponses());
        }
    }
}