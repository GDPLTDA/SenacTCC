using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;

namespace MapWindow
{
    public class MapWindow : GameWindow
    {
        int width, heght;
        int blocksize = 20;
        int FPS = 2;

        public MapWindow(int twidth,int theght)
            : base(twidth, theght)
        {
            width = twidth;
            heght = theght;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //GL.Disable(EnableCap.Texture2D);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, width, 0.0, heght, -2.0, 2.0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        void drawline(float x1, float y1, float x2, float y2, Color c)
        {
            GL.LineWidth(1.0f);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(c);
            GL.Vertex2(x1, y1);
            GL.Vertex2(x2, y2);
            GL.End();
        }

        void drawblock(int x, int y, int s, Color c)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(c);
            GL.Vertex2(x, y);
            GL.Vertex2(x+s, y);
            GL.Vertex2(x+s, y+s);
            GL.Vertex2(x, y+s);
            GL.End();
        }
        int tx = 0, ty = 0;
        void drawgrid()
        {
            //GL.ClearColor(Color.White);

            tx += 10;
            ty += 10;

            for (int i = 0; i < width; i+=blocksize)
            {
                for (int j = 0; j < heght; j+= blocksize)
                {
                    if(i == tx && j == ty)
                        drawblock(i, j, blocksize, Color.Red);
                    else
                        drawblock(i, j, blocksize, Color.White);
                }
            }

            for (float i = 0; i < heght; i += blocksize)
                drawline(0, i, width, i, Color.Gray);

            for (float i = 0; i < width; i += blocksize)
                drawline(i, 0, i, heght, Color.Gray);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            drawgrid();

            

            Thread.Sleep(1000 / FPS);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            SwapBuffers();
        }
    }
}
