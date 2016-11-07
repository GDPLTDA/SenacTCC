using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public abstract class AbstractMutate : IMutate
    {
        public AbstractMutate()
        {
            var settings = new GASettings();
            MutationRate = settings.MutationRate;
        }
        public double MutationRate { get; set; }

        public abstract IGenome Calc(IGenome baby);

       

        }

    }



