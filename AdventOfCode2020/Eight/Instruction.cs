namespace AdventOfCode2020.Eight
{
    public class Instruction
    {
        // example:  acc -99
        public Instruction(string input, int id)
        {
            string[] splits = input.Split(' ');
            Operation = splits[0].Trim();
            Argument = int.Parse(splits[1].Trim());
            Id = id;
        }

        public int Id { get; set; }

        public string Operation { get; set; }

        public int Argument { get; set; }
    }
}