using Microsoft.Extensions.Configuration;
using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class Settings
    {
        public IConfigurationRoot Configuration { get; set; }
      
        public char Start { get; set; }
        public char End { get; set; }
        public char Empty { get; set; }
        public char Wall { get; set; }
        public char Path { get; set; }
        public char Opened { get; set; }
        public char Closed { get; set; }
        public int OpenGlBlockSize { get; set; }

        public int IDAStarFinderTimeOut { get; set; }
        public bool IDATrackRecursion { get; set; }

        public int MapViwer { get; set; }
        public int MapOrigin { get; set; }
        public int Heuristic { get; set; }
        public int Algorithn { get; set; }
        public Constants.DiagonalMovement AllowDiagonal { get; set; }

        public double RandomSeed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinimumPath { get; set; }

        public Settings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


            Start = Configuration[nameof(Start)].ToString()[0];
            End = Configuration[nameof(End)].ToString()[0];
            Empty = Configuration[nameof(Empty)].ToString()[0];
            Wall = Configuration[nameof(Wall)].ToString()[0];
            Path = Configuration[nameof(Path)].ToString()[0];
            Opened = Configuration[nameof(Opened)].ToString()[0];
            Closed = Configuration[nameof(Closed)].ToString()[0];
            OpenGlBlockSize = int.Parse(Configuration[nameof(OpenGlBlockSize)]);

            MapViwer = int.Parse(Configuration[nameof(MapViwer)]);
            MapOrigin = int.Parse(Configuration[nameof(MapOrigin)]);
            Heuristic = int.Parse(Configuration[nameof(Heuristic)]);
            Algorithn = int.Parse(Configuration[nameof(Algorithn)]);
            IDAStarFinderTimeOut = int.Parse(Configuration[nameof(IDAStarFinderTimeOut)]);
            IDATrackRecursion = bool.Parse(Configuration[nameof(IDATrackRecursion)]);
            AllowDiagonal = (Constants.DiagonalMovement)int.Parse(Configuration[nameof(AllowDiagonal)]);

            RandomSeed = double.Parse(Configuration[nameof(RandomSeed)]);
            Width = int.Parse(Configuration[nameof(Width)]);
            Height = int.Parse(Configuration[nameof(Height)]);
            MinimumPath = int.Parse(Configuration[nameof(MinimumPath)]);
        }


        public IFinder GetFinder(IHeuristic heuri)
        {
            IFinder ret = null;

            switch (Algorithn)
            {
                case 0:
                    ret = FinderFactory.GetAStarImplementation(AllowDiagonal, heuri);
                    break;
                case 1:
                    ret = FinderFactory.GetBFSImplementation(AllowDiagonal, heuri);
                    break;
                case 2:
                    ret = FinderFactory.GetDijkstraImplementation(AllowDiagonal, heuri);
                    break;
                case 4:
                    ret = FinderFactory.GetGAImplementation(AllowDiagonal);
                    break;
                case 3:
                    ret = FinderFactory.GetIDAStarImplementation(AllowDiagonal, heuri);
                    break;

            }

            return ret;
        }

        public IHeuristic GetHeuristic()
        {
            IHeuristic ret = null;
            switch (Heuristic)
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

        public IMapGenerator GetGenerator()
        {
            IMapGenerator ret = null;

            switch (MapOrigin)
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

        public IViewer GetViewer(IFinder finder)
        {
            IViewer ret = null;

            switch (MapViwer)
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
