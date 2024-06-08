using MgEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.UI
{

    public struct Margin
    {
        public short Left { get; set; }
        public short Top { get; set; }
        public short Right { get; set; }
        public short Bottom { get; set; }

        public Margin(short left = 0, short top = 0, short right = 0, short bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public readonly int Width
        {
            get { return Left + Right;}
        }

        public readonly int Height
        {
            get { return Top + Bottom; }
        }

        public static Margin Tiny
        {
            get { return new(3, 3, 3, 3); }
        }

        public static Margin Regular
        {
            get { return new(5, 5, 5, 5); }
        }

        public static Margin Big
        {
            get { return new(10, 10, 10, 10); }
        }
    }
}
