using System;
using TicTacToe.Core.Game.Player;
using TicTacToe.Core.Game;

public class Game
{
    private PlayerManager _playerManager;
    private Board _board;

    public Game()
    {
        InitializeGame(); // Initialize the game immediately
    }

    private void CheckGameState()
    {
        if (_board.IsFull())
        {
            _board.CurrentState = BoardState.Draw; // Triggers event
        }
        else
        {
            if (_board.HasPlayerWon(_playerManager.GetCurrentPlayerSymbol()))
            {
                _playerManager.NotifyPlayerWon(_playerManager.GetCurrentPlayerSymbol());
                _board.CurrentState = BoardState.Wins; // Triggers event
            }
        }
    }


    private void OnBoardStateChanged(object sender, BoardState newState)
    {
        Console.WriteLine($"Game Over! State: {newState}");
        // Handle game over scenario
    }

    public void InitializeGame()
    {
        this._playerManager = new PlayerManager();
        this._board = new Board();
        this._board.StateChanged += OnBoardStateChanged; // Subscribe to events
    }

    public void UpdateBoard(int row, int col)
    {
        if (this._board.MakeMove(row, col, _playerManager.GetCurrentPlayerSymbol()))
        {
            CheckGameState(); // Check state after a valid move
        }
    }
}
