using MgEngine.Input;
using MgEngine.Sprites;
using MgEngine.Time;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Scene
{
    public abstract class Scene
    {
        public Scene() 
        {
        }

        public abstract void Initialize();

        public abstract void LoadContent(SpritesDraw sprites, ContentManager content);

        public abstract void Update(float dt, Inputter inputter);

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
