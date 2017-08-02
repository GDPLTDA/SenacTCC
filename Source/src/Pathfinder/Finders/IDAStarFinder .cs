using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
namespace Pathfinder.Finders
{
    public class IDAStarFinder : AbstractFinder
    {
        readonly bool TrackRecursion;
        readonly double TimeLimit;
        int nodesVisited;
        public IDAStarFinder(
            DiagonalMovement diag,
            IHeuristic heuristic,
            int weight = 1
          ) : base(diag, heuristic, weight)
        {
            Name = "IDA* (IDA Star)";
            SleepUITimeInMs = 30;
            var ms = Settings.IDAStarFinderTimeOut;
            TimeLimit = ms == 0 ? double.PositiveInfinity : ms;
            nodesVisited = 0;
            TrackRecursion = Settings.IDATrackRecursion;
        }
        private double H(Node a, Node b)
        {
            return Heuristic.Calc(Abs(b.X - a.X), Abs(b.Y - a.Y));
        }
        private static double Cost(Node a, Node b)
        {
            return (a.X == b.X || a.Y == b.Y) ? 1 : Sqrt(2);
        }
        public override void StepConfig()
        {
            if (GridMap == null || !TrackRecursion)
                return;

            UpdateOpenList(new List<Node>());
            for (int i = 0; i < GridMap.Height; i++)
                for (int j = 0; j < GridMap.Width; j++)
                    if (GridMap[i, j].Tested)
                        AddInOpenList(GridMap[i, j]);
        }
        private Tuple<Node, double> Search(Node node, double g, double cutoff, Dictionary<int, Node> route, int depth, Node end, int k)
        {
            nodesVisited++;
            // Enforce timelimit:
            if (_stopwatch.ElapsedMilliseconds > 0 &&
                _stopwatch.ElapsedMilliseconds > TimeLimit)
            {
                // Enforced as "path-not-found".
                return null;
            }
            var f = g + H(node, end) * Weight;
            // We've searched too deep for this iteration.
            if (f > cutoff)
            {
                return new Tuple<Node, double>(null, f); ;
            }
            if (node == end)
            {
                if (route.ContainsKey(depth))
                    route[depth] = node;
                else
                    route.Add(depth, node);
                return new Tuple<Node, double>(node, 0);
            }
            var neighbours = GridMap.GetNeighbors(node, DiagonalMovement);
            var min = double.PositiveInfinity;
            Tuple<Node, double> t;
            Node neighbour;
            var x = 0;
            for (x = 0; x < neighbours.Count; x++)
            {
                neighbour = neighbours[x];
                if (TrackRecursion)
                {
                    // Retain a copy for visualisation. Due to recursion, this
                    // node may be part of other paths too.
                    neighbour.RetainCount = neighbour.RetainCount + 1;
                    if (!neighbour.Tested)
                        neighbour.Tested = true;
                    OnStep(BuildArgs(k));
                }
                t = Search(neighbour, g + Cost(node, neighbour), cutoff, route, depth + 1, end, k);
                if (t == null)
                    return null;
                if (t.Item1 != null)
                {
                    if (route.ContainsKey(depth))
                        route[depth] = node;
                    else
                        route.Add(depth, node);
                    return t;
                }
                // Decrement count, then determine whether it's actually closed.
                if (TrackRecursion && (--neighbour.RetainCount) == 0)
                {
                    neighbour.Tested = false;
                }
                if (t.Item2 < min)
                {
                    min = t.Item2;
                }
            }
            return new Tuple<Node, double>(null, min);
        }
        public override bool Find(IMap grid)
        {
            Clear();
            var sqrt2 = Sqrt(2);
            GridMap = grid;
            var end = _endNode = grid.EndNode;
            var start = _startNode = grid.StartNode;
            var cutOff = H(start, end);
            Dictionary<int, Node> route;
            Tuple<Node, double> t;
            OnStart(BuildArgs(0));
            var k = 0;
            for (k = 0; true; k++)
            {
                route = new Dictionary<int, Node>();
                t = Search(start, 0, cutOff, route, 0, end, k);
                if (t == null || t.Item2 == double.PositiveInfinity)
                {
                    OnEnd(BuildArgs(k, false));
                    return false;
                }
                if (t.Item1 != null)
                {
                    var lis = route.OrderByDescending(e => e.Key).Select(e => e.Value).ToList();
                    for (int i = 1; i < lis.Count; i++)
                    {
                        lis[i - 1].ParentNode = lis[i];
                    }
                    OnEnd(BuildArgs(k, true));
                    return true;
                }
                cutOff = t.Item2;
            }
            // OnEnd(BuildArgs(0, false));
            // return false;
        }
        public void UpdateOpenList(Dictionary<int, Node> route)
        {
            var lis = route.OrderByDescending(e => e.Key).Select(e => e.Value).ToList();
            UpdateOpenList(lis);
        }
    }
}