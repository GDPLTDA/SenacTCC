using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;


namespace AStar
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    public class AStar
    {
        //private bool[,] map;
        //private SearchParameters searchParameters;

        /// <summary>
        /// Outputs three examples of path finding to the Console.
        /// </summary>
        /// <remarks>The examples have copied from the unit tests!</remarks>
        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            var WallConfig1 = WallSimple();
            PathFinder pathFinder = new PathFinder(WallConfig1);
            List<Point> path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a direct path without obstacles:", path, WallConfig1);

            // Now add an obstacle
            var WallConfig2 = WallWithGap();
            pathFinder = new PathFinder(WallConfig2);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a route around the obstacle:", path, WallConfig2);

            // Finally, create a barrier between the start and end points
            var WallConfig3 = WallWithoutGap();
            pathFinder = new PathFinder(WallConfig3);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should not be able to find a route around the barrier:", path, WallConfig3);

            var WallConfig4 = WallFile();
            pathFinder = new PathFinder(WallConfig4);
            path = pathFinder.FindPath();
            ShowRoute("Lendo o arquivo:", path, WallConfig4);

            //for (int i = 0; i < 5; i++)
            //{
            //    InitializeMap();
            //    Thread.Sleep(1000);
            //    pathFinder = new PathFinder(WallRandom(););
            //    path = pathFinder.FindPath();
            //    ShowRoute("Random map", path);
            //}
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        /// <summary>
        /// Displays the map and path as a simple grid to the console
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private void ShowRoute(string title, IEnumerable<Point> path, SearchParameters searchParameters)
        {
            Console.WriteLine("{0}\r\n", title);
            bool[,] map = searchParameters.Map;

            for (int y = 0; y < map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (searchParameters.StartLocation.Equals(new Point(x, y)))
                        // Show the start position
                        Console.Write('S');
                    else if (searchParameters.EndLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Console.Write('E');
                    else if (map[x, y] == false)
                        // Show any barriers
                        Console.Write('#');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('-');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool[,] GetMap()
        {
            int w = 30, h = 20;
            var map = new bool[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    map[x, y] = true;

            return map;
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private SearchParameters WallSimple()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            var map = GetMap();

            var startLocation = new Point(1, 2);
            var endLocation = new Point(9, 2);

            return new SearchParameters(startLocation, endLocation, map);
        }

        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private SearchParameters WallWithGap()
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
            var startLocation = new Point(1, 2);
            var endLocation = new Point(9, 2);
            return new SearchParameters(startLocation, endLocation, map);
        }


        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private SearchParameters WallRandom()
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

            var startLocation = new Point(r.Next(h), r.Next(w));
            var endLocation = new Point(r.Next(h), r.Next(w));
            return new SearchParameters(startLocation, endLocation, map);
        }


        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        private SearchParameters WallWithoutGap()
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
            var startLocation = new Point(1, 2);
            var endLocation = new Point(9, 2);
            return new SearchParameters(startLocation, endLocation, map);
        }

        private SearchParameters WallFile()
        {
            var map = GetMap();
            int x = 0, y = 0;
            byte Dig;
            var startLocation = new Point(0, 0);
            var endLocation = new Point(0, 0);

            using (FileStream oFileStream = new FileStream(@"c:\test.txt", FileMode.Open))
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

                    if (cDig == 'S')
                        startLocation = new Point(x, y);

                    if (cDig == 'E')
                        endLocation = new Point(x, y);

                    if (cDig == '#')
                        map[x, y] = false;
                    x++;
                }
            }
            
            return new SearchParameters(startLocation, endLocation, map);
        }

    }
}
