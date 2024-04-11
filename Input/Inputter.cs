﻿using Microsoft.Xna.Framework.Input;

namespace MgEngine.Input
{
    public class Inputter
    {
        private KeyboardState _keyState;
        private KeyboardState _lastKeyState;

        public Inputter()
        {
        }

        public void Update(KeyboardState keyState)
        {
            _lastKeyState = _keyState;
            _keyState = keyState;
        }

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
    }
}