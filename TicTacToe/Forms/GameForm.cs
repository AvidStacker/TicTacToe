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
            InitializeComponent(); // Call to initialize controls defined in the designer
            this._game = new Game();

            // Set up event handlers for the Game class
            this._game.TurnChanged += this.OnTurnChanged;
            this._game.GameWon += this.OnGameWon;
            this._game.IllegalMove += this.OnIllegalMove;
            this._game.Draw += this.OnDraw;
            this._game.GameReset += this.OnGameReset;

            UpdatePlayerDisplay();
            InitializeButtonEvents(); // Initialize button events
        }

        private void InitializeButtonEvents()
        {
            // Attach click events to board buttons
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int r = row; // Capture the current value of row
                    int c = col; // Capture the current value of col
                    _boardButtons[r, c].Click += (sender, args) => OnCellClicked(r, c);
                }
            }

            // Attach events to Save, Load, Reset, and New Game buttons
            saveButton.Click += (sender, args) => _game.SaveGame("gameState.json");
            loadButton.Click += (sender, args) => _game.LoadGame("gameState.json");
            resetButton.Click += (sender, args) => ResetGame();
            newGameButton.Click += (sender, args) => StartNewGame(); // New Game button event
        }

        private void OnCellClicked(int row, int col)
        {
            _game.UpdateBoard(row, col);
        }

        private void UpdateBoardUI()
        {
            char[,] boardData = _game.GetBoardState(); // Get the current board state

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _boardButtons[row, col].Text = boardData[row, col].ToString();
                    _boardButtons[row, col].Enabled = boardData[row, col] == ' '; // Enable button only if cell is blank
                }
            }
        }

        private void UpdatePlayerDisplay()
        {
            playerLabel.Text = $"Player's turn: {_game.CurrentPlayerName}";
        }

        private void OnTurnChanged(string playerName)
        {
            UpdatePlayerDisplay(); // Update player display when turn changes
            UpdateBoardUI();
        }

        private void OnGameWon(string playerName)
        {
            messageLabel.Text = $"Game won by {playerName}!";
            DisableBoard(); // Disable buttons to prevent further moves
        }

        private void OnIllegalMove(string message)
        {
            messageLabel.Text = message; // Display the illegal move message
        }

        private void OnDraw()
        {
            messageLabel.Text = "It's a draw!";
            DisableBoard();
        }

        private void OnGameReset()
        {
            messageLabel.Text = "Game reset!"; // Notify that the game has been reset
            UpdateBoardUI(); // Clear board UI
            EnableBoard(); // Re-enable board buttons
        }

        private void ResetGame()
        {
            _game.StartNewGame(); // Reset the game state
            UpdateBoardUI(); // Update the board display
            EnableBoard(); // Ensure the board is enabled for a new game
        }

        private void StartNewGame()
        {
            _game.ResetGame(); // Reset the game state for a fresh start
            messageLabel.Text = "Starting a new game!"; // Update message label
            UpdateBoardUI(); // Clear the board UI
            EnableBoard(); // Enable board buttons
            UpdatePlayerDisplay(); // Show the initial player's turn
        }

        private void DisableBoard()
        {
            foreach (var button in _boardButtons)
                button.Enabled = false; // Disable all buttons
        }

        private void EnableBoard()
        {
            foreach (var button in _boardButtons)
                button.Enabled = true; // Enable all buttons
        }
    }
}
