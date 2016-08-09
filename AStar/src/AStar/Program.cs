﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;


namespace AStar
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class Program
    {
        private bool[,] map;
        private SearchParameters searchParameters;

        static void Main(string[] args)
        {
            var program = new Program();
            
            //seta enconding correto no programa, bug do core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            program.Run();
        }

        /// <summary>
        /// Outputs three examples of path finding to the Console.
        /// </summary>
        /// <remarks>The examples have copied from the unit tests!</remarks>
        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            InitializeMap();
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a direct path without obstacles:", path);
            Console.WriteLine();

            // Now add an obstacle
            InitializeMap();
            AddWallWithGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a route around the obstacle:", path);
            Console.WriteLine();

            // Finally, create a barrier between the start and end points
            InitializeMap();
            AddWallWithoutGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should not be able to find a route around the barrier:", path);
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                InitializeMap();
                Thread.Sleep(1000);
                AddRandomWall();
                pathFinder = new PathFinder(searchParameters);
                path = pathFinder.FindPath();
                ShowRoute("Random map", path);
            }
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        /// <summary>
        /// Displays the map and path as a simple grid to the console
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private void ShowRoute(string title, IEnumerable<Point> path)
        {
            Console.WriteLine("{0}\r\n", title);
            for (int y = this.map.GetLength(1) - 1; y >= 0 ; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (this.searchParameters.StartLocation.Equals(new Point(x, y)))
                        // Show the start position
                        Console.Write('S');
                    else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Console.Write('F');
                    else if (this.map[x, y] == false)
                        // Show any barriers
                        Console.Write('#');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('.');
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private void InitializeMap()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[7, 5];
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 7; x++)
                    map[x, y] = true;

            var startLocation = new Point(1, 2);
            var endLocation = new Point(5, 2);
            this.searchParameters = new SearchParameters(startLocation, endLocation, map);
        }

        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;
        }


        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private void AddRandomWall()
        {
            var h = map.GetLength(0);
            var w = map.GetLength(1);

            var r = new Random();

            var qtWalls = r.Next((int) ( (h * w)/3*2) );

            for (int i = 0; i < qtWalls; i++)
            {
                map[r.Next(h), r.Next(w)] =  false;
            }

            var startLocation = new Point(r.Next(h), r.Next(w));
            var endLocation = new Point(r.Next(h), r.Next(w));
            this.searchParameters = new SearchParameters(startLocation, endLocation, map);
        }

            
        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }
    }
}
