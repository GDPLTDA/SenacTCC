

using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;

namespace Pathfinder
{
    public class Resolver
    {
        public static T Resolve<T>()
        {
            IFactory<T> factory = null;
                        
            var @switch = new Dictionary<Type, Action> {
                { typeof(IHeuristic),
                    () =>factory = Set<T,HeuristicFactory>() },
                { typeof(IAppMode),
                    () =>factory = Set<T,AppModeFactory>()  },
                { typeof(IMapGenerator),
                    () =>factory = Set<T,MapGeneratorFactory>() },
                { typeof(IViewer),
                    () =>factory = Set<T,ViewerFactory>() },
                 { typeof(IFinder),
                    () =>factory = Set<T,FinderFactory>() },
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