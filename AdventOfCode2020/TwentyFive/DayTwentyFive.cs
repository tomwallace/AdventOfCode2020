namespace AdventOfCode2020.TwentyFive
{
    public class DayTwentyFive : IAdventProblemSet
    {
        public static long DoorPublic = 11404017;
        public static long CardPublic = 13768789;

        public string Description()
        {
            return "Combo Breaker";
        }

        public int SortOrder()
        {
            return 25;
        }

        public string PartA()
        {
            EncryptionBreaker breaker = new EncryptionBreaker(DoorPublic, CardPublic);
            long encryptionKey = breaker.CrackEncryptionKey();
            return encryptionKey.ToString();
        }

        public string PartB()
        {
            // Part B is that you have to finish all other puzzles and then you complete it
            return "Day25PartB";
        }
    }
}