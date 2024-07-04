using MgEngine.Component;
using MgEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Effect
{
#pragma warning disable CS8618
    public static class Particlerr
    {
        private static List<ParticleEntity> _particles;
        private static Dictionary<object, ParticleMoveEffect> _moveEffects;

        public static void Initialize()
        {
            _particles = new();
            _moveEffects = new();
        }

        public static void AddMoveEffect(object key, ParticleMoveEffect moveEffect)
        {
            _moveEffects.Add(key, moveEffect);
        }

        public static void Add(int quant, ParticleShape shape, object moveEffectKey, Vector2 pos, Color color)
        {
            Texture2D texture;

            if (shape == ParticleShape.Circle)
                texture = MgDefault.CircleTexture;
            else if (shape == ParticleShape.Rect)
                texture = MgDefault.RectTexture;
            else if (shape == ParticleShape.Triangle)
                texture = MgDefault.TriangleTexture;
            else
                texture = MgDefault.CircleTexture;

            Add(quant, texture, moveEffectKey, pos, color);
        }

        public static void Add(int quant, ParticleShape shape, object moveEffectKey, Vector2 minPos, Vector2 maxPos, Color color)
        {
            for(int i = 0; i < quant; i++)
            {
                float x = MgMath.RandFloat(minPos.X, maxPos.X);
                float y = MgMath.RandFloat(minPos.Y, maxPos.Y);

                Add(1, shape, moveEffectKey, new Vector2(x, y), color);
            }
        }

        public static void Add(int quant, Texture2D texture, object moveEffectKey, Vector2 pos, Color color)
        {
            for (int i = 0; i < quant; i++)
            {
                var particle = new ParticleEntity(texture, _moveEffects[moveEffectKey], pos);
                particle.ColorEffect = color;
                _particles.Add(particle);
            }
        }

        public static void Add(int quant, Texture2D texture, object moveEffectKey, Vector2 minPos, Vector2 maxPos, Color color)
        {
            for (int i = 0; i < quant; i++)
            {
                float x = MgMath.RandFloat(minPos.X, maxPos.X);
                float y = MgMath.RandFloat(minPos.Y, maxPos.Y);

                Add(1, texture, moveEffectKey, new Vector2(x, y), color);
            }
        }

        public static void Update(float dt)
        {
            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                var p = _particles[i];

                p.Update(dt);

                if (p.Destroy)
                    _particles.RemoveAt(i);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            foreach(var p in _particles)
            {
                p.Draw(spriteBatch, scrollX, scrollY);
            }
        }

    }
}
