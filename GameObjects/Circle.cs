using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    class Circle : Entity
    {
        public float thickness = 8;
        public float radius
        {
            get {
                return this.image.Width / 2f - thickness / 2f;
            }
        }
        public Circle(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
        }

        public void Update(GameTime gameTime, Player player)
        {
            this.HandleCollision(player);
        }

        private void HandleCollision(Player player)
        {
            if (!player.isOrbiting && player.attachedCircle != this) {
                if (Vector2.Distance(this.position, player.position) <= this.radius) {
                    player.attachedCircle = this;
                    player.isOrbiting = true;
                }
            }
        }
    }
}