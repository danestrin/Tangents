using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    class Player : Entity
    {
        private float angularVelocity = 5f;
        private float angle;

        public bool isOribiting = true;
        public Circle attachedCircle;
        public Player(Texture2D image, Vector2 position, Circle circle)
        {
            this.image = image;
            this.position = position;
            this.attachedCircle = circle;
            this.angle = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isOribiting) {
                this.angle -= ((float) gameTime.ElapsedGameTime.TotalSeconds * this.angularVelocity);
                this.position = new Vector2(this.attachedCircle.position.X + this.attachedCircle.radius * (float) Math.Sin(this.angle), this.attachedCircle.position.Y + this.attachedCircle.radius * (float) Math.Cos(this.angle));
            }
        }
    }
}