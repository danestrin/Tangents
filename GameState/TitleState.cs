using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public class TitleState : GameState
    {
        private string titleString = "TANGENTS";
        private string subtitleString = "Press SPACE to jump from circle to circle and don't go out of bounds!";
        private string startString = "Press SPACE to start";
        private Vector2 titleMidPoint;
        private Vector2 subtitleMidPoint;
        private Vector2 startMidPoint;
        private Vector2 titlePos;
        private Vector2 subtitlePos;
        private Vector2 startPos;

        public TitleState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            titleMidPoint = AssetManager.Header.MeasureString(titleString) / 2;
            subtitleMidPoint = AssetManager.SubHeader.MeasureString(subtitleString) / 2;
            startMidPoint = AssetManager.SubHeader.MeasureString(startString) / 2;
            titlePos = new Vector2(width / 2, height / 3);
            subtitlePos = new Vector2(width / 2, 17 * height / 36);
            startPos = new Vector2(width / 2, 23 * height / 36);

        }

        public override void OnBegin()
        {
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.WasKeyReleased(Keys.Space)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(AssetManager.Header, titleString, titlePos, Color.Blue, 0, titleMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, subtitleString, subtitlePos, Color.Blue, 0, subtitleMidPoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(AssetManager.SubHeader, startString, startPos, Color.Blue, 0, startMidPoint, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }
    }
}
