﻿using MgEngine.Input;
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
        private byte _jumps { get; set; }
        private bool _jumpBufferActive { get; set; }
        private bool _jumpActive {  get; set; }

        public Keys KeyLeft { get; set; }
        public Keys KeyUp { get; set; }
        public Keys KeyRight { get; set; }
        public Keys KeyDown { get; set; }
        public Keys KeyJump { get; set; }

        public bool EnableVerticalMovement { get; set; }

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
            JumpForce = 15;
            JumpLimit = 1;
            JumpBuffer = 70;
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

        public void UpdateCollision(List<Entity> tiles)
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

            if (inputter.KeyDown(KeyJump) || _jumpBufferActive)
            {
                _jumpActive = true;

                if (_jumps > 0)
                {
                    _jumps--;
                    _jumpBufferActive = false;
                    _jumpActive = false;
                    Velocity += new Vector2(0, -JumpForce);
                }
            }

            Move(dt);
        }

        public void UpdatePhysics(Physics physics, float dt)
        {
            Velocity += physics.Gravity * dt;
        }

        private void Move(float dt)
        {
            if (_isMoveRight)
                X += HorizontalSpeed * dt;

            if (_isMoveLeft)
                X -= HorizontalSpeed * dt;

            if (_isMoveUp && EnableVerticalMovement)
                Y -= VerticalSpeed * dt;

            if (_isMoveDown && EnableVerticalMovement)
                Y += VerticalSpeed * dt;

            Pos += Velocity;
        }

    }
}
