using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private string gameOverString = "GAME OVER";
        private string gameOverSubString = "Press SPACE to play again!";
        private string returnTitleString = "Press R to return to title";
        private string scoreString;
        private string hiScoreString;
        private Vector2 gameOverMidPoint;
        private Vector2 gameOverSubMidPoint;
        private Vector2 returnTitleMidPoint;
        private Vector2 gameOverPos;
        private Vector2 gameOverSubPos;
        private Vector2 returnTitlePos;
        private Vector2 scorePos;
        private Vector2 hiScorePos;

        public GameOverState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            this.gameOverMidPoint = AssetManager.Header.MeasureString(gameOverString) / 2;
            this.gameOverSubMidPoint = AssetManager.SubHeader.MeasureString(gameOverSubString) / 2;
            this.returnTitleMidPoint = AssetManager.SubHeader.MeasureString(returnTitleString) / 2;
            this.gameOverPos = new Vector2(width / 2, height / 3);
            this.scorePos = ((InGameState) this.gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame]).ScoreStringPos;

            // the background image is a square grid with 18 squares vertically
            // the text looks best when it is positioned directly inside the squares, meaning the vertical alignment has to be (n.5)/18
            // 3/4 gives 13.5/18, and 23/36 gives 11.5/18
            // similar reasoning is used in TitleState.cs
            this.gameOverSubPos = new Vector2(width / 2, 23 * height / 36);
            this.returnTitlePos = new Vector2(width / 2, 3 * height / 4);
        }

        public override void OnBegin()
        {
            ScoreManager.CheckAndUpdateHighScore();

            this.scoreString = $"Score: {ScoreManager.Score}";
            this.hiScoreString = $"Hi-Score: {ScoreManager.HiScore}";
            this.hiScorePos = new Vector2(this.width - AssetManager.SubHeader.MeasureString(hiScoreString).X - this.width / 64, 0);
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // check that Space was released instead of pressed, since Space (pressed) is used in the InGame state for gameplay (leads to interference otherwise)
            // same logic applies to Title state
            if (InputManager.WasKeyReleased(Keys.Space)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }

            // uses same logic (released instead of pressed) just for consistency
            if (InputManager.WasKeyReleased(Keys.R)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.Title];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, Vector2.Zero, Color.White);

            spriteBatch.DrawString(AssetManager.Header, gameOverString, gameOverPos, Color.Red, 0, gameOverMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, gameOverSubString, gameOverSubPos, Color.Red, 0, gameOverSubMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, returnTitleString, returnTitlePos, Color.Red, 0, returnTitleMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, scoreString, scorePos, Color.Red, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, hiScoreString, hiScorePos, Color.Red, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);



            spriteBatch.End();
        }
    }
}