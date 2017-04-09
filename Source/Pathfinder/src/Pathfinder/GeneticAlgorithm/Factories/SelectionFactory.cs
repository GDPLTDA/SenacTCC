using Pathfinder.Abstraction;
using Pathfinder.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class SelectionFactory : IFactory<ISelection>
    {
        public static ISelection GetRandomImplementation()
            => new SelectionRandom();
        public static ISelection GetRouletteWheelSelectionImplementation()
            => new SelectionRouletteWheel();
        public ISelection GetImplementation()
            => Decide(GASettings.SelectionAlgorithm);
        public ISelection GetImplementation(int option)
            => Decide((SelectionEnum)option);
        private static ISelection Decide(SelectionEnum option)
        {
            switch (option)
            {
                case SelectionEnum.Random:
                    return GetRandomImplementation();
                case SelectionEnum.RouletteWheel:
                    return GetRouletteWheelSelectionImplementation();
            }
            throw new Exception("No Selection selected");
        }
    }
}