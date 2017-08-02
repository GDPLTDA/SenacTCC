using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
    public abstract class AbstractMutate : IMutate
    {
        protected AbstractMutate()
        {
            MutationRate = GASettings.MutationRate;
        }
        public double MutationRate { get; set; }
        public abstract IGenome Calc(IGenome baby);
        }
    }