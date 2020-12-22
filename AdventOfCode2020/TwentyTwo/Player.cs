using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.TwentyTwo
{
    public class Player
    {
        public static string Separator = "|";

        private HashSet<string> _seenHands;

        public Player(string lineGroup)
        {
            List<string> lines = lineGroup.Split('|').ToList();
            Number = int.Parse(lines.First()
                .Split(new[] { "Player ", ":" }, StringSplitOptions.RemoveEmptyEntries)
                .First());
            Cards = new Queue<int>();
            for (int i = 1; i < lines.Count; i++)
            {
                Cards.Enqueue(int.Parse(lines[i]));
            }

            _seenHands = new HashSet<string>();
        }

        public Player(Player parent, int cardDepth)
        {
            Number = parent.Number;
            _seenHands = new HashSet<string>(parent.GetSeenHands());
            Cards = new Queue<int>();
            int[] cardArray = parent.Cards.ToArray();

            for (int i = 0; i < cardDepth; i++)
            {
                Cards.Enqueue(cardArray[i]);
            }
        }

        public HashSet<string> GetSeenHands()
        {
            return _seenHands;
        }

        public int Number { get; set; }

        public Queue<int> Cards { get; set; }

        public int CalculateScore()
        {
            int score = 0;
            int[] winnersCards = Cards.ToArray();
            int counter = 1;
            for (int i = winnersCards.Length - 1; i >= 0; i--)
            {
                score += winnersCards[i] * counter;
                counter++;
            }

            return score;
        }

        public bool TryRecordSeenHand()
        {
            string cards = string.Join("", Cards.Select(c => c.ToString()).ToArray());
            if (_seenHands.Contains(cards))
                return false;

            _seenHands.Add(cards);
            return true;
        }
    }
}