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
        private float scoreStringHeight;

        public Vector2 ScoreStringPos { get; private set; }

        public InGameState(GameStateManager gameStateManager, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameStateManager = gameStateManager;

            ScoreStringPos = new Vector2(this.width / 64, 0);
            scoreStringHeight = AssetManager.SubHeader.MeasureString("Score").Y;

            random = new Random();
        }

        public override void OnBegin()
        {
            ScoreManager.ResetScore();

            Circle circle1 = new Circle(AssetManager.Circle, new Vector2(this.width / 2, this.height / 2));
            player = new Player(AssetManager.Player, circle1);

            circleLowerBound = 0 + (int) circle1.Radius + (int) circle1.Thickness + (int) player.Radius + (int) player.Thickness;
            circleUpperBound = height - (int) circle1.Radius - (int) circle1.Thickness - (int) player.Radius - (int) player.Thickness - (int) scoreStringHeight;

            Circle circle2 = new Circle(AssetManager.Circle, new Vector2(7 * this.width / 8, random.Next(circleLowerBound, circleUpperBound)));
            Circle circle3 = new Circle(AssetManager.Circle, new Vector2(10 * this.width / 8, random.Next(circleLowerBound, circleUpperBound)));

            circles = new Circle[] { circle1, circle2, circle3 };

            foreach (Circle circle in circles)
            {
                circle.PlayerAttached += HandlePlayerAttached;
            }
        }

        public override void OnEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.WasKeyPressed(Keys.Space)) {
                player.IsOrbiting = false;
            }

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
            spriteBatch.DrawString(AssetManager.SubHeader, $"Score: {ScoreManager.Score}", ScoreStringPos, Color.Blue, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

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

        private void HandlePlayerAttached(object sender, EventArgs eventArgs)
        {
            ScoreManager.UpdateScore(player.PassedCircles.Count + 1);
            player.PassedCircles.Clear();
        }
    }
}