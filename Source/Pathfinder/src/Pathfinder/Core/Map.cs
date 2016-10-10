using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;

namespace Pathfinder
{
    public class Map : IMap
    {
        public Node[,] Nodes { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Node StartNode { get { return _startNode; }  set { DefineNode(value); _startNode = value; } }
        public Node EndNode   { get { return _endNode; }    set { DefineNode(value); _endNode = value; } }
        Node _startNode;
        Node _endNode;

        public Map(int width, int height)
        {
            Setup(width, height, 0, 0, width - 1, height - 1);
        }

        public Map(int width, int height, int startNodeX, int startNodeY, int endNodeX, int endNodeY)
        {
            Setup(width, height, startNodeX, startNodeY, endNodeX, endNodeY);
        }

        private void Setup(int width, int height, int startNodeX, int startNodeY, int endNodeX, int endNodeY)
        {
            Width = width;
            Height = height;
            Nodes = new Node[height, width];
            DefineAllNodes();
            StartNode = Nodes[startNodeY, startNodeX];
            EndNode = Nodes[endNodeY, endNodeX];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    Nodes[j, i].X = i;
                    Nodes[j, i].Y = j;
                    Nodes[j, i].Walkable = true;
                }
        }

        public Node this[int y, int x]
        {
            get { return Nodes[y, x]; }
            set { Nodes[y, x] = value; }
        }

        public bool IsInside(int x, int y)
        {
            return (x >= 0 && x < this.Width) && (y >= 0 && y < this.Height);
        }

        public bool IsWalkableAt(int x, int y)
        {
            return IsInside(x, y) && Nodes[y,x].Walkable;
        }

        public IList<Node> GetNeighbors(Node node, DiagonalMovement diag)
        {
            int x = node.X, y = node.Y;

            var neighbors = new List<Node>();
            bool s0 = false, d0 = false,
              s1 = false, d1 = false,
              s2 = false, d2 = false,
              s3 = false, d3 = false;


            // ↑
            if (this.IsWalkableAt(x, y - 1))
            {
                neighbors.Add(Nodes[y - 1,x]);
                s0 = true;
            }
            // →
            if (this.IsWalkableAt(x + 1, y))
            {
                neighbors.Add(Nodes[y, x + 1]);
                s1 = true;
            }
            // ↓
            if (this.IsWalkableAt(x, y + 1))
            {
                neighbors.Add(Nodes[y + 1, x]);
                s2 = true;
            }
            // ←
            if (this.IsWalkableAt(x - 1, y))
            {
                neighbors.Add(Nodes[y, x - 1]);
                s3 = true;
            }


            if (diag == DiagonalMovement.Never)
            {
                return neighbors;
            }

            if (diag == DiagonalMovement.OnlyWhenNoObstacles)
            {
                d0 = s3 && s0;
                d1 = s0 && s1;
                d2 = s1 && s2;
                d3 = s2 && s3;
            }
        
            else if (diag == DiagonalMovement.Always)
            {
                d0 = true;
                d1 = true;
                d2 = true;
                d3 = true;
            }
            else
            {
                throw new Exception("Incorrect value of diagonalMovement");
            }

            // ↖
            if (d0 && this.IsWalkableAt(x - 1, y - 1))
            {
                neighbors.Add(Nodes[y - 1, x - 1]);
            }
            // ↗
            if (d1 && this.IsWalkableAt(x + 1, y - 1))
            {
                neighbors.Add(Nodes[y - 1, x + 1]);
            }
            // ↘
            if (d2 && this.IsWalkableAt( x + 1, y + 1))
            {
                neighbors.Add(Nodes[y + 1, x + 1]);
            }
            // ↙
            if (d3 && this.IsWalkableAt(x - 1, y + 1))
            {
                neighbors.Add(Nodes[y + 1, x - 1 ]);
            }

            if (neighbors.Any(e => !e.Walkable))
                throw new Exception("NO!!");

            return neighbors;

        }

        public void DefineNode(Node node)
        {
            this[node.Y, node.X] = node;
        }

        public bool ValidMap()
        {

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    if (this[i, j] == null)
                        return false;
                    else if (this[i, j].X != j || this[i, j].Y != i)
                        return false;
                    

            if (StartNode == null || EndNode == null)
                return false;

            return true;
        }

        public void DefineAllNodes(IList<Node> nodes)
        {
            foreach (var item in nodes)
                DefineNode(item);
        }

        public void DefineAllNodes()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    this[i, j] = new Node(j, i);
        }
    }
}
