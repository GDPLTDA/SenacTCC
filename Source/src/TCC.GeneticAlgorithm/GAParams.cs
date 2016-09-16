using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAParams
    {
        public SeachParameters Params { get; set; }
        public int MapaSize { get; set; }

        public int MapWidth { get; set; }

        public int MapHeight { get; set; }

        public double MutationRate { get; set; }
        public double CrossoverRate { get; set; }
        public int PopulationSize { get; set; }
        public int NumberOfRoutes { get; set; }
    }
}
