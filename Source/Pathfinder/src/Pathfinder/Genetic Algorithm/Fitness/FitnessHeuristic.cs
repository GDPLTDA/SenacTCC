using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;

namespace Pathfinder.Fitness
{
    public class FitnessHeuristic : IFitness
    {
        IHeuristic Heuristic { get; set; }
        public double Calc(IGenome genome)
        {
            var settings = new Settings();
            Heuristic = settings.GetHeuristic();
            var _endNode = genome.Map.EndNode;
            var lastnode = genome.ListNodes.Last();
            return Heuristic.Calc(Abs(lastnode.X - _endNode.X), Abs(lastnode.Y - _endNode.Y));
        }
    }
}
