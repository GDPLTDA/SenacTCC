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
        public IHeuristic Heuristic;
        public FitnessHeuristic() {
            Heuristic = Container.Resolve<IHeuristic>();
        }
        public double Calc(IGenome genome)
        {
            var _endNode = genome.Map.EndNode;
            var lastnode = genome.ListNodes.Last();
            return Heuristic.Calc(Abs(lastnode.X - _endNode.X), Abs(lastnode.Y - _endNode.Y));
        }
    }
}