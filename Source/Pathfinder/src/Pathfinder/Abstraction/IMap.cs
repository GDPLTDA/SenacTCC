using Pathfinder.Constants;
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
        IList<Node> GetNeighbors(Node node, DiagonalMovement diag);
        Node StartNode { get; set; }
        Node EndNode { get; set; }
        void DefineNode(Node node);
        void DefineAllNodes(IList<Node> nodes);
        void DefineAllNodes();
        bool ValidMap();
    }
}
