using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Six
{
    public class GroupResponse
    {
        public static char Separator => '|';

        public GroupResponse(string lineGroup)
        {
            DistinctResponses = new HashSet<char>(lineGroup.ToCharArray().Where(c => c != Separator));
            IndividualResponses = lineGroup.Split('|').ToList();
        }

        // Collected distinct responses for the group in one line, ensuring no Separator's are present
        public HashSet<char> DistinctResponses { get; set; }

        // List of individual response strings
        public List<string> IndividualResponses { get; set; }

        // Number of responses that everyone in the group had
        public int NumberOfCommonResponses()
        {
            return DistinctResponses.Count(c => IndividualResponses.All(ir => ir.Contains(c)));
        }
    }
}