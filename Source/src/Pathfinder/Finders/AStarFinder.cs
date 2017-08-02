using Pathfinder.Abstraction;
using static System.Math;
namespace Pathfinder.Finders
{
    public class AStarFinder : AbstractFinder
    {
        public AStarFinder(
            DiagonalMovement diag,
            IHeuristic heuristic,
            int weight = 1
          ) : base(diag, heuristic, weight)
        {
            Name = "A* (A Star)";
            SleepUITimeInMs = 200;
            // When diagonal movement is allowed the manhattan heuristic is not
            //admissible. It should be octile instead
            if (DiagonalMovement == DiagonalMovement.Never)
                Heuristic = heuristic ?? Container.Resolve<IHeuristic>((int)HeuristicEnum.Manhattan);
            else
                Heuristic = heuristic ?? Container.Resolve<IHeuristic>((int)HeuristicEnum.Octile);
        }
        public virtual double CalcH(int dx, int dy)
        {
            return Heuristic.Calc(dx, dy);
        }
        public override bool Find(IMap grid)
        {
            Clear();
            var sqrt2 = Sqrt(2);
            GridMap = grid;
            _startNode = grid.StartNode;
            _endNode = grid.EndNode;
            _startNode.Cost = 0;
            _endNode.Cost = 0;
            AddInOpenList(_startNode);
            var step = 0;
            OnStart(BuildArgs(step));
            while (OpenListCount() != 0)
            {
                var node = PopOpenList();
                AddInClosedList(node);
                if (node == _endNode)
                {
                    //_endNode = node;
                    OnEnd(BuildArgs(step, true));
                    return true;
                }
                var neighbors = grid.GetNeighbors(node, DiagonalMovement);
                for (var i = 0; i < neighbors.Count; ++i)
                {
                    var neighbor = neighbors[i];
                    if (IsClosed(neighbor))
                        continue;
                    var x = neighbor.X;
                    var y = neighbor.Y;
                    // get the distance between current node and the neighbor
                    // and calculate the next g score
                    var ng = node.G + ((x - node.X == 0 || y - node.Y == 0) ? 1 : sqrt2);
                    // check if the neighbor has not been inspected yet, or
                    // can be reached with smaller cost from the current node
                    if (!IsOpen(neighbor) || ng < neighbor.G)
                    {
                        neighbor.G = ng;
                        neighbor.H = Weight * CalcH(Abs(x - _endNode.X), Abs(y - _endNode.Y));
                        neighbor.Cost = neighbor.G + neighbor.H;
                        neighbor.ParentNode = node;
                        if (!IsOpen(neighbor))
                            PushInOpenList(neighbor);
                    }
                }
                OrderOpenList(e => e.Cost);
                OnStep(BuildArgs(step++));
            }
            OnEnd(BuildArgs(step, false));
            return false;
        }
    }
}