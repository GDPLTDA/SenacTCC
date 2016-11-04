using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;

namespace Pathfinder.Finders
{
    public class GAFinder : AbstractFinder, IGeneticAlgorithm
    {
        List<IGenome> Populations { get; set; } = new List<IGenome>();
        GASettings setting { get; set; }
        public IFitness Fitness { get; set; }
        public IMutate Mutate { get; set; }
        public ICrossover Crossover { get; set; }
        public ISelection Selection { get; set; }
        public int Generations { get; set; }

        public GAFinder(DiagonalMovement diag, GASettings gasettings, int weight = 1) : base(diag, weight)
        {
            SleepUITimeInMs = 15;

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
                Populations.Add(new Genome(map, DiagonalMovement));
            CalcFitness();
            int step = 0;
            
            OnStart(BuildArgs(step));

            for (int i = 0; i < setting.GenerationLimit; i++)
            {
                var newpopulations = new List<IGenome>();
                
                Populations = Populations.OrderBy(o => o.Fitness).ToList();

                for (int j = 0; j < setting.BestSolution; j++)
                {
                    Populations[j].Fitness = Fitness.Calc(Populations[j]);
                    newpopulations.Add(Populations[j]);
                }

                int ran = Settings.Random.Next(1, Populations.Count);
                var best = Populations.First().ListNodes;
                var best2 = Selection.Select(Populations).ListNodes;
                _endNode = best.Last();
                
                if (_endNode.Equals(map.EndNode))
                {
                    OnEnd(BuildArgs(step, true));
                    Generations = i;
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
                    //// Mutation
                    nodemom = Mutate.Calc(cross.Mom);
                    nodedad = Mutate.Calc(cross.Dad);
                    // Adaptation
                    nodemom = Adaptation.Calc(nodemom);
                    nodedad = Adaptation.Calc(nodedad);

                    nodemom.Fitness = Fitness.Calc(nodemom);
                    nodedad.Fitness = Fitness.Calc(nodedad);

                    // Add in new population
                    newpopulations.Add(nodemom);
                    newpopulations.Add(nodedad);
                }
                Populations = null;
                Populations = newpopulations.ToList();
                
                OnStep(BuildArgs(step++));
            }
            Generations = setting.GenerationLimit;
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
