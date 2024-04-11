﻿using Microsoft.Xna.Framework;

namespace MgEngine.Window
{
    public class Window
    {
        private GraphicsDeviceManager _graphics;
        private Canvas _canvas;

        public Window(GraphicsDeviceManager graphics, int canvasWidth = 1280, int canvasHeight = 720)
        {
            _graphics = graphics;
            _canvas = new(_graphics.GraphicsDevice, canvasWidth, canvasHeight);
            _canvas.SetDestinationRectangle();
        }

        public int Width { get { return _graphics.PreferredBackBufferWidth; } }
        public int Height { get { return _graphics.PreferredBackBufferHeight; } }
        public Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }

        public void SetResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();

            _canvas.SetDestinationRectangle();
        }

        public void ToggleFullScreen()
        {
            _graphics.ToggleFullScreen();
            _canvas.SetDestinationRectangle();
        }

        public Canvas Canvas { get { return _canvas; } }
    }

}