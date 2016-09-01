using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TCC.AStar;
using TCC.Core;

namespace TCC.Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            //seta enconding correto no programa, bug do core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var WallConfig1 = WallSimple();
            Run(WallConfig1, "The algorithm should find a direct path without obstacles:");

            var WallConfig4 = WallWithGap();
            Run(WallConfig4, "Lendo o arquivo:");
        }
        static void Run(SeachParameters tParams, string Msg)
        {
            var pathFinder = new ASPathFinder(tParams);
            var path = pathFinder.FindPath();
            ShowRoute(Msg, path, tParams);
        }
        static bool[,] GetMap()
        {
            int w = 30, h = 20;
            var map = new bool[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    map[x, y] = true;

            return map;
        }
        static void ShowRoute(string title, IEnumerable<Coordinate> path, SeachParameters searchParameters)
        {
            Console.WriteLine("{0}\r\n", title);
            bool[,] map = searchParameters.Map;

            for (int y = 0; y < map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (searchParameters.LocationStart.X == x && searchParameters.LocationStart.Y == y)
                        // Show the start position
                        Console.Write(MapSymbol.Start);
                    else if (searchParameters.LocationEnd.X == x && searchParameters.LocationEnd.Y == y)
                        // Show the end position
                        Console.Write(MapSymbol.End);
                    else if (!map[x, y])
                        // Show any barriers
                        Console.Write(MapSymbol.Block);
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write(MapSymbol.Step);
                    else
                        // Show nodes that aren't part of the path
                        Console.Write(MapSymbol.Empty);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        static SeachParameters WallSimple()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            var map = GetMap();

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
            var map = GetMap();
            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[4, 1] = false;
            var startLocation = new Coordinate(1, 2);
            var endLocation = new Coordinate(9, 2);
            return new SeachParameters(startLocation, endLocation, map);
        }
        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        static SeachParameters WallRandom()
        {
            var map = GetMap();
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
            var map = GetMap();
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
            var map = GetMap();
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
                        map[x, y] = false;
                    x++;
                }
            }
            return new SeachParameters(startLocation, endLocation, map);
        }
    }
}
