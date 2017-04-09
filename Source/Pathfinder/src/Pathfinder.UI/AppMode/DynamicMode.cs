﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Abstraction;
using static System.Console;
using Pathfinder.Factories;
using Pathfinder.UI.Abstraction;
namespace Pathfinder.UI.AppMode
{
    public class DynamicMode : IAppMode
    {
        public void Run()
        {
            int v = ReadInput(@"Select viewer( 0=Console 1=OpenGL ):", 0,1 );
            int m = ReadInput(@"Select map origin( 0=Static 1=FromFile 2=Random ):",0, 2);
            int h = ReadInput(@"Select Heuristic ( 0=Manhatam 1=Euclidean 2=Octile 3=Chebyshev ):", 0, 3);
            int d = ReadInput(@"Allow Diagonal? ( 0=Never 1=OnlyWhenNoObstacles 2=IfAtMostOneObstacle 3=Always ):",0, 3);
            int pf = ReadInput(@"Select algorithm ( 0=AStar 1=BFS 2=Dijkstra 3=IDA* 4=GA ):",0, 4);
            if (pf == 4)
            {
                GASettings.GenerationLimit = ReadInput(@"Select generation limit:", 0, 10000);
                GASettings.MutationRate = ReadInput(@"Select mutation rate percent:", 0, 100) / 100;
                GASettings.MutationAlgorithm = (MutateEnum)ReadInput(@"Select algorithm mutation ( 0=Simple 1=DIVM 2=DM 3=IM 4=IVM 5=SM ):", 0, 5);
                GASettings.CrossoverRate = ReadInput(@"Select crossover rate percent:", 0, 100) / 100;
                GASettings.CrossoverAlgorithm = (CrossoverEnum)ReadInput(@"Select algorithm crossover ( 0=Simple 1=OBX 2=PBX ):", 0, 2);
                GASettings.PopulationSize = ReadInput(@"Select population size:", 0, 10000);
                GASettings.FitnessAlgorithm = (FitnessEnum)ReadInput(@"Select algorithm fitness ( 0=Heuristic):", 0, 0);
                GASettings.SelectionAlgorithm = (SelectionEnum)ReadInput(@"Select algorithm selection ( 0=Simple):", 0, 0);
                GASettings.BestSolutionToPick = ReadInput(@"Select count best solution:", 0, GASettings.PopulationSize);
            }
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
                    Settings.OpenGlBlockSize = blocksize;
                }
            }
            Settings.AllowDiagonal = (DiagonalMovement)d;
            var heuristic = Container.Resolve<IHeuristic>(h);
            var finder = Container.Resolve<IFinder>(pf);
            finder.Heuristic = heuristic;
            var generator = Container.Resolve<IMapGenerator>(m);
            var viewer = Container.Resolve<IViewer>(v);
            viewer.SetFinder(finder);
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
