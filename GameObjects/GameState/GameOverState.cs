using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private string gameOverString = "GAME OVER";
        private string gameOverSubString = "Press SPACE to play again!";
        private Vector2 gameOverMidPoint;
        private Vector2 gameOverSubMidPoint;
        private Vector2 gameOverPos;
        private Vector2 gameOverSubPos;
        
        public GameOverState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            this.gameOverMidPoint = AssetManager.Header.MeasureString(gameOverString) / 2;
            this.gameOverSubMidPoint = AssetManager.SubHeader.MeasureString(gameOverSubString) / 2;
            this.gameOverPos = new Vector2(width / 2, height / 3);
            this.gameOverSubPos = new Vector2(width / 2, 3* height / 4);
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

            prevKeyState = newKeyState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(AssetManager.Header, gameOverString, gameOverPos, Color.Red, 0, gameOverMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, gameOverSubString, gameOverSubPos, Color.Red, 0, gameOverSubMidPoint, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}