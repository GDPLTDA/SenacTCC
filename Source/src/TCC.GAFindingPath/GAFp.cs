using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Astar;
using TCC.GeneticAlgorithm;
using TCC.Core;

namespace TCC.GAFindingPath
{
    public class GAFP
    {
        private List<GAGenome> lstPopulation { get; set; } = new List<GAGenome>();
        private GAParams GaParams { get; set; }
        public SeachParameters SeachParams { get; set; }
        public GAFP(GAParams tParams, SeachParameters tSeachParams)
        {
            GaParams = tParams;
            SeachParams = tSeachParams;

            CreateStartingPopulation();
        }
        private void CreateStartingPopulation()
        {
            for (int i = 0; i < GaParams.PopulationSize; i++)
            {
                var objGenome = new GAGenome(SeachParams);
                lstPopulation.Add(objGenome);
            }
        }
        public void Epoch()
        {
            
        }

        public List<Coordinate> GetBestPath()
        {
            return new List<Coordinate>();
        }
    }
}
