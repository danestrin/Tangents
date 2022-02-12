using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tangents
{
    public abstract class Entity
    {
        protected Texture2D image;
        protected float x;
        protected float y;

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                Position.X = x;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                Position.Y = y;
            }
        }

        public float Thickness = 8f;

        public float Radius
        {
            get
            {
                return image.Width / 2f - Thickness / 2f;
            }
        }

        public Vector2 Position;

        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        protected Entity(Texture2D image, float X, float Y)
        {
            this.image = image;
            this.X = X;
            this.Y = Y;
            Position = new Vector2(X, Y);
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, 0f, Size / 2f, 1, 0, 0);
        }

        public void SetPosition(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}