using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;


namespace AStar
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var program = new AStar();
            
            //seta enconding correto no programa, bug do core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            program.Run();
        }
    }
}
