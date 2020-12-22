using AdventOfCode2020.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.TwentyTwo
{
    public class DayTwentyTwo : IAdventProblemSet
    {
        public string Description()
        {
            return "Crab Combat";
        }

        public int SortOrder()
        {
            return 22;
        }

        public string PartA()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
            int score = PlayGameAndReturnWinningScore(filePath, false);
            return score.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
            int score = PlayGameAndReturnWinningScore(filePath, true);
            return score.ToString();
        }

        public int PlayGameAndReturnWinningScore(string filePath, bool isRecursiveCombat)
        {
            List<Player> players = FileUtility.ParseFileToMultiLineList(filePath, lineGroup => new Player(lineGroup), Player.Separator);
            Player one = players.First();
            Player two = players.Last();

            // Determine winner
            Player winner = PlayGameRecursive(one, two, isRecursiveCombat);

            return winner.CalculateScore();
        }

        public Player PlayGameRecursive(Player one, Player two, bool isRecursiveCombat)
        {
            do
            {
                // Check for forever recursive group
                if (!one.TryRecordSeenHand() && !two.TryRecordSeenHand())
                    return one;

                // Get cards for each player
                int oneCard = one.Cards.Dequeue();
                int twoCard = two.Cards.Dequeue();

                if (oneCard == twoCard)
                    throw new Exception("The cards are equal and this should not happen");

                bool oneWins = oneCard > twoCard;
                // See if we trigger recursion
                if (isRecursiveCombat && one.Cards.Count >= oneCard && two.Cards.Count >= twoCard)
                {
                    Player childOne = new Player(one, oneCard);
                    Player childTwo = new Player(two, twoCard);

                    Player childWinner = PlayGameRecursive(childOne, childTwo, true);

                    oneWins = childWinner.Number == one.Number;
                }

                // Player One wins
                if (oneWins)
                {
                    one.Cards.Enqueue(oneCard);
                    one.Cards.Enqueue(twoCard);
                }
                else
                {
                    // Player Two wins
                    two.Cards.Enqueue(twoCard);
                    two.Cards.Enqueue(oneCard);
                }
            } while (one.Cards.Any() && two.Cards.Any());

            // Determine winner
            Player winner = one.Cards.Any() ? one : two;
            return winner;
        }
    }
}