using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Shape;
using MgEngine.Util;
using MgEngine.Interface;
using MgEngine.Input;
using System;
using System.Collections.Generic;

#pragma warning disable CS8618
namespace MgEngine.Component
{
    public class Entity : Box2D, IUpdate
    {
        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;
        protected bool _firstTextureLoaded;

        public Color ColorEffect { get; set; }
        private Color _borderColor { get; set; }
        private Texture2D _borderTexture { get; set; }
        public bool IsBorderEnabled { get; set; }
        public bool IsBorderAutoUpdate { get; set; }
        public int BorderWidth { get; set; }
        public SpriteEffects Effect { get; set; }
        public float Scale { get; set; }

        public event Action? TextureChanged;

        #region Constructor
        public Entity(Texture2D texture)
        {
            Initialize();
            SetTexture(texture);
        }

        public Entity()
        {
            Initialize();
        }

        private void Initialize()
        {
            BorderWidth = 1;
            _borderColor = Color.White;
            ColorEffect = Color.White;
            Effect = SpriteEffects.None;
            TextureChanged += Entity_TextureChanged;
            Scale = MgDefault.Scale;
        }

        #endregion
        public void SetTexture(Texture2D texture, Rectangle? sourceRectangle = null)
        {
            bool resize = (_texture is not null && (texture.Width != _texture.Width || texture.Height != _texture.Height));

            _texture = texture;
            _sourceRectangle = sourceRectangle ?? new Rectangle(0, 0, texture.Width, texture.Height);

            if (!_firstTextureLoaded || resize)
            {
                Width = _sourceRectangle.Width;
                Height = _sourceRectangle.Height;
                ResizeScale(Scale);
                _firstTextureLoaded = true;
            }

            TextureChanged?.Invoke();
        }

        public Texture2D GetTexture() { return _texture;}

        public Texture2D GetBorderTexture() 
        { 
            if (_borderTexture is null)
                _borderTexture = MgUtil.PaintTexture(_texture, _borderColor);

            return _borderTexture; 
        }

        public Color BorderColor
        {
            get { return _borderColor; }

            set
            {
                _borderColor = value;
                _borderTexture = MgUtil.PaintTexture(_texture, _borderColor);
            }
        }

        public Vector2 SourceCenter { get { return new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2); } }

        public virtual void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            if (_texture == null)
                throw new Exception("Texture was null, you have to SetTexture before Draw!");

            var destRectangle = new Rectangle((int)(X + scrollX), (int)(Y + scrollY), Width, Height);

            if (IsBorderEnabled)
                DrawBorder(spriteBatch, destRectangle);

            spriteBatch.Draw(_texture, destRectangle, _sourceRectangle, ColorEffect, Rotation, SourceCenter, Effect, 0f);
        }

        private void DrawBorder(SpriteBatch spriteBatch, Rectangle destRectangle)
        {
            if (_borderTexture is null)
                _borderTexture = MgUtil.PaintTexture(_texture, _borderColor);

            int x = destRectangle.X;
            int y = destRectangle.Y;
            int width = destRectangle.Width;
            int height = destRectangle.Height;

            spriteBatch.Draw(_borderTexture, new Rectangle(x - BorderWidth, y - BorderWidth, width, height), _sourceRectangle, _borderColor, Rotation, SourceCenter, Effect, 0f);
            spriteBatch.Draw(_borderTexture, new Rectangle(x + BorderWidth, y - BorderWidth, width, height), _sourceRectangle, _borderColor, Rotation, SourceCenter, Effect, 0f);
            spriteBatch.Draw(_borderTexture, new Rectangle(x + BorderWidth, y + BorderWidth, width, height), _sourceRectangle, _borderColor, Rotation, SourceCenter, Effect, 0f);
            spriteBatch.Draw(_borderTexture, new Rectangle(x - BorderWidth, y + BorderWidth, width, height), _sourceRectangle, _borderColor, Rotation, SourceCenter, Effect, 0f);
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

        private void Entity_TextureChanged()
        {
            if (IsBorderEnabled || IsBorderAutoUpdate)
                _borderTexture = MgUtil.PaintTexture(_texture, _borderColor);
        }
    }
}
