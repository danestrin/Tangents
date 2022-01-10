using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public class Tangents : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState prevKeyState;

        Circle circle1, circle2;
        Player player;

        public Tangents()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 540;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManager.Load(Content);
            circle1 = new Circle(AssetManager.Circle, new Vector2(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 2));
            player = new Player(AssetManager.Player, circle1);
            circle2 = new Circle(AssetManager.Circle, new Vector2(3 * graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 2));
        }

        protected override void Update(GameTime gameTime)
        {
            // input
            KeyboardState newKeyState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (prevKeyState.IsKeyUp(Keys.Space) && newKeyState.IsKeyDown(Keys.Space)) {
                player.IsOrbiting = !player.IsOrbiting;
            }

            prevKeyState = newKeyState;

            player.Update(gameTime);
            circle1.Update(gameTime, player);
            circle2.Update(gameTime, player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);
            circle1.Draw(gameTime, spriteBatch);
            circle2.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
