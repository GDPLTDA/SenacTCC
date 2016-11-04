using Pathfinder.Abstraction;
using Pathfinder.Constants;
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
            if (Settings.Random.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;

            var i = (int)Math.Floor(Settings.Random.NextDouble() * baby.ListNodes.Count());
            int qtdBits;

            var newDirection = -1;
            while (newDirection > 9 || newDirection == -1){
                
                // choose the bit to swap                
                qtdBits = (int)Math.Floor(3 * Settings.Random.NextDouble());

                newDirection = (int)baby.ListNodes[i].Direction;
                newDirection ^= 1 << qtdBits;
            }

            baby.ListNodes[i].Direction = (DirectionMovement)newDirection;
            return baby;
        }
    }
}
