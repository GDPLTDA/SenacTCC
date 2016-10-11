using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.MapGenerators
{
    public class FileMapGenerator : IMapGenerator
    {
        public IMap DefineMap(string argument)
        {
            var ft = new FileTool();
            var set = new Settings();
            
            if (string.IsNullOrEmpty(argument))
                argument = set.FileToLoad;

            var map = ft.ReadMapFromFile(argument);

            
            return map;
            
        }
    }
}