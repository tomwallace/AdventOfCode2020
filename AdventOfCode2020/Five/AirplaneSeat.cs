using System;

namespace AdventOfCode2020.Five
{
    public class AirplaneSeat
    {
        private readonly int _highestRow = 127;
        private readonly int _highestSeat = 7;

        public AirplaneSeat(string seatLocator)
        {
            // Calculate row
            char[] rowInst = seatLocator.Substring(0, 7).ToCharArray();
            Row = CalculateValueBinary(rowInst, 'F', 'B', _highestRow);

            // Calculate seat
            char[] seatInst = seatLocator.Substring(7, 3).ToCharArray();
            Seat = CalculateValueBinary(seatInst, 'L', 'R', _highestSeat);
        }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int GetSeatId()
        {
            int seatId = (Row * 8) + Seat;
            return seatId;
        }

        public override string ToString()
        {
            return $"row: {Row}, seat: {Seat}, id: {GetSeatId()}";
        }

        private int CalculateValueBinary(char[] instructions, char lower, char upper, int startMax)
        {
            int min = 0;
            int max = startMax;
            int actual = -1;

            foreach (char inst in instructions)
            {
                int midPoint = (max - min) / 2;  // should be truncated
                if (inst == lower)
                    max = min + midPoint;
                else if (inst == upper)
                    min += midPoint + 1;
                else
                    throw new Exception($"Illegal instruction character {inst}.  Allowed are: {lower}, {upper}");

                // If they are equal, we have our value
                if (min == max)
                    actual = min;
            }

            if (actual == -1)
                throw new Exception($"Did not actually calculate value.  min={min}, maxRow={max}");

            return actual;
        }
    }
}