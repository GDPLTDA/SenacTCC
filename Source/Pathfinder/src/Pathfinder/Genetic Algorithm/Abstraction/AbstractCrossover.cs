using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public class CrossoverOperation
    {
        public CrossoverOperation()
        {

        }

        public CrossoverOperation(List<Node> mon, List<Node> dad)
        {
            Dad = mon;
            Mom = dad;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsEqual()
        {
            if (Mom.Count != Dad.Count)
                return false;

            for (int i = 0; i < Mom.Count; i++)
            {
                if (Mom[i] != Dad[i])
                    return false;
            }
            return true;
        }

        public List<Node> Copy(List<Node> listnode)
        {
            var returnnode = new List<Node>();

            foreach (var item in listnode)
                returnnode.Add(new Node(item));

            return returnnode;
        }

        public List<Node> Mom { get; set; }
        public List<Node> Dad { get; set; }
    }
    public abstract class AbstractCrossover : ICrossover
    {
        protected double CrossoverRate { get; set; }
        CrossoverOperation Operation { get; set; }
        public abstract CrossoverOperation Calc(CrossoverOperation Operation);
    }
}
