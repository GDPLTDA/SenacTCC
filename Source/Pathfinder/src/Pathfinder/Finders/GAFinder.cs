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
        public GAFinder(DiagonalMovement diag, GASettings gasettings, int weight = 1) : base(diag, weight)
        {
            Name = "Genetic Algorithm";
            if(gasettings == null)
                gasettings = new GASettings();
            setting = gasettings;

            Mutate = setting.GetMutate();
            Crossover = setting.GetCrossover();
            Fitness = setting.GetFitness();
            Selection = setting.GetSelection();
        }

        protected override void UpdateMaxNodes()
        {
            var atualNodes = Populations.Sum(o => o.ListNodes.Count);
            if (atualNodes >= _maxExpandedNodes)
                _maxExpandedNodes = atualNodes;
        }

        public override bool Find(IMap map)
        {
            var Adaptation = new Adaptation(map);

            GridMap = map;
            _startNode = map.StartNode;
            _endNode = map.EndNode;

            for (int i = 0; i < setting.PopulationSize; i++)
                Populations.Add(new Genome(map));

            int step = 0;
            OnStart(BuildArgs(step));

            for (int i = 0; i < setting.GenerationLimit; i++)
            {
                var newpopulations = new List<IGenome>();

                CalcFitness();
                Populations = Populations.OrderBy(o => o.Fitness).ToList();

                for (int j = 0; j < setting.BestSolution; j++)
                    newpopulations.Add(new Genome(Populations[j]));

                int ran = Settings.Random.Next(1, Populations.Count);
                var best = Populations.First().ListNodes;
                var best2 = Populations[ran].ListNodes;
                _endNode = best.Last();
                
                if (_endNode.Equals(map.EndNode))
                {
                    OnEnd(BuildArgs(step, true));
                    return true;
                }

                _closedList = best;
                _openList = best2;

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
                Populations = newpopulations.Select(o => (IGenome)new Genome(o)).ToList();
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
