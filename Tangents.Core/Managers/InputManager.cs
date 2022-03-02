using Microsoft.Xna.Framework;
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

        public static bool WasTextReleased(GameText text, Matrix scaleMatrix)
        {
            if (WasMouseReleased()) {
                return CheckCollision(prevMouseState.Position.ToVector2(), text);
            }

            if (WasScreenReleased()) {
                Vector2 scaleVector = new Vector2(scaleMatrix.M11, scaleMatrix.M22);
                TouchCollection.Enumerator touches = prevTouchState.GetEnumerator();

                while (touches.MoveNext()) {
                    if (CheckCollision(touches.Current.Position / scaleVector, text)) {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckCollision(Vector2 point, GameText text)
        {
            if (text.Centered) {
                float textX = text.Position.X - text.Midpoint.X;
                float textY = text.Position.Y - text.Midpoint.Y;
                return point.X >= textX && point.X <= textX + text.Size.X && point.Y >= textY && point.Y <= textY + text.Size.Y;
            } else {
                return point.X >= text.Position.X && point.X <= text.Position.X + text.Size.X && point.Y >= text.Position.Y && point.Y <= text.Position.Y + text.Size.Y;
            }
        }
    }
}
