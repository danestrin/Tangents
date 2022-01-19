using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
        public Vector2 TangentPoint { get; private set; }

        // used for scoring purposes - the player receives extra points for skipping circles (e.g. going from the first circle to the third) 
        public HashSet<Circle> PassedCircles { get; set; } = new HashSet<Circle>();

        public Player(Texture2D image, Circle circle)
        {
            this.image = image;
            AttachedCircle = circle;
            Position = new Vector2(AttachedCircle.Position.X + AttachedCircle.Radius, AttachedCircle.Position.Y);
            IsOrbiting = true;
            speed = AttachedCircle.Radius * angularVelocity * speedConstant;
        }

        public void Update(GameTime gameTime)
        {
            if (IsOrbiting) {
                if (trajectory != Vector2.Zero) {
                    trajectory = Vector2.Zero;
                    angle = (float) Math.Atan2(Position.Y - AttachedCircle.Position.Y, Position.X - AttachedCircle.Position.X);
                }

                angle += ((float) gameTime.ElapsedGameTime.TotalSeconds * angularVelocity);
                Position = new Vector2(AttachedCircle.Position.X + AttachedCircle.Radius * (float) Math.Cos(angle), AttachedCircle.Position.Y + AttachedCircle.Radius * (float) Math.Sin(angle));
            } else {
                if (trajectory == Vector2.Zero) {
                    trajectory = Vector2.Normalize(new Vector2(Position.Y - AttachedCircle.Position.Y, Position.X - AttachedCircle.Position.X));
                    TangentPoint = Position;
                }

                Position = new Vector2(Position.X - trajectory.X * (float) gameTime.ElapsedGameTime.TotalSeconds * speed, Position.Y + trajectory.Y * (float) gameTime.ElapsedGameTime.TotalSeconds * speed);
            }
        }
    }
}