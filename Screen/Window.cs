using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Screen
{
    public class Window
    {
        private GraphicsDeviceManager _graphics;
        private Canvas _canvas;
        private SpriteBatch _spriteBatch;

        public Window(GraphicsDeviceManager graphics, SpriteBatch spriteBatch,int canvasWidth = 768, int canvasHeight = 480)
        {
            _graphics = graphics;
            _canvas = new(_graphics.GraphicsDevice, canvasWidth, canvasHeight);
            _canvas.SetDestinationRectangle();
            _spriteBatch = spriteBatch;

            SetResolution(1280, 800);
        }

        public int Width { get { return _graphics.PreferredBackBufferWidth; } }
        public int Height { get { return _graphics.PreferredBackBufferHeight; } }
        public Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }

        #region Methods
        public RenderTarget2D GetRenderTarget()
        {
            return _canvas.RenderTarget;
        }

        public void SetResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();

            _canvas.SetDestinationRectangle();
        }

        public void ToggleFullScreen()
        {
            _graphics.ToggleFullScreen();
            _canvas.SetDestinationRectangle();
        }

        public void Begin(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            blendState = blendState ?? BlendState.AlphaBlend;
            samplerState = samplerState ?? SamplerState.PointClamp;

            _canvas.Activate();
            _spriteBatch.Begin(spriteSortMode, blendState, samplerState);
        }

        public void End(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            blendState = blendState ?? BlendState.AlphaBlend;
            samplerState = samplerState ?? SamplerState.PointClamp;

            _spriteBatch.End();
            _canvas.Dispose();

            _canvas.Draw(_spriteBatch, spriteSortMode, blendState, samplerState);
        }
        #endregion

    }

}
