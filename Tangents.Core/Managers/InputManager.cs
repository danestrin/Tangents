using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tangents
{
    public static class InputManager
    {
        private static KeyboardState prevState, currentState;

        public static void Update()
        {
            prevState = currentState;
            currentState = Keyboard.GetState();
        }

        public static bool WasKeyReleased(Keys key)
        {
            return prevState.IsKeyDown(key) && currentState.IsKeyUp(key);
        }

        public static bool WasKeyPressed(Keys key)
        {
            return prevState.IsKeyUp(key) && currentState.IsKeyDown(key);
        }
    }
}
