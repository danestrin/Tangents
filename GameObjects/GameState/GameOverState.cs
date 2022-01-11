using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{    public class GameOverState : GameState
    {
        private SpriteFont header;
        private string gameOverString = "GAME OVER";
        private Vector2 gameOverMidPoint;
        private Vector2 screenMidPoint;
        
        public GameOverState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.header = AssetManager.Header;
            this.gameOverMidPoint = header.MeasureString(gameOverString) / 2;
            this.screenMidPoint = new Vector2(width/2, height/2);
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
            
            if (prevKeyState.IsKeyUp(Keys.Enter) && newKeyState.IsKeyDown(Keys.Enter)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }

            prevKeyState = newKeyState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(header, gameOverString, screenMidPoint, Color.Red, 0, gameOverMidPoint, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}