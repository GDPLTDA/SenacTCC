using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateDIVM : IMutate
    {
        public IGenome Calc(IGenome baby)
        {
            return baby;
        }
    }
}
