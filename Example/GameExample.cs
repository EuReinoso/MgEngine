using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MgEngine.Input;
using MgEngine.Scene;
using MgEngine.Font;
using MgEngine.Sprites;
using MgEngine.Time;
using MgEngine.Screen;

namespace GameExample
{
    public class GameExample : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Window _window;
        private Font _font;
        private Clock _clock;
        private SpritesDraw _sprites;
        private MainScene _scene;
        private Inputter _inputter;

        public GameExample()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clock = new(this);
            _inputter = new();
        }

        protected override void Initialize()
        {
            _window = new(_graphics, _spriteBatch);

            _sprites = new(GraphicsDevice, _window);

            _font = new(Content, "Font/monogram", new() { 8, 9, 10, 11, 12, 13, 14, 15 });

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            _scene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _scene.LoadContent(_sprites, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputter.Update(Keyboard.GetState());

            _clock.Update(gameTime);

            _scene.Update(_clock.Dt, _inputter);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _window.Begin();

            _scene.Draw(_spriteBatch);

            _font.DrawText(_spriteBatch, "FPS: " + _clock.Fps.ToString(), new Vector2(10, 10), 11, Color.White);

            _window.End();

            base.Draw(gameTime);
        }
    }
}