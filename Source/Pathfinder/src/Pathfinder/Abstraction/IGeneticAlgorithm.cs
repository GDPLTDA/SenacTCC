using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
   public interface IGeneticAlgorithm : IFinder
    {
        IFitness Fitness { get; set; }
        IMutate Mutate { get; set; }
        ICrossover Crossover { get; set; }
        ISelection Selection { get; set; }
        int Generations { get; set; }
    }
}