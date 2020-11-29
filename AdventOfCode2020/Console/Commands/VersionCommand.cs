using System.Configuration;

namespace AdventOfCode2020.Console.Commands
{
    public class VersionCommand : ICommand
    {
        public void Execute()
        {
            string versionNumber = ConfigurationManager.AppSettings["version"];
            System.Console.WriteLine($"AdventOfCode2020 version: {versionNumber}"); 
            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}