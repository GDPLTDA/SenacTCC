

using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;
namespace Pathfinder
{
    public static class Container
    {
        static readonly Dictionary<Type, Type> _factories = new Dictionary<Type, Type>();
        static Container()
        {
            Register<IHeuristic, HeuristicFactory>();
            Register<IMapGenerator, MapGeneratorFactory>();
            Register<IFinder, FinderFactory>();
            Register<ICrossover, CrossoverFactory>();
            Register<IMutate, MutateFactory>();
            Register<IFitness, FitnessFactory>();
            Register<ISelection, SelectionFactory>();
            Register<IRandom, RandomFactory>();
        }
        public static T Resolve<T>()
            => GetFactory<T>().GetImplementation();

        public static T Resolve<T>(int option)
            => GetFactory<T>().GetImplementation(option);


        public static IFactory<T> GetFactory<T>()
        {
            var fType = _factories[typeof(T)];
            var factory = Activator.CreateInstance(fType) as IFactory<T>;
            return factory;
        }
        public static void Register<T, TFactory>() where TFactory : IFactory<T>
        {
            _factories.Add(typeof(T), typeof(TFactory));
        }
    }
}