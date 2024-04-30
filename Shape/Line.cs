using Microsoft.Xna.Framework;

namespace MgEngine.Shape
{
    public class Line
    {
        #region Variables
        private Vector2 _p1;
        private Vector2 _p2;
        private int _width;

        private Vector2[] _vertices;
        #endregion

        #region Properties
        public Vector2 P1
        {
            get { return _p1; }

            set
            {
                _p1 = value;
                CalculateVertices();
            }
        }

        public Vector2 P2
        {
            get { return _p2; }

            set
            {
                _p2 = value;
                CalculateVertices();
            }
        }

        public int Width
        {
            get { return _width; }

            set
            {
                _width = value;

                CalculateVertices();
            }
        }

        public Vector2[] Vertices { get { return _vertices; } }

        public int Length
        {
            get { return (int)Vector2.Distance(_p1, _p2); }
        }

        #endregion

        #region Constructor
        public Line(Vector2 p1, Vector2 p2, int width = 1)
        {
            _p1 = p1;
            _p2 = p2;
            _width = width;
            
            _vertices = new Vector2[4];
            CalculateVertices();
        }

        public Line(int p1x, int p1y, int p2x, int p2y, int width = 1)
        {
            _p1 = new Vector2(p1x, p1y);
            _p2 = new Vector2(p2x, p2y);
            _width = width;

            _vertices = new Vector2[4];
            CalculateVertices();
        }
        #endregion

        #region Methods
        private void CalculateVertices()
        {
            float halfWidth = _width / 2f;

            Vector2 e1 = _p2 - _p1;
            e1.Normalize();
            e1 *= halfWidth;

            Vector2 e2 = -e1;
            Vector2 n1 = new Vector2(-e1.Y, e1.X);
            Vector2 n2 = -n1;

            _vertices[0] = _p1 + n1 + e2;
            _vertices[1] = _p2 + n1 + e1;
            _vertices[2] = _p2 + n2 + e1;
            _vertices[3] = _p1 + n2 + e2;
        }
        #endregion
    }
}