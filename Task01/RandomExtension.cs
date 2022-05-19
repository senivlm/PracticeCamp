using System;

namespace hw_entry
{
    public static class RandomDoubleExtension
    {
        public static double NextDouble(this Random random, in double minValue, in double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}