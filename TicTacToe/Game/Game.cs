using System;
using System.IO;
using System.Text.Json;
using TicTacToe.Core.Game.Player;
using TicTacToe.Core.Game;
using TicTacToe.Game.Board;
using TicTacToe.Game;

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
        _playerManager = new PlayerManager();
        InitializeGame();
    }

    public void InitializeGame() // Implementing the interface method
    {
        _board = new Board();
        _board.StateChanged += OnBoardStateChanged;
    }

    public void SaveGame(string filePath)
    {
        var gameState = new GameState
        {
            Players = _playerManager.GetPlayersData(),
            CurrentPlayerSymbol = _playerManager.GetCurrentPlayerSymbol(),
            CurrentPlayerName = _playerManager.GetCurrentPlayerName(),
            BoardState = new BoardStateData(_board.GetCurrentGrid(), _board.GetBoardState()) // Use the constructor
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
            _playerManager.RestorePlayers(gameState.Players);

            // Restore the board state and current player
            _board.LoadState(gameState.BoardState); // This should work if gameState.BoardState is BoardStateData
            _playerManager.SetCurrentPlayer(gameState.CurrentPlayerName); // Set the current player based on the game state
        }
    }

    public void OnBoardStateChanged(object sender, BoardState newState) // Implementing the interface method
    {
        CheckGameState();
    }

    public void CheckGameState() // Implementing the interface method
    {
        if (_board.CurrentState == BoardState.Draw)
        {
            Draw?.Invoke(); // Notify the GameForm of a draw
        }
        else if (_board.CurrentState == BoardState.XWins || _board.CurrentState == BoardState.OWins)
        {
            GameWon?.Invoke(_playerManager.GetCurrentPlayerName()); // Notify the GameForm of a win
        }
    }

    public void UpdateBoard(int row, int col) // Implementing the interface method
    {
        char currentSymbol = _playerManager.GetCurrentPlayerSymbol()[0];

        if (_board.MakeMove(row, col, currentSymbol))
        {
            TurnChanged?.Invoke(_playerManager.GetCurrentPlayerName()); // Notify the current player change
            _playerManager.SwitchPlayer();
        }
        else
        {
            // Handle invalid move (you might want to raise an event or log this)
            IllegalMove?.Invoke("Illegal move"); // Corrected invocation
        }
    }

    public void ResetGame()
    {
        _playerManager.Reset(); // Reset player states if necessary
        _board.Reset(); // Reset the board
    }
}
