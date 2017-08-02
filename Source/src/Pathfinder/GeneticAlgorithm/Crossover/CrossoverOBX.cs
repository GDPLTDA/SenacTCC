using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Crossover
{
    public class CrossoverOBX : AbstractCrossover
    {
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > CrossoverRate || Operation.IsEqual())
                return Operation;
            var babymom = CrossoverOperation.Copy(Operation.Mom);
            var babydad = CrossoverOperation.Copy(Operation.Dad);
            var lstTempCities = new List<Node>();
            var lstPositions = new List<int>();
            var listmom = Operation.Mom.ListNodes;
            var listdad = Operation.Dad.ListNodes;
            var minindex = Math.Min(listmom.Count, listdad.Count);
            var pos = rand.Next(0, minindex - 1);
            while (pos < minindex)
            {
                lstPositions.Add(pos);
                lstTempCities.Add(listmom[pos]);
                pos += rand.Next(1, minindex - pos);
            }
            var cPos = 0;
            for (int cit = 0; cit < minindex; ++cit)
            {
                for (int i = 0; i < lstTempCities.Count; ++i)
                {
                    if (babydad.ListNodes[cit].EqualsAll(lstTempCities[i]))
                    {
                        if (lstTempCities.Count < cPos)
                            babydad.ListNodes[cit] = lstTempCities[cPos];
                        ++cPos;
                        break;
                    }
                }
            }
            lstTempCities.Clear();
            cPos = 0;
            for (int i = 0; i < lstPositions.Count; ++i)
            {
                var x = lstPositions[i];
                lstTempCities.Add(listdad[x]);
            }
            for (int cit = 0; cit < minindex; ++cit)
            {
                for (int i = 0; i < lstTempCities.Count; ++i)
                {
                    if (babymom.ListNodes[cit].EqualsAll(lstTempCities[i]))
                    {
                        if(lstTempCities.Count < cPos)
                            babymom.ListNodes[cit] = lstTempCities[cPos];
                        ++cPos;
                        break;
                    }
                }
            }
            return new CrossoverOperation(babymom, babydad);
        }
    }
}