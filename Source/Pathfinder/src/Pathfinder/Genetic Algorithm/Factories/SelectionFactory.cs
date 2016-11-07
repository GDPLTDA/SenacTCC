using Pathfinder.Abstraction;
using Pathfinder.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class SelectionFactory
    {
        public static ISelection GetSimpleImplementation()
        {
            return new SelectionRouletteWheel();
        }

        public static ISelection GetRouletteWheelSelectionImplementation()
        {
            return new SelectionRouletteWheel();
        }
    }
}
