using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tangents
{
    public class InGameState : GameState
    {
        private Player player;
        private Circle[] circles;
        private Random random;
        private int circleUpperBound;
        private int circleLowerBound;

        public InGameState(GameStateManager gameStateManager, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameStateManager = gameStateManager;

            random = new Random();
        }

        public override void OnBegin()
        {
            Circle circle1 = new Circle(AssetManager.Circle, new Vector2(this.width / 2, this.height / 2));
            player = new Player(AssetManager.Player, circle1);

            circleLowerBound = 0 + (int) circle1.Radius + (int)circle1.Thickness + (int) player.Radius + (int) player.Thickness;
            circleUpperBound = height - (int)circle1.Radius - (int)circle1.Thickness - (int) player.Radius - (int) player.Thickness;

            Circle circle2 = new Circle(AssetManager.Circle, new Vector2(7 * this.width / 8, random.Next(circleLowerBound, circleUpperBound)));
            Circle circle3 = new Circle(AssetManager.Circle, new Vector2(10 * this.width / 8, random.Next(circleLowerBound, circleUpperBound)));

            circles = new Circle[] { circle1, circle2, circle3 };
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();

            if (prevKeyState.IsKeyUp(Keys.Space) && newKeyState.IsKeyDown(Keys.Space)) {
                player.IsOrbiting = false;
            }

            prevKeyState = newKeyState;

            foreach (Circle circle in circles) {
                circle.Update(gameTime, player);
                CheckCircleBounds(circle);
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

        private void CheckCircleBounds(Circle circle)
        {
            if (circle.Position.X < 0 - circle.Radius - circle.Thickness)
            {
                circle.Position = new Vector2(width + circle.Radius + circle.Thickness, random.Next(circleLowerBound, circleUpperBound));
            }
        }
    }
}