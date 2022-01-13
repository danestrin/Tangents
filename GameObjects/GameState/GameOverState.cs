using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private string gameOverString = "GAME OVER";
        private string gameOverSubString = "Press SPACE to play again!";
        private string returnTitleString = "Press R to return to title";
        private Vector2 gameOverMidPoint;
        private Vector2 gameOverSubMidPoint;
        private Vector2 returnTitleMidPoint;
        private Vector2 gameOverPos;
        private Vector2 gameOverSubPos;
        private Vector2 returnTitlePos;
        
        public GameOverState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            this.gameOverMidPoint = AssetManager.Header.MeasureString(gameOverString) / 2;
            this.gameOverSubMidPoint = AssetManager.SubHeader.MeasureString(gameOverSubString) / 2;
            this.returnTitleMidPoint = AssetManager.SubHeader.MeasureString(returnTitleString) / 2;
            this.gameOverPos = new Vector2(width / 2, height / 3);

            // the background image is a square grid with 18 squares vertically
            // the text looks best when it is positioned directly inside the squares, meaning the vertical alignment has to be (n.5)/18
            // 3/4 gives 13.5/18, and 23/36 gives 11.5/18
            // similar reasoning is used in TitleState.cs
            this.gameOverSubPos = new Vector2(width / 2, 23 * height / 36);
            this.returnTitlePos = new Vector2(width / 2, 3 * height / 4);
        }

        public override void OnBegin()
        {
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();
            
            if (prevKeyState.IsKeyDown(Keys.Space) && newKeyState.IsKeyUp(Keys.Space)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }

            if (prevKeyState.IsKeyDown(Keys.R) && newKeyState.IsKeyUp(Keys.R)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.Title];
            }

            prevKeyState = newKeyState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(AssetManager.Header, gameOverString, gameOverPos, Color.Red, 0, gameOverMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, gameOverSubString, gameOverSubPos, Color.Red, 0, gameOverSubMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, returnTitleString, returnTitlePos, Color.Red, 0, returnTitleMidPoint, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}