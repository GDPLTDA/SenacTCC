using Pathfinder.Abstraction;
using System.Linq;
using static System.Math;

namespace Pathfinder.Fitness
{
    public class FitnessWithCollisionDetection : IFitness
    {
        IHeuristic Heuristic;
        GASettings gasettings;

        public FitnessWithCollisionDetection()
        {
            var settings = new Settings();
            Heuristic = settings.GetHeuristic();
            gasettings = new GASettings();
        }
        public double Calc(IGenome genome)
        {
            var _endNode = genome.Map.EndNode;
            var lastnode = genome.ListNodes.Last();

            var penalty = (double)0;
            if (lastnode.Collision)
                penalty = gasettings.Penalty;

            return penalty + Heuristic.Calc(Abs(lastnode.X - _endNode.X), Abs(lastnode.Y - _endNode.Y));
        }
    }
}
