using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Shape;
using System;
using MgEngine.Util;
using MgEngine.Interface;
using MgEngine.Input;

#pragma warning disable CS8618
namespace MgEngine.Component
{
    public class Entity : Box2D, IUpdate
    {
        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;

        public Color ColorEffect { get; set; }
        public SpriteEffects Effect { get; set; }
        
        #region Constructor
        public Entity(Texture2D texture)
        {
            ColorEffect = Color.White;
            Effect = SpriteEffects.None;

            SetTexture(texture);
        }

        public Entity()
        {
            ColorEffect = Color.White;
            Effect = SpriteEffects.None;
        }

        #endregion
        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
            _sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            Width = texture.Width;
            Height = texture.Height;

            ResizeScale(MgDefault.Scale);
        }

        public Texture2D GetTexture() { return _texture;}

        public Vector2 SourceCenter { get { return new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2); } }

        public virtual void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            if (_texture == null)
                throw new Exception("Texture was null, you have to SetTexture before Draw!");

            var destRectangle = new Rectangle((int)(X + scrollX), (int)(Y + scrollY), Width, Height);

            spriteBatch.Draw(_texture, destRectangle, _sourceRectangle, ColorEffect, Rotation, SourceCenter, Effect, 0f);
        }

        public void DrawRect(ShapeBatch shapeBatch,  Color color, float scrollX = 0, float scrollY = 0)
        {
            Rect rect = new Rect(X + scrollX, Y + scrollY, Width, Height, Rotation);

            shapeBatch.DrawRect(rect, color);
        }

        public virtual void Update(Inputter inputter, float dt) { }

        public static void DrawList<T>(List<T> list, SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0) where T : Entity
        {
            foreach(var entity in list)
            {
                entity.Draw(spriteBatch, scrollX, scrollY);
            }
        }
    }
}
