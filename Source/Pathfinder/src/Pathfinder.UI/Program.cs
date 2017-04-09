using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Pathfinder.Factories;
using Pathfinder.Abstraction;
using Pathfinder.UI.Abstraction;
namespace Pathfinder.UI.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RegisterConfig.BindProjectRegisters();
            var app = Container.Resolve<IAppMode>();
            app.Run();
        }
    }
}
