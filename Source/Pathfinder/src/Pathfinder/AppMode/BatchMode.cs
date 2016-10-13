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
            var folder = Path.Combine(setting.Batch_folder, $"batch_{setting.Width}x{setting.Height}_{setting.RandomSeed*100}_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}_{setting.AllowDiagonal}");
            var dataFile = Path.Combine(folder, "_data.csv");
            
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (setting.Batch_map_origin==0)
            {
                //generate maps
                var generator = MapGeneratorFactory.GetRandomMapGeneratorImplementation();
                for (int i = 0; i < qtdMaps; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Generating maps...");
                    drawTextProgressBar(i, qtdMaps);
                    var map = generator.DefineMap();
                    ft.SaveFileFromMap(map, Path.Combine(folder, i.ToString()+".txt"));
                }                

            }
            else
            {
                //if will load the map, use the configured root path
                folder = setting.Batch_folder;
            }

            var files = Directory.GetFiles(folder);
            var fileCount = files.Count();

            var finders =new int[]{0, 1 , 2, 3};
            var heuristics = new int[] { 0, 1, 2, 3 };
            var diagonals = new DiagonalMovement[]
            {
                DiagonalMovement.Never,
                DiagonalMovement.OnlyWhenNoObstacles,
                DiagonalMovement.IfAtMostOneObstacle,
                DiagonalMovement.Always
            };

            var console_color = Console.ForegroundColor;
            var csvFile = new StringBuilder();

           
                Console.Clear();
                csvFile.Append(new TextWrapper().GetHeader());
                for (int i = 0; i < fileCount; i++)
                {
                    
                    var map = ft.ReadMapFromFile(files[i]);

                    
                    //Console.WriteLine(ft.GetTextRepresentation(map));

                    foreach (var _finder in finders)
                        foreach (var _h in heuristics)
                        // foreach (var diag in diagonals)
                        {
                                var csv = new TextWrapper();
                                var h = setting.GetHeuristic(_h);
                                var finder = setting.GetFinder(h, _finder);
                                // finder.DiagonalMovement = diag;
                                csv.map = i.ToString() ;
                                csv.alg = finder.Name;
                                csv.heuristic = h.GetType().Name;
                                csv.diagonal= finder.DiagonalMovement.ToString();


                                Console.CursorLeft = 0;
                                if (Console.CursorTop > 0)
                                {
                                    Console.Write(new string(' ', 80));
                                    Console.CursorLeft = 0;
                                }

                               
                                Console.WriteLine($"    ({i}){csv.alg} - { csv.heuristic } - {csv.diagonal}");
                                drawTextProgressBar(i, fileCount);
                                

                                if (finder.Find(map))
                                {
                                    csv.solution = "Yes";
                                    csv.pathLength = finder.GetPath().OrderBy(x => x.G).Last().G.ToString();
                                    Console.ForegroundColor = ConsoleColor.Green;
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
                                Console.WriteLine(" "+csv.solution);
                                Console.ForegroundColor = console_color;

                                csvFile.Append(csv.ToString());
                            
                            }
                }
            

            
            drawTextProgressBar(fileCount, fileCount);

            File.WriteAllText(dataFile, csvFile.ToString());
            Console.WriteLine("\n\nComplete...");
            Console.ReadKey();
        }

        class TextWrapper
        {
            public string alg { get; set; }
            public string map { get; set; }
            public string heuristic { get; set; }
            public string diagonal { get; set; }
            public string solution { get; set; }
            public string time { get; set; }
            public string maxNodes { get; set; }
            public string pathLength { get; set; }

            public string GetHeader() {
                var ret = new StringBuilder();

                var props = typeof(TextWrapper).GetProperties();

                foreach (var item in props)
                {
                    ret.Append(item.Name);
                    ret.Append(";");
                }

                return ret.ToString()+"\n";
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

                return ret.ToString()+"\n";
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
