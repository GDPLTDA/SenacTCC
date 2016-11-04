using Microsoft.Extensions.Configuration;
using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class GASettings
    {
        public IConfigurationRoot Configuration { get; set; }

        public int GenerationLimit { get; set; }
        public double MutationRate { get; set; }
        public double CrossoverRate { get; set; }
        public int PopulationSize { get; set; }
        public int MutationAlgorithn { get; set; }
        public int CrossoverAlgorithn { get; set; }
        public int SelectionAlgorithn { get; set; }
        public int FitnessAlgorithn { get; set; }
        public int BestSolution { get; set; }
        public double Penalty { get; set; }

        public GASettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("GASettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


            GenerationLimit = int.Parse(Configuration[nameof(GenerationLimit)]);
            MutationRate = double.Parse(Configuration[nameof(MutationRate)]);
            CrossoverRate = double.Parse(Configuration[nameof(CrossoverRate)]);
            PopulationSize = int.Parse(Configuration[nameof(PopulationSize)]);
            MutationAlgorithn = int.Parse(Configuration[nameof(MutationAlgorithn)]);
            CrossoverAlgorithn = int.Parse(Configuration[nameof(CrossoverAlgorithn)]);
            FitnessAlgorithn = int.Parse(Configuration[nameof(FitnessAlgorithn)]);
            SelectionAlgorithn = int.Parse(Configuration[nameof(SelectionAlgorithn)]);
            BestSolution = int.Parse(Configuration[nameof(BestSolution)]);
            Penalty = double.Parse(Configuration[nameof(Penalty)]);
        }
        public ICrossover GetCrossover(int option = -1)
        {
            ICrossover ret = null;
            var opt = option >= 0 ? option : CrossoverAlgorithn;
            switch (opt)
            {
                case 0:
                    ret = CrossoverFactory.GetSimpleImplementation();
                    break;
                case 1:
                    ret = CrossoverFactory.GetOBXImplementation();
                    break;
                case 2:
                    ret = CrossoverFactory.GetPBXImplementation();
                    break;
            }

            return ret;

        }
        public IMutate GetMutate(int option = -1)
        {
            IMutate ret = null;
            var opt = option >= 0 ? option : MutationAlgorithn;

            switch (opt)
            {
                case 0:
                    ret = MutateFactory.GetSimpleImplementation();
                    break;
                case 1:
                    ret = MutateFactory.GetDIVMImplementation();
                    break;
                case 2:
                    ret = MutateFactory.GetDMImplementation();
                    break;
                case 3:
                    ret = MutateFactory.GetIMImplementation();
                    break;
                case 4:
                    ret = MutateFactory.GetIVMImplementation();
                    break;
                case 5:
                    ret = MutateFactory.GetSMImplementation();
                    break;
            }

            return ret;
        }
        public IFitness GetFitness(int option = -1)
        {
            IFitness ret = null;
            var opt = option >= 0 ? option : FitnessAlgorithn;
            switch (opt)
            {
                case 0:
                    ret = FitnessFactory.GetSimpleImplementation();
                    break;
                case 1:
                    ret = FitnessFactory.GetSimpleWithCollisionDetectionImplementation();
                    break;
            }

            return ret;
        }
        public ISelection GetSelection(int option = -1)
        {
            ISelection ret = null;
            var opt = option >= 0 ? option : SelectionAlgorithn;
            switch (opt)
            {
                case 0:
                    ret = SelectionFactory.GetSimpleImplementation();
                    break;
                case 1:
                    ret = SelectionFactory.GetRouletteWheelSelectionImplementation();
                    break;
            }

            return ret;
        }

    }
}
