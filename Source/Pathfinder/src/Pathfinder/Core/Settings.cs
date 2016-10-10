using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class Settings
    {
        public IConfigurationRoot Configuration { get; set; }
        public Settings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


            Start = Configuration[nameof(Start)].ToString()[0];
            End = Configuration[nameof(End)].ToString()[0];
            Empty = Configuration[nameof(Empty)].ToString()[0];
            Wall = Configuration[nameof(Wall)].ToString()[0];
            Path = Configuration[nameof(Path)].ToString()[0];
            Opened = Configuration[nameof(Opened)].ToString()[0];
            Closed = Configuration[nameof(Closed)].ToString()[0];
            OpenGlBlockSize = int.Parse(Configuration[nameof(OpenGlBlockSize)]);

            MapViwer = int.Parse(Configuration[nameof(MapViwer)]);
            MapOrigin = int.Parse(Configuration[nameof(MapOrigin)]);
            Heuristic = int.Parse(Configuration[nameof(Heuristic)]);
            Algorithn = int.Parse(Configuration[nameof(Algorithn)]);
            AllowDiagonal = (Constants.DiagonalMovement) int.Parse(Configuration[nameof(AllowDiagonal)]);
        }

        public char Start { get; set; }
        public char End { get; set; }
        public char Empty { get; set; }
        public char Wall { get; set; }
        public char Path { get; set; }
        public char Opened { get; set; }
        public char Closed { get; set; }
        public int OpenGlBlockSize { get; set; }

        public int MapViwer { get; set; }
        public int MapOrigin { get; set; }
        public int Heuristic { get; set; }
        public int Algorithn { get; set; }
        public Constants.DiagonalMovement AllowDiagonal { get; set; }

    }
}
