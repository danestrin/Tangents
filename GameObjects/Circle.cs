using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    public class Circle : Entity
    {
        public float thickness
        {
            get {
                return 8f;
            }
        }

        public float Radius
        {
            get {
                return this.image.Width / 2f - thickness / 2f;
            }
        }
        public Circle(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.Position = position;
        }

        public void Update(GameTime gameTime, Player player)
        {
            this.HandleCollision(player);
        }

        private void HandleCollision(Player player)
        {
            if (!player.IsOrbiting && player.AttachedCircle != this) {
                if (Vector2.Distance(this.Position, player.Position) <= this.Radius) {
                    player.AttachedCircle = this;
                    player.IsOrbiting = true;
                }
            }
        }
    }
}