﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Shape;
using System;

namespace MgEngine.Component
{
    public class Entity : Box2D
    {
        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;
        
        #region Constructor
        public Entity(Texture2D texture)
        {
            _texture = texture;
            _sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            Width = texture.Width;
            Height = texture.Height;
        }

        protected Entity()
        {

        }
        #endregion

        public Vector2 SourceCenter { get { return new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2); } }

        public void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0, Color? color = null)
        {
            if (_texture == null)
                throw new Exception("Texture was null, you have to Add a Texture to start Animation");

            Color colorEffect = color ?? Color.White;

            var destRectangle = new Rectangle((int)(X + scrollX), (int)(Y + scrollY), Width, Height);

            spriteBatch.Draw(_texture, destRectangle, _sourceRectangle, colorEffect, Rotation, SourceCenter, SpriteEffects.None, 0f);
        }

        public void DrawRect(ShapeBatch shapeBatch,  Color color, float scrollX = 0, float scrollY = 0)
        {
            Rect rect = new Rect(X + scrollX, Y + scrollY, Width, Height, Rotation);

            shapeBatch.DrawRect(rect, color);
        }

    }
}
