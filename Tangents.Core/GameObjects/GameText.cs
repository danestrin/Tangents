using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tangents
{
    public class GameText
    {
        private bool centered;

        public SpriteFont Font { get; private set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public Vector2 Size
        {
            get
            {
                return Font.MeasureString(Text);
            }
        }

        public Vector2 Midpoint
        {
            get
            {
                return Size / 2;
            }
        }

        public GameText(SpriteFont font, string text, Vector2 position, Color color, bool centered)
        {
            this.centered = centered;
            Font = font;
            Text = text;
            Position = position;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (centered) {
                spriteBatch.DrawString(Font, Text, Position, Color, 0, Midpoint, 1.0f, SpriteEffects.None, 0.5f);
            }
            else {
                spriteBatch.DrawString(Font, Text, Position, Color, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
    }
}
