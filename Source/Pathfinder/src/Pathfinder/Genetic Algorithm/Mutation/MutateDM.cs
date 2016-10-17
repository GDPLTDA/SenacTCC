using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateDM : IMutate
    {
        public double MutationRate { get; set; }
        public MutateDM(double rate)
        {
            MutationRate = rate;
        }
        public IGenome Calc(IGenome baby)
        {
            return baby;
        }
    }
}
