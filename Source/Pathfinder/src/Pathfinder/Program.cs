using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Pathfinder.Factories;
using Pathfinder.Abstraction;

namespace Pathfinder
{
    public class Program
    {
        static Settings settings;
        public static void Main(string[] args)
        {
            settings = new Settings();

            var heuristic = settings.GetHeuristic();
            var finder    = settings.GetFinder(heuristic);
            var generator = settings.GetGenerator();
            var viewer    = settings.GetViewer(finder);

            var map =  generator.DefineMap();
            viewer.Run(map);
            
        }    

    }

        
}
