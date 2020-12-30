namespace AdventOfCode2020.Sixteen
{
    public class Rule
    {
        public Rule(string input)
        {
            string[] splits = input.Split('-');
            Lower = int.Parse(splits[0]);
            Upper = int.Parse(splits[1]);
        }

        public int Lower { get; set; }

        public int Upper { get; set; }

        public bool IsValid(int field)
        {
            bool isValid = field >= Lower && field <= Upper;
            return isValid;
        }
    }
}