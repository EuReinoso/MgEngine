using MgEngine.Audio;
using MgEngine.Input;
using MgEngine.Shape;
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
        public byte JumpLimit { get; set; }
        public float JumpBuffer { get; set; }
        protected byte _jumps { get; set; }
        protected bool _jumpBufferActive { get; set; }
        protected bool _jumpActive {  get; set; }

        public Keys KeyLeft { get; set; }
        public Keys KeyUp { get; set; }
        public Keys KeyRight { get; set; }
        public Keys KeyDown { get; set; }
        public Keys KeyJump { get; set; }

        public bool EnableVerticalMovement { get; set; }

        protected bool _isMoveRight;
        protected bool _isMoveLeft;
        protected bool _isMoveUp;
        protected bool _isMoveDown;

        public event Action? JumpKeyDown;
        public event Action? OnCollideBottom;

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
            JumpForce = 12;
            JumpLimit = 1;
            JumpBuffer = 80;
            _jumps = JumpLimit;
            HorizontalSpeed = 6;
            VerticalSpeed = 3;
            EnableVerticalMovement = false;

            KeyLeft = Keys.Left;
            KeyUp = Keys.Up;
            KeyRight = Keys.Right;
            KeyDown = Keys.Down;
            KeyJump = Keys.Space;
        }

        public virtual void UpdateCollision(List<Entity> tiles)
        {
            foreach(var tile in tiles)
            {
                if (Polygon.CollidePolygon(Rect.Vertices.ToList(), tile.Rect.Vertices.ToList(), out Vector2 normal, out float depth))
                {
                    Pos -= normal * depth;

                    if (normal.Y > 0) 
                    {
                        Velocity = new Vector2(Velocity.X, 0);
                        _jumps = JumpLimit;
                        OnCollideBottom?.Invoke();
                    }
                }
                else
                {
                    var jumpBufferRect = Rect;

                    jumpBufferRect.Y += JumpBuffer;

                    if (Polygon.CollidePolygon(jumpBufferRect.Vertices.ToList(), tile.Rect.Vertices.ToList(), out Vector2 normalB, out float depthB))
                    {
                        if (normalB.Y > 0 && _jumpActive)
                            _jumpBufferActive = true;
                    }
                }
            }

            _jumpActive = false;
        }

        public virtual void UpdateMove(Inputter inputter)
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

            if (inputter.KeyDown(KeyJump) || _jumpBufferActive)
            {
                _jumpActive = true;

                JumpKeyDown?.Invoke();

                if (_jumps > 0)
                {
                    _jumps--;
                    _jumpBufferActive = false;
                    _jumpActive = false;
                    Velocity += new Vector2(0, -JumpForce);
                    Singer.PlaySound("Jump", 1, true);
                }
            }
        }

        public virtual void UpdatePhysics(Physics physics, float dt)
        {
            Velocity += physics.Gravity * Mass * dt;
        }

        public virtual void Move(float dt)
        {
            if (_isMoveRight)
                X += HorizontalSpeed * dt;

            if (_isMoveLeft)
                X -= HorizontalSpeed * dt;

            if (_isMoveUp && EnableVerticalMovement)
                Y -= VerticalSpeed * dt;

            if (_isMoveDown && EnableVerticalMovement)
                Y += VerticalSpeed * dt;

            Pos += Velocity * dt;
        }

    }
}
