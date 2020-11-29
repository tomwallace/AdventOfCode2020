﻿namespace AdventOfCode2020.Console.Commands
{
    public class ClearCommand : ICommand
    {
        public void Execute()
        {
            System.Console.Clear();
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}