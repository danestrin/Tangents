using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public class Tangents : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private int width;
        private int height;
        private GameStateManager gameStateManager;

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

            this.width = graphics.PreferredBackBufferWidth;
            this.height = graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManager.Load(Content);
            ScoreManager.LoadHighScore();

            LoadGameStates();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            if (InputManager.WasKeyPressed(Keys.Escape)) {
                Exit();
            }

            gameStateManager.CurrentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gameStateManager.CurrentGameState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        private void LoadGameStates()
        {
            this.gameStateManager = GameStateManager.Instance;
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.Title, new TitleState(gameStateManager, width, height));
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.InGame, new InGameState(gameStateManager, width, height));
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.GameOver, new GameOverState(gameStateManager, width, height));

            gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.Title];
        }
    }
}
