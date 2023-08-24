using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Util
{
    public static class UGeneric
    {
        public static List<T> GetList<T>(params T[] args)
        {
            return new List<T>(args);
        }
    }
}
