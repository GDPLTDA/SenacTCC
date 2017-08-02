
using Pathfinder.Abstraction;
using Pathfinder.Fitness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class FitnessFactory : IFactory<IFitness>
    {
        public static IFitness GetHeuristicImplementation()
            => new FitnessHeuristic();
        public static IFitness GetCirclicValidationImplementation()
            => new FitnessWithCirclicValidation();
        public static IFitness GetCollisionDetectionImplementation()
            => new FitnessWithCollisionDetection();
        public static IFitness GetCollisionDetectionAndCirclicValidationImplementation()
            => new FitnessWithCollisionDetectionAndCirclicValidation();
        public IFitness GetImplementation()
            => Decide(GASettings.FitnessAlgorithm);
        public IFitness GetImplementation(int option)
            => Decide((FitnessEnum)option);
        private static IFitness Decide(FitnessEnum option)
        {
            switch (option)
            {
                case FitnessEnum.Heuristic:
                    return GetHeuristicImplementation();
                case FitnessEnum.CirclicValidation:
                    return GetCirclicValidationImplementation();
                case FitnessEnum.CollisionDetection:
                    return GetCollisionDetectionImplementation();
                case FitnessEnum.CollisionDetectionAndCirclicValidation:
                    return GetCollisionDetectionAndCirclicValidationImplementation();
            }
            throw new Exception("No finder selected");
        }
    }
}