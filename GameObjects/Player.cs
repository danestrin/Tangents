using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tangents
{
    public class Player : Entity
    {
        private float angularVelocity = 5f;
        private float speed;
        private float speedConstant = 2;
        private float angle;
        private Vector2 trajectory;

        public bool IsOrbiting { get; set; }
        public Circle AttachedCircle { get; set; }

        public Player(Texture2D image, Circle circle)
        {
            this.image = image;
            this.AttachedCircle = circle;
            this.Position = new Vector2(this.AttachedCircle.Position.X + this.AttachedCircle.Radius, this.AttachedCircle.Position.Y);
            this.IsOrbiting = true;
            this.speed = this.AttachedCircle.Radius * this.angularVelocity * this.speedConstant;
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsOrbiting) {
                if (this.trajectory != Vector2.Zero) {
                    this.trajectory = Vector2.Zero;
                    this.angle = (float) Math.Atan2(this.Position.Y - this.AttachedCircle.Position.Y, this.Position.X - this.AttachedCircle.Position.X);
                }

                this.angle += ((float) gameTime.ElapsedGameTime.TotalSeconds * this.angularVelocity);
                this.Position = new Vector2(this.AttachedCircle.Position.X + this.AttachedCircle.Radius * (float) Math.Cos(this.angle), this.AttachedCircle.Position.Y + this.AttachedCircle.Radius * (float) Math.Sin(this.angle));
            } else {
                if (this.trajectory == Vector2.Zero) {
                    this.trajectory = Vector2.Normalize(new Vector2(this.Position.Y - this.AttachedCircle.Position.Y, this.Position.X - this.AttachedCircle.Position.X));
                }

                this.Position = new Vector2(this.Position.X - this.trajectory.X * (float) gameTime.ElapsedGameTime.TotalSeconds * this.speed, this.Position.Y + this.trajectory.Y * (float) gameTime.ElapsedGameTime.TotalSeconds * this.speed);

            }
        }
    }
}