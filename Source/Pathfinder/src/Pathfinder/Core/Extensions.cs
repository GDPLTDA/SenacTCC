using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public static class Extensions
    {
        public static T Pop<T>(this IList<T> list )
        {
            var ret = list.Last();
            list.Remove(ret);
            return ret;
        } 

        

    }
}
