using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Core;

namespace TCC.AStar
{
    public class ASPathFinder
    {
        private int width;
        private int height;
        private ASNode[,] nodes;
        private ASNode startNode;
        private ASNode endNode;
        private ASSearchParameters searchParameters;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public ASPathFinder(ASSearchParameters tsearchParameters)
        {
            searchParameters = tsearchParameters;
            InitializeNodes(searchParameters.Map);
            int x = (int)searchParameters.StartLocation.X;
            int y = (int)searchParameters.StartLocation.Y;

            startNode = nodes[x, y];
            startNode.State = ASNodeState.Open;

            x = (int)searchParameters.EndLocation.X;
            y = (int)searchParameters.EndLocation.Y;
            endNode = nodes[x, y];
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Coordinate> FindPath()
        {
            // The start node is the first entry in the 'open' list
            var path = new List<Coordinate>();
            bool success = Search(startNode);
            if (success)
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                ASNode node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }

                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }

            return path;
        }

        /// <summary>
        /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
        /// </summary>
        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
        private void InitializeNodes(bool[,] map)
        {
            width = map.GetLength(0);
            height = map.GetLength(1);
            nodes = new ASNode[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    nodes[x, y] = new ASNode(x, y, map[x, y], searchParameters.EndLocation);
        }

        /// <summary>
        /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
        /// </summary>
        /// <param name="currentNode">The node from which to find a path</param>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search(ASNode currentNode)
        {
            // Set the current node to Closed since it cannot be traversed more than once
            currentNode.State = ASNodeState.Closed;
            List<ASNode> nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
                // Check whether the end node has been reached
                if (nextNode.Location.X == endNode.Location.X && nextNode.Location.Y == endNode.Location.Y)
                    return true;
                else
                    // If not, check the next set of nodes
                    if (Search(nextNode)) // Note: Recurses back into Search(Node)
                        return true;

            // The method returns false if this path leads to be a dead end
            return false;
        }

        /// <summary>
        /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
        /// </summary>
        /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
        /// <returns>A list of next possible nodes in the path</returns>
        private List<ASNode> GetAdjacentWalkableNodes(ASNode fromNode)
        {
            List<ASNode> walkableNodes = new List<ASNode>();
            IEnumerable<Coordinate> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                int x = (int)location.X;
                int y = (int)location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= width || y < 0 || y >= height)
                    continue;

                ASNode node = nodes[x, y];
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

                // Ignore already-closed nodes
                if (node.State == ASNodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == ASNodeState.Open)
                {
                    double traversalCost = ASNode.GetTraversalCost(node.Location, node.ParentNode.Location);
                    double gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = ASNodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        private static IEnumerable<Coordinate> GetAdjacentLocations(Coordinate fromLocation)
        {
            return new Coordinate[]
            {
                new Coordinate(fromLocation.X-1, fromLocation.Y-1),
                new Coordinate(fromLocation.X-1, fromLocation.Y  ),
                new Coordinate(fromLocation.X-1, fromLocation.Y+1),
                new Coordinate(fromLocation.X,   fromLocation.Y+1),
                new Coordinate(fromLocation.X+1, fromLocation.Y+1),
                new Coordinate(fromLocation.X+1, fromLocation.Y  ),
                new Coordinate(fromLocation.X+1, fromLocation.Y-1),
                new Coordinate(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
