using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using TCC.Core;

namespace TCC.AStar
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var program = new AStar();
            
            //seta enconding correto no programa, bug do core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var WallConfig1 = program.WallSimple();
            Run(WallConfig1, "The algorithm should find a direct path without obstacles:");

            var WallConfig4 = program.WallFile();
            Run(WallConfig4, "Lendo o arquivo:");
        }

        static void Run(ASSearchParameters tParams, string Msg)
        {
            var pathFinder = new ASPathFinder(tParams);
            var path = pathFinder.FindPath();
            ShowRoute(Msg, path, tParams);
        }

        static void ShowRoute(string title, IEnumerable<Coordinate> path, ASSearchParameters searchParameters)
        {
            Console.WriteLine("{0}\r\n", title);
            bool[,] map = searchParameters.Map;

            for (int y = 0; y < map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (searchParameters.StartLocation .X == x && searchParameters.StartLocation.Y == y)
                        // Show the start position
                        Console.Write('S');
                    else if (searchParameters.EndLocation.X == x && searchParameters.EndLocation.Y == y)
                        // Show the end position
                        Console.Write('E');
                    else if (!map[x, y])
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
            Console.ReadKey();

        }

    }
}
