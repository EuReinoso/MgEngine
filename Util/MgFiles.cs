using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Util
{
    public static class MgFiles
    {

        public static string RootDirectory 
        {   
            get {return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;} 
        }

    }
}
