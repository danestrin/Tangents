using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Tangents
{
    public static class InputManager
    {
        private static KeyboardState prevKeyState, currentKeyState;
        private static TouchCollection prevTouchState, currentTouchState;
        private static MouseState prevMouseState, currentMouseState;

        public static void Update()
        {
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            prevTouchState = currentTouchState;
            currentTouchState = TouchPanel.GetState();

            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public static bool WasKeyReleased(Keys key)
        {
            return prevKeyState.IsKeyDown(key) && currentKeyState.IsKeyUp(key);
        }

        public static bool WasKeyPressed(Keys key)
        {
            return prevKeyState.IsKeyUp(key) && currentKeyState.IsKeyDown(key);
        }

        public static bool WasScreenTouched()
        {
            return prevTouchState.Count == 0 && currentTouchState.Count > 0;
        }

        public static bool WasScreenReleased()
        {
            return prevTouchState.Count > 0 && currentTouchState.Count == 0;
        }

        public static bool WasMouseClicked()
        {
            return prevMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool WasMouseReleased()
        {
            return prevMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released;
        }
    }
}
