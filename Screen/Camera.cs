using MgEngine.Component;
using MgEngine.Font;
using MgEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MgEngine.Screen
{
    public class Camera
    {
        #region Variables
        private float _minZ;
        private float _maxZ;

        private float _x;
        private float _y;
        private float _z;
        private float _baseZ;

        private float _aspectRatio;
        private float _fieldOfView;
        private float _zoom;

        private float _pitch;
        private float _yaw;
        private float _roll;

        private Matrix _projection;
        private Matrix _view;

        private Vector3 _up;
        private Vector3 _right;
        private Vector3 _front;

        private bool _updateRequired;
        #endregion

        public Camera(Window window)
        {
            _x = window.Center.X;
            _y = window.Center.Y;

            _view = Matrix.Identity;
            _projection = Matrix.Identity;

            _fieldOfView = MathHelper.PiOver2;
            _baseZ = -GetZFromHeight(window.Height);
            _z = _baseZ;
            _aspectRatio = (float)window.Width / (float)window.Height;

            _minZ = 1f;
            _maxZ = 2048f;

            _zoom = 1f;

            _right = -Vector3.UnitX;
            _up = -Vector3.UnitY;
            _front = Vector3.UnitZ;

            _pitch = 0;
            _yaw = 90f;
            _roll = 0;

            _updateRequired = true;

            Update();
        }

        #region Properties
        public Vector3 Pos 
        {  
            get { return new Vector3(_x, _y, _z); }  

            set 
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;

                _updateRequired = true;
            } 
        }

        public float X 
        { 
            get { return _x; } 
            set 
            {
                _updateRequired = true;

                _x = value;
            } 
        }

        public float Y
        {
            get { return _y; }
            set
            {
                _updateRequired = true;

                _y = value;
            }
        }

        public float Z
        {
            get { return _z; }
            set
            {
                _updateRequired = true;

                _z = value;
            }
        }

        public float Zoom
        {
            get { return _zoom; }

            set
            {
                if (value <= 0.01f)
                    value = 0.01f;
                else if (value >= 1)
                    value = 1;

                _zoom = value;
                _z = _baseZ * _zoom;
                _updateRequired = true;
            }
        }

        public float Rotation 
        {
            get { return _roll; }

            set 
            {
                _roll = value;
                _updateRequired = true;
            } 
        }

        public Matrix GetProjection() { return _projection; }

        public Matrix GetView() { return _view; }

        #endregion

        #region Methods
        private float GetZFromHeight(float height)
        {
            return (float)(height * 0.5d) / (float)Math.Tan(_fieldOfView * 0.5d);
        }

        public void Update()
        {
            if (!_updateRequired)
                return;

            UpdateVectors();

            _view = Matrix.CreateLookAt(Pos, Pos + _front, _up);

            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, _aspectRatio, _minZ, _maxZ);

            _updateRequired = false;

        }

        private void UpdateVectors()
        {
            if (_pitch > 89.0f)
            {
                _pitch = 89.0f;
            }
            if (_pitch < -89.0f)
            {
                _pitch = -89.0f;
            }

            _front.X = MathF.Cos(MathHelper.ToRadians(_pitch)) * MathF.Cos(MathHelper.ToRadians(_yaw));
            _front.Y = MathF.Sin(MathHelper.ToRadians(_pitch));
            _front.Z = MathF.Cos(MathHelper.ToRadians(_pitch)) * MathF.Sin(MathHelper.ToRadians(_yaw));
            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(-_right, _front));

            //ToDo _roll do not work yet

        }

        public void UpdateMoveKeys(Inputter inputter, float dt, float velocity = 10)
        {

            if (inputter.IsKeyDown(Keys.W))
            {
                Pos += _front * velocity * dt;
            }

            if (inputter.IsKeyDown(Keys.S))
            {
                Pos -= _front * velocity * dt;
            }

            if (inputter.IsKeyDown(Keys.A))
            {
                Pos += _right * velocity * dt;
            }
            if (inputter.IsKeyDown(Keys.D))
            {
                Pos -= _right * velocity * dt;
            }

            if (inputter.IsKeyDown(Keys.Space))
            {
                Pos += _up * velocity * dt;
            }

            if (inputter.IsKeyDown(Keys.LeftControl))
            {
                Pos -= _up * velocity * dt;
            }

            Vector2 mouseMov = inputter.GetMouseMovement();

            _yaw -= mouseMov.X * dt * inputter.MouseSense;
            _pitch += mouseMov.Y * dt * inputter.MouseSense;

            _updateRequired = true;
        }

        public void ResetZoom()
        {
            Zoom = 1;
        }

        #endregion

    }
}
