using Pathfinder.Abstraction;
using Pathfinder.Constants;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinder.AppMode
{
    public class BatchMode : IAppMode
    {
        public void Run()
        {
            var setting = Program.settings;
            var ft = new FileTool();
            setting.IDATrackRecursion = false;
            var qtdMaps = setting.Batch_map_qtd_to_generate;
            var now = DateTime.Now;
            var folder = Path.Combine(setting.Batch_folder, $"batch_{setting.Width}x{setting.Height}_{setting.RandomSeed * 100}_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}");
            var dataFile = Path.Combine(folder, "_data.csv");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            var diags = Enum.GetValues(typeof(DiagonalMovement));
            var diagonals = new DiagonalMovement[diags.Length];
            for (int i = 0; i < diags.Length; i++)
                diagonals[i] = (DiagonalMovement)diags.GetValue(i);


            var divblock = Math.Ceiling((double)qtdMaps / (double)diagonals.Count());
            var diagIndex = 0;

            if (setting.Batch_map_origin == 0)
            {
                //generate maps
                var generator =setting.Batch_generate_pattern==0 ? 
                                    MapGeneratorFactory.GetRandomMapGeneratorImplementation() :
                                    MapGeneratorFactory.GetStandardMapGeneratorImplementation() ;

                for (int i = 0; i < qtdMaps; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Generating maps...");
                    drawTextProgressBar(i, qtdMaps);
                    var map = generator.DefineMap();
                    map.AllowDiagonal = diagonals[diagIndex];
                    ft.SaveFileFromMap(map, Path.Combine(folder, i.ToString() + ".txt"));

                    if ((i + 1) % divblock == 0)
                    {
                        diagIndex++;
                    }
                }

            }
            else
            {
                //if will load the map, use the configured root path
                folder = setting.Batch_folder;
            }

            var files = Directory.GetFiles(folder);
            var fileCount = files.Count();

            var finders = new int[] { 0, 1, 2, 3 };
            var heuristics = new int[] { 0, 1, 2, 3 };

            var Mutation = new int[] { 0, 1, 2, 3, 4, 5 };
            var Crossover = new int[] { 0, 1, 2 };
            var Fitness = new int[] { 0 };
            var Selection = new int[] { 0 };

            var csvFile = new StringBuilder();
            var csvGAFile = new StringBuilder();

            Console.Clear();
            csvFile.Append(new TextWrapper().GetHeader());
            csvGAFile.Append(new TextGAWrapper().GetHeader());

            for (int i = 0; i < fileCount; i++)
            {

                var map = ft.ReadMapFromFile(files[i]);


                foreach (var _finder in finders)
                {

                    foreach (var _h in heuristics)
                    {
                       
                        var h = setting.GetHeuristic(_h);
                        var finder = setting.GetFinder(h, _finder);

                        if (finder is IGeneticAlgorithm) {
                            


                        } else {
                            var csv = RunStep(i,fileCount,map,h,finder);
                            csvFile.Append(csv.ToString());
                        }



                    }
                }
            }



            drawTextProgressBar(fileCount, fileCount);

            File.WriteAllText(dataFile, csvFile.ToString());
            Console.WriteLine("\n\nComplete...");
            Console.ReadKey();
        }


        private TextWrapper RunStep(int i, int fileCount, IMap map, IHeuristic h, IFinder finder)
        {
            var csv = new TextWrapper();
            csv.map = i.ToString();
            csv.alg = finder.Name;
            csv.heuristic = h.GetType().Name;
            csv.diagonal = map.AllowDiagonal.HasValue ? map.AllowDiagonal.Value.ToString() : finder.DiagonalMovement.ToString();
            Console.CursorLeft = 0;
            if (Console.CursorTop > 0)
            {
                Console.Write(new string(' ', 80));
                Console.CursorLeft = 0;
            }


            Console.WriteLine($"            ({i}) {csv.alg} - { csv.heuristic } - {csv.diagonal}");
            drawTextProgressBar(i, fileCount);


            if (finder.Find(map))
            {

                csv.pathLength = finder.GetPath().OrderBy(x => x.G).Last().G.ToString();
                Console.ForegroundColor = ConsoleColor.Green;
                csv.solution = "Yes (" + finder.GetProcessedTime().ToString() + "ms )";

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
            Console.WriteLine(" " + csv.solution);
            Console.ForegroundColor = ConsoleColor.White;

            return csv;
        }

        class TextWrapper : BaseTextWrapper
        {
            public string alg { get; set; }
            public string map { get; set; }
            public string heuristic { get; set; }
            public string diagonal { get; set; }
            public string solution { get; set; }
            public string time { get; set; }
            public string maxNodes { get; set; }
            public string pathLength { get; set; }
        }

        class TextGAWrapper : BaseTextWrapper
        {
            public string map { get; set; }
            public string diagonal { get; set; }
            public string fitness { get; set; }
            public string Mutation { get; set; }
            public string Crossover { get; set; }
            public string Selection { get; set; }
            public string Generations { get; set; }
            public string solution { get; set; }
            public string time { get; set; }
            public string maxNodes { get; set; }
            public string pathLength { get; set; }
        }



        public abstract class BaseTextWrapper
        {

            public string GetHeader()
            {
                var ret = new StringBuilder();

                var props = typeof(TextWrapper).GetProperties();

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
            char loadCchar = ' ';
            Console.CursorLeft = left;
            Console.Write("[");
            Console.CursorLeft = barLength + left + 1;
            Console.Write("]");
            Console.CursorLeft = 1 + left;
            var step = ((double)barLength / total);

            //draw filled part
            int position = 1;
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
