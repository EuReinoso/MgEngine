using MgEngine.Font;
using MgEngine.Sprites;
using MgEngine.Time;
using MgEngine.Window;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public GameExample()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clock = new(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _window = new(_graphics, 1920, 1080);
            _window.SetResolution(1920, 1080);

            _sprites = new(GraphicsDevice, _window);

            _font = new(Content, "Font");

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _clock.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _window.Canvas.Activate();
            _spriteBatch.Begin();

            // TODO: Add your drawing code here

            _font.DrawText(_spriteBatch, _clock.Fps.ToString(), new(10, 10), Color.White);

            _spriteBatch.End();
            _window.Canvas.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}