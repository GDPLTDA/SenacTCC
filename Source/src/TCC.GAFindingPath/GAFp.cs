using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GeneticAlgorithm;

namespace TCC.GAFindingPath
{
    public class GAFP
    {
        private List<GAGenome> lstPopulation { get; set; } = new List<GAGenome>();
        private GAParams Params { get; set; }
        public GAFP(GAParams tParams)
        {
            Params = tParams;

            CreateStartingPopulation();
        }
        private void CreateStartingPopulation()
        {
            for (int i = 0; i < Params.PopulationSize; i++)
            {
                var objGenome = new GAGenome(Params.NumberOfRoutes);
                lstPopulation.Add(objGenome);
            }
        }
        public void Epoch()
        {
            
        }
    }
}
