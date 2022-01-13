using System.Collections.Generic;

namespace Tangents
{
    public class GameStateManager
    {
        private static GameStateManager instance;
        private GameState currentGameState;
        private GameState prevGameState;

        private GameStateManager()
        {
            GameStateMap = new Dictionary<GameStateID, GameState>();
        }

        public GameState CurrentGameState {
            get
            {
                return currentGameState;
            }
            set
            {
                if (currentGameState != null)
                {
                    this.prevGameState = currentGameState;
                    this.prevGameState.OnEnd();
                }

                this.currentGameState = value;
                this.currentGameState.OnBegin();
            }
        }

        public Dictionary<GameStateID, GameState> GameStateMap { get; set; }

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

        public enum GameStateID
        {
            Title,
            InGame,
            GameOver
        }
    }
}