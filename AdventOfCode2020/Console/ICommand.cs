namespace AdventOfCode2020.Console
{
    public interface ICommand
    {
        void Execute();

        bool HadErrorInCreation();
    }
}