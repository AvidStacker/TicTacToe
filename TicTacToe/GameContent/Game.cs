using System;
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

        // Define events to notify the GameForm of state changes
        public event Action<string> TurnChanged; // Notifies when the turn changes
        public event Action<string> GameWon; // Notifies when the game is won
        public event Action<string> IllegalMove; // Notifies when an illegal move is attempted
        public event Action Draw; // Notifies when the game is a draw
        public event Action GameReset; // Notifies when the game is reset

        public Game()
        {
            this._playerManager = new PlayerManager();
            this._board = new Board();
            this._board.StateChanged += this.OnBoardStateChanged;
        }

        public void StartNewGame()
        {
            this._board.Reset();
            this._playerManager.LoadPlayers();
            GameReset?.Invoke();
            TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
        }

        public void SaveGame(string filePath)
        {
            var gameState = new GameState
            {
                Players = this._playerManager.GetPlayersData(),
                CurrentPlayerSymbol = this._playerManager.GetCurrentPlayerSymbol(),
                CurrentPlayerName = this._playerManager.GetCurrentPlayerName(),
                BoardStateData = this._board.GetBoardStateData()
            };
            this._playerManager.SavePlayers();

            string jsonString = JsonSerializer.Serialize(gameState);
            File.WriteAllText(filePath, jsonString);
        }

        public void LoadGame(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var gameState = JsonSerializer.Deserialize<GameState>(jsonString);

                this._playerManager.RestorePlayers(gameState.Players);
                this._board.LoadState(gameState.BoardStateData);
                this._playerManager.SetCurrentPlayer(gameState.CurrentPlayerName);
                

                // Update GameForm based on the loaded state
                this.OnBoardStateChanged(this, this._board.GetBoardState());

                // Trigger events to refresh the UI
                TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
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

        public void UpdateBoard(int row, int col)
        {
            char currentSymbol = this._playerManager.GetCurrentPlayerSymbol();

            if (this._board.MakeMove(row, col, currentSymbol))
            {
                this.TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
                this.OnBoardStateChanged(this, this._board.GetBoardState());
                this._playerManager.SwitchPlayer();
            }
            else
            {
                this.IllegalMove?.Invoke("Illegal move");
            }
        }

        public void ResetGame()
        {
            this._playerManager.Reset();
            this._board.Reset();
            this.GameReset?.Invoke();
        }

        // Provide current player's name and symbol for UI
        public string CurrentPlayerName => this._playerManager.GetCurrentPlayerName();
        public char CurrentPlayerSymbol => this._playerManager.GetCurrentPlayerSymbol();
        public int CurrentPlayerHighScore => this._playerManager.GetCurrentPlayerHighScore();

        // Retrieve the current state of the board
        public char[,] GetBoardState()
        {
            return this._board.GetGridState(); // Assumes GetCells returns a 2D char array of the board
        }
    }
}
