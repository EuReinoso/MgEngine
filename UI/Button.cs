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

        private TextWrap _textWrap;
        private FontGroup _font;
        private string _text;
        private HorizontalAlign _textAlign;
        private int _fontSize;

        public Color PressedColor { get; set; }
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
            _font = MgDefault.Font;
            _textAlign = HorizontalAlign.Center;
            _fontSize = 11;
            _text = "Button";
            ButtonColor = Color.White;
            FontColor = Color.Black;

            _textWrap = new(Font, FontSize, Text, Width * 0.8f, TextAlign);
        }

        #region Properties
        public FontGroup Font
        {
            get { return _font; }
            set { _font = value; _textWrap.Font = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; _textWrap.Text = value; }
        }

        public HorizontalAlign TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; _textWrap.TextAlign = value; }
        }

        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; _textWrap.FontSize = value; }
        }

        public new int Width
        {
            get { return base.Width; }

            set { base.Width = value; _textWrap.MaxWidth = value * 0.8f; }
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

        #endregion

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

            Vector2 measure = Font.MeasureString(_textWrap.WrapText, FontSize);

            Font.DrawText(spriteBatch, _textWrap.WrapText, Pos - measure / 2, FontSize, FontColor);
        }

    }
}
