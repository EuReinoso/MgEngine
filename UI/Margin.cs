using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.UI
{

    public struct Margin
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public Margin(short left = 0, short top = 0, short right = 0, short bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
