using Pathfinder.Abstraction;
using System.Collections.Generic;
using System.Linq;
namespace Pathfinder.Finders
{
    public class GAFinder : AbstractFinder, IGeneticAlgorithm
    {
        List<IGenome> Populations { get; set; } = new List<IGenome>();
        public IFitness Fitness { get; set; }
        public IMutate Mutate { get; set; }
        public ICrossover Crossover { get; set; }
        public ISelection Selection { get; set; }
        public int Generations { get; set; }
        public GAFinder(DiagonalMovement diag, int weight = 1) : base(diag, weight)
        {
            SleepUITimeInMs = 200;
            Name = "Genetic Algorithm";
            Mutate = Container.Resolve<IMutate>();
            Crossover = Container.Resolve<ICrossover>();
            Fitness = Container.Resolve<IFitness>();
            Selection = Container.Resolve<ISelection>();
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
            var rand = Container.Resolve<IRandom>();
            GridMap = map;
            _startNode = map.StartNode;
            _endNode = map.EndNode;
            for (int i = 0; i < GASettings.PopulationSize; i++)
                Populations.Add(new Genome(map, DiagonalMovement));
            CalcFitness();
            var step = 0;
            OnStart(BuildArgs(step));
            for (int i = 0; i < GASettings.GenerationLimit; i++)
            {
                var newpopulations = new List<IGenome>();
                Populations = Populations.OrderBy(o => o.Fitness).ToList();
                for (int j = 0; j < GASettings.BestSolutionToPick; j++)
                {
                    Populations[j].Fitness = Fitness.Calc(Populations[j]);
                    newpopulations.Add(Populations[j]);
                }
                var ran = rand.Next(1, Populations.Count);
                var best = Populations.First().ListNodes;
                var best2 = Selection.Select(Populations).ListNodes;
                _endNode = best.Last();
                if (_endNode.Equals(map.EndNode))
                {
                    if (!best.First().Equals(map.StartNode))
                    {
                        int c = 0;

                    }

                    OnEnd(BuildArgs(step, true));
                    Generations = i;
                    return true;
                }
                UpdateClosedList(best);
                UpdateOpenList(best2);

                while (newpopulations.Count < Populations.Count)
                {
                    // Selection
                    var nodemom = Selection.Select(Populations);
                    var nodedad = Selection.Select(Populations);
                    // CrossOver
                    var cross = Crossover.Calc(new CrossoverOperation(nodemom, nodedad));

                    if (!cross.Mom.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }
                    if (!cross.Dad.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }

                    // Mutation
                    nodemom = Mutate.Calc(cross.Mom);
                    nodedad = Mutate.Calc(cross.Dad);

                    if (!nodemom.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }
                    if (!nodedad.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }

                    // Adaptation
                    nodemom = Adaptation.Calc(nodemom);
                    nodedad = Adaptation.Calc(nodedad);
                    nodemom.Fitness = Fitness.Calc(nodemom);
                    nodedad.Fitness = Fitness.Calc(nodedad);

                    if (!nodemom.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }
                    if (!nodedad.ListNodes.First().Equals(map.StartNode))
                    {
                        int c = 0;
                    }

                    // Add in new population
                    newpopulations.Add(nodemom);
                    newpopulations.Add(nodedad);
                }
                Populations = newpopulations.ToList();
                OnStep(BuildArgs(step++));
            }
            Generations = GASettings.GenerationLimit;
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