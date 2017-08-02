
using Pathfinder.Abstraction;
using Pathfinder.Crossover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class CrossoverFactory : IFactory<ICrossover>
    {
        public static ICrossover GetSimpleImplementation()
            => new CrossoverSimple();
        public static ICrossover GetOBXImplementation()
            => new CrossoverOBX();
        public static ICrossover GetPBXImplementation()
            => new CrossoverPBX();
        public ICrossover GetImplementation()
            => Decide(GASettings.CrossoverAlgorithm);
        public ICrossover GetImplementation(int option)
            => Decide((CrossoverEnum)option);
        private static ICrossover Decide(CrossoverEnum option)
        {
            switch (option)
            {
                case CrossoverEnum.Simple:
                    return GetSimpleImplementation();
                case CrossoverEnum.OBX:
                    return GetOBXImplementation();
                case CrossoverEnum.PBX:
                    return GetPBXImplementation();
            }
            throw new Exception("No crossover selected");
        }
    }
}