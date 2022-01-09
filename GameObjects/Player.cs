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

        public bool isOribiting = true;
        public Circle attachedCircle;
        public Player(Texture2D image, Circle circle)
        {
            this.image = image;
            this.attachedCircle = circle;
            this.position = new Vector2(this.attachedCircle.position.X, this.attachedCircle.position.Y + this.attachedCircle.radius);
            this.speed = this.attachedCircle.radius * this.angularVelocity * this.speedConstant;
            this.angle = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isOribiting) {
                if (this.trajectory != Vector2.Zero) {
                    this.trajectory = Vector2.Zero;
                }

                this.angle -= ((float) gameTime.ElapsedGameTime.TotalSeconds * this.angularVelocity);
                this.position = new Vector2(this.attachedCircle.position.X + this.attachedCircle.radius * (float) Math.Sin(this.angle), this.attachedCircle.position.Y + this.attachedCircle.radius * (float) Math.Cos(this.angle));
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