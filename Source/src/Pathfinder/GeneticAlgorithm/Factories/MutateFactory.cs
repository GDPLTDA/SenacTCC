
using Pathfinder.Abstraction;
using Pathfinder.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class MutateFactory  : IFactory<IMutate>
    {
        public static IMutate GetEMImplementation()
            => new MutateEM();
        public static IMutate GetDIVMImplementation()
            => new MutateDIVM();
        public static IMutate GetDMImplementation()
            => new MutateDM();
        public static IMutate GetIMImplementation()
            => new MutateIM();
        public static IMutate GetIVMImplementation()
            => new MutateIVM();
        public static IMutate GetSMImplementation()
            => new MutateSM();
        public static IMutate GetBitwiseImplementation()
            => new MutateBitwise();
        public IMutate GetImplementation()
           => Decide(GASettings.MutationAlgorithm);
        public IMutate GetImplementation(int option)
            => Decide((MutateEnum)option);
        private static IMutate Decide(MutateEnum option)
        {
            switch (option)
            {
                case MutateEnum.EM:
                    return GetEMImplementation();
                case MutateEnum.DIVM:
                    return GetDIVMImplementation();
                case MutateEnum.DM:
                    return GetDMImplementation();
                case MutateEnum.IM:
                    return GetIMImplementation();
                case MutateEnum.IVM:
                    return GetIVMImplementation();
                case MutateEnum.SM:
                    return GetSMImplementation();
                case MutateEnum.Bitwise:
                    return GetBitwiseImplementation();
            }
            throw new Exception("No mutate passed");
        }
    }
}