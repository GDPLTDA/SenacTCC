﻿using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Crossover
{
    public class CrossoverSimple : AbstractCrossover
    {
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            return Operation;
        }
    }
}
