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
            this.UpdatePlayerDisplay();
            this.InitializeButtonEvents(); // Initialize button events
            this.UpdatePlayerHighScore();
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
                    this._boardButtons[r, c].Click += (sender, args) => this.OnCellClicked(r, c);
                }
            }

            // Attach events to Save, Load, Reset, and New Game buttons
            this.saveButton.Click += (sender, args) => this._game.SaveGame("gameState.json");
            this.loadButton.Click += (sender, args) => this._game.LoadGame("gameState.json");
            
            this.newGameButton.Click += (sender, args) => this.StartNewGame(); // New Game button event
        }

        private void OnCellClicked(int row, int col)
        {
            this._game.UpdateBoard(row, col);
        }

        private void UpdateBoardUI()
        {
            char[,] boardData = this._game.GetBoardState(); // Get the current board state

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    this._boardButtons[row, col].Text = boardData[row, col].ToString();
                    this._boardButtons[row, col].Enabled = boardData[row, col] == ' '; // Enable button only if cell is blank
                }
            }
        }

        private void UpdatePlayerDisplay()
        {
            this.playerLabel.Text = $"Player's turn: {this._game.CurrentPlayerName}";
        }

        private void UpdatePlayerHighScore()
        {
            this.HighScore.Text = $"HighScore: {this._game.CurrentPlayerHighScore}";
        }

        private void OnTurnChanged()
        {
            this.UpdatePlayerDisplay(); // Update player display when turn changes
            this.UpdatePlayerHighScore();
            this.UpdateBoardUI();
        }

        private void OnGameWon(string playerName)
        {
            this.messageLabel.Text = $"Game won by {playerName}!";
            this.DisableBoard(); // Disable buttons to prevent further moves
        }

        private void OnIllegalMove(string message)
        {
            this.messageLabel.Text = message; // Display the illegal move message
        }

        private void OnDraw()
        {
            this.messageLabel.Text = "It's a draw!";
            this.DisableBoard();
        }

        private void OnGameReset()
        {
            this.messageLabel.Text = "Game reset!"; // Notify that the game has been reset
            this.UpdateBoardUI(); // Clear board UI
            this.EnableBoard(); // Re-enable board buttons
        }

        private void StartNewGame()
        {
            this._game.StartNewGame(); // Reset the game state for a fresh start
            this.messageLabel.Text = "Starting a new game!"; // Update message label
            this.UpdateBoardUI(); // Clear the board UI
            this.EnableBoard(); // Enable board buttons
            this.UpdatePlayerDisplay(); // Show the initial player's turn
            this.UpdatePlayerHighScore();
        }

        private void DisableBoard()
        {
            foreach (var button in this._boardButtons)
                button.Enabled = false; // Disable all buttons
        }

        private void EnableBoard()
        {
            foreach (var button in this._boardButtons)
                button.Enabled = true; // Enable all buttons
        }
    }
}
