using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tangents
{
    abstract class Entity
    {
        protected Texture2D image;

        public Vector2 Position { get; set; }
        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, this.Position, null, Color.White, 0f, this.Size / 2f, 1, 0, 0);
        }
    }
}