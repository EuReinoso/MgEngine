﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Entity
{
    public class Surface
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        public Surface(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public RenderTarget2D DrawTextureOnSurface(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(_graphicsDevice, destinationRectangle.Width, destinationRectangle.Height);

            _graphicsDevice.SetRenderTarget(renderTarget);
            _graphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap);
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            _spriteBatch.End();

            return renderTarget;
        }


    }
}
