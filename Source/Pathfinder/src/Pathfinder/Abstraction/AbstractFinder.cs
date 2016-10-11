using Pathfinder.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public abstract class AbstractFinder : IFinder
    {
        public event EventHandler Step;
        public event EventHandler End;
        public event EventHandler Start;

        protected IMap GridMap { get; set; }
        public DiagonalMovement DiagonalMovement { get; set; }
        public IHeuristic Heuristic { get; set; }
        public int Weight { get; set; }

        protected int _maxExpandedNodes = 0;
        protected Stopwatch _stopwatch;
        protected IList<Node> _openList;
        protected IList<Node> _closedList;

        protected Node _startNode;
        protected Node _endNode;
        public string Name { get; set; }

        public int SleepUITimeInMs { get; set; }


        public virtual bool isOpen (Node e) => _openList.Contains(e);
        public virtual bool isClosed(Node e) => _closedList.Contains(e);

        public virtual IList<Node> GetNodesInOpenedList() => _openList;
        public virtual IList<Node> GetNodesInClosedList() => _closedList;
        public virtual int GetMaxExpandedNodes() => _maxExpandedNodes;
        public virtual long GetProcessedTime() => _stopwatch.ElapsedMilliseconds;

        public AbstractFinder(
             DiagonalMovement diag,
             IHeuristic heuristic,
             int weight = 1
           )
        {
            DiagonalMovement = diag;
            Heuristic = heuristic;
            Weight = weight;
            Clear();
        }


        protected virtual void Clear()
        {
            GridMap = null;
            _openList = new List<Node>();
            _closedList = new List<Node>();
            _stopwatch = new Stopwatch();
        }

        protected virtual void OnStep(FinderEventArgs e)
        {
            _stopwatch.Stop();
            StepConfig();
            UpdateMaxNodes();
            Step?.Invoke(this, e);
            _stopwatch.Start();
        }


        public virtual void StepConfig()
        {

        }

        protected virtual void OnEnd(FinderEventArgs e)
        {
            _stopwatch.Stop();
            End?.Invoke(this, e);
        }

        protected virtual void OnStart(FinderEventArgs e)
        {
            Start?.Invoke(this, e);
            _stopwatch.Start();
        }


        public abstract bool Find(IMap grid);
        
      
        

        public virtual IList<Node> GetPath()
        {
            var path = new List<Node>();

            Node node = _endNode;
            while(node != null || node == _startNode)
            {
                path.Add(node);
                node = node.ParentNode;
            }
            
            return path;
        }

        protected void UpdateMaxNodes()
        {
            var atualNodes = _openList.Count + _closedList.Count;
            if (atualNodes >= _maxExpandedNodes)
                _maxExpandedNodes = atualNodes;

        }


        protected FinderEventArgs BuildArgs(int i, bool finded = false)
        {
            var args = new FinderEventArgs();
            args.PassedTimeInMs = _stopwatch.ElapsedMilliseconds;
            args.Step = i;
            args.ExpandedNodesCount = _openList.Count() + _closedList.Count();
            args.Finded = finded;
            args.GridMap = GridMap;
            return args;
        }

    }
}
