using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TicTacToe.GameContent.PlayerContent;
using TicTacToe.GameContent.BoardContent;
using TicTacToe.GameContent;

namespace TicTacToe.GameContent
{
    public class Game : IGame
    {
        private PlayerManager _playerManager;
        private Board _board;

        // Stack to store the history of game states for undo functionality
        private Stack<GameState> _history;

        // Define events to notify the GameForm of state changes
        public event Action<string> TurnChanged;
        public event Action<string> GameWon;
        public event Action<string> IllegalMove;
        public event Action Draw;
        public event Action GameReset;

        public Game()
        {
            this._playerManager = new PlayerManager();
            this._board = new Board();
            this._history = new Stack<GameState>(); // Initialize the history stack
            this._board.StateChanged += this.OnBoardStateChanged;
        }

        public void StartNewGame()
        {
            this._board.Reset();
            this._playerManager.LoadPlayers();
            _history.Clear(); // Clear history at the start of a new game
            GameReset?.Invoke();
            TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
        }

        public void SaveGame(string filePath)
        {
            var gameState = CreateGameState();
            string jsonString = JsonSerializer.Serialize(gameState);
            File.WriteAllText(filePath, jsonString);
        }

        public void LoadGame(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var gameState = JsonSerializer.Deserialize<GameState>(jsonString);

                RestoreGameState(gameState);

                OnBoardStateChanged(this, this._board.GetBoardState());
                TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
            }
        }

        private GameState CreateGameState()
        {
            return new GameState
            {
                Players = this._playerManager.GetPlayersData(),
                CurrentPlayerSymbol = this._playerManager.GetCurrentPlayerSymbol(),
                CurrentPlayerName = this._playerManager.GetCurrentPlayerName(),
                BoardStateData = this._board.GetBoardStateData()
            };
        }

        private void RestoreGameState(GameState gameState)
        {
            this._playerManager.RestorePlayers(gameState.Players);
            this._board.LoadState(gameState.BoardStateData);
            this._playerManager.SetCurrentPlayer(gameState.CurrentPlayerName);
        }

        public void UpdateBoard(int row, int col)
        {
            char currentSymbol = this._playerManager.GetCurrentPlayerSymbol();

            // Only save the current game state to history if the move is legal
            if (this._board.MakeMove(row, col, currentSymbol))
            {
                // Save the game state after making the move
                this._history.Push(CreateGameState());

                // Notify listeners and switch player
                this.OnBoardStateChanged(this, this._board.GetBoardState());
                this._playerManager.SwitchPlayer();
                this.TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
            }
            else
            {
                this.IllegalMove?.Invoke("Illegal move");
            }
        }


        public void Undo()
        {
            if (this._history.Count > 0)
            {
                var previousState = this._history.Pop();
                RestoreGameState(previousState);

                // Notify listeners to update the UI based on the restored state
                OnBoardStateChanged(this, _board.GetBoardState());
                this.TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
            }
        }

        public void OnBoardStateChanged(object sender, BoardState newState)
        {
            this.CheckGameState();
        }

        public void CheckGameState()
        {
            if (this._board.GetBoardState() == BoardState.Draw)
            {
                this.Draw?.Invoke();
            }
            else if (this._board.GetBoardState() == BoardState.XWins || this._board.GetBoardState() == BoardState.OWins)
            {
                string winningPlayer = this._playerManager.GetCurrentPlayerName();
                this.GameWon?.Invoke(winningPlayer);
                this._playerManager.UpdatePlayerHighscore(winningPlayer);
            }
        }

        public void ResetGame()
        {
            this._playerManager.Reset();
            this._board.Reset();
            this._history.Clear(); // Clear history on reset
            this.GameReset?.Invoke();
        }

        public string CurrentPlayerName => this._playerManager.GetCurrentPlayerName();
        public char CurrentPlayerSymbol => this._playerManager.GetCurrentPlayerSymbol();
        public int CurrentPlayerHighScore => this._playerManager.GetCurrentPlayerHighScore();

        public char[,] GetBoardState()
        {
            return this._board.GetGridState();
        }
    }
}
