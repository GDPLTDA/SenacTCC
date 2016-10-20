﻿using Pathfinder.Abstraction;
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

        public DiagonalMovement? AllowDiagonal{get; set;}

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

        public bool IsWalkableAt(Node node)
        {
            return IsInside(node.X, node.Y) && node.Walkable;
        }

        public bool IsWalkableAt(int y, int x)
        {
            return IsInside(x, y) && Nodes[y,x].Walkable;
        }

        public Node GetDirectionNode(Node node, bool ByRef = true)
        {
            var dir = (DirectionMovement)Settings.Random.Next(1, Enum.GetNames(typeof(DirectionMovement)).Length);
            return GetDirectionNode(node, dir, ByRef);
        }

        public Node GetDirectionNode(Node node, DirectionMovement direction, bool ByRef = true)
        {
            int x = node.X, y = node.Y;
            Node newnode = null;
            switch (direction)
            {
                case DirectionMovement.Up:
                    if (IsWalkableAt(y - 1, x))
                    {
                        newnode = Nodes[y - 1, x];
                        newnode.Direction = DirectionMovement.Up;
                    }
                    break;
                case DirectionMovement.Down:
                    if (IsWalkableAt(y + 1, x))
                    {
                        newnode = Nodes[y + 1, x];
                        newnode.Direction = DirectionMovement.Down;
                    }
                    break;
                case DirectionMovement.Left:
                    if (IsWalkableAt(y, x - 1))
                    {
                        newnode = Nodes[y, x - 1];
                        newnode.Direction = DirectionMovement.Left;
                    }
                    break;
                case DirectionMovement.Right:
                    if (IsWalkableAt(y, x + 1))
                    {
                        newnode = Nodes[y, x + 1];
                        newnode.Direction = DirectionMovement.Right;
                    }
                    break;
                // Diagonais
                case DirectionMovement.UpLeft:
                    if (IsWalkableAt(y - 1, x - 1))
                    {
                        newnode = Nodes[y - 1, x - 1];
                        newnode.Direction = DirectionMovement.UpLeft;
                    }
                    break;
                case DirectionMovement.UpRight:
                    if (IsWalkableAt(y - 1, x + 1))
                    {
                        newnode = Nodes[y - 1, x + 1];
                        newnode.Direction = DirectionMovement.UpRight;
                    }
                    break;
                case DirectionMovement.DownLeft:
                    if (IsWalkableAt(y + 1, x - 1))
                    {
                        newnode = Nodes[y + 1, x - 1];
                        newnode.Direction = DirectionMovement.DownLeft;
                    }
                    break;
                case DirectionMovement.DownRight:
                    if (IsWalkableAt(y + 1, x + 1))
                    {
                        newnode = Nodes[y + 1, x + 1];
                        newnode.Direction = DirectionMovement.DownRight;
                    }
                    break;
            }

            if (newnode == null)
                return null;

            return ByRef ? newnode : new Node(newnode, node, direction);
        }

        public IList<Node> GetNeighbors(Node node, bool ByRef = true)
        {
            var settings = new Settings();
            return GetNeighbors(node, settings.AllowDiagonal, ByRef);
        }
        
        public IList<Node> GetNeighbors(Node node, DiagonalMovement diag, bool ByRef = true)
        {
            Node newnode;
            var neighbors = new List<Node>();

            bool s0 = false, d0 = false,
              s1 = false, d1 = false,
              s2 = false, d2 = false,
              s3 = false, d3 = false;

            newnode = GetDirectionNode(node, DirectionMovement.Up, ByRef);
            if (newnode != null)
            {
                neighbors.Add(newnode);
                s0 = true;
            }
            newnode = GetDirectionNode(node, DirectionMovement.Down, ByRef);
            if (newnode != null)
            {
                neighbors.Add(newnode);
                s2 = true;
            }
            newnode = GetDirectionNode(node, DirectionMovement.Left, ByRef);
            if (newnode != null)
            {
                neighbors.Add(newnode);
                s1 = true;
            }
            newnode = GetDirectionNode(node, DirectionMovement.Right, ByRef);
            if (newnode != null)
            {
                neighbors.Add(newnode);
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
            else if (diag == DiagonalMovement.IfAtMostOneObstacle) {
                d0 = s3 || s0;
                d1 = s0 || s1;
                d2 = s1 || s2;
                d3 = s2 || s3;
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

            newnode = GetDirectionNode(node, DirectionMovement.UpLeft, ByRef);
            if (d0 && newnode != null)
                neighbors.Add(newnode);
            newnode = GetDirectionNode(node, DirectionMovement.UpRight, ByRef);
            if (d1 && newnode != null)
                neighbors.Add(newnode);
            newnode = GetDirectionNode(node, DirectionMovement.DownRight, ByRef);
            if (d2 && newnode != null)
                neighbors.Add(newnode);
            newnode = GetDirectionNode(node, DirectionMovement.DownLeft, ByRef);
            if (d3 && newnode != null)
                neighbors.Add(newnode);

            if (neighbors.Any(e => e == null || !e.Walkable ))
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
