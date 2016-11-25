using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public interface IViewer
    {
        void Run(IMap map);
        void SetFinder(IFinder finder);
    }
}
