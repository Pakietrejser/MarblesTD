using System;
using System.Globalization;

namespace MarblesTD.Core.Common.Extensions
{
    public static class Seed
    {
        static readonly Random Rand = new Random(DateTime.Now.ToString("o", CultureInfo.CurrentCulture).GetHashCode());

        public static int Random(int min, int max) => Rand.Next(min, max);

        public static bool CoinFlip() => Random(2) == 1;

        public static int Random(int max) => Rand.Next(0, max);
        
        public static float Random(float min, float max) => (float) Rand.NextDouble() * (max - min) + min;
        
        public static float Random(float max) => (float) Rand.NextDouble() * max;
    }
}