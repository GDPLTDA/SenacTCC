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
        public static Settings Settings;
        public static GASettings GASettings;
        public static void Main(string[] args)
        {

            Settings = new Settings();
            GASettings = new GASettings();
            var app = Settings.GetAppMode();
            app.Run();
            
        }    
    }
}
