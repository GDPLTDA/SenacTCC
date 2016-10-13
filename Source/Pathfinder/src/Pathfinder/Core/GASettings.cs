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
        }


        public ICrossover GetCrossover()
        {
            ICrossover ret = null;
            switch (CrossoverAlgorithn)
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

        public IMutate GetMutate()
        {
            IMutate ret = null;
            switch (MutationAlgorithn)
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

        public IFitness GetFitness()
        {
            IFitness ret = null;
            switch (MutationAlgorithn)
            {
                case 0:
                    ret = FitnessFactory.GetSimpleImplementation();
                    break;
            }

            return ret;
        }

        public ISelection GetSelection()
        {
            ISelection ret = null;
            switch (SelectionAlgorithn)
            {
                case 0:
                    ret = SelectionFactory.GetSimpleImplementation();
                    break;
            }

            return ret;
        }

    }
}
