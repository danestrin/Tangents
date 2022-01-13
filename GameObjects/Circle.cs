using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    public class Circle : Entity
    {
        private float speed = 200f;

        public Circle(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.Position = position;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Position = new Vector2(Position.X - (float) gameTime.ElapsedGameTime.TotalSeconds * speed, Position.Y);
            this.HandleCollision(player);
        }

        private void HandleCollision(Player player)
        {
            if (!player.IsOrbiting) {
                if (player.AttachedCircle != this) {
                    if (Vector2.Distance(this.Position, player.Position) <= this.Radius)
                    {
                        player.AttachedCircle = this;
                        player.IsOrbiting = true;
                    }
                } else {
                    // need to detach the player's current circle, but only once the player is far enough away so there's no interference when collision checking
                    if (Vector2.Distance(this.Position, player.Position) >= this.Radius + player.Radius) {
                        player.AttachedCircle = null;
                    }
                }

            }
        }
    }
}