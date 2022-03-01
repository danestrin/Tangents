using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private string gameOverString = "GAME OVER";
        private string playAgainString = $"{input} to play again!";
        private string hiScoreString = $"Hi-Score: {ScoreManager.HiScore}";
        private GameText gameOver;
        private GameText playAgain;
        private GameText score;
        private GameText hiScore;

        public GameOverState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            // the background image is a square grid with 18 squares vertically
            // the text looks best when it is positioned directly inside the squares, meaning the vertical alignment has to be (n.5)/18
            // e.g. 25/36 gives 12.5/18
            // similar reasoning is used in TitleState.cs
            gameOver = new GameText(AssetManager.Header, gameOverString, new Vector2(width / 2, height / 3), Color.Red, true);
            playAgain = new GameText(AssetManager.SubHeader, playAgainString, new Vector2(width / 2, 25 * height / 36), Color.Red, true);

            score = ((InGameState) this.gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame]).Score;

            hiScore = new GameText(AssetManager.SubHeader, hiScoreString, new Vector2(width - AssetManager.SubHeader.MeasureString(hiScoreString).X - width / 64, 0), Color.Red, false);
        }

        public override void OnBegin()
        {
            ScoreManager.CheckAndUpdateHighScore();

            score.Text = $"Score: {ScoreManager.Score}";
            score.Color = Color.Red;

            hiScore.Text = $"Hi-Score: {ScoreManager.HiScore}";
            hiScore.Position = new Vector2(width - hiScore.Size.X - width / 64, 0);
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // check that Space was released instead of pressed, since Space (pressed) is used in the InGame state for gameplay (leads to interference otherwise)
            // same logic applies to Title state
            if (InputManager.WasMouseReleased() || InputManager.WasScreenReleased()) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix scaleMatrix)
        {
            spriteBatch.Begin(transformMatrix: scaleMatrix);

            spriteBatch.Draw(AssetManager.BG, Vector2.Zero, Color.White);

            gameOver.Draw(spriteBatch);
            playAgain.Draw(spriteBatch);
            score.Draw(spriteBatch);
            hiScore.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}