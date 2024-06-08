using MgEngine.Component;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static MgEngine.UI.UITypes;
using MgEngine.Input;

namespace MgEngine.UI
{
    public abstract class Widget : Entity
    {
        public Visibility Visibility { get; set; }
        public bool IsEnabled { get; set; }
        public Margin Margin { get; set; }

        public Color DisabledColor { get; set; }

        public Widget() : base()
        { 
            Visibility = Visibility.Visible;
            IsEnabled = true;
            Margin = new Margin();
            DisabledColor = Color.Gray;
        }
        
        public Widget(Texture2D texture) : base(texture)
        {
            Visibility = Visibility.Visible;
            IsEnabled = true;
            Margin = new Margin();
            DisabledColor = Color.Gray;
        }

        public abstract void Update(Inputter inputter);
    }
}
