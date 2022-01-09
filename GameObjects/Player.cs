using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    class Player : Entity
    {
        private float angularVelocity = 5f;
        private float speed;
        private float speedConstant = 2;
        private float angle;
        private Vector2 trajectory;

        public bool isOrbiting = true;
        public Circle attachedCircle;
        public Player(Texture2D image, Circle circle)
        {
            this.image = image;
            this.attachedCircle = circle;
            this.position = new Vector2(this.attachedCircle.position.X + this.attachedCircle.radius, this.attachedCircle.position.Y);
            this.speed = this.attachedCircle.radius * this.angularVelocity * this.speedConstant;
        }

        public void Update(GameTime gameTime)
        {
            if (this.isOrbiting) {
                if (this.trajectory != Vector2.Zero) {
                    this.trajectory = Vector2.Zero;
                    this.angle = (float) Math.Atan2(this.position.Y - this.attachedCircle.position.Y, this.position.X - this.attachedCircle.position.X);
                }

                this.angle += ((float) gameTime.ElapsedGameTime.TotalSeconds * this.angularVelocity);
                this.position = new Vector2(this.attachedCircle.position.X + this.attachedCircle.radius * (float) Math.Cos(this.angle), this.attachedCircle.position.Y + this.attachedCircle.radius * (float) Math.Sin(this.angle));
            } else {
                if (this.trajectory == Vector2.Zero) {
                    this.trajectory = Vector2.Normalize(new Vector2(this.position.Y - this.attachedCircle.position.Y, this.position.X - this.attachedCircle.position.X));
                }

                this.position.X -= this.trajectory.X * (float) gameTime.ElapsedGameTime.TotalSeconds * this.speed;
                this.position.Y += this.trajectory.Y * (float) gameTime.ElapsedGameTime.TotalSeconds * this.speed;

            }
        }
    }
}