namespace AdventOfCode2020.TwentyThree
{
    public class DayTwentyThree : IAdventProblemSet
    {
        public static string Input = "123487596";

        public string Description()
        {
            return "Crab Cups";
        }

        public int SortOrder()
        {
            return 23;
        }

        public string PartA()
        {
            CupGame game = new CupGame(Input);
            game.Play(100);
            string cupOrder = game.ListCupOrder();
            return cupOrder;
        }

        public string PartB()
        {
            CupGame game = new CupGame(Input, 1000000);
            game.Play(10000000);
            var one = game.GetCup(1);
            long right = one.Next.Value;
            long doubleRight = one.Next.Next.Value;
            return (right * doubleRight).ToString();
        }
    }
}