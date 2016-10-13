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
        public static Settings settings;
        public static void Main(string[] args)
        {

            settings = new Settings();
            var app = settings.GetAppMode();
            app.Run();
            
        }    
    }
}
