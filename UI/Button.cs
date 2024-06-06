using MgEngine.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Font;
using MgEngine.Util;
using MgEngine.Shape;
using static MgEngine.UI.UITypes;

#pragma warning disable CS8618
namespace MgEngine.UI
{
    public class Button : UIComponent
    {
        private bool _isPressed;
        private Color _buttonColor;
        private bool _isHover;

        public FontGroup Font;
        public Color PressedColor { get; set; }
        public string Text { get; set; }
        public HorizontalAlign TextAlign { get; set; }
        public int FontSize { get; set; }
        public Color FontColor { get; set; }

        public Button() : base(MgDefault.ButtonTexture)
        {
            Initialize();
        }

        public Button(Texture2D texture) : base(texture)
        {
            Initialize();
        }

        private void Initialize()
        {
            Font = MgDefault.Font;
            ButtonColor = Color.White;
            TextAlign = HorizontalAlign.Center;
            FontSize = 11;
            FontColor = Color.Black;
            Text = "Button";
        }

        public bool IsPressed { get {  return _isPressed; } }

        public bool IsHover { get { return _isHover; } }

        public Color ButtonColor 
        { 
            get { return _buttonColor; }

            set
            {
                _buttonColor = value;

                PressedColor = MgUtil.ColorLight(_buttonColor, 0.8f);
            }
        }

        public event Action? OnClick;

        public override void Update(Inputter inputter)
        {
            _isHover = Rect.IsCollidePoint(inputter.GetMousePos());

            if (_isHover && inputter.IsMouseLeftDown())
                _isPressed = true;
            else
                _isPressed = false;

            if (_isHover && inputter.MouseLeftDown())
                OnClick?.Invoke();
        }

        public new void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            ColorEffect = _isPressed == true ? PressedColor : ButtonColor;

            base.Draw(spriteBatch, scrollX, scrollY);

            var text = new TextWrap(Font, FontSize, Text, Width * 0.8f, TextAlign);

            Vector2 measure = Font.MeasureString(text.WrapText, FontSize);

            Font.DrawText(spriteBatch, text.WrapText, Pos - measure / 2, FontSize, FontColor);
        }

    }
}
