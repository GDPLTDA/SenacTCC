using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;

namespace Pathfinder.Finders
{
    public class GAFinder : AbstractFinder
    {
        List<Genome> Populations { get; set; } = new List<Genome>();
        GASettings setting { get; set; }
        IFitness Fitness { get; set; }
        IMutate Mutate { get; set; }
        ICrossover Crossover { get; set; }
        ISelection Selection { get; set; }
        public GAFinder(DiagonalMovement diag, int weight = 1) : base(diag, weight)
        {
            Name = "Genetic Algorithm";
            setting = new GASettings();

            Mutate = setting.GetMutate();
            Crossover = setting.GetCrossover();
            Fitness = setting.GetFitness();
            Selection = setting.GetSelection();
        }

        public override bool Find(IMap map)
        {
            var Adaptation = new Adaptation(map);

            GridMap = map;
            _startNode = map.StartNode;
            _endNode = map.EndNode;

            for (int i = 0; i < setting.PopulationSize; i++)
                Populations.Add(new Genome(map));
            Populations = Populations.OrderByDescending(i => i.ListNodes.Count).ToList();

            int step = 0;
            OnStart(BuildArgs(step));

            for (int i = 0; i < setting.GenerationLimit; i++)
            {
                CalcFitness();

                var nodemom = Selection.Select(Populations);
                var nodedad = Selection.Select(Populations);

                var cross = Crossover.Calc(new CrossoverOperation(nodemom, nodedad));
                nodemom = Mutate.Calc(cross.Mom);
                nodedad = Mutate.Calc(cross.Dad);

                nodemom = Adaptation.Calc(nodemom);
                nodedad = Adaptation.Calc(nodedad);

                _openList = Populations[0].ListNodes;
                OnStep(BuildArgs(step++));
            }
            OnEnd(BuildArgs(step, false));

            return false;
        }

        void CalcFitness()
        {
            foreach (var item in Populations)
                item.Fitness = Fitness.Calc(item.ListNodes);
        }
    }
}
