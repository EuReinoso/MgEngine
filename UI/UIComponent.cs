using MgEngine.Component;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static MgEngine.UI.UITypes;
using MgEngine.Input;

namespace MgEngine.UI
{
    public abstract class UIComponent : Entity
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

        public abstract void Update(Inputter inputter);
    }
}
