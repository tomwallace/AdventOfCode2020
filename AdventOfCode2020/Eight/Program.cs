using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Eight
{
    public class Program
    {
        private int _accumulator;
        private int _currentLocation;

        private readonly HashSet<int> _visitedInstructions;

        public Program(string filePath)
        {
            _accumulator = 0;
            _currentLocation = 0;
            _visitedInstructions = new HashSet<int>();

            ReplacedInstructions = new HashSet<int>();
            Instructions = new List<Instruction>();

            int counter = 0;

            List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
            foreach (string line in lines)
            {
                Instructions.Add(new Instruction(line, counter));
                counter++;
            }
        }

        public List<Instruction> Instructions { get; set; }

        public HashSet<int> ReplacedInstructions { get; set; }

        // Get the value of the register needed as output
        public int GetAccumulator()
        {
            return _accumulator;
        }

        // Run until we would repeat an instruction, thus entering into the continuous loop
        // Exit before that instruction is applied
        public void RunUntilInstructionRepeated()
        {
            do
            {
                ApplyInstruction();
            } while (EnteringEndlessLoop());
        }

        // Run and try to replace one jmp and nop, making sure to only replace one that has not
        // been tried before
        public int? RunWithReplace()
        {
            int indexOfReplaced = -1;
            do
            {
                Instruction currentInstruction = Instructions[_currentLocation];
                // If we have not replaced this before and we only replace one
                if (indexOfReplaced == -1 && !ReplacedInstructions.Contains(currentInstruction.Id)
                                          && (currentInstruction.Operation == "jmp" ||
                                              currentInstruction.Operation == "nop"))
                {
                    indexOfReplaced = _currentLocation;
                    // Toggle
                    currentInstruction.Operation = currentInstruction.Operation == "jmp" ? "nop" : "jmp";
                }

                ApplyInstruction();

                // We have reached past the end, so exit
                if (_currentLocation >= Instructions.Count)
                    return null;
            } while (EnteringEndlessLoop());

            // In this case we would be entering an endless loop, exit and provide the GUID replaced
            return indexOfReplaced;
        }

        private bool EnteringEndlessLoop()
        {
            return !_visitedInstructions.Contains(Instructions[_currentLocation].Id);
        }

        private void ApplyInstruction()
        {
            Instruction currentInstruction = Instructions[_currentLocation];
            _visitedInstructions.Add(currentInstruction.Id);

            switch (currentInstruction.Operation)
            {
                case "acc":
                    _accumulator += currentInstruction.Argument;
                    _currentLocation += 1;
                    break;

                case "jmp":
                    _currentLocation += currentInstruction.Argument;
                    break;

                case "nop":
                    _currentLocation += 1;
                    break;

                default:
                    throw new Exception($"Unrecognized instruction operation: {currentInstruction.Operation}");
            }
        }
    }
}