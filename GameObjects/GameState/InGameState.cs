using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public class InGameState : GameState
    {
        private Player player;
        private Circle[] circles;

        public InGameState(GameStateManager gameStateManager, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameStateManager = gameStateManager;
        }

        public override void OnBegin()
        {
            Circle circle1 = new Circle(AssetManager.Circle, new Vector2(this.width / 4, this.height / 2));
            player = new Player(AssetManager.Player, circle1);
            Circle circle2 = new Circle(AssetManager.Circle, new Vector2(3 * this.width / 4, this.height / 2));

            circles = new Circle[] { circle1, circle2 };
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();

            if (prevKeyState.IsKeyUp(Keys.Space) && newKeyState.IsKeyDown(Keys.Space)) {
                player.IsOrbiting = !player.IsOrbiting;
            }

            prevKeyState = newKeyState;

            foreach (Circle circle in circles) {
                circle.Update(gameTime, player);
            }

            player.Update(gameTime);

            CheckPlayerBounds();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);

            foreach (Circle circle in circles) {
                circle.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void CheckPlayerBounds()
        {
            if (player.Position.X < 0 || player.Position.X > this.width || player.Position.Y < 0 || player.Position.Y > this.height)
            {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.GameOver];
            }
        }
    }
}