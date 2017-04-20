
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Pathfinder.Abstraction
{
    public abstract class AbstractFinder : IFinder
    {
        public event EventHandler Step;
        public event EventHandler End;
        public event EventHandler Start;
        private IMap _map;
        readonly object _lockClosed = new object();
        readonly object _lockOpen = new object();

        protected IMap GridMap
        {
            get { return _map; }
            set
            {
                if (value?.AllowDiagonal.HasValue ?? false)
                    DiagonalMovement = value.AllowDiagonal.Value;
                _map = value;
            }
        }
        public DiagonalMovement DiagonalMovement { get; set; }
        public IHeuristic Heuristic { get; set; }
        public int Weight { get; set; }
        protected int _maxExpandedNodes { get; set; } = 0;
        protected Stopwatch _stopwatch { get; set; }
        private IList<Node> _openList { get; set; }
        private IList<Node> _closedList { get; set; }
        protected Node _startNode { get; set; }
        protected Node _endNode { get; set; }
        public string Name { get; set; }
        public int SleepUITimeInMs { get; set; }
        public virtual IList<Node> GetNodesInClosedList() => _closedList;
        public virtual int GetMaxExpandedNodes() => _maxExpandedNodes;
        public virtual long GetProcessedTime() => _stopwatch.ElapsedMilliseconds;
        public virtual IList<Node> GetNodesInOpenedList() => _openList;

        protected AbstractFinder(
             DiagonalMovement diag,
             int weight = 1
           )
        {
            DiagonalMovement = diag;
            Weight = weight;
            Clear();
        }

        protected void AddInOpenList(Node node)
        {
            lock (_lockOpen)
                _openList.Add(node);
        }
        protected void AddInClosedList(Node node)
        {
            lock (_lockClosed)
                _closedList.Add(node);
        }


        protected IEnumerable<Node> NodesInOpenList()
        {
            lock (_lockOpen)
                foreach (var item in _openList)
                    yield return item;

        }

        protected IEnumerable<Node> NodesInClosedLit()
        {
            lock (_lockClosed)
                foreach (var item in _closedList)
                    yield return item;
        }


        protected int OpenListCount()
        {
            lock (_lockOpen)
                return _openList.Count();
        }


        protected int ClosedListCount()
        {
            lock (_lockClosed)
                return _closedList.Count();
        }

        protected Node PopOpenList()
        {
            lock (_lockOpen)
                return _openList.Pop();

        }

        protected void PushInOpenList(Node node)
        {
            lock (_lockOpen)
                _openList.Push(node);

        }

        protected void OrderOpenList(Func<Node, object> predicate)
        {
            _openList = _openList.OrderByDescending(predicate).ToList();
        }


        protected void UpdateOpenList(IList<Node> newList)
        {
            lock (_lockOpen)
                _openList = newList;
        }

        protected void UpdateClosedList(IList<Node> newList)
        {
            lock (_lockClosed)
                _closedList = newList;
        }
        public virtual bool IsOpen(Node e)
        {
            lock (_lockOpen)
                return _openList.ToList().Exists(i => i != null && i.Equals(e));
        }
        public virtual bool IsClosed(Node e)
        {
            lock (_lockClosed)
                return _closedList.ToList().Exists(i => i != null && i.Equals(e));
        }



        protected AbstractFinder(
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
        protected void Clear()
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
            _stopwatch.Reset();
            _stopwatch.Start();
        }
        public abstract bool Find(IMap grid);
        public virtual List<Node> GetPath()
        {
            var path = new List<Node>();
            var node = _endNode;
            while (node != null || node == _startNode)
            {
                path.Add(node);
                node = node.ParentNode;
            }
            return path;
        }
        protected virtual void UpdateMaxNodes()
        {
            var atualNodes = _openList.Count + _closedList.Count;
            if (atualNodes >= _maxExpandedNodes)
                _maxExpandedNodes = atualNodes;
        }
        protected FinderEventArgs BuildArgs(int i, bool finded = false)
        {
            var args = new FinderEventArgs
            {
                PassedTimeInMs = _stopwatch.ElapsedMilliseconds,
                Step = i,
                ExpandedNodesCount = _openList.Count() + _closedList.Count(),
                Finded = finded,
                GridMap = GridMap
            };
            return args;
        }
    }
}