using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Screen;
using System;

namespace MgEngine.Shape
{
    public class ShapeBatch
    {
        #region Variables
        private GraphicsDevice _graphicsDevice;
        private int[] _rectIndexes;
        private BasicEffect _effects;

        private VertexPositionColor[] _vertices;
        private int[] _indices;

        private int _verticesCount;
        private int _indexCount;
        public int _maxVertices;

        #endregion

        #region Properties
        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }
        #endregion

        #region Constructor
        public ShapeBatch(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _effects = new(_graphicsDevice);
            _rectIndexes = new int[6];

            _maxVertices = 2048;
            _vertices = new VertexPositionColor[_maxVertices];
            _indices = new int[_vertices.Length * 3];
            _verticesCount = 0;
            _indexCount = 0;

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

        #region Methods
        public void Begin()
        {
            _effects.View = Matrix.Identity;
            _effects.Projection = Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, 0f, 1f);
        }

        private void Draw()
        {
            if (_verticesCount == 0)
                return;

            int primitiveCount = _indexCount / 3;

            foreach (EffectPass pass in _effects.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    _verticesCount,
                    _indices,
                    0,
                    primitiveCount);
            }

            _verticesCount = 0;
            _indexCount = 0;
        }

        public void End()
        {
            Draw();
        }

        private void VerifyVerticeSpace(int verticesNeeded)
        {
            if (_verticesCount + verticesNeeded >= _maxVertices)
                Draw();
        }

        public void SetMaxVerticesSize(int newSize)
        {
            _maxVertices = newSize;
            _vertices = new VertexPositionColor[newSize];
            _indices = new int[newSize * 3];
        }

        public void DrawRect(Rect rect, Color color)
        {
            VerifyVerticeSpace(4);

            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 1 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 3 + _verticesCount;

            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(rect.Vertices[0], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(rect.Vertices[1], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(rect.Vertices[2], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(rect.Vertices[3], 0f), color);
        }

        public void DrawLine(Line line, Color color)
        {
            VerifyVerticeSpace(4);

            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 1 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 3 + _verticesCount;

            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(line.Vertices[0], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(line.Vertices[1], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(line.Vertices[2], 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(line.Vertices[3], 0f), color);
        }


        #endregion

    }
}
