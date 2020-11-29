using AdventOfCode2020.Console;

namespace AdventOfCode2020
{
    internal class Program
    {
        private static readonly string LINE_PREFIX = "AoC2020:> ";

        private static void Main(string[] args)
        {
            while (true)
            {
                System.Console.Write(LINE_PREFIX);
                string commandLine = System.Console.ReadLine();

                CommandBuilder builder = new CommandBuilder();
                ICommand command = builder.Build(commandLine);

                if (!command.HadErrorInCreation())
                {
                    command.Execute();
                }
            }
        }
    }
}