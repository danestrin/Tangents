using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tangents
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private readonly int width = 960;
        private readonly int height = 540;
        int deviceWidth;
        int deviceHeight;
        private Matrix scaleMatrix;
        private GameStateManager gameStateManager;
        private PlatformID pid;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            OperatingSystem os = Environment.OSVersion;
            pid = os.Platform;

            Window.Title = "Tangents";

            if (pid == PlatformID.Win32NT || pid == PlatformID.Unix || pid == PlatformID.MacOSX)
            {
                deviceWidth = width;
                deviceHeight = height;
            } else
            {
                deviceWidth = graphics.GraphicsDevice.DisplayMode.Width;
                deviceHeight = graphics.GraphicsDevice.DisplayMode.Height;
            }

            graphics.PreferredBackBufferWidth = deviceWidth;
            graphics.PreferredBackBufferHeight = deviceHeight;
            graphics.ApplyChanges();

            float scaleX = (float) deviceWidth / width;
            float scaleY = (float) deviceHeight / height;
            scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

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
            gameStateManager.CurrentGameState.Draw(gameTime, spriteBatch, scaleMatrix);

            base.Draw(gameTime);
        }

        private void LoadGameStates()
        {
            gameStateManager = GameStateManager.Instance;
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.Title, new TitleState(gameStateManager, width, height));
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.InGame, new InGameState(gameStateManager, width, height));
            gameStateManager.GameStateMap.Add(GameStateManager.GameStateID.GameOver, new GameOverState(gameStateManager, width, height));

            gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.Title];
        }
    }
}
