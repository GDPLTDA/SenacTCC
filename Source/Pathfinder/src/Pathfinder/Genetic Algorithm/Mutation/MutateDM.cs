using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateDM : IMutate
    {
        public List<Node> Calc(List<Node> baby)
        {
            return baby;
        }
    }
}
