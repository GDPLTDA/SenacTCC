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
        public IMap DefineMap(string argument, DiagonalMovement? diagonal = null)
        {
            var ft = new FileTool();
            
            if (string.IsNullOrEmpty(argument))
                argument = Settings.FileToLoad;

            var map = FileTool.ReadMapFromFile(argument);

            
            return map;
            
        }
    }
}