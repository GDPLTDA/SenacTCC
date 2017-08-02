using Pathfinder.Abstraction;
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



