
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
    public interface IFinder
    {
        string Name { get; set; }
        int SleepUITimeInMs { get; set; }
        bool Find(IMap grid);
        DiagonalMovement DiagonalMovement { get; set; }
        IHeuristic Heuristic { get; set; }
        int Weight { get; set; }
        IList<Node> GetNodesInOpenedList();
        IList<Node> GetNodesInClosedList();
        List<Node> GetPath();
        bool IsOpen(Node e);
        bool IsClosed(Node e);
        long GetProcessedTime();
        int GetMaxExpandedNodes();
        event EventHandler Start;
        event EventHandler Step;
        event EventHandler End;
    }
    public class FinderEventArgs : EventArgs
    {
       public long PassedTimeInMs { get; set; }
       public int Step { get; set; }
       public int ExpandedNodesCount { get; set; }
       public bool Finded { get; set; }
        public IMap GridMap { get; set; }
    }
}