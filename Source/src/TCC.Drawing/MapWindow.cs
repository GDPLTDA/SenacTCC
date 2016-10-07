using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;

namespace TCC.Drawing
{
    public class MapWindow : GameWindow
    {
        int width, heght;
        int BlockSize;
        int FPS = 2;
        List<Point2D> GridMap;

        /// <summary>
        /// Cria uma tela divida em blocos
        /// </summary>
        /// <param name="twidth">Larga em quantidade de blocos</param>
        /// <param name="theght">Altura em quantidade de blocos</param>
        /// <param name="tBlockSize">tamanho dos blocos</param>
        public MapWindow(int twidth, int theght, int tBlockSize)
            : base(twidth * tBlockSize, theght * tBlockSize)
        {
            width = twidth * tBlockSize;
            heght = theght * tBlockSize;
            BlockSize = tBlockSize;
            Title = "TCC";
        }
        public MapWindow(MapGenerate tMap, int tBlockSize)
            :base(tMap.Width * tBlockSize, tMap.Heght * tBlockSize)
        {
            width = tMap.Width * tBlockSize;
            heght = tMap.Heght * tBlockSize;
            BlockSize = tBlockSize;
            Title = "TCC";
            GridMap = tMap.GridMap;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, width, 0.0, heght, -2.0, 2.0);

            base.OnLoad(e);
        }

        public void SetMap(List<Point2D> tMap)
        {
            GridMap = tMap;
        }

        public void DrawMap(List<Point2D> tMap)
        {
            foreach (var item in tMap)
                DrawBlock(item.X * BlockSize, item.Y * BlockSize, BlockSize, item.Color);
        }

        void DrawLine(float tX1, float tY1, float tX2, float tY2, Color tC)
        {
            tY1 = heght - tY1;
            tY2 = heght - tY2;

            GL.LineWidth(1.0f);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(tC);
            GL.Vertex2(tX1, tY1);
            GL.Vertex2(tX2, tY2);
            GL.End();
        }
        void DrawBlock(int tX, int tY, int tS, Color tC)
        {
            tY = heght - tY;
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(tC);
            GL.Vertex2(tX, tY);
            GL.Vertex2(tX+tS, tY);
            GL.Vertex2(tX+tS, tY-tS);
            GL.Vertex2(tX, tY-tS);
            GL.End();
        }
        int tx = 0, ty = 0;
        void DrawGrid()
        {
            for (float i = 0; i < heght; i += BlockSize)
                DrawLine(0, i, width, i, Color.LightGray);

            for (float i = 0; i < width; i += BlockSize)
                DrawLine(i, 0, i, heght, Color.LightGray);

            tx++;
            ty++;
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            base.OnUpdateFrame(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawGrid();
            DrawMap(GridMap);
            base.OnRenderFrame(e);
            SwapBuffers();
            Thread.Sleep(1000 / FPS);
        }
    }
}
