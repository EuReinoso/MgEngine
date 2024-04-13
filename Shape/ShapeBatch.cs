using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Screen;

namespace MgEngine.Shape
{
    public class ShapeBatch
    {
        #region Variables
        private GraphicsDevice _graphicsDevice;
        private int[] _rectIndexes;
        private BasicEffect _effects;
        private Window _window;

        #endregion

        #region Properties
        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }
        #endregion

        #region Constructor
        public ShapeBatch(GraphicsDevice graphicsDevice, Window window)
        {
            _graphicsDevice = graphicsDevice;
            _effects = new(_graphicsDevice);
            _rectIndexes = new int[6];
            _window = window;

            LoadEffects();
            LoadRectIndexes();
        }
        #endregion

        #region Loading
        private void LoadEffects()
        {
            _effects.Alpha = 1.0f;
            _effects.VertexColorEnabled = true;
            _effects.TextureEnabled = false;
            _effects.LightingEnabled = false;
            _effects.World = Matrix.Identity;
            _effects.View = Matrix.Identity;
            _effects.Projection = Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, 0f, 1f);
        }

        private void LoadRectIndexes()
        {
            _rectIndexes[0] = 0;
            _rectIndexes[1] = 1;
            _rectIndexes[2] = 2;
            _rectIndexes[3] = 0;
            _rectIndexes[4] = 2;
            _rectIndexes[5] = 3;
        }
        #endregion

        private void ReloadProjection()
        {
            _effects.Projection = Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, 0f, 1f);
        }

        #region Draw
        public void DrawRect(Rect rect, Color color)
        {
            ReloadProjection();

            VertexPositionColor[] vertices = new VertexPositionColor[4];

            vertices[0] = new VertexPositionColor(new Vector3(rect.Vertices[0], 0f), color);
            vertices[1] = new VertexPositionColor(new Vector3(rect.Vertices[1], 0f), color);
            vertices[2] = new VertexPositionColor(new Vector3(rect.Vertices[2], 0f), color);
            vertices[3] = new VertexPositionColor(new Vector3(rect.Vertices[3], 0f), color);

            foreach (EffectPass pass in _effects.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    4,
                    _rectIndexes,
                    0,
                    2);
            }
        }

        public void DrawLine(Line line, Color color)
        {
            ReloadProjection();

            VertexPositionColor[] vertices = new VertexPositionColor[4];

            vertices[0] = new VertexPositionColor(new Vector3(line.Vertices[0], 0f), color);
            vertices[1] = new VertexPositionColor(new Vector3(line.Vertices[1], 0f), color);
            vertices[2] = new VertexPositionColor(new Vector3(line.Vertices[2], 0f), color);
            vertices[3] = new VertexPositionColor(new Vector3(line.Vertices[3], 0f), color);

            foreach (EffectPass pass in _effects.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    4,
                    _rectIndexes,
                    0,
                    2);
            }
        }

        #endregion

    }
}
