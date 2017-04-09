using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace Pathfinder
{
    public class Node
    {
        public Node(Node node)
        {
            var type = node.GetType();
            foreach (var item in type.GetProperties())
                item.SetValue(this, item.GetValue(node));
        }
        public Node(Node node, Node parent, DirectionMovement direction)
        {
            var type = node.GetType();
            foreach (var item in type.GetProperties())
            {
                item.SetValue(this, item.GetValue(node));
            }
            ParentNode = parent;
            Direction = direction;
        }
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
        public Node(int x, int y, DirectionMovement direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public Node(int x, int y, bool walkable, DirectionMovement direction)
        {
            X = x;
            Y = y;
            Walkable = walkable;
            Direction = direction;
        }
        public DirectionMovement Direction { get; set; } = DirectionMovement.None;
        public Node ParentNode { get; set; }
        public bool Walkable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double G { get; set; }
        public double H { get; set; }
        public double Cost { get; set; }
        public bool Tested { get; set; }
        public int RetainCount { get; set; }
        public bool Collision { get; set; }
        public override bool Equals(object _other)
        {
            var other = (Node)_other;
            return X == other?.X && Y == other?.Y;
        }
        public bool EqualsAll(Node other)
        {
            return X == other?.X && Y == other?.Y && Direction == other.Direction;
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
            return $"{{{this.X},{this.Y},{(!Walkable?"Wall": nameof(Walkable))},{Cost}}} {Direction} {(Collision?" COLIDE!":"")}";
        }
    }
}