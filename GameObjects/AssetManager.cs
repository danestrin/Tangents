using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tangents
{
    static class AssetManager
    {
        public static Texture2D BG { get; private set; }
        public static Texture2D Circle { get; private set; }
        public static Texture2D Player { get; private set; }
        public static SpriteFont Header { get; private set; }

        public static void Load(ContentManager content)
        {
            BG = content.Load<Texture2D>("bg");
            Circle = content.Load<Texture2D>("circle");
            Player = content.Load<Texture2D>("player");
            Header = content.Load<SpriteFont>("Header");
        }
    }
}