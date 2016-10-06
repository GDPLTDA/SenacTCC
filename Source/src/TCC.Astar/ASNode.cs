using System;
using TCC.Core;

namespace TCC.Astar
{
    /// <summary>
    /// Represents a single node on a grid that is being searched for a path between two points
    /// </summary>
    public class ASNode
    {
        private ASNode parentNode { get; set; }
        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Coordinate Location { get; private set; }
        /// <summary>
        /// True when the node may be traversed, otherwise false
        /// </summary>
        public bool IsWalkable { get; set; }
        /// <summary>
        /// Cost from start to here
        /// </summary>
        public double G { get; private set; }
        /// <summary>
        /// Estimated cost from here to end
        /// </summary>
        public double H { get; private set; }
        /// <summary>
        /// Flags whether the node is open, closed or untested by the PathFinder
        /// </summary>
        public ASNodeState State { get; set; }
        /// <summary>
        /// Estimated total cost (F = G + H)
        /// </summary>
        public double F
        {
            get { return G + H; }
        }
        /// <summary>
        /// Gets or sets the parent node. The start node's parent is always null.
        /// </summary>
        public ASNode ParentNode
        {
            get { return parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                parentNode = value;
                G = parentNode.G + GetTraversalCost(Location, parentNode.Location);
            }
        }
        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="y">The node's location along the Y axis</param>
        /// <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
        /// <param name="endLocation">The location of the destination node</param>
        public ASNode(int x, int y, bool isWalkable, Coordinate endLocation)
        {
            Location = new Coordinate(0, x, y);
            State = ASNodeState.Untested;
            IsWalkable = isWalkable;
            H = GetTraversalCost(Location, endLocation);
            G = 0;
        }
        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        internal static double GetTraversalCost(Coordinate location, Coordinate otherLocation)
        {
            double deltaX = otherLocation.X - location.X;
            double deltaY = otherLocation.Y - location.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
