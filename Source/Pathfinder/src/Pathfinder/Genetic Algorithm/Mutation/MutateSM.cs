using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateSM : AbstractMutate
    {
        public override IGenome Calc(IGenome baby)
        {
            return new Genome();
        }
    }
}
