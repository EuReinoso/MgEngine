using MgEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Component
{
    public class Platformer : EntityAnimated
    {
        public float JumpForce { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }

        public Keys KeyLeft { get; set; }
        public Keys KeyUp { get; set; }
        public Keys KeyRight { get; set; }
        public Keys KeyDown { get; set; }

        private bool _isMoveRight;
        private bool _isMoveLeft;
        private bool _isMoveUp;
        private bool _isMoveDown;

        public Platformer()
        {
            Initialize();
        }

        public Platformer(Animator animator) : base(animator)
        {
            Initialize();
        }

        private void Initialize()
        {
            JumpForce = 10;
            HorizontalSpeed = 5;
            VerticalSpeed = 3;

            KeyLeft = Keys.Left;
            KeyUp = Keys.Up;
            KeyRight = Keys.Right;
            KeyDown = Keys.Down;
        }

        public void UpdateMove(Inputter inputter, float dt)
        {
            if (inputter.KeyDown(KeyLeft))
            {
                _isMoveLeft = true;

                if (!_isMoveRight)
                    Effect = SpriteEffects.FlipHorizontally;
            }
            else if (inputter.KeyUp(KeyLeft))
            {
                _isMoveLeft = false;

                if (_isMoveRight)
                    Effect = SpriteEffects.None;
            }

            if (inputter.KeyDown(KeyRight))
            {
                _isMoveRight = true;

                if (!_isMoveLeft)
                    Effect = SpriteEffects.None;
            }
            else if (inputter.KeyUp(KeyRight))
            {
                _isMoveRight = false;

                if (_isMoveLeft)
                    Effect = SpriteEffects.FlipHorizontally;
            }

            if ((!_isMoveLeft && !_isMoveRight) || (_isMoveLeft && _isMoveRight))
                SetAction("Idle");
            else
                SetAction("Walk");

            if (inputter.KeyDown(KeyUp))
            {
                _isMoveUp = true;
            }
            else if (inputter.KeyUp(KeyUp))
            {
                _isMoveUp= false;
            }

            if (inputter.KeyDown(KeyDown))
            {
                _isMoveDown = true;
            }
            else if (inputter.KeyUp(KeyDown))
            {
                _isMoveDown = false;
            }

            Move(dt);
        }

        private void Move(float dt)
        {
            if (_isMoveRight)
                X += HorizontalSpeed * dt;

            if (_isMoveLeft)
                X -= HorizontalSpeed * dt;

            if (_isMoveUp)
                Y -= VerticalSpeed * dt;

            if (_isMoveDown)
                Y += VerticalSpeed * dt;
        }
    }
}
