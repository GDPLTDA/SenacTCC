using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TCC.Astar;
using TCC.Core;
using TCC.GAFindingPath;
using TCC.GeneticAlgorithm;

namespace TCC.OutPutConsole
{
    class Program
    {
        static void Main(string[] args) {
            //seta enconding correto no programa, bug do core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var WallGA = WallFile();
            var tGaParams = TesteGA(WallGA);

            RunGA(tGaParams, "Algoritmo Genetico:");

            var WallConfig4 = WallFile();
            Run(WallConfig4, "AStar");
        }
        static void Run(SeachParameters tParams, string Msg)
        {
            var pathFinder = new ASPathFinder(tParams);
            var path = pathFinder.FindPath();
            ShowRoute(Msg, path, tParams);
            Console.ReadKey();
        }

        static void RunGA(GAParams tGaParams, string Msg)
        {
            var run = true;
            var pathFinder = new GAFP(tGaParams);

            while (run)
            {
                // Cria uma nova população
                pathFinder.Epoch();
                var path = pathFinder.GetBestPath();

                // exibe o desenho do mapa
                Console.Clear();
                Console.WriteLine("Geracao:{0}\r\n", pathFinder.Generation);
                ShowRoute(Msg, path, tGaParams);
                
                // verifica se encontrou o destino
                var last = path.Last();
                var end = tGaParams.Params.LocEnd;
                run = !(last.Equals(end));
            }
        }
        static GAParams TesteGA(SeachParameters tSeach)
        {
            var map = tSeach.Map;
            var Retorno = new GAParams
            {
                MutationRate = 0.3,
                CrossoverRate = 0.4,
                PopulationSize = 100,
                MapWidth = map.GetLength(0),
                MapHeight = map.GetLength(1),
                Params = tSeach
            };

            return Retorno;
        }
        static void ShowRoute(string title, List<Coordinate> path, GAParams tGaParams)
        {
            ShowRoute(title, path, tGaParams.Params);
        }

        static void ShowRoute(string title, List<Coordinate> path, SeachParameters tSearch)
        {
            Console.WriteLine("{0}\r", title);
            bool[,] map = tSearch.Map;

            for (int y = 0; y < map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (tSearch.LocStart.Equals(x, y))
                        // Show the start position
                        Console.Write(MapSymbol.Start);
                    else if (tSearch.LocEnd.Equals(x, y))
                        // Show the end position
                        Console.Write(MapSymbol.End);
                    else if (!map[x, y])
                        // Show any barriers
                        Console.Write(MapSymbol.Block);
                    else if (path.Where(p => p.Equals(x, y)).Any())
                        // Show the path in between
                        Console.Write(MapSymbol.Step);
                    else
                        // Show nodes that aren't part of the path
                        Console.Write(MapSymbol.Empty);
                }
                Console.WriteLine();
            }
            System.Threading.Thread.Sleep(500);
        }
        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        static SeachParameters WallSimple()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ F □ □ □ S □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            var map = JJFunc.GetMap();

            var startLocation = new Coordinate(1, 2);
            var endLocation = new Coordinate(9, 2); 

            return new SeachParameters(startLocation, endLocation, map);
        }
        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        static SeachParameters WallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2
            var map = JJFunc.GetMap();
            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[4, 1] = false;
            var startLocation = new Coordinate(1, 2);
            var endLocation = new Coordinate(15, 15);
            return new SeachParameters(startLocation, endLocation, map);
        }
        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        static SeachParameters WallRandom()
        {
            var map = JJFunc.GetMap();
            var h = map.GetLength(0);
            var w = map.GetLength(1);

            var r = new Random();

            var qtWalls = r.Next((int)((h * w) / 3 * 2));

            for (int i = 0; i < qtWalls; i++)
            {
                map[r.Next(h), r.Next(w)] = false;
            }

            var startLocation = new Coordinate(r.Next(h), r.Next(w));
            var endLocation = new Coordinate(r.Next(h), r.Next(w));
            return new SeachParameters(startLocation, endLocation, map);
        }
        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        static SeachParameters WallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path
            var map = JJFunc.GetMap();
            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[3, 0] = false;
            var startLocation = new Coordinate(1, 2);
            var endLocation = new Coordinate(9, 2);
            return new SeachParameters(startLocation, endLocation, map);
        }
        static SeachParameters WallFile()
        {
            var listblock = new List<Coordinate>();
            int x = 0, y = 0;
            byte Dig;
            var startLocation = new Coordinate(0, 0);
            var endLocation = new Coordinate(0, 0);

            using (FileStream oFileStream = new FileStream(@"test.txt", FileMode.Open))
            {
                x = 0;
                BinaryReader oReader = new BinaryReader(oFileStream);

                while (!(oReader.BaseStream.Position == oReader.BaseStream.Length))
                {
                    Dig = oReader.ReadByte();

                    var cDig = (char)Dig;

                    if (Dig == 13)
                    {
                        Dig = oReader.ReadByte();
                    }

                    if (Dig == 10)
                    {
                        y++;
                        x = 0;
                    }

                    if (cDig == MapSymbol.Start)
                        startLocation = new Coordinate(x, y);

                    if (cDig == MapSymbol.End)
                        endLocation = new Coordinate(x, y);

                    if (cDig == MapSymbol.Block)
                        listblock.Add(new Coordinate(x, y));
                    x++;
                }
            }
            var map = JJFunc.GetMap(x, y + 1);

            foreach (var item in listblock)
            {
                map[item.Xi, item.Yi] = false;
            }
            return new SeachParameters(startLocation, endLocation, map);
        }
    }
}
