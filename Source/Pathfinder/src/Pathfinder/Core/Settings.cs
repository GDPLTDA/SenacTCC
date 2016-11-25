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

        public static IConfigurationRoot Configuration { get; set; }
      
        public static char Start { get; set; }
        public static char End { get; set; }
        public static char Empty { get; set; }
        public static char Wall { get; set; }
        public static char Path { get; set; }
        public static char Opened { get; set; }
        public static char Closed { get; set; }
        public static int OpenGlBlockSize { get; set; }

        public static int IDAStarFinderTimeOut { get; set; }
        public static bool IDATrackRecursion { get; set; }

        public static ViewerEnum MapViwer { get; set; }
        public static MapGeneratorEnum MapOrigin { get; set; }
        public static HeuristicEnum Heuristic { get; set; }
        public static FinderEnum Algorithm { get; set; }
        public static Constants.DiagonalMovement AllowDiagonal { get; set; }

        public static int[] Batch_list_finders { get; set; }
        public static int[] Batch_list_heuristics { get; set; }
        public static int[] Batch_list_Mutation { get; set; }
        public static int[] Batch_list_Crossover { get; set; }
        public static int[] Batch_list_Fitness { get; set; }
        public static int[] Batch_list_Selection { get; set; }


        public static double RandomSeed { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int MinimumPath { get; set; }
        public static int RandomBlock { get; set; }

        public static string FileToLoad { get; set; }
        public static string FolderToSaveMaps { get; set; }

        public static AppModeEnum AppMode { get; set; }

        public static string Batch_folder { get; set; }
        public static int Batch_generate_pattern { get; set; }
        public static int Batch_map_origin { get; set; }
        public static int Batch_map_qtd_to_generate { get; set; }
        public static int Batch_GATimesToRunPerMap { get; set; }

        static Settings()
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

            MapViwer = (ViewerEnum)int.Parse(Configuration[nameof(MapViwer)]);
            MapOrigin =(MapGeneratorEnum) int.Parse(Configuration[nameof(MapOrigin)]);
            Heuristic = (HeuristicEnum)int.Parse(Configuration[nameof(Heuristic)]);
            Algorithm = (FinderEnum)int.Parse(Configuration[nameof(Algorithm)]);
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
            AppMode = (AppModeEnum)int.Parse(Configuration[nameof(AppMode)].ToString());

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

                

    }
}
