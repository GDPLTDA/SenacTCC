using Pathfinder.UI.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Pathfinder.Abstraction;
namespace Pathfinder.UI.AppMode
{
    public class BatchMode : IAppMode
    {
        MapGeneratorEnum mapType;
        public void Run()
        {
            var ft = new FileTool();
            Settings.IDATrackRecursion = false;
            Settings.AutoSaveMaps = false;
            var qtdMaps = UISettings.Batch_map_qtd_to_generate;
            var now = DateTime.Now;
            var folder = Path.Combine(UISettings.Batch_folder, $"batch_{Settings.Width}x{Settings.Height}_{Settings.RandomSeed * 100}_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}");
            var dataFile = Path.Combine(folder, "_data.csv");
            var dataFileGA = Path.Combine(folder, "_dataGA.csv");
            var diags = Enum.GetValues(typeof(DiagonalMovement));
            var diagonals = new DiagonalMovement[diags.Length];
            for (int i = 0; i < diags.Length; i++)
                diagonals[i] = (DiagonalMovement)diags.GetValue(i);
            var divblock = Math.Ceiling((double)qtdMaps / (double)diagonals.Count());
            if (UISettings.Batch_map_origin == 0)
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                //generate maps
                mapType = UISettings.Batch_generate_pattern == 0 ? MapGeneratorEnum.Random : MapGeneratorEnum.WithPattern;
                var generator = Container.Resolve<IMapGenerator>((int)mapType);
                for (int i = 0; i < qtdMaps; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Generating maps...");
                    drawTextProgressBar(i, qtdMaps);
                    var map = generator.DefineMap(diagonal: diagonals[UISettings.Batch_map_diagonal]);
                    map.AllowDiagonal = diagonals[UISettings.Batch_map_diagonal];
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
            var files = Directory.GetFiles(folder);
            var fileCount = files.Count();
            var finders = UISettings.Batch_list_finders;
            var heuristics = UISettings.Batch_list_heuristics;
            var Mutation = UISettings.Batch_list_Mutation;
            var Crossover = UISettings.Batch_list_Crossover;
            var Fitness = UISettings.Batch_list_Fitness;
            var Selection = UISettings.Batch_list_Selection ;
            var csvFile = new StreamWriter(File.Open(dataFile, FileMode.OpenOrCreate),Encoding.UTF8, 4096, true);
            var csvGAFile = new StreamWriter(File.Open(dataFileGA, FileMode.OpenOrCreate), Encoding.UTF8, 4096, true);
            Console.Clear();
            csvFile.Write(new TextWrapper().GetHeader());
            csvGAFile.Write( new TextGAWrapper().GetHeader());
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
                                                GAFinder.Crossover = Container.Resolve<ICrossover>(Crossover[cross]) ;
                                                GAFinder.Mutate    = Container.Resolve<IMutate>(Mutation[mut]);
                                                GAFinder.Fitness   = Container.Resolve<IFitness>(Fitness[fit]);
                                                GAFinder.Selection = Container.Resolve<ISelection>(Selection[sel]);
                                                var helper = $"\n                n:{j},cx:{GAFinder.Crossover.GetType().Name},m:{GAFinder.Mutate.GetType().Name},f:{GAFinder.Fitness.GetType().Name},s:{GAFinder.Selection.GetType().Name}";
                                                var csv = new TextWrapper();
                                                csv = RunStep(csv, i, fileCount, map, h, GAFinder, helper);
                                                var csvGA = new TextGAWrapper
                                                {
                                                    alg = csv.alg,
                                                    map = csv.map,
                                                    heuristic = csv.heuristic,
                                                    mapType = csv.mapType,
                                                    diagonal = csv.diagonal,
                                                    solution = csv.solution,
                                                    time = csv.time,
                                                    maxNodes = csv.maxNodes,
                                                    pathLength = csv.pathLength,
                                                    Crossover = GAFinder.Crossover.GetType().Name,
                                                    Mutation = GAFinder.Mutate.GetType().Name,
                                                    fitness = GAFinder.Fitness.GetType().Name,
                                                    Selection = GAFinder.Selection.GetType().Name,
                                                    Generations = GAFinder.Generations.ToString(),
                                                };
                                                csvGAFile.Write(csvGA.ToString());
                                            }
                        }
                        else
                        {
                            var csv = new TextWrapper();
                            csv = RunStep(csv, i, fileCount, map, h, finder);
                            csvFile.Write(csv.ToString());
                        }
                        csvFile.Flush();
                    }
                }
            }
            drawTextProgressBar(fileCount, fileCount);
            csvFile.Dispose();
            csvGAFile.Dispose();
            Console.WriteLine("\n\nComplete...");
            Console.ReadKey();
        }
        private TextWrapper RunStep(TextWrapper baseScv, int i, int fileCount, IMap map, IHeuristic h, IFinder finder, string plus = "")
        {
            var csv = baseScv;
            csv.map = i.ToString();
            csv.mapType = mapType.ToString();
            csv.alg = finder.Name;
            csv.heuristic = h.GetType().Name;
            csv.diagonal = map.AllowDiagonal.HasValue ? map.AllowDiagonal.Value.ToString() : finder.DiagonalMovement.ToString();
            Console.CursorLeft = 0;
            if (Console.CursorTop > 0)
            {
                Console.Write(new string(' ', 80));
                Console.CursorLeft = 0;
            }
            Console.WriteLine($"            ({i}) {csv.alg} - { csv.heuristic } - {csv.diagonal} ({plus})");
            drawTextProgressBar(i, fileCount);
            if (finder.Find(map))
            {
                csv.pathLength = finder.GetPath().OrderBy(x => x.G).Last().G.ToString();
                Console.ForegroundColor = ConsoleColor.Green;
                csv.solution = "Yes";
            }
            else
            {
                csv.solution = "No";
                csv.pathLength = "-1";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            csv.time = finder.GetProcessedTime().ToString();
            csv.maxNodes = finder.GetMaxExpandedNodes().ToString();
            Console.CursorTop -= 1;
            Console.CursorLeft = 0;
            Console.WriteLine($"{csv.solution}-{csv.time}ms");
            Console.ForegroundColor = ConsoleColor.White;
            return csv;
        }
        class TextWrapper : BaseTextWrapper
        {
            public string alg { get; set; }
            public string map { get; set; }
            public string mapType { get; set; }
            public string heuristic { get; set; }
            public string diagonal { get; set; }
            public string solution { get; set; }
            public string time { get; set; }
            public string maxNodes { get; set; }
            public string pathLength { get; set; }
        }
        class TextGAWrapper : TextWrapper
        {
            public string fitness { get; set; }
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
        private static void drawTextProgressBar(int progress, int total, int barLength = 30, int left = 0, ConsoleColor color = ConsoleColor.Green)
        {
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