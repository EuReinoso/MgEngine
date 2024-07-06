using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MgEngine.Font
{
    public class FontGroup
    {
        private Dictionary<int, SpriteFont> _spriteFonts;

        public FontGroup(ContentManager content, string path, List<int> sizes)
        {
            _spriteFonts = new();

            foreach (int size in sizes)
            {
                _spriteFonts.Add(size, content.Load<SpriteFont>(path + size.ToString()));
            }
        }


        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 Pos, int size, Color color, float rotation = 0)
        {
            spriteBatch.DrawString(_spriteFonts[size], text, Pos, color, rotation, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }

        public Vector2 MeasureString(string text, int size)
        {
            return _spriteFonts[size].MeasureString(text);
        }

    }
}
