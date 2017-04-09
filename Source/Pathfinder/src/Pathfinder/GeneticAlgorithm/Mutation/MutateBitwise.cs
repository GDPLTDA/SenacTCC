using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Mutation
{
    public class MutateBitwise : AbstractMutate
    {
        public override IGenome Calc(IGenome baby)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;
            var i = (int)Math.Floor(rand.NextDouble() * baby.ListNodes.Count());
            int qtdBits;
            var newDirection = -1;
            while (newDirection > 9 || newDirection == -1 || newDirection==0)
            {
                // choose the bit to swap
                qtdBits = (int)Math.Floor(3 * rand.NextDouble());
                newDirection = (int)baby.ListNodes[i].Direction;
                newDirection ^= 1 << qtdBits;
            }
            baby.ListNodes[i].Direction = (DirectionMovement)newDirection;
            return baby;
        }
    }
}