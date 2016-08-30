using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public static class MapSymbol
    {
        public static char Start { get; set; } = 'S';
        public static char End { get; set; } = 'E';
        public static char Empty { get; set; } = '-';
        public static char Block { get; set; } = '#';
        public static char Step { get; set; } = '*';
    }
}
