using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Selection
{
    public class SelectionRouletteWheel : ISelection
    {
        public IGenome Select(List<IGenome> listnode)
        {
            var maxFitness = listnode.Max(e => e.Fitness);
            var weight = new double[listnode.Count()];
            // calculate the weights
            for (int i = 0; i < listnode.Count(); i++)
                weight[i] = 100 - ( (listnode[i].Fitness * 100) / maxFitness );
            var index = -1;
            var weight_sum = 0d;
            for (int i = 0; i < weight.Length; i++)
                weight_sum += weight[i];
            // get a random value
            var value = Container.Resolve<IRandom>()
                            .NextDouble() * weight_sum;
            // locate the random value based on the weights
            for (int i = 0; i < weight.Length; i++)
            {
                value -= weight[i];
                if (value <= 0)
                {
                    index = i;
                    break;
                }
            }
            // when rounding errors occur, we return the last item's index
            if (index == -1)
                index = weight.Length - 1;
            return listnode[index];
        }
    }
}