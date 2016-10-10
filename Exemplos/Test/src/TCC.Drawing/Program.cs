using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using TCC.Drawing;

namespace TCC
{
    static class Program
    {
        static MapWindow Window;
        static void Main(string[] args)
        {
            // Mapa criado por código
            //Window = new MapWindow(new MapGenerate(10,10, WallWithGap()), 30);
            // Mapa Aleatorio
            Window = new MapWindow(new MapGenerate(20, 20, 0.3), 5);
            // Mapa carregador por um arquivo
            //Window = new MapWindow(new MapGenerate("test.txt"), 30);

            Window.UpdateFrame += new EventHandler<FrameEventArgs>(UpdateMap);
            Window.Run();
        }

        public static void UpdateMap(object o, FrameEventArgs e)
        {
            
        }
        static List<Point2D> WallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            var map = new List<Point2D>();
            map.Add(new Point2D(3, 0, Color.Gray));
            map.Add(new Point2D(3, 1, Color.Gray));
            map.Add(new Point2D(3, 2, Color.Gray));
            map.Add(new Point2D(3, 3, Color.Gray));
            map.Add(new Point2D(4, 3, Color.Gray));

            map.Add(new Point2D(1, 2, Color.Red));
            map.Add(new Point2D(5, 2, Color.Blue));

            return map;
        }
    }
}
