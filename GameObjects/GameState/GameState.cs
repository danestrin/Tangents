using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public abstract class GameState
    {
        protected GameStateManager gameStateManager;
        protected int width;
        protected int height;
        protected KeyboardState prevKeyState;

        public abstract void OnBegin();

        public abstract void OnEnd();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}