using Microsoft.Xna.Framework;

namespace MgEngine.Screen
{
    public class Camera
    {
        private float _minZ = 1f;
        private float _maxZ = 2000f;

        private double _x;
        private double _y;
        private double _z;
        private double _baseZ;
        private float _angle;

        private float _aspectRatio;
        private float _fieldOfView;
        private float _zoom;

        private Matrix _projection;
        private Matrix _view;
        private Vector2 _up;
        private Vector2 _center;

        private bool _updateRequired;

        public Camera(Window window)
        {
            _center = window.Center;
            _x = window.Center.X;
            _y = window.Center.Y;


            _view = Matrix.Identity;
            _projection = Matrix.Identity;

            _fieldOfView = MathHelper.PiOver2;
            _baseZ = GetZFromHeight((double)window.Height);
            _z = _baseZ;
            _aspectRatio = (float)window.Width / (float)window.Height;

            _angle = 0f;
            _zoom = 1f;
            _up = new Vector2(MathF.Sin(_angle), MathF.Cos(_angle));

            _updateRequired = true;
            Update();
        }

        public double X { get { return _x; } set { _x = value; } }
        public double Y { get { return _y; } set { _y = value; } }
        public double Z { get { return _z; } set { _z = value; } }

        public Matrix GetProjection() { return _projection; }

        public Matrix GetView() { return _view; }

        private double GetZFromHeight(double height)
        {
            return (height * 0.5d) / Math.Tan(_fieldOfView * 0.5d);
        }

        public void Update()
        {
            //if (_updateRequired)
            //{
            //    return;
            //}

            _view = Matrix.CreateLookAt(new Vector3((float)_x, (float)_y, (float)_z), new Vector3 (_center, 0), Vector3.Down);
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, _aspectRatio, _minZ, _maxZ);

            _updateRequired = false;
        }
    }
}
