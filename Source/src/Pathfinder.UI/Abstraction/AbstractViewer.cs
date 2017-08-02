using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Pathfinder.UI.Abstraction
{
    public abstract class AbstractViewer : IViewer
    {
        protected IFinder _finder;
        public abstract void Run(IMap map);
        protected AbstractViewer()
        {
        }
        public void SetFinder(IFinder finder)
        {
            _finder = finder;
            _finder.Step += LoopWraper;
            _finder.End += EndWraper;
            _finder.Start += StartWraper;
        }
        private void LoopWraper(object sender, EventArgs e)
        {
            var args = (FinderEventArgs)e;
            Loop(args);
        }
        private void EndWraper(object sender, EventArgs e)
        {
            var args = (FinderEventArgs)e;
            End(args);
        }
        private void StartWraper(object sender, EventArgs e)
        {
            Start();
        }
        public abstract void Loop(FinderEventArgs e);
        public abstract void End(FinderEventArgs e);
        public abstract void Start();
        public static void ShowStepLog(IFinder finder, FinderEventArgs e)
        {
            Console.WriteLine($"Alg={finder.Name}\nDiagonal={finder.DiagonalMovement.ToString()}\nMax Expanded Nodes = {finder.GetMaxExpandedNodes()}\nProcess Time = {finder.GetProcessedTime()} ms\nSteps|Generations:{e.Step} ");
        }
        public static void ShowEndLog(IFinder finder, IList<Node> path, FinderEventArgs e)
        {
            if (path?.Any(x => !x.Walkable) ?? false)
                throw new Exception("Why is there a wall on the path?");
            Console.WriteLine($"Alg={finder.Name}\nDiagonal={finder.DiagonalMovement.ToString()}\nMax Expanded Nodes = {finder.GetMaxExpandedNodes()}\nProcess Time = {finder.GetProcessedTime()} ms\nSteps|Generation:{e.Step}");
            if (e.Finded)
                Console.WriteLine($"Path Length: {path.OrderBy(x => x.G).Last().G}");
            else
                Console.WriteLine("CANT FIND A PATH!");
            Console.ReadKey();
        }
    }
}
