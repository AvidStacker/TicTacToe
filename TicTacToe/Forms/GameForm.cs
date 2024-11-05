using System;
using System.Windows.Forms;
using TicTacToe.GameContent;

namespace TicTacToe.Forms
{
    public partial class GameForm : Form
    {
        private Game _game;

        public GameForm()
        {
            InitializeComponent();
            this._game = new Game();

            // Set up event handlers for the Game class
            this._game.TurnChanged += this.OnTurnChanged;
            this._game.GameWon += this.OnGameWon;
            this._game.IllegalMove += this.OnIllegalMove;
            this._game.Draw += this.OnDraw;
            this._game.GameReset += this.OnGameReset;
            UpdatePlayerDisplay();
            InitializeButtonEvents();
            UpdatePlayerHighscore();
        }

        private void InitializeButtonEvents()
        {
            // Attach click events to board buttons
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int r = row;
                    int c = col;
                    _boardButtons[r, c].Click += (sender, args) => OnCellClicked(r, c);
                    _boardButtons[r, c].TabStop = false;
                }
                this.ActiveControl = null;
            }

            // Attach events to Save, Load, Reset, New Game, and Undo buttons
            saveButton.Click += (sender, args) => _game.SaveGame("gameState.json");
            loadButton.Click += (sender, args) => _game.LoadGame("gameState.json");
            newGameButton.Click += (sender, args) => StartNewGame();

            // Add an Undo button handler to call the Undo method
            undoButton.Click += (sender, args) => UndoLastMove();
        }

        private void UndoLastMove()
        {
            _game.Undo(); // Call the Undo method in Game

            // Update the UI after an undo operation
            UpdateBoardUI();
            UpdatePlayerDisplay();
            messageLabel.Text = "Last move undone!";
        }

        private void OnCellClicked(int row, int col)
        {
            _game.UpdateBoard(row, col);
        }

        private void UpdateBoardUI()
        {
            char[,] boardData = _game.GetBoardState();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _boardButtons[row, col].Text = boardData[row, col].ToString();
                    _boardButtons[row, col].Enabled = boardData[row, col] == ' ';
                }
            }
        }

        private void UpdatePlayerDisplay()
        {
            playerLabel.Text = $"Player's turn: {_game.CurrentPlayerName}";
        }

        private void UpdatePlayerHighscore()
        {
            highscore.Text = $"Highscore: {_game.CurrentPlayerHighScore}";
        }

        private void OnTurnChanged(string playerName)
        {
            UpdatePlayerDisplay();
            UpdateBoardUI();
        }

        private void OnGameWon(string playerName)
        {
            messageLabel.Text = $"Game won by {playerName}!";
            DisableBoard();
        }

        private void OnIllegalMove(string message)
        {
            messageLabel.Text = message;
        }

        private void OnDraw()
        {
            messageLabel.Text = "It's a draw!";
            DisableBoard();
        }

        private void OnGameReset()
        {
            messageLabel.Text = "Game reset!";
            UpdateBoardUI();
            EnableBoard();
        }

        private void StartNewGame()
        {
            _game.StartNewGame();
            messageLabel.Text = "Starting a new game!";
            UpdateBoardUI();
            EnableBoard();
            UpdatePlayerDisplay();
            UpdatePlayerHighscore();
        }

        private void DisableBoard()
        {
            foreach (var button in _boardButtons)
                button.Enabled = false;
        }

        private void EnableBoard()
        {
            foreach (var button in _boardButtons)
                button.Enabled = true;
        }
    }
}
