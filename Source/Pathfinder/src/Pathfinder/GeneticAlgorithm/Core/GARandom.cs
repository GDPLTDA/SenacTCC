
using Pathfinder.Abstraction;
using System;
namespace Pathfinder
{
    public class GARandom : IRandom
    {
        private readonly Random me;
        public  GARandom()
        {
            me = new Random();
        }
        public int Next() => me.Next();
        public int Next(int maxValue) => me.Next(maxValue);
        public int Next(int minValue, int maxValue) => me.Next(minValue,maxValue);
        public void NextBytes(byte[] buffer) => me.NextBytes(buffer);
        public double NextDouble() => me.NextDouble();
    }
}