using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.UI
{
    public class UITypes
    {
        public enum Visibility
        {
            Visible,
            Hidden,
            Collapse
        }

        public enum HorizontalAlign
        {
            Left,
            Center,
            Right
        }

        public enum VerticalAlign
        {
            Top,
            Center,
            Bottom
        }

        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        public enum SizeMode
        {
            Auto,
            Fixed
        }

    }
}
