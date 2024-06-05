using MgEngine.Component;
using MgEngine.Shape;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace MgEngine.Font
{
    public class TextWrap
    {
        public FontGroup Font;
        public int FontSize;
        private string _text;
        public float MaxWidth;
        private string _wrapText;

        public TextWrap(FontGroup font, int fontSize, string text, float maxWidth)
        {
            Font = font;
            FontSize = fontSize;
            _text = text;
            MaxWidth = maxWidth;

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
                    //sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    line = TextAlign(line, lineWidth + size.X, spaceWidth);

                    sb.Append(line + "\n");
                    line = word + " ";
                    lineWidth = size.X + spaceWidth;
                }

                if (count == words.Length - 1)
                {
                    line = TextAlign(line, lineWidth + size.X, spaceWidth);
                    sb.Append(line);
                }

                count++;
            }

            _wrapText = sb.ToString();
        }

        private string TextAlign(string text, float textWidth, float spaceWidth)
        {
            float totalWidth = textWidth;

            while(totalWidth < MaxWidth)
            {
                text = " " + text + " ";
                totalWidth += spaceWidth;
            }

            return text;
        }

        //private void Measure()
        //{
        //    string[] words = Text.Split(' ');
        //    StringBuilder sb = new StringBuilder();
        //    float lineWidth = 0f;
        //    float spaceWidth = Font.MeasureString(" ", FontSize).X;

        //    foreach (string word in words)
        //    {
        //        Vector2 size = Font.MeasureString(word, FontSize);

        //        if (lineWidth + size.X < MaxWidth)
        //        {
        //            sb.Append(word + " ");
        //            lineWidth += size.X + spaceWidth;
        //        }
        //        else
        //        {
        //            sb.Append("\n" + word + " ");
        //            lineWidth = size.X + spaceWidth;
        //        }
        //    }

        //    _wrapText = sb.ToString();
        //}

    }
}
