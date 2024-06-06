using MgEngine.Component;
using MgEngine.Shape;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using static MgEngine.UI.UITypes;

#pragma warning disable CS8618
namespace MgEngine.Font
{
    public class TextWrap
    {
        private string _text;
        private string _wrapText;

        public FontGroup Font { get; set; }
        public int FontSize { get; set; }
        public float MaxWidth { get; set; }
        public HorizontalAlign TextAlign { get; set; }

        public TextWrap(FontGroup font, int fontSize, string text, float maxWidth, HorizontalAlign textAlign = HorizontalAlign.Center)
        {
            Font = font;
            FontSize = fontSize;
            _text = text;
            MaxWidth = maxWidth;
            TextAlign = textAlign;

            Measure();
        }

        public string Text
        {
            get { return _text; }

            set
            {
                _text = value;
                Measure();
            }
        }

        public string WrapText 
        {
            get { return _wrapText; }
        }


        private void Measure()
        {
            string[] words = Text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = Font.MeasureString(" ", FontSize).X;

            string line = "";
            int count = 0;

            foreach (string word in words)
            {
                Vector2 size = Font.MeasureString(word, FontSize);

                if (lineWidth + size.X < MaxWidth)
                {
                    line += word + " ";
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    line = GetTextAlign(line, lineWidth, spaceWidth);

                    sb.Append(line + "\n");
                    line = word + " ";
                    lineWidth = size.X + spaceWidth;
                }

                if (count == words.Length - 1)
                {
                    line = GetTextAlign(line, lineWidth, spaceWidth);
                    sb.Append(line);
                }

                count++;
            }

            _wrapText = sb.ToString();
        }

        private string GetTextAlign(string text, float textWidth, float spaceWidth)
        {
            float totalWidth = textWidth;

            while(totalWidth < MaxWidth)
            {
                if (TextAlign == HorizontalAlign.Center)
                {
                    text = " " + text + " ";
                    totalWidth += spaceWidth * 2;
                }
                else if (TextAlign == HorizontalAlign.Left)
                {
                    text = text + " ";
                    totalWidth += spaceWidth;
                }
                else if (TextAlign == HorizontalAlign.Right)
                {
                    text = " " + text;
                    totalWidth += spaceWidth;
                }
            }

            return text;
        }

    }
}
