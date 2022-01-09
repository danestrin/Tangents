using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public override void Update(GameTime gameTime)
        {
            // TODO
        }
    }
}