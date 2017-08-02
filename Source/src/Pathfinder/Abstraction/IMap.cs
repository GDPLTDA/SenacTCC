
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
    public interface IMap
    {
        int Height { get; set; }
        int Width { get; set; }
        Node[,] Nodes { get; set; }
        Node this[int x, int y] { get; set; }
        Node GetDirectionNode(Node node, bool ByRef=true, bool valid = true);
        Node GetDirectionNode(Node node, DirectionMovement direction, bool ByRef=true, bool valid = true);
        IList<Node> GetNeighbors(Node node, bool ByRef = true, bool valid = true);
        IList<Node> GetNeighbors(Node node, DiagonalMovement diag, bool ByRef = true, bool valid = true);
        Node StartNode { get; set; }
        Node EndNode { get; set; }
        void DefineNode(Node node);
        void DefineAllNodes(IList<Node> nodes);
        void DefineAllNodes();
        bool ValidMap();
        void Clear();
        DiagonalMovement? AllowDiagonal { get; set; }
    }
}