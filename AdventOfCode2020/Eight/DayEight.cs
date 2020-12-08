using System.Collections.Generic;

namespace AdventOfCode2020.Eight
{
    public class DayEight : IAdventProblemSet
    {
        public string Description()
        {
            return "Handheld Halting";
        }

        public int SortOrder()
        {
            return 8;
        }

        public string PartA()
        {
            string filePath = @"Eight\DayEightInput.txt";

            Program program = new Program(filePath);
            program.RunUntilInstructionRepeated();

            return program.GetAccumulator().ToString();
        }

        public string PartB()
        {
            string filePath = @"Eight\DayEightInput.txt";
            int accumulatorValue = TryProgramUntilReachBottomAndReturnAccumulator(filePath);

            return accumulatorValue.ToString();
        }

        public int TryProgramUntilReachBottomAndReturnAccumulator(string filePath)
        {
            HashSet<int> replacedInstructions = new HashSet<int>();

            // Loop through and try different substitutions
            do
            {
                Program program = new Program(filePath);
                program.ReplacedInstructions = replacedInstructions;

                int? instructionId = program.RunWithReplace();

                // We have achieved the solution, so return
                if (instructionId == null)
                    return program.GetAccumulator();

                // Otherwise, add the id to list of Ids we have tried and try again
                replacedInstructions.Add(instructionId.Value);
            } while (true);
        }
    }
}