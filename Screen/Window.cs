using MgEngine.Shape;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#pragma warning disable CS8618
namespace MgEngine.Screen
{
    public class Window
    {
        #region Variables
        private GraphicsDeviceManager _graphics;
        private Canvas _canvas;
        private SpriteBatch _spriteBatch;
        private BasicEffect _effects;
        private Color _backGroundColor;
        private Camera _camera;

        #endregion

        public Window(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, int canvasWidth = 768, int canvasHeight = 480)
        {
            _graphics = graphics;
            _canvas = new(_graphics.GraphicsDevice, canvasWidth, canvasHeight);
            _canvas.SetDestinationRectangle();
            _spriteBatch = spriteBatch;

            _effects = new BasicEffect(_graphics.GraphicsDevice);
            _effects.FogEnabled = false;
            _effects.LightingEnabled = false;
            _effects.PreferPerPixelLighting = false;
            _effects.VertexColorEnabled = true;
            _effects.Texture = null;
            _effects.TextureEnabled = true;
            _effects.Projection = Matrix.Identity;
            _effects.View = Matrix.Identity;
            _effects.World = Matrix.Identity;

            SetResolution(1280, 800);
        }

        #region Properties
        public int Width { get { return _graphics.PreferredBackBufferWidth; } }
        public int Height { get { return _graphics.PreferredBackBufferHeight; } }
        public Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }

        public Canvas Canvas { get { return _canvas; } }

        #endregion

        #region Methods

        public Vector2 GetCanvasRatio()
        {
            return new Vector2((float)Canvas.Width / (float)Width, (float)Canvas.Height / (float)Height);
        }

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public void SetBackGroundColor(Color color)
        {
            _backGroundColor = color;
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

        public void ReloadProjection()
        {
            if (_camera == null)
            {
                _effects.View = Matrix.Identity;
                _effects.Projection = Matrix.CreateOrthographicOffCenter(0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height, 0, 0f, 1f);

                return;
            }

            _effects.View = _camera.GetView();
            _effects.Projection = _camera.GetProjection();
        }

        public void Begin(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            blendState = blendState ?? BlendState.AlphaBlend;
            samplerState = samplerState ?? SamplerState.PointClamp;

            ReloadProjection();

            if (_camera != null)
                _camera.Update();

            _canvas.Activate(_backGroundColor);

            _spriteBatch.Begin(spriteSortMode, blendState, samplerState);
        }

        public void End(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            blendState = blendState ?? BlendState.AlphaBlend;
            samplerState = samplerState ?? SamplerState.PointClamp;

            _spriteBatch.End();

            _canvas.Dispose();

            _canvas.Draw(_spriteBatch, spriteSortMode, blendState, samplerState, _effects);
        }

        public void NextLayer(SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null)
        {
            blendState = blendState ?? BlendState.AlphaBlend;
            samplerState = samplerState ?? SamplerState.PointClamp;

            _spriteBatch.End();
            _spriteBatch.Begin(spriteSortMode, blendState, samplerState);
        }

        #endregion

    }

}
