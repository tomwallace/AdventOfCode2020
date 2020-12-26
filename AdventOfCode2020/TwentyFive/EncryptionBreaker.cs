using System;
using System.Collections.Generic;

namespace AdventOfCode2020.TwentyFive
{
    public class EncryptionBreaker
    {
        public readonly long _doorPublic;
        public readonly long _cardPublic;
        public Dictionary<string, bool> _seen;

        public EncryptionBreaker(long doorPublic, long cardPublic)
        {
            _doorPublic = doorPublic;
            _cardPublic = cardPublic;
            _seen = new Dictionary<string, bool>();
        }

        public long CrackEncryptionKey()
        {
            LoopResult loopResult = FindLoopResults();
            long cardEncryptionKey = TransformNumber(loopResult.CardLoopSize, _doorPublic);
            long doorEncryptionKey = TransformNumber(loopResult.DoorLoopSize, _cardPublic);

            if (cardEncryptionKey != doorEncryptionKey)
                throw new Exception("If done right, the calculated encryption keys should be the same.");

            return cardEncryptionKey;
        }

        // loopSize is unknown.  door loopSize != card loopSize
        // Left public for unit tests
        public LoopResult FindLoopResults()
        {
            LoopResult result = new LoopResult();
            result.DoorLoopSize = FindLoopSize(_doorPublic);
            result.CardLoopSize = FindLoopSize(_cardPublic);

            return result;
        }

        // Initially tried to use TransformNumber here in a loop, but it was taking forever because
        // of the increasing size of the for loop.
        // So had to reframe the equation to apply to itself a number of times.  Then went much faster.  Thanks to reddit for the suggestion
        private long FindLoopSize(long publicKey)
        {
            long counter = 1;
            long currentTransform = 1;

            while (true)
            {
                currentTransform = (currentTransform * 7) % 20201227;
                if (currentTransform == publicKey)
                {
                    return counter;
                }

                counter++;
            }
        }

        // Left public for unit tests
        public long TransformNumber(long loopSize, long subjectNumber)
        {
            long value = 1;
            for (int i = 0; i < loopSize; i++)
            {
                value *= subjectNumber;
                value = value % 20201227;
            }

            return value;
        }
    }
}