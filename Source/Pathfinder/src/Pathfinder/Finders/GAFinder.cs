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
        List<IGenome> Populations { get; set; } = new List<IGenome>();
        GASettings setting { get; set; }
        IFitness Fitness { get; set; }
        IMutate Mutate { get; set; }
        ICrossover Crossover { get; set; }
        ISelection Selection { get; set; }
        public GAFinder(DiagonalMovement diag, int weight = 1) : base(diag, weight)
        {
            Name = "Genetic Algorithm";
            setting = new GASettings();

            Mutate = setting.GetMutate(setting.MutationRate);
            Crossover = setting.GetCrossover(setting.CrossoverRate);
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
                var newpopulations = new List<IGenome>();

                CalcFitness();
                Populations = Populations.OrderByDescending(o => o.Fitness).ToList();

                while (newpopulations.Count < Populations.Count)
                {
                    // Selection
                    var nodemom = Selection.Select(Populations);
                    var nodedad = Selection.Select(Populations);
                    // CrossOver
                    var cross = Crossover.Calc(new CrossoverOperation(nodemom, nodedad));
                    // Mutation
                    nodemom = Mutate.Calc(cross.Mom);
                    nodedad = Mutate.Calc(cross.Dad);
                    // Adaptation
                    nodemom = Adaptation.Calc(nodemom);
                    nodedad = Adaptation.Calc(nodedad);
                    // Add in new population
                    newpopulations.Add(nodemom);
                    newpopulations.Add(nodedad);
                }
                Populations = newpopulations.Select(o => (IGenome)new Genome(o)).OrderByDescending(o => o.ListNodes.Count).ToList();

                _openList = Populations[0].ListNodes;
                OnStep(BuildArgs(step++));
            }
            OnEnd(BuildArgs(step, false));

            return false;
        }

        void CalcFitness()
        {
            foreach (var item in Populations)
                item.Fitness = Fitness.Calc(item);
        }
    }
}
