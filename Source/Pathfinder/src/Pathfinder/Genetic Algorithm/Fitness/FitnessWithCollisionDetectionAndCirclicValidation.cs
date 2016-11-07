using Pathfinder.Abstraction;
using System.Linq;
using static System.Math;

namespace Pathfinder.Fitness
{
    public class FitnessWithCollisionDetectionAndCirclicValidation : IFitness
    {
        IHeuristic Heuristic;
        GASettings GAsettings;

        public FitnessWithCollisionDetectionAndCirclicValidation()
        {
            Heuristic = Program.Settings.GetHeuristic();
            GAsettings = Program.GASettings;
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
                penalty += GAsettings.Penalty * (HeuristicValue / HeuristicMaxDistance);

            var IsCirclic = genome
                          .ListNodes
                          .GroupBy(e => new { e.X, e.Y })
                          .Select(e => new { e.Key.X, e.Key.Y, qtd = e.Count() })
                          .Any(e => e.qtd > 1);

           
            if (IsCirclic)
                penalty += GAsettings.Penalty * (HeuristicValue / HeuristicMaxDistance); 




            return penalty + HeuristicValue;
        }
    }
}
