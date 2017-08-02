using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Genetic_Algorithm.Fitness
{
    public static class FinessHelper
    {
        public static Dictionary<xy, int> RepeatControl = new Dictionary<xy, int>();
    }
    public struct xy
    {
        public int x;
        public int y;
        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}
