using Pathfinder.Abstraction;
using Pathfinder.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class MutateFactory
    {
        public static IMutate GetSimpleImplementation(double rate)
        {
            return new MutateSimple(rate);
        }
        public static IMutate GetDIVMImplementation(double rate)
        {
            return new MutateDIVM(rate);
        }
        public static IMutate GetDMImplementation(double rate)
        {
            return new MutateDM(rate);
        }

        public static IMutate GetIMImplementation(double rate)
        {
            return new MutateIM(rate);
        }

        public static IMutate GetIVMImplementation(double rate)
        {
            return new MutateIVM(rate);
        }

        public static IMutate GetSMImplementation(double rate)
        {
            return new MutateSM(rate);
        }
    }
}
