using Microsoft.Extensions.Configuration;
using System.IO;
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
        public static MapGeneratorEnum MapOrigin { get; set; }
        public static HeuristicEnum Heuristic { get; set; }
        public static FinderEnum Algorithm { get; set; }
        public static DiagonalMovement AllowDiagonal { get; set; }
        public static double RandomSeed { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int MinimumPath { get; set; }
        public static int RandomBlock { get; set; }
        public static string FileToLoad { get; set; }
        public static string FolderToSaveMaps { get; set; }
        public static bool AutoSaveMaps { get; set; } = true;
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
            MapOrigin = (MapGeneratorEnum)int.Parse(Configuration[nameof(MapOrigin)]);
            Heuristic = (HeuristicEnum)int.Parse(Configuration[nameof(Heuristic)]);
            Algorithm = (FinderEnum)int.Parse(Configuration[nameof(Algorithm)]);
            IDAStarFinderTimeOut = int.Parse(Configuration[nameof(IDAStarFinderTimeOut)]);
            IDATrackRecursion = bool.Parse(Configuration[nameof(IDATrackRecursion)]);
            AllowDiagonal = (DiagonalMovement)int.Parse(Configuration[nameof(AllowDiagonal)]);
            RandomSeed = double.Parse(Configuration[nameof(RandomSeed)]);
            Width = int.Parse(Configuration[nameof(Width)]);
            Height = int.Parse(Configuration[nameof(Height)]);
            MinimumPath = int.Parse(Configuration[nameof(MinimumPath)]);
            RandomBlock = int.Parse(Configuration[nameof(RandomBlock)]);
            FileToLoad = Configuration[nameof(FileToLoad)].ToString();
            FolderToSaveMaps = Configuration[nameof(FolderToSaveMaps)].ToString();
        }
    }
}