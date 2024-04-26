using MgEngine.Input;
using MgEngine.Screen;
using MgEngine.Shape;
using MgEngine.Time;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Scene
{
    public abstract class Scene
    {
        public Window Window;
        public Camera Camera;
        public Scroller Scroller;

        public Scene(Window window, Camera camera) 
        {
            Window = window;
            Camera = camera;
            Scroller = new Scroller(window.Canvas);
        }

        public abstract void Initialize();

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(float dt, Inputter inputter);

        public abstract void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);

        public void NextLayer(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            Window.NextLayer(spriteSortMode, blendState, samplerState);
        }
    }
}
