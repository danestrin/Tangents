using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    public class Circle : Entity
    {
        private float speed = 200f;

        public event EventHandler PlayerAttached;

        public Circle(Texture2D image, Vector2 position)
        {
            this.image = image;
            Position = position;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Position = new Vector2(Position.X - (float) gameTime.ElapsedGameTime.TotalSeconds * speed, Position.Y);
            HandleCollision(player);
        }

        public void OnPlayerAttached()
        {
            if (PlayerAttached != null) {
                PlayerAttached(this, EventArgs.Empty);
            }
        }

        private void HandleCollision(Player player)
        {
            if (!player.IsOrbiting) {
                if (player.AttachedCircle != this) {
                    if (Vector2.Distance(Position, player.Position) < Radius + Thickness) {
                        player.AttachedCircle = this;
                        player.IsOrbiting = true;
                        OnPlayerAttached();
                    } else {
                        // for scoring - add the circles that the player passes (i.e. between the player's launch point and next circle)
                        if (Position.X > player.TangentPoint.X + Radius && Position.X < player.Position.X && player.AttachedCircle == null)
                        {
                            player.PassedCircles.Add(this);
                        }
                    }
                } else {
                    // need to detach the player's current circle, but only once the player is far enough away so there's no interference when collision checking
                    if (Vector2.Distance(Position, player.Position) >= Radius + player.Radius) {
                        player.AttachedCircle = null;
                    }
                }

            }
        }
    }
}