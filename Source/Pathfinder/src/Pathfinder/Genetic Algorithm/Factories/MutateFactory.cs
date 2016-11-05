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
        public static IMutate GetSimpleImplementation()
        {
            return new MutateEM();
        }
        public static IMutate GetDIVMImplementation()
        {
            return new MutateDIVM();
        }
        public static IMutate GetDMImplementation()
        {
            return new MutateDM();
        }

        public static IMutate GetIMImplementation()
        {
            return new MutateIM();
        }

        public static IMutate GetIVMImplementation()
        {
            return new MutateIVM();
        }

        public static IMutate GetSMImplementation()
        {
            return new MutateSM();
        }
        public static IMutate GetBitwiseImplementation()
        {
            return new MutateBitwise();
        }


    }
}
