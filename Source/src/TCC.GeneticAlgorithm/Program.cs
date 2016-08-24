using TCC.GeneticAlgorithm;

namespace TCC.GeneticAlgorithm
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var program = new GATSP(new GAParams());
            

            program.Epoch();
        }
    }
}
