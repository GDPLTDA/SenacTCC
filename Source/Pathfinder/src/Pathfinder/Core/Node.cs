using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;

namespace Pathfinder
{
    public class Node
    {

        public Node(int x, int y)
        {
            X = x;
            Y = y;
            Walkable = true;
        }


        public Node(int x, int y, bool walkable)
        {
            X = x;
            Y = y;
            Walkable = walkable;
        }

        
        public Node ParentNode { get; set; }
        public bool Walkable  { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }

        public double G { get; set; }
        public double H { get; set; }
        public double Cost { get; set; }

        public override bool Equals(object other)
        {
            var ee = (Node)other;
            return X == ee?.X && Y == ee?.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator == (Node node1, Node node2)
        {
            if (object.ReferenceEquals(node1, null))
            {
                return object.ReferenceEquals(node2, null);
            }

            return node1.Equals(node2);
        }
        public static bool operator !=(Node node1, Node node2)
        {

            if (object.ReferenceEquals(node1, null))
            {
                return !object.ReferenceEquals(node2, null);
            }
            return !node1.Equals(node2);
        }

        public override string ToString()
        {
            return $"{{{this.X},{this.Y},{(!Walkable?"Wall": "Walkable")},{Cost}}}";
        }
    }
}
