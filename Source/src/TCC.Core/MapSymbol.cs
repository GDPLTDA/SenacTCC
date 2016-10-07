using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public static class MapSymbol
    {
        public static char Start { get; set; } = 'S';
        public static char End { get; set; } = 'E';
        public static char Empty { get; set; } = '-';
        public static char Block { get; set; } = '#';
        public static char Step { get; set; } = '*';

        static SeachParameters MapFile(string tFile)
        {
            var listblock = new List<Coordinate>();
            int x = 0, y = 0;
            byte Dig;
            var startLocation = new Coordinate(0, 0);
            var endLocation = new Coordinate(0, 0);

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

                    if (cDig == MapSymbol.Start)
                        startLocation = new Coordinate(x, y);

                    if (cDig == MapSymbol.End)
                        endLocation = new Coordinate(x, y);

                    if (cDig == MapSymbol.Block)
                        listblock.Add(new Coordinate(x, y));
                    x++;
                }
                y++;
            }
            var map = JJFunc.GetMap(x, y);

            foreach (var item in listblock)
            {
                map[item.Xi, item.Yi] = false;
            }
            return new SeachParameters(startLocation, endLocation, map);
        }
    }

    
}
