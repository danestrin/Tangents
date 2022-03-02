using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Tangents
{
    public class TitleState : GameState
    {
        private string titleString = "TANGENTS";
        private string instructionString = $"{input} the screen to jump from circle to circle and don't go out of bounds!";
        private string startString = $"{input} here to start";
        private string creditString = "Created by Dan Estrin - github.com/danestrin/Tangents";
        private GameText title;
        private GameText instruction;
        private GameText start;
        private GameText credit;

        public TitleState(GameStateManager gameStateManager, int width, int height)
        {
            this.gameStateManager = gameStateManager;
            this.width = width;
            this.height = height;

            title = new GameText(AssetManager.Header, titleString, new Vector2(width / 2, height / 3), Color.Blue, true);
            instruction = new GameText(AssetManager.SubHeader, instructionString, new Vector2(width / 2, 17 * height / 36), Color.Blue, true);
            start = new GameText(AssetManager.SubHeader, startString, new Vector2(width / 2, 25 * height / 36), Color.Blue, true);
            credit = new GameText(AssetManager.SubHeader, creditString, new Vector2(width / 2, 33 * height / 36), Color.Black, true);
        }

        public override void OnBegin()
        {
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime, Matrix scaleMatrix)
        {
            if (InputManager.WasTextReleased(start, scaleMatrix)) {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.InGame];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix scaleMatrix)
        {
            spriteBatch.Begin(transformMatrix: scaleMatrix);

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            title.Draw(spriteBatch);
            instruction.Draw(spriteBatch);
            start.Draw(spriteBatch);
            credit.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
