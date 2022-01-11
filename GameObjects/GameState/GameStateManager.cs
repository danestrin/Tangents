using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tangents
{
    public class GameStateManager
    {
        private static GameStateManager instance;

        private GameStateManager() {}

        public GameState GameState { get; set; }

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null) {
                    instance = new GameStateManager();
                }

                return instance;
            }
        }
    }
}