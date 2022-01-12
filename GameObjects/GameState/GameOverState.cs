using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private SpriteFont header;
        private SpriteFont subHeader;
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

            this.header = AssetManager.Header;
            this.subHeader = AssetManager.SubHeader;
            this.gameOverMidPoint = header.MeasureString(gameOverString) / 2;
            this.gameOverSubMidPoint = subHeader.MeasureString(gameOverSubString) / 2;
            this.gameOverPos = new Vector2(width / 2, height / 3);
            this.gameOverSubPos = new Vector2(width / 2, 2 * height / 3);
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

            spriteBatch.DrawString(header, gameOverString, gameOverPos, Color.Red, 0, gameOverMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(subHeader, gameOverSubString, gameOverSubPos, Color.Red, 0, gameOverSubMidPoint, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}