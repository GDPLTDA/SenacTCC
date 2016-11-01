using Microsoft.Extensions.Configuration;
using Pathfinder;
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
        public static Random Random { get; set; } = new Random();
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
        public int Algorithm { get; set; }
        public Constants.DiagonalMovement AllowDiagonal { get; set; }

        public int[] Batch_list_finders { get; set; }
        public int[] Batch_list_heuristics { get; set; }
        public int[] Batch_list_Mutation { get; set; }
        public int[] Batch_list_Crossover { get; set; }
        public int[] Batch_list_Fitness { get; set; }
        public int[] Batch_list_Selection { get; set; }


        public double RandomSeed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinimumPath { get; set; }
        public int RandomBlock { get; set; }

        public string FileToLoad { get; set; }
        public string FolderToSaveMaps { get; set; }

        public int AppMode { get; set; }

        public string Batch_folder { get; set; }
        public int Batch_generate_pattern { get; set; }
        public int Batch_map_origin { get; set; }
        public int Batch_map_qtd_to_generate { get; set; }
        public int Batch_GATimesToRunPerMap { get; set; }

        public Settings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
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
            Algorithm = int.Parse(Configuration[nameof(Algorithm)]);
            IDAStarFinderTimeOut = int.Parse(Configuration[nameof(IDAStarFinderTimeOut)]);
            IDATrackRecursion = bool.Parse(Configuration[nameof(IDATrackRecursion)]);
            AllowDiagonal = (Constants.DiagonalMovement)int.Parse(Configuration[nameof(AllowDiagonal)]);

            RandomSeed = double.Parse(Configuration[nameof(RandomSeed)]);
            Width = int.Parse(Configuration[nameof(Width)]);
            Height = int.Parse(Configuration[nameof(Height)]);
            MinimumPath = int.Parse(Configuration[nameof(MinimumPath)]);
            RandomBlock = int.Parse(Configuration[nameof(RandomBlock)]);

            FileToLoad = Configuration[nameof(FileToLoad)].ToString();
            FolderToSaveMaps = Configuration[nameof(FolderToSaveMaps)].ToString();
            AppMode = int.Parse(Configuration[nameof(AppMode)].ToString());

            Batch_folder = Configuration[nameof(Batch_folder)].ToString();
            Batch_map_origin = int.Parse(Configuration[nameof(Batch_map_origin)].ToString());
            Batch_generate_pattern = int.Parse(Configuration[nameof(Batch_generate_pattern)].ToString());
            Batch_map_qtd_to_generate = int.Parse(Configuration[nameof(Batch_map_qtd_to_generate)].ToString());
            Batch_GATimesToRunPerMap = int.Parse(Configuration[nameof(Batch_GATimesToRunPerMap)].ToString());

            Batch_list_finders      = Configuration.GetSection(nameof(Batch_list_finders)).GetChildren().Select(e=>int.Parse(e.Value)).ToArray();
            Batch_list_heuristics   = Configuration.GetSection(nameof(Batch_list_heuristics)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Mutation     = Configuration.GetSection(nameof(Batch_list_Mutation)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Crossover    = Configuration.GetSection(nameof(Batch_list_Crossover)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Fitness      = Configuration.GetSection(nameof(Batch_list_Fitness)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Selection    = Configuration.GetSection(nameof(Batch_list_Selection)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();

     

        }

        public IAppMode GetAppMode( int option = -1)
        {

            var opt = option >= 0 ? option : AppMode;  
            switch (opt)
            {
                case 0:
                    return AppModeFactory.GetSingleRunImplementation();
                case 1:
                    return AppModeFactory.GetDynamicImplementation();
                case 2:
                    return AppModeFactory.GetBatchImplementation();
            }

            throw new Exception("No app mode selected");
        }

        public IFinder GetFinder(IHeuristic heuri, int option = -1, GASettings gasttings = null)
        {
            IFinder ret = null;

            var opt = option >= 0 ? option : Algorithm;
            switch (opt)
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
                    ret = FinderFactory.GetGAImplementation(AllowDiagonal, gasttings);
                    break;
                case 3:
                    ret = FinderFactory.GetIDAStarImplementation(AllowDiagonal, heuri);
                    break;

            }

            return ret;
        }

        public IHeuristic GetHeuristic( int option = -1)
        {
            IHeuristic ret = null;
            var opt = option >= 0 ? option : Heuristic;
            switch (opt)
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

        public IMapGenerator GetGenerator( int option = -1)
        {
            IMapGenerator ret = null;

            var opt = option >= 0 ? option : MapOrigin;
            switch (opt)
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
                case 3:
                    ret = MapGeneratorFactory.GetStandardMapGeneratorImplementation();
                    break;
            }

            return ret;
        }

        public IViewer GetViewer(IFinder finder, int option =-1)
        {
            IViewer ret = null;

            var opt = option >= 0 ? option : MapViwer;
            switch (opt)
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
