using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Pathfinder.Factories
{
    public class RandomFactory : IFactory<IRandom>
    {
        public static GARandom Rand;
        static RandomFactory()
        {
            Rand = new GARandom();
        }
        public IRandom GetImplementation()
            => Rand;
        public IRandom GetImplementation(int option)
        {
            throw new NotImplementedException();
        }
    }
}