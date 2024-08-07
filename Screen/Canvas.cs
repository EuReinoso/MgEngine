﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MgEngine.Screen
{
    public class Canvas
    {
        private GraphicsDevice _graphicsDevice;
        private RenderTarget2D _renderTarget;
        private Rectangle _destinationRectangle;
        public Color ColorEffect { get; set; }

        public Canvas(GraphicsDevice graphicsDevice, int width, int height)
        {
            _graphicsDevice = graphicsDevice;
            _renderTarget = new(_graphicsDevice, width, height);
            ColorEffect = Color.White;
        }

        public int Width { 
            get { return _renderTarget.Width; }
            set 
            {
                SetResolution(value, Height);
            }
        }

        public int Height
        {
            get { return _renderTarget.Height; }
            set
            {
                SetResolution(Width, value);
            }
        }

        public Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }

        public RenderTarget2D RenderTarget { get { return _renderTarget; } }

        public void SetResolution(int width, int height)
        {
            _renderTarget = new(_graphicsDevice, width, height);
            SetDestinationRectangle();
        }

        public void SetDestinationRectangle()
        {
            var windowSize = _graphicsDevice.PresentationParameters.Bounds;

            float scaleWidth = (float)windowSize.Width / _renderTarget.Width;
            float scaleHeight = (float)windowSize.Height / _renderTarget.Height;
            float scale = Math.Min(scaleWidth, scaleHeight);

            int newWidth = (int)(_renderTarget.Width * scale);
            int newHeight = (int)(_renderTarget.Height * scale);

            int x = (windowSize.Width - newWidth) / 2;
            int y = (windowSize.Height - newHeight) / 2;

            _destinationRectangle = new Rectangle(x, y, newWidth, newHeight);
        }

        public void Activate(Color backGroundColor)
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(backGroundColor);
        }

        public void Dispose()
        {
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(new Color(5, 5, 5));
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSortMode spriteSortMode = SpriteSortMode.BackToFront, BlendState? blendState = null, SamplerState? samplerState = null, BasicEffect? effects = null)
        {
            spriteBatch.Begin(spriteSortMode, blendState, samplerState,rasterizerState : RasterizerState.CullNone, effect : effects);
            spriteBatch.Draw(_renderTarget, _destinationRectangle, ColorEffect);
            spriteBatch.End();
        }

    }
}
