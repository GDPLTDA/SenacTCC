using Pathfinder.Abstraction;
using Pathfinder.Genetic_Algorithm.Fitness;
using System.Linq;
using static System.Math;
namespace Pathfinder.Fitness
{
    public class FitnessWithCollisionDetectionAndCirclicValidation : IFitness
    {
        IHeuristic Heuristic;
        public FitnessWithCollisionDetectionAndCirclicValidation()
        {
            Heuristic = Container.Resolve<IHeuristic>();
        }
        public double Calc(IGenome genome)
        {
            var _endNode = genome.Map.EndNode;
            var lastnode = genome.ListNodes.Last();
            var startnode = genome.ListNodes.First();
            var HeuristicMaxDistance = Heuristic.Calc(Abs(startnode.X - _endNode.X), Abs(startnode.Y - _endNode.Y));
            var HeuristicValue = Heuristic.Calc(Abs(lastnode.X - _endNode.X), Abs(lastnode.Y - _endNode.Y));
            var penalty = (double)0;
            if (lastnode.Collision)
            {
                var xy = new xy() { x = lastnode.X, y = lastnode.Y };
                var badPath = 0;
                if (FinessHelper.RepeatControl.ContainsKey(xy))
                {
                    FinessHelper.RepeatControl[xy]++;
                    badPath = FinessHelper.RepeatControl[xy];
                }
                else
                    FinessHelper.RepeatControl.Add(xy, 1);
                penalty += (GASettings.Penalty * (HeuristicValue / HeuristicMaxDistance)) + (badPath*100);
            }
            var IsCirclic = genome
                          .ListNodes
                          .GroupBy(e => new { e.X, e.Y })
                          .Select(e => new { e.Key.X, e.Key.Y, qtd = e.Count() })
                          .Any(e => e.qtd > 1);
            if (IsCirclic)
                penalty += GASettings.Penalty * (HeuristicValue / HeuristicMaxDistance);
            return penalty + HeuristicValue;
        }
    }
}
