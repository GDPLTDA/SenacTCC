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
        public List<Point2D> GridMap { get; set; }
        public int Width { get; set; }
        public int Heght { get; set; }
        /// <summary>
        /// Gera um mapa aleatorio
        /// </summary>
        /// <param name="tWidth">Largura em blocos</param>
        /// <param name="tHeght">Altura em blocos</param>
        /// <param name="Seed">% de barreiras</param>
        public MapGenerate(int tWidth, int tHeght, double Seed = 0.1)
        {
            Width = tWidth;
            Heght = tHeght;
            MapRandom(tWidth, tHeght, Seed);
        }
        /// <summary>
        /// Adiciona um mapa ja existente
        /// </summary>
        /// <param name="tWidth">Largura em blocos</param>
        /// <param name="tHeght">Altura em blocos</param>
        /// <param name="tList">Lista de pontos</param>
        public MapGenerate(int tWidth, int tHeght, List<Point2D> tList)
        {
            GridMap = tList;
            Width = tWidth;
            Heght = tHeght;
        }
        /// <summary>
        /// Gera o mapa apartir de um arquivo
        /// </summary>
        /// <param name="tFile">Caminho do arquivo</param>
        public MapGenerate(string tFile)
        {
            MapFile(tFile);
        }
        
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

        void MapRandom(int tWidth, int tHeght, double tSeed)
        {
            GridMap = new List<Point2D>();
            
            int size = Convert.ToInt32((tWidth * tHeght) * tSeed);
            AddPointsMap(size, tWidth, tHeght, Color.Gray);
            AddPointsMap(1, tWidth, tHeght, Color.Red);
            AddPointsMap(1, tWidth, tHeght, Color.Blue);
        }

        void AddPointsMap(int tSize, int tWidth, int tHeght, Color tCol)
        {
            Random Ran = new Random();
            while (tSize > 0)
            {
                int x = Ran.Next(0, tWidth);
                int y = Ran.Next(0, tHeght);
                var p = new Point2D(x, y, tCol);

                if (!GridMap.Exists(i => i.Equals(p)))
                {
                    GridMap.Add(p);
                    tSize--;
                }
            }
        }
    }
}

