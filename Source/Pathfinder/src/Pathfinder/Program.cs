using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Pathfinder.Factories;
using Pathfinder.Abstraction;

namespace Pathfinder
{
    public class Program
    {
        static Settings settings;
        public static void Main(string[] args)
        {
            settings = new Settings();

            var heu = GetHeuristic();
            var finder = GetFinder(heu);
            var generator = GetGenerator();
            var viewer = GetViewer(finder);

            var map = generator.ReadMap();
            viewer.Run(map);

        }


        static IFinder GetFinder(IHeuristic heuri)
        {
            IFinder ret = null;

            switch (settings.Algorithn)
            {
                case 0:
                    ret = FinderFactory.GetAStarImplementation(settings.AllowDiagonal, heuri);
                    break;
                case 1:
                    ret = FinderFactory.GetBFSImplementation(settings.AllowDiagonal, heuri);
                    break;
            }

            return ret;
        }

        static IHeuristic GetHeuristic()
        {
            IHeuristic ret = null;
            switch (settings.Heuristic)
            {
                case 0:
                    ret = HeuristicFactory.GetManhattamImplementation();
                    break;
                case 1:
                    ret = HeuristicFactory.GetEuclideanImplementation();
                    break;
                case 2:
                    ret = HeuristicFactory.GetOctileImplementation();
                    break;
                case 3:
                    ret = HeuristicFactory.GetChebyshevImplementation();
                    break;
            }

            return ret;
            
        }

        static IMapGenerator GetGenerator()
        {
            IMapGenerator ret = null;

            switch (settings.MapOrigin)
            {
                case 0:
                    ret = MapGeneratorFactory.GetStaticMapGeneratorImplementation();
                    break;

                case 1:
                    ret = MapGeneratorFactory.GetFileMapGeneratorImplementation();
                    break;

                case 2:
                    ret = MapGeneratorFactory.GetRandomMapGeneratorImplementation();
                    break;
            }

            return ret;
        }

        static IViewer GetViewer(IFinder finder)
        {
            IViewer ret = null;

            switch (settings.MapViwer)
            {
                case 0:
                    ret = ViewerFactory.GetConsoleViewerImplementation(finder);
                    break;
                case 1:
                    ret = ViewerFactory.GetOpenGlViewerImplementation(finder);
                    break;
            }

            return ret;
        }

    }

        
}
