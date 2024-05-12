using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MgEngine.Screen;

namespace MgEngine.Input
{
    public class Inputter
    {
        private KeyboardState _keyState;
        private KeyboardState _lastKeyState;
        private MouseState _mouseState;
        private MouseState _lastMouseState;

        private float _mouseSense;
        private Window _window;

        public Inputter(Window window)
        {
            _mouseSense = 0.5f;
            _window = window;
        }

        public void Update(KeyboardState keyState, MouseState mouseState)
        {
            _lastKeyState = _keyState;
            _keyState = keyState;
            _lastMouseState = _mouseState;
            _mouseState = mouseState;
        }

        #region KeyBoard
        public bool KeyDown(Keys key)
        {
            return (_keyState.IsKeyDown(key) && _lastKeyState.IsKeyUp(key));
        }

        public bool KeyUp(Keys key)
        {
            return (_keyState.IsKeyUp(key) && _lastKeyState.IsKeyDown(key));
        }

        public bool IsKeyDown(Keys key)
        {
            return _keyState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _keyState.IsKeyUp(key);
        }
        #endregion

        #region Mouse
        public float MouseSense 
        { 
            get { return _mouseSense; } 

            set {_mouseSense = value; } 
        }

        public Vector2 GetMousePos()
        {
            Vector2 ratio = _window.GetCanvasRatio();

            return new Vector2(_mouseState.X * ratio.X, _mouseState.Y * ratio.Y);
        }

        public void SetMousePos(Vector2 pos)
        {
            Mouse.SetPosition((int)pos.X, (int)pos.Y);
        }

        public bool MouseLeftDown()
        {
            return (_mouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released);
        }

        public bool MouseRightDown()
        {
            return (_mouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released);
        }

        public bool IsMouseLeftDown()
        {
            return _mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsMouseRightDown()
        {
            return _mouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMouseCollide(Point pos)
        {
            return _mouseState.Position == pos;
        }

        public bool IsMouseCollide(Vector2 pos)
        {
            return IsMouseCollide(new Point((int)pos.X, (int)pos.Y));
        }

        public bool IsMouseCollide(int x, int y)
        {
            return IsMouseCollide(new Point(x, y));
        }

        public Vector2 GetMouseMovement()
        {
            float x =  _mouseState.Position.X - _lastMouseState.Position.X;
            float y =  _mouseState.Position.Y - _lastMouseState.Position.Y;
            return new Vector2(x, y);
        }
       #endregion
    }

}
