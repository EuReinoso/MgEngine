using MgEngine.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Font;
using MgEngine.Util;

namespace MgEngine.UI
{
    public class Button : UIComponent
    {
        private bool _isPressed;
        private Color _buttonColor;

        public FontGroup Font;
        public string Text = "Button";
        public int FontSize = 11;
        public Color FontColor = Color.Black;
        public Color PressedColor;

        public Button() : base(MgDefault.ButtonTexture)
        {
            Font = MgDefault.Font;
            ButtonColor = Color.White;
        }

        public Button(Texture2D texture) : base(texture)
        {
            Font = MgDefault.Font;
            ButtonColor = Color.White;
        }

        public bool IsPressed { get {  return _isPressed; } }

        public Color ButtonColor 
        { 
            get { return _buttonColor; }

            set
            {
                _buttonColor = value;

                PressedColor = MgUtil.ColorLight(_buttonColor, 0.8f);
            }
        }

        public bool IsHover(Inputter inputter)
        {
            return Rect.IsCollidePoint(inputter.GetMousePos());
        }

        public bool IsClickDown(Inputter inputter)
        {
            if (IsHover(inputter) && inputter.IsMouseLeftDown())
            {
                _isPressed = true;
                return true;
            }

            _isPressed = false;
            return false;
        }

        public new void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0, Color? color = null)
        {
            Color btnColor = color ?? (_isPressed == true ? PressedColor : ButtonColor);

            btnColor = color ?? btnColor;

            base.Draw(spriteBatch, scrollX, scrollY, btnColor);

            Font.DrawText(spriteBatch, Text, Pos, FontSize, FontColor);
        }

    }
}
