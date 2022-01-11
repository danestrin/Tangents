using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tangents
{
    public abstract class GameState
    {
        protected GameStateManager gameStateManager;
        protected int width;
        protected int height;

        public abstract void OnBegin();

        public abstract void OnEnd();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}