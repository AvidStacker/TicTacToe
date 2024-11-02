using System;
using System.Windows.Forms;
using TicTacToe.GameContent

namespace TicTacToe.Forms
{
    public partial class GameForm : Form
    {
        private Game _game;
        private Button[,] _boardButtons;
        private Label playerLabel;
        private Label messageLabel;

        public GameForm()
        {
            InitializeComponent();
            _game = new Game();

            // Set up event handlers for the Game class
            _game.TurnChanged += OnTurnChanged;
            _game.GameWon += OnGameWon;
            _game.IllegalMove += OnIllegalMove;
            _game.Draw += OnDraw;
            _game.GameReset += OnGameReset;

            InitializeBoard();
            UpdatePlayerDisplay();
        }

        private void InitializeBoard()
        {
            _boardButtons = new Button[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var button = new Button
                    {
                        Width = 100,
                        Height = 100,
                        Location = new System.Drawing.Point(100 * col, 100 * row),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold)
                    };
                    button.Click += (sender, args) => OnCellClicked(row, col);
                    _boardButtons[row, col] = button;
                    this.Controls.Add(button);
                }
            }

            // Add Labels and Buttons
            playerLabel = new Label { Text = "Player's turn: ", Location = new System.Drawing.Point(10, 320), AutoSize = true };
            messageLabel = new Label { Text = "Game status", Location = new System.Drawing.Point(10, 350), AutoSize = true };

            var saveButton = new Button { Text = "Save", Location = new System.Drawing.Point(250, 320) };
            saveButton.Click += (sender, args) => _game.SaveGame("gameState.json");

            var loadButton = new Button { Text = "Load", Location = new System.Drawing.Point(320, 320) };
            loadButton.Click += (sender, args) => _game.LoadGame("gameState.json");

            var resetButton = new Button { Text = "Reset", Location = new System.Drawing.Point(390, 320) };
            resetButton.Click += (sender, args) => ResetGame();

            this.Controls.Add(playerLabel);
            this.Controls.Add(messageLabel);
            this.Controls.Add(saveButton);
            this.Controls.Add(loadButton);
            this.Controls.Add(resetButton);
        }

        private void OnCellClicked(int row, int col)
        {
            _game.UpdateBoard(row, col);
            UpdateBoardUI();
        }

        private void UpdateBoardUI()
        {
            char[,] boardData = _game.GetBoardState(); // Get the current board state

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _boardButtons[row, col].Text = boardData[row, col].ToString();
                    _boardButtons[row, col].Enabled = boardData[row, col] == '\0'; // Enable button only if cell is empty
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
            _game.ResetGame(); // Reset the game state
            UpdateBoardUI(); // Update the board display
            EnableBoard(); // Ensure the board is enabled for a new game
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
