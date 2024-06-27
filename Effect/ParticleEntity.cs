using MgEngine.Component;
using MgEngine.Input;
using MgEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Effect
{
    public class ParticleEntity : Entity
    {
        public bool Destroy { get; set; }
        private ParticleMoveEffect _moveEffect;
        private float _trueSize;

        public ParticleEntity(Texture2D texture, ParticleMoveEffect moveEffect, Vector2 pos) : base(texture)
        {
            _moveEffect = moveEffect;
            Pos = pos;
            Initialize();
        }

        private void Initialize()
        {
            int size = MgMath.RandInt(_moveEffect.SizeMinStart, _moveEffect.SizeMaxStart);
            Width = size;
            Height = size;
            _trueSize = size;

            float xVel = MgMath.RandFloat(_moveEffect.VelocityMinStart.X, _moveEffect.VelocityMaxStart.X);
            float yVel = MgMath.RandFloat(_moveEffect.VelocityMinStart.Y, _moveEffect.VelocityMaxStart.Y);
            Velocity = new Vector2(xVel, yVel);

            RotationVelocity = MgMath.RandFloat(_moveEffect.MinRotationVelocity, _moveEffect.MaxRotationVelocity);
        }

        public void Update(float dt)
        {
            _trueSize -= _moveEffect.SizeDecay * dt;

            Width = (int)_trueSize;
            Height = (int)_trueSize;

            if (_moveEffect.IsSizeClampOn)
            {
                int clampSize = MgMath.Clamp(Width, _moveEffect.MinSize, _moveEffect.MaxSize);

                Width = clampSize;
                Height = clampSize;
            }

            Rotation += RotationVelocity;

            Pos += Velocity * dt;

            Velocity += _moveEffect.VelocityDecay + _moveEffect.Wind + _moveEffect.Gravity;

            if (Width <= 0 || Height <= 0)
                Destroy = true;
        }
    }
}
