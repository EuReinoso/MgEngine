using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Font
{
    public class Font
    {
        private SpriteFont _spriteFont;

        public Font(ContentManager content, string path)
        {
            _spriteFont = content.Load<SpriteFont>(path);
        }

        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 Pos, Color color, float rotation = 0)
        {
            spriteBatch.DrawString(_spriteFont, text, Pos, color, rotation, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }

    }
}
