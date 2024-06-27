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
        private static ContentManager _content;
        private static List<ParticleEntity> _particles;
        private static Dictionary<object, ParticleMoveEffect> _moveEffects;
        private static Dictionary<object, Texture2D> _textures;

        public static void Initialize(ContentManager content)
        {
            _content = content;
            _particles = new();
            _moveEffects = new();
        }

        public static void AddMoveEffect(object key, ParticleMoveEffect moveEffect)
        {
            _moveEffects.Add(key, moveEffect);
        }

        private static void AddTexture(object textureKey, string path)
        {
            _textures.Add(textureKey, _content.Load<Texture2D>(path));
        }
        public static void Add(ParticleShape shape, object moveEffectKey, int quant, Vector2 pos, Color color)
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

            for (int i = 0; i < quant; i++)
            {
                var particle = new ParticleEntity(texture, _moveEffects[moveEffectKey], pos);
                particle.ColorEffect = color;
                _particles.Add(particle);
            }
        }

        public static void Add(object textureKey, object moveEffectKey, Vector2 pos, Color color)
        {
            var particle = new ParticleEntity(_textures[textureKey], _moveEffects[moveEffectKey], pos);

            _particles.Add(particle);
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
