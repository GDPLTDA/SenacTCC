using Pathfinder.Abstraction;
using Pathfinder.UI.Abstraction;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Pathfinder.UI.AppMode
{
    public class BatchMode : IAppMode
    {
        public void Run()
        {
            var mapTypes = UISettings.Batch_list_generate_pattern.Select(
                t => t == 0 ? MapGeneratorEnum.Random : MapGeneratorEnum.WithPattern
            );

            var diagonals = Enum.GetValues(typeof(DiagonalMovement)).Cast<DiagonalMovement>()
                                .Where(d => UISettings.Batch_list_map_diagonal.Contains((int)d));

            foreach (var type in mapTypes)
                foreach (var diag in diagonals)
                    Process(diag, type);

            Console.WriteLine("\n\nComplete...");
            Console.ReadKey();
        }


        static void Process(DiagonalMovement movement, MapGeneratorEnum mapType)
        {
            var ft = new FileTool();
            Settings.IDATrackRecursion = false;
            Settings.AutoSaveMaps = false;
            var qtdMaps = UISettings.Batch_map_qtd_to_generate;
            var now = DateTime.Now;
            var folder = Path.Combine(UISettings.Batch_folder, $"batch_{Settings.Width}x{Settings.Height}_{mapType}_{movement}_{Settings.RandomSeed * 100}_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}");
            var dataFile = Path.Combine(folder, "_data.csv");
            var dataFileGA = Path.Combine(folder, "_dataGA.csv");

            if (UISettings.Batch_map_origin == 0)
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                //generate maps
                var generator = Container.Resolve<IMapGenerator>((int)mapType);
                for (int i = 0; i < qtdMaps; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"Generating maps({movement},{mapType})...");
                    DrawTextProgressBar(i, qtdMaps);
                    var map = generator.DefineMap(diagonal: movement);
                    map.AllowDiagonal = movement;
                    FileTool.SaveFileFromMap(map, Path.Combine(folder, i.ToString().PadLeft(qtdMaps.ToString().Length, '0') + ".txt"));
                }
            }
            else
            {
                //if will load the map, use the configured root path
                folder = UISettings.Batch_folder;
                dataFile = Path.Combine(folder, $"_data_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}.csv");
                dataFileGA = Path.Combine(folder, $"_dataGA_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}.csv");
            }
            var files = Directory.GetFiles(folder, "*.txt");
            var fileCount = files.Count();
            var finders = UISettings.Batch_list_finders;
            var heuristics = UISettings.Batch_list_heuristics;
            var Mutation = UISettings.Batch_list_Mutation;
            var Crossover = UISettings.Batch_list_Crossover;
            var Fitness = UISettings.Batch_list_Fitness;
            var Selection = UISettings.Batch_list_Selection;
            var csvFile = new StreamWriter(File.Open(dataFile, FileMode.OpenOrCreate), Encoding.UTF8, 4096, false);
            var csvGAFile = new StreamWriter(File.Open(dataFileGA, FileMode.OpenOrCreate), Encoding.UTF8, 4096, false);
            Console.Clear();
            csvFile.Write(new TextWrapper().GetHeader());
            csvGAFile.Write(new TextGAWrapper().GetHeader());
            for (int i = 0; i < fileCount; i++)
            {
                var map = FileTool.ReadMapFromFile(files[i]);
                foreach (var _finder in finders)
                {
                    foreach (var _h in heuristics)
                    {
                        var h = Container.Resolve<IHeuristic>(_h);
                        var finder = Container.Resolve<IFinder>(_finder);
                        finder.Heuristic = h;
                        if (finder is IGeneticAlgorithm)
                        {
                            for (int cross = 0; cross < Crossover.Count(); cross++)
                                for (int mut = 0; mut < Mutation.Count(); mut++)
                                    for (int fit = 0; fit < Fitness.Count(); fit++)
                                        for (int sel = 0; sel < Selection.Count(); sel++)
                                            for (int j = 0; j < UISettings.Batch_GATimesToRunPerMap; j++)
                                            {
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                                var GAFinder = (IGeneticAlgorithm)Container.Resolve<IFinder>(_finder);
                                                GAFinder.Heuristic = h;
                                                GAFinder.Crossover = Container.Resolve<ICrossover>(Crossover[cross]);
                                                GAFinder.Mutate = Container.Resolve<IMutate>(Mutation[mut]);
                                                GAFinder.Fitness = Container.Resolve<IFitness>(Fitness[fit]);
                                                GAFinder.Selection = Container.Resolve<ISelection>(Selection[sel]);
                                                var helper = $"\n                n:{j},cx:{GAFinder.Crossover.GetType().Name},m:{GAFinder.Mutate.GetType().Name},f:{GAFinder.Fitness.GetType().Name},s:{GAFinder.Selection.GetType().Name}";
                                                var csv = new TextWrapper();
                                                csv = RunStep(csv, i, fileCount, map, h, GAFinder, mapType, helper);
                                                var csvGA = new TextGAWrapper
                                                {
                                                    Alg = csv.Alg,
                                                    Map = csv.Map,
                                                    Heuristic = csv.Heuristic,
                                                    MapType = csv.MapType,
                                                    Diagonal = csv.Diagonal,
                                                    Solution = csv.Solution,
                                                    Time = csv.Time,
                                                    MaxNodes = csv.MaxNodes,
                                                    PathLength = csv.PathLength,
                                                    Crossover = GAFinder.Crossover.GetType().Name,
                                                    Mutation = GAFinder.Mutate.GetType().Name,
                                                    Fitness = GAFinder.Fitness.GetType().Name,
                                                    Selection = GAFinder.Selection.GetType().Name,
                                                    Generations = GAFinder.Generations.ToString(),
                                                };
                                                csvGAFile.Write(csvGA.ToString());
                                            }
                        }
                        else
                        {
                            var csv = new TextWrapper();
                            csv = RunStep(csv, i, fileCount, map, h, finder, mapType);
                            csvFile.Write(csv.ToString());
                        }
                        csvFile.Flush();
                        csvGAFile.Flush();
                    }
                }
            }
            DrawTextProgressBar(fileCount, fileCount);

            csvFile.Dispose();
            csvGAFile.Dispose();

        }


        static TextWrapper RunStep(TextWrapper baseScv, int i, int fileCount, IMap map, IHeuristic h, IFinder finder, MapGeneratorEnum mapType, string plus = "")
        {
            var csv = baseScv;
            csv.Map = i.ToString();
            csv.MapType = mapType.ToString();
            csv.Alg = finder.Name;
            csv.Heuristic = h.GetType().Name;
            csv.Diagonal = map.AllowDiagonal.HasValue ? map.AllowDiagonal.Value.ToString() : finder.DiagonalMovement.ToString();
            Console.CursorLeft = 0;
            if (Console.CursorTop > 0)
            {
                Console.Write(new string(' ', 80));
                Console.CursorLeft = 0;
            }
            Console.WriteLine($"            ({i}) {csv.Alg} - { csv.Heuristic } - {csv.Diagonal} ({plus})");
            DrawTextProgressBar(i, fileCount);
            if (finder.Find(map))
            {
                csv.PathLength = finder.GetPath().OrderBy(x => x.G).Last().G.ToString();
                Console.ForegroundColor = ConsoleColor.Green;
                csv.Solution = "Yes";
            }
            else
            {
                csv.Solution = "No";
                csv.PathLength = "-1";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            csv.Time = finder.GetProcessedTime().ToString();
            csv.MaxNodes = finder.GetMaxExpandedNodes().ToString();
            Console.CursorTop -= 1;
            Console.CursorLeft = 0;
            Console.WriteLine($"{csv.Solution}-{csv.Time}ms");
            Console.ForegroundColor = ConsoleColor.White;
            return csv;
        }
        class TextWrapper : BaseTextWrapper
        {
            public string Alg { get; set; }
            public string Map { get; set; }
            public string MapType { get; set; }
            public string Heuristic { get; set; }
            public string Diagonal { get; set; }
            public string Solution { get; set; }
            public string Time { get; set; }
            public string MaxNodes { get; set; }
            public string PathLength { get; set; }
        }
        class TextGAWrapper : TextWrapper
        {
            public string Fitness { get; set; }
            public string Mutation { get; set; }
            public string Crossover { get; set; }
            public string Selection { get; set; }
            public string Generations { get; set; }
        }
        abstract class BaseTextWrapper
        {
            public string GetHeader()
            {
                var ret = new StringBuilder();
                var props = GetType().GetProperties();
                foreach (var item in props)
                {
                    ret.Append(item.Name);
                    ret.Append(";");
                }
                return ret.ToString() + "\n";
            }
            public override string ToString()
            {
                var ret = new StringBuilder();
                var type = GetType();
                var props = type.GetProperties();
                foreach (var item in props)
                {
                    var prop = type.GetProperty(item.Name);
                    ret.Append(prop.GetValue(this, null).ToString());
                    ret.Append(";");
                }
                return ret.ToString() + "\n";
            }
        }
        static void DrawTextProgressBar(int progress, int total, int barLength = 30, int left = 0, ConsoleColor color = ConsoleColor.Green)
        {
            if (total == 0)
                return;

            const char loadCchar = ' ';
            Console.CursorLeft = left;
            Console.Write("[");
            Console.CursorLeft = barLength + left + 1;
            Console.Write("]");
            Console.CursorLeft = 1 + left;
            var step = ((double)barLength / total);
            //draw filled part
            var position = 1;
            for (int i = 0; i < step * progress; i++)
            {
                Console.BackgroundColor = color;
                Console.CursorLeft = left + position++;
                Console.Write(loadCchar);
            }
            //draw unfilled part
            for (int i = position; i <= barLength; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = left + position++;
                Console.Write(loadCchar);
            }
            //draw totals
            Console.CursorLeft = left + barLength + 4;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress * 100 / total + "%    ");
        }
    }
}