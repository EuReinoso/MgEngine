﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MgEngine.Input;
using MgEngine.Scene;
using MgEngine.Font;
using MgEngine.Time;
using MgEngine.Screen;
using MgEngine.Shape;
using MgEngine.Audio;
using MgEngine.Util;
using MgEngine.Effect;

namespace GameExample
{
    public class GameExample : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatch _shapeBatch;
        private FontGroup _font;

        private Window _window;
        private Camera _camera;
        private Clock _clock;
        private Inputter _inputter;

        private MainScene _scene;

        public GameExample()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clock = new(this);
        }

        protected override void Initialize()
        {
            MgDefault.Initialize(Content);

            Singer.Initialize(Content);
            Particlerr.Initialize();

            _shapeBatch = new(GraphicsDevice);
            _spriteBatch = new(GraphicsDevice);

            _window = new(_graphics, _spriteBatch);
            _window.SetBackGroundColor(Color.Black);

            _inputter = new(_window);

            _font = new(Content, "Font/monogram", new() { 8, 9, 10, 11, 12, 13, 14, 15 });

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            _scene = new(_window, _camera, _clock);
            _scene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _scene.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Singer.Update();

            _inputter.Update(Keyboard.GetState(), Mouse.GetState());

            _clock.Update(gameTime);

            _scene.Update(_clock.Dt, _inputter);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _window.Begin();

            _shapeBatch.Begin();

            _scene.Draw(_spriteBatch, _shapeBatch);

            _shapeBatch.End();

            _font.DrawText(_spriteBatch, "FPS: " + _clock.Fps.ToString(), new Vector2(10, 10), 11, Color.White);

            _window.End();

            base.Draw(gameTime);
        }
    }
}