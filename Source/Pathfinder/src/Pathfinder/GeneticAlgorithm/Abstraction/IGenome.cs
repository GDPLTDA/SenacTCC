using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
    public interface IGenome
    {
        IMap Map { get; set; }
        List<Node> ListNodes { get; set; }
        double Fitness { get; set; }
        bool IsEqual(IGenome genome);
    }
}