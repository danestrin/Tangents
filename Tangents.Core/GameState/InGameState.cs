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
        private string scoreString = $"Score: {ScoreManager.Score}";

        public GameText Score;

        public InGameState(GameStateManager gameStateManager, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameStateManager = gameStateManager;

            Score = new GameText(AssetManager.SubHeader, scoreString, new Vector2(this.width / 64, 0), Color.Blue, false);

            random = new Random();
        }

        public override void OnBegin()
        {
            ScoreManager.ResetScore();
            Score.Text = $"Score: {ScoreManager.Score}";
            Score.Color = Color.Blue;

            Circle circle1 = new Circle(AssetManager.Circle, width / 2, height / 2);
            player = new Player(AssetManager.Player, circle1);

            circleLowerBound = 0 + (int) circle1.Radius + (int) circle1.Thickness + (int) player.Radius + (int) player.Thickness + (int) Score.Size.Y;
            circleUpperBound = height - (int) circle1.Radius - (int) circle1.Thickness - (int) player.Radius - (int) player.Thickness;

            Circle circle2 = new Circle(AssetManager.Circle, 7 * width / 8, random.Next(circleLowerBound, circleUpperBound));
            Circle circle3 = new Circle(AssetManager.Circle, 10 * width / 8, random.Next(circleLowerBound, circleUpperBound));

            circles = new Circle[] { circle1, circle2, circle3 };

            foreach (Circle circle in circles)
            {
                circle.PlayerAttached += HandlePlayerAttached;
            }
        }

        public override void OnEnd()
        {
            foreach (Circle circle in circles)
            {
                circle.PlayerAttached -= HandlePlayerAttached;
            }
        }

        public override void Update(GameTime gameTime, Matrix scaleMatrix)
        {
            if (InputManager.WasMouseClicked() || InputManager.WasScreenTouched()) {
                player.IsOrbiting = false;
            }

            foreach (Circle circle in circles) {
                circle.Update(gameTime, player);
                CheckCircleBounds(circle);
            }

            player.Update(gameTime);
            CheckPlayerBounds();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix scaleMatrix)
        {
            spriteBatch.Begin(transformMatrix: scaleMatrix);

            spriteBatch.Draw(AssetManager.BG, new Vector2(0, 0), Color.White);
            Score.Draw(spriteBatch);

            foreach (Circle circle in circles) {
                circle.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void CheckPlayerBounds()
        {
            if (player.X < 0 || player.X > width || player.Y < 0 || player.Y > height)
            {
                gameStateManager.CurrentGameState = gameStateManager.GameStateMap[GameStateManager.GameStateID.GameOver];
            }
        }

        private void CheckCircleBounds(Circle circle)
        {
            if (circle.X < 0 - circle.Radius - circle.Thickness)
            {
                circle.SetPosition(width + circle.Radius + circle.Thickness, random.Next(circleLowerBound, circleUpperBound));
            }
        }

        private void HandlePlayerAttached(object sender, EventArgs eventArgs)
        {
            ScoreManager.IncrementScore(player.PassedCircles.Count + 1);
            Score.Text = $"Score: {ScoreManager.Score}";
            player.PassedCircles.Clear();
        }
    }
}