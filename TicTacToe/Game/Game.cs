using System;
using System.IO;
using System.Text.Json;
using TicTacToe.Core.Game.Player;
using TicTacToe.Core.Game;
using TicTacToe.Game.Board;
using TicTacToe.Game;

namespace TicTacToe.Core.Game
{
    public class Game : IGame
    {
        private PlayerManager _playerManager;
        private Board _board;

        // Define events to notify the GameForm of state changes
        public event Action<string> TurnChanged; // Notifies when the turn changes
        public event Action<string> GameWon; // Notifies when the game is won
        public event Action<string> IllegalMove; // Notifies when an illegal move is attempted
        public event Action Draw; // Notifies when the game is a draw

        public Game()
        {
            this._playerManager = new PlayerManager();
            this._board = new Board();
            this._board.StateChanged += OnBoardStateChanged;
        }

        public void SaveGame(string filePath)
        {
            var gameState = new GameState
            {
                Players = this._playerManager.GetPlayersData(),
                CurrentPlayerSymbol = this._playerManager.GetCurrentPlayerSymbol(),
                CurrentPlayerName = this._playerManager.GetCurrentPlayerName(),
                BoardStateData = this._board.GetBoardStateData() // Use the constructor
            };

            string jsonString = JsonSerializer.Serialize(gameState);
            File.WriteAllText(filePath, jsonString);
        }

        public void LoadGame(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var gameState = JsonSerializer.Deserialize<GameState>(jsonString);

                // Restore players
                this._playerManager.RestorePlayers(gameState.Players);

                // Restore the board state and current player
                this._board.LoadState(gameState.BoardStateData); // This should work if gameState.BoardState is BoardStateData
                this._playerManager.SetCurrentPlayer(gameState.CurrentPlayerName); // Set the current player based on the game state
            }
        }

        public void OnBoardStateChanged(object sender, BoardState newState) // Implementing the interface method
        {
            this.CheckGameState();
        }

        public void CheckGameState() // Implementing the interface method
        {
            if (this._board.CurrentState == BoardState.Draw)
            {
                this.Draw?.Invoke(); // Notify the GameForm of a draw
            }
            else if (this._board.CurrentState == BoardState.XWins || this._board.CurrentState == BoardState.OWins)
            {
                this.GameWon?.Invoke(this._playerManager.GetCurrentPlayerName()); // Notify the GameForm of a win
            }
        }

        public void UpdateBoard(int row, int col) // Implementing the interface method
        {
            char currentSymbol = this._playerManager.GetCurrentPlayerSymbol()[0];

            if (this._board.MakeMove(row, col, currentSymbol))
            {
                this.TurnChanged?.Invoke(_playerManager.GetCurrentPlayerName()); // Notify the current player change
                this._playerManager.SwitchPlayer();
            }
            else
            {
                // Handle invalid move (you might want to raise an event or log this)
                this.IllegalMove?.Invoke("Illegal move"); // Corrected invocation
            }
        }

        public void ResetGame()
        {
            this._playerManager.Reset(); // Reset player states if necessary
            this._board.Reset(); // Reset the board
        }
    }
}
