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
        GASettings setting { get; set; }
        IFitness Fitness { get; set; }
        IMutate Mutate { get; set; }
        ICrossover Crossover { get; set; }
        public GAFinder(DiagonalMovement diag, int weight = 1) : base(diag, weight)
        {
            Name = "Genetic Algorithm";
            setting = new GASettings();

            Mutate = setting.GetMutate();
            Crossover = setting.GetCrossover();
            Fitness = setting.GetFitness();
        }

        public override bool Find(IGenome ganome)
        {
            //GridMap = grid;
            //_startNode = grid.StartNode;
            //_endNode = grid.EndNode;

            int step = 0;
            OnStart(BuildArgs(step));

            for (int i = 0; i < setting.GenerationLimit; i++)
            {


                OnStep(BuildArgs(step++));
            }
            OnEnd(BuildArgs(step, false));

            return false;
        }
    }
}
