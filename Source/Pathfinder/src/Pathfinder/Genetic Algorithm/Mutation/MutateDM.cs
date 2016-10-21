﻿using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateDM : AbstractMutate
    {
        public override  IGenome Calc(IGenome baby)
        {
            if (Settings.Random.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;

            int listcount = baby.ListNodes.Count;
            const int minSpanSize = 3;

            if (listcount <= minSpanSize)
                return baby;

            int beg, end;
            beg = end = 0;
            var spanSize = Settings.Random.Next(minSpanSize, listcount);
            beg = Settings.Random.Next(1, listcount - spanSize);
            end = beg + spanSize;

            var lstTemp = new List<Node>();
            for (int i = beg; i < end; i++)
            {
                lstTemp.Add(baby.ListNodes[beg]);
                baby.ListNodes.RemoveAt(beg);
            }

            var insertLocation = Settings.Random.Next(1, baby.ListNodes.Count);
            var count = 0;
            for (int i = insertLocation; count < lstTemp.Count; i++)
            {
                baby.ListNodes.Insert(i, lstTemp[count]);
                count++;
            }
            return baby;
        }
    }
}