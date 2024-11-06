using System;
using System.Windows.Forms;

namespace TicTacToe.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        // Declare UI components
        private System.Windows.Forms.Label HighScore;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button newGameButton; // New Game button
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button[,] _boardButtons;
        private int _windowWidth = 1200;
        private int _windowHeight = 600;

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 580);
            this.Text = "Tic Tac Toe";
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;


            // Panel for the HighScore, player turn, and game status labels
            Panel infoPanel = new Panel
            {
                Size = new System.Drawing.Size(400, 100),
                Location = new System.Drawing.Point((this.ClientSize.Width - 400) / 2, 20),
                BackColor = System.Drawing.Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // HighScore Label
            this.HighScore = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkBlue,
                Location = new System.Drawing.Point(10, 10),
                Name = "HighScore",
                Size = new System.Drawing.Size(130, 20),
                TabIndex = 0,
                Text = "HighScore: 0"
            };

            // Player Turn Label
            this.playerLabel = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkGreen,
                Location = new System.Drawing.Point(10, 40),
                Name = "playerLabel",
                Size = new System.Drawing.Size(130, 20),
                TabIndex = 1,
                Text = "Player's turn: X"
            };

            // Game Status Label
            this.messageLabel = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkRed,
                Location = new System.Drawing.Point(10, 70),
                Name = "messageLabel",
                Size = new System.Drawing.Size(130, 20),
                TabIndex = 2,
                Text = "Game in progress"
            };

            // Add labels to the info panel
            infoPanel.Controls.Add(this.HighScore);
            infoPanel.Controls.Add(this.playerLabel);
            infoPanel.Controls.Add(this.messageLabel);

            // Initialize Game Board Buttons and center them
            this._boardButtons = new System.Windows.Forms.Button[3, 3];

            int buttonSize = 100;
            int spacing = 10;
            int boardWidth = 3 * buttonSize + 2 * spacing;
            int boardHeight = 3 * buttonSize + 2 * spacing;

            // Calculate the starting position for centering the board
            int startX = (this.ClientSize.Width - boardWidth) / 2;
            int startY = 150; // Position below the info panel

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    this._boardButtons[row, col] = new System.Windows.Forms.Button
                    {
                        Width = buttonSize,
                        Height = buttonSize,
                        Location = new System.Drawing.Point(startX + col * (buttonSize + spacing), startY + row * (buttonSize + spacing)),
                        Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold),
                        TabIndex = 6 + (row * 3 + col),
                        UseVisualStyleBackColor = true
                    };
                    this.Controls.Add(this._boardButtons[row, col]); // Add button to the form
                }
            }

            // Center the control buttons (New Game, Save, Load) below the board
            int buttonWidth = 75;
            int buttonHeight = 30;
            int buttonSpacing = 20; // spacing between the buttons
            int totalButtonsWidth = (buttonWidth * 3) + (buttonSpacing * 2);

            // Calculate starting X position to center the buttons below the board
            int buttonsStartX = (this.ClientSize.Width - totalButtonsWidth) / 2;
            int buttonsStartY = startY + boardHeight + 20; // Add a gap below the board

            // New Game Button
            this.newGameButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(buttonsStartX, buttonsStartY),
                Name = "newGameButton",
                Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                TabIndex = 3,
                Text = "New Game",
                UseVisualStyleBackColor = true
            };
            this.newGameButton.Click += new EventHandler(this.NewGameButton_Click);

            // Save Button
            this.saveButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(buttonsStartX + buttonWidth + buttonSpacing, buttonsStartY),
                Name = "saveButton",
                Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                TabIndex = 4,
                Text = "Save",
                UseVisualStyleBackColor = true
            };

            // Load Button
            this.loadButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(buttonsStartX + 2 * (buttonWidth + buttonSpacing), buttonsStartY),
                Name = "loadButton",
                Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                TabIndex = 5,
                Text = "Load",
                UseVisualStyleBackColor = true
            };

            // Add all controls to the form
            this.Controls.Add(infoPanel); // Add the info panel with labels
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
        }




        // Event handler for New Game button click
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            // Logic to start a new game, such as calling StartNewGame method from the Game class
            // Example: game.StartNewGame("Player 1", "Player 2");
        }

        // Cleanup method for disposing resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
