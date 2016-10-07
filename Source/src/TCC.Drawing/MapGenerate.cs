using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Drawing
{
    public class MapGenerate
    {
        public MapGenerate(string tFile)
        {
            MapFile(tFile);
        }

        public List<Point2D> GridMap { get; set; }
        public int Width { get; set; }
        public int Heght { get; set; }
        char Start { get; set; } = 'S';
        char End { get; set; } = 'E';
        char Empty { get; set; } = '-';
        char Block { get; set; } = '#';
        char Step { get; set; } = '*';

        void MapFile(string tFile)
        {
            GridMap = new List<Point2D>();
            int x = 0, y = 0;
            byte Dig;

            using (FileStream oFileStream = new FileStream(tFile, FileMode.Open))
            {
                BinaryReader oReader = new BinaryReader(oFileStream);

                while (!(oReader.BaseStream.Position == oReader.BaseStream.Length))
                {
                    Dig = oReader.ReadByte();

                    var cDig = (char)Dig;

                    if (Dig == 13)
                        continue;

                    if (Dig == 10)
                    {
                        y++;
                        x = 0;
                        continue;
                    }

                    if (cDig == Start)
                        GridMap.Add(new Point2D(x, y, Color.Red));

                    if (cDig == End)
                        GridMap.Add(new Point2D(x, y, Color.Blue));

                    if (cDig == Block)
                        GridMap.Add(new Point2D(x, y, Color.Gray));

                    if (cDig == Empty)
                        GridMap.Add(new Point2D(x, y, Color.White));
                    x++;
                }
                y++;
            }
            Width = x;
            Heght = y;
        }
    }
}

