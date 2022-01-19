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

        public Player(Texture2D image, Circle circle): base(image, circle.X + circle.Radius, circle.Y)
        {
            AttachedCircle = circle;

            IsOrbiting = true;
            speed = AttachedCircle.Radius * angularVelocity * speedConstant;
        }

        public void Update(GameTime gameTime)
        {
            if (IsOrbiting) {
                if (trajectory != Vector2.Zero) {
                    trajectory = Vector2.Zero;
                    angle = (float) Math.Atan2(Y - AttachedCircle.Y, X - AttachedCircle.X);
                }

                angle += ((float) gameTime.ElapsedGameTime.TotalSeconds * angularVelocity);
                X = AttachedCircle.X + AttachedCircle.Radius * (float)Math.Cos(angle);
                Y = AttachedCircle.Y + AttachedCircle.Radius * (float)Math.Sin(angle);
            } else {
                if (trajectory == Vector2.Zero) {
                    trajectory = Vector2.Normalize(new Vector2(Y - AttachedCircle.Y, X - AttachedCircle.X));
                    TangentPoint = Position;
                }

                X -= trajectory.X * (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
                Y += trajectory.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            }
        }
    }
}