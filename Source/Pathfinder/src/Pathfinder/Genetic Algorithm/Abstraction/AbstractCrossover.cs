using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public class CrossoverOperation
    {
        public IGenome Mom { get; set; }
        public IGenome Dad { get; set; }

        public CrossoverOperation()
        {

        }

        public CrossoverOperation(IGenome mon, IGenome dad)
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
            return Mom.Equals(Dad);
        }
        public IGenome Copy(IGenome genome)
        {
            return new Genome(genome);
        }
    }
    public abstract class AbstractCrossover : ICrossover
    {
        public AbstractCrossover(double rate)
        {
            CrossoverRate = rate;
        }
        protected double CrossoverRate { get; set; }
        CrossoverOperation Operation { get; set; }
        public abstract CrossoverOperation Calc(CrossoverOperation Operation);
    }
}
