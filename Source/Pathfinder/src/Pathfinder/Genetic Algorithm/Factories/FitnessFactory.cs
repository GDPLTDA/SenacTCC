using Pathfinder.Abstraction;
using Pathfinder.Fitness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class FitnessFactory
    {
        public static IFitness GetSimpleImplementation()
        {
            return new FitnessHeuristic();
        }
        public static IFitness GetSimpleWithCollisionDetectionImplementation()
        {
            return new FitnessWithCollisionDetection();
        }

        public static IFitness GetCirclicValidationImplementation()
        {
            return new FitnessWithCirclicValidation();
        }

        public static IFitness GetColisionDetectAndCirclicValidationImplementation()
        {
            return new FitnessWithCollisionDetectionAndCirclicValidation();
        }
    }
}
