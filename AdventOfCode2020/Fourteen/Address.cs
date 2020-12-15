using System;

namespace AdventOfCode2020.Fourteen
{
    public class Address
    {
        public Address(long location, string value)
        {
            Location = location;
            Value = value;
        }
        
        // Used when we want the address to be the long of value
        public Address(string value)
        {
            Value = value;
            Location = ValueToLong();
        }
        public long Location { get; set; }

        public string Value { get; set; }

        public long ValueToLong()
        {
            return Convert.ToInt64(Value, 2);
        }

        public override string ToString()
        {
            return $"Loc: {Location}, Value: {Value}";
        }
    }
}