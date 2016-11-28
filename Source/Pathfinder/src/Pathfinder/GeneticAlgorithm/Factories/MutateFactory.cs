
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
        public IMutate GetEMImplementation()
            => new MutateEM();

        public IMutate GetDIVMImplementation()
            => new MutateDIVM();
        
        public IMutate GetDMImplementation()
            => new MutateDM();
      
        public IMutate GetIMImplementation()
            => new MutateIM();
        
        public IMutate GetIVMImplementation()
            => new MutateIVM();

        public IMutate GetSMImplementation()
            => new MutateSM();
        
        public IMutate GetBitwiseImplementation()
            => new MutateBitwise();
        

        public IMutate GetImplementation()
           => Decide(GASettings.MutationAlgorithm);
         
        
        public IMutate GetImplementation(int option)
            => Decide((MutateEnum)option);

        private IMutate Decide(MutateEnum option)
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
