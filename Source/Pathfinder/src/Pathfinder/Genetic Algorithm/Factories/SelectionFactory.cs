using Pathfinder.Abstraction;
using Pathfinder.Selection;

namespace Pathfinder.Factories
{
    public class SelectionFactory
    {
        public static ISelection GetSimpleImplementation()
        {
            return new SelectionRandom();
        }

        public static ISelection GetRouletteWheelSelectionImplementation()
        {
            return new SelectionRouletteWheel();
        }
    }
}
