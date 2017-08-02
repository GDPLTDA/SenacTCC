using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Crossover
{
    public class CrossoverPBX : AbstractCrossover
    {
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > CrossoverRate || Operation.IsEqual())
                return Operation;
            var babymom = CrossoverOperation.Copy(Operation.Mom);
            var babydad = CrossoverOperation.Copy(Operation.Dad);
            var lstPositions = new List<int>();
            var listmom = Operation.Mom.ListNodes;
            var listdad = Operation.Dad.ListNodes;
            var minindex = Math.Min(listmom.Count, listdad.Count);
            for (int i = 0; i < minindex; i++)
                babymom.ListNodes[i] = babydad.ListNodes[i] = new Node(-1, -1,DirectionMovement.None);
            var Pos = rand.Next(0, minindex - 1);
            while (Pos < minindex)
            {
                lstPositions.Add(Pos);
                Pos += rand.Next(1, minindex - Pos);
            }
            for (int pos = 0; pos < lstPositions.Count; ++pos)
            {
                babymom.ListNodes[lstPositions[pos]] = listmom[lstPositions[pos]];
                babydad.ListNodes[lstPositions[pos]] = listdad[lstPositions[pos]];
            }
            int c1, c2;
            c1 = c2 = 0;
            for (int pos = 0; pos < minindex; pos++)
            {
                while (c2 < minindex && babydad.ListNodes[c2].X > -1)
                    ++c2;
                if (c2 < babydad.ListNodes.Count)
                    if (!babydad.ListNodes.Exists(i => i.EqualsAll(listmom[pos])))
                        babydad.ListNodes[c2] = listmom[pos];
                while (c1 < minindex && babymom.ListNodes[c1].X > -1)
                    ++c1;
                if (c1 < babymom.ListNodes.Count)
                    if (!babymom.ListNodes.Exists(i => i.EqualsAll(listdad[pos])))
                        babymom.ListNodes[c1] = listdad[pos];
            }
            return new CrossoverOperation(babymom, babydad);
        }
    }
}