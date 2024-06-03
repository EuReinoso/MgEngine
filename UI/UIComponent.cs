using MgEngine.Component;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MgEngine.UI.UITypes;

namespace MgEngine.UI
{
    public class UIComponent : Entity
    {
        public Visibility Visibility = Visibility.Visible;
        public bool IsEnabled = true;
        public Margin Margin = new();

        public UIComponent() : base()
        { 

        }
        
        public UIComponent(Texture2D texture) : base(texture)
        {

        }
    }
}
