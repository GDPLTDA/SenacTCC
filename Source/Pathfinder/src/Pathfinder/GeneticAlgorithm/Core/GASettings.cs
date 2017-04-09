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
        public static IConfigurationRoot Configuration { get; set; }
        public static int GenerationLimit { get; set; }
        public static double MutationRate { get; set; }
        public static double CrossoverRate { get; set; }
        public static int PopulationSize { get; set; }
        public static MutateEnum MutationAlgorithm { get; set; }
        public static CrossoverEnum CrossoverAlgorithm { get; set; }
        public static SelectionEnum SelectionAlgorithm { get; set; }
        public static FitnessEnum FitnessAlgorithm { get; set; }
        public static int BestSolutionToPick { get; set; }
        public static double Penalty { get; set; }
        static GASettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("GASettings.json", optional: true, reloadOnChange: true);
            var conf = Configuration = builder.Build();
            GenerationLimit = int.Parse(conf[nameof(GenerationLimit)]);
            MutationRate = double.Parse(conf[nameof(MutationRate)]);
            CrossoverRate = double.Parse(conf[nameof(CrossoverRate)]);
            PopulationSize = int.Parse(conf[nameof(PopulationSize)]);
            MutationAlgorithm = (MutateEnum)int.Parse(conf[nameof(MutationAlgorithm)]);
            CrossoverAlgorithm = (CrossoverEnum)int.Parse(conf[nameof(CrossoverAlgorithm)]);
            FitnessAlgorithm = (FitnessEnum)int.Parse(conf[nameof(FitnessAlgorithm)]);
            SelectionAlgorithm = (SelectionEnum)int.Parse(conf[nameof(SelectionAlgorithm)]);
            BestSolutionToPick = int.Parse(conf[nameof(BestSolutionToPick)]);
            Penalty = double.Parse(Configuration[nameof(Penalty)]);
        }
    }
}