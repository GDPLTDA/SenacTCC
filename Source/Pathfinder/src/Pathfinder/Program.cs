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

            var heu = settings.GetHeuristic();
            var finder = settings.GetFinder(heu);
            var generator = settings.GetGenerator();
            var viewer = settings.GetViewer(finder);

            var map = generator.ReadMap();
            viewer.Run(map);


        }


     

    }

        
}
