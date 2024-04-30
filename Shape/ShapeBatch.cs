using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Screen;
using System;
using MgEngine.Util;

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

        public void DrawLine(float p1x, float p1y, float p2x, float p2y, float lineWidth, Color color)
        {
            VerifyVerticeSpace(4);

            float halfWidth = lineWidth / 2f;

            float e1x = p2x - p1x;
            float e1y = p2y - p1y;

            MgMath.Normalize(ref e1x, ref e1y);

            e1x *= halfWidth;
            e1y *= halfWidth;

            float e2x = -e1x;
            float e2y = -e1y;

            float n1x = -e1y;
            float n1y = e1x;

            float n2x = -n1x;
            float n2y = -n1y;

            float ax = p1x + n1x + e2x;
            float ay = p1y + n1y + e2y;

            float bx = p2x + n1x + e1x;
            float by = p2y + n1y + e1y;

            float cx = p2x + n2x + e1x;
            float cy = p2y + n2y + e1y;

            float dx = p1x + n2x + e2x;
            float dy = p1y + n2y + e2y;

            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 1 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 0 + _verticesCount;
            _indices[_indexCount++] = 2 + _verticesCount;
            _indices[_indexCount++] = 3 + _verticesCount;

            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(ax, ay, 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(bx, by, 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(cx, cy, 0f), color);
            _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(dx, dy, 0f), color);
        }

        public void DrawCircle(Circle circle, Color color, int lineWidth = 1)
        {
            for (int i = 0; i < circle.Vertices.Length; i++)
            {
                float p1x = circle.Vertices[i].X;
                float p1y = circle.Vertices[i].Y;

                float p2x = circle.Vertices[(i + 1) % circle.Vertices.Length].X;
                float p2y = circle.Vertices[(i + 1) % circle.Vertices.Length].Y;

                DrawLine(p1x, p1y, p2x, p2y, lineWidth, color);
            }
        }

        public void DrawPolygon(Vector2[] vertices, Color color, int lineWidth = 1)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                float p1x = vertices[i].X;
                float p1y = vertices[i].Y;

                float p2x = vertices[(i + 1) % vertices.Length].X;
                float p2y = vertices[(i + 1) % vertices.Length].Y;

                DrawLine(p1x, p1y, p2x, p2y, lineWidth, color);
            }
        }


        #endregion

    }
}
