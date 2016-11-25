

using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;

namespace Pathfinder
{
    public class GAResolver
    {
        public static T Resolve<T>()
        {
            IFactory<T> factory = null;

            
            var @switch = new Dictionary<Type, Action> {
                { typeof(ICrossover),
                    () =>factory = Set<T,CrossoverFactory>() },
                { typeof(IMutate),
                    () =>factory = Set<T,MutateFactory>()  },
                { typeof(IFitness),
                    () =>factory = Set<T,FitnessFactory>() },
                { typeof(ISelection),
                    () =>factory = Set<T,SelectionFactory>() },
                 { typeof(IRandom),
                    () =>factory = Set<T,RandomFactory>() },
            };

            
            @switch[typeof(T)]();

            return factory.GetImplementation();
        }


        private static IFactory<T> Set<T, TImp>() where TImp : class, new()
        {
            return new TImp() as IFactory<T>;

        }

        
    }
}