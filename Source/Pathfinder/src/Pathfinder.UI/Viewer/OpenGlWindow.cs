﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using Pathfinder.Abstraction;
using Pathfinder.UI.Abstraction;

namespace Pathfinder.UI.Viewer
{
    public class OpenGlWindow : GameWindow
    {
        
        int width, heght;
        int BlockSize;
        int FPS = 60;
        public bool drawPath = false;
        public List<Node> path;
        IFinder _finder;
        IMap GridMap;
        Thread finderThread;

        public OpenGlWindow(IMap map, IFinder finder, int blocksize)
             : base(map.Width * blocksize, map.Height * blocksize)
        {
            Title = "PathFinding";
            
            BlockSize = blocksize;
            width = map.Width * BlockSize;
            heght = map.Height * BlockSize;
            _finder = finder;

            _finder.Step += LoopWraper;
            _finder.End += EndWraper;
            _finder.Start += StartWraper;

            GridMap = map;

            finderThread = new Thread(e => { _finder.Find(map); });
            finderThread.Start();
        }

        private void LoopWraper(object sender, EventArgs _e)
        {
            
            Console.Clear();
            var e = (FinderEventArgs)_e;
            AbstractViewer.ShowStepLog(_finder, e);

            Thread.Sleep(_finder.SleepUITimeInMs);
        }

        private void EndWraper(object sender, EventArgs _e)
        {
            Console.Clear();
            var e = (FinderEventArgs)_e;
            if (e.Finded)
            {
                path = _finder.GetPath();
                drawPath = true;
            }
                      
            AbstractViewer.ShowEndLog(_finder,path, e);

        }

        private void StartWraper(object sender, EventArgs e)
        {
           
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, width, 0.0, heght, -2.0, 2.0);

            base.OnLoad(e);
        }


        public void DrawMap(IMap map, bool showPath = false)
        {
            for (int i = 0; i < map.Height; i++)
                for (int j = 0; j < map.Width; j++)
                {
                    var node = map[i, j];
                    Color c = Color.White;

                    if (node == map.StartNode)
                        c = Color.Green;
                    else if (node == map.EndNode)
                        c = Color.Red;
                    else if (showPath && path.Exists(o=>o.Equals(node)))
                        c = Color.Yellow;
                    else if (!node.Walkable)
                        c = Color.DarkGray;
                    else if (_finder.isClosed(node))
                        c = Color.LightGreen;
                    else if (_finder.isOpen(node))
                        c = Color.LightBlue;


                    DrawBlock(node.X * BlockSize, node.Y * BlockSize, BlockSize, c);
                }
        }

        void DrawBlock(int tX, int tY, int tS, Color tC)
        {
            tY = heght - tY;
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(tC);
            GL.Vertex2(tX, tY);
            GL.Vertex2(tX + tS, tY);
            GL.Vertex2(tX + tS, tY - tS);
            GL.Vertex2(tX, tY - tS);
            GL.End();
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

        int tx = 0, ty = 0;
        void DrawGrid()
        {
            for (float i = 0; i < heght; i += BlockSize)
                DrawLine(0, i, width, i, Color.Gray);

            for (float i = 0; i < width; i += BlockSize)
                DrawLine(i, 0, i, heght, Color.Gray);

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
            DrawMap(GridMap, drawPath);
            DrawGrid();
            base.OnRenderFrame(e);
            SwapBuffers();
            Thread.Sleep(1000 / FPS);
        }


    }
}