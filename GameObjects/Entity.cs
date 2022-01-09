using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tangents
{
    abstract class Entity
    {
        protected Texture2D image;

        public Vector2 position;
        public Vector2 size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, null, Color.White, 0f, size / 2f, 1, 0, 0);
        }
    }
}