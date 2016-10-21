﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Abstraction;
using static System.Console;
using Pathfinder.Constants;

namespace Pathfinder.AppMode
{
    public class DynamicMode : IAppMode
    {
        public void Run()
        {
            var settings = Program.settings;
            int v = ReadInput(@"Select viewer( 0=Console 1=OpenGL ):", 0,1 );
            int m = ReadInput(@"Select map origin( 0=Static 1=FromFile 2=Random ):",0, 2);            
            int h = ReadInput(@"Select Heuristic ( 0=Manhatam 1=Euclidean 2=Octile 3=Chebyshev ):", 0, 3);
            int d = ReadInput(@"Allow Diagonal? ( 0=Never 1=OnlyWhenNoObstacles 2=IfAtMostOneObstacle 3=Always ):",0, 3);
            int pf = ReadInput(@"Select algorithm ( 0=AStar 1=BFS 2=Dijkstra 3=IDA* 4=GA ):",0, 4);

            string mapGenArgs=string.Empty;
            if (m==2)
            {
                int mapWidth = ReadInput(@"Map width:", 1, 300);
                int mapHeight = ReadInput(@"Map height:", 1, 300);
                int mapGranu = ReadInput(@"Map wall percent:", 1, 100);
                int minPath = ReadInput(@"Min path length:", 1, 100);

                mapGenArgs = $"{mapWidth}|{mapHeight}|{mapGranu}|{minPath}";
                if (v==1)
                {
                    int blocksize= ReadInput(@"block size in px:", 1, 100);
                    settings.OpenGlBlockSize = blocksize;
                }
            }

            settings.AllowDiagonal = (DiagonalMovement)d;
            var heuristic = settings.GetHeuristic(h);
            var finder = settings.GetFinder(heuristic,pf);
            var generator = settings.GetGenerator(m);
            var viewer = settings.GetViewer(finder,v);

            viewer.Run(generator.DefineMap(mapGenArgs));

        }

        

        public int ReadInput(string message, int min, int max)
        {
            int ret=-1;

            while(ret < min || ret >max)
            {
                Write(message);
                if (!int.TryParse((ReadLine()).ToString(), out ret))
                    ret = -1;
                WriteLine();
            }


            return ret;
        }


    }
}