using System;
using System.Windows.Forms;

namespace TicTacToe.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        // Declare UI components
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button[,] _boardButtons;

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Tic Tac Toe";

            // Player Label
            this.playerLabel = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(10, 20),
                Name = "playerLabel",
                Size = new System.Drawing.Size(130, 20),
                TabIndex = 0,
                Text = "Player's turn: "
            };

            // Message Label
            this.messageLabel = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(10, 50),
                Name = "messageLabel",
                Size = new System.Drawing.Size(100, 20),
                TabIndex = 1,
                Text = "Game status"
            };

            // Save Button
            this.saveButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(250, 20),
                Name = "saveButton",
                Size = new System.Drawing.Size(75, 30),
                TabIndex = 2,
                Text = "Save",
                UseVisualStyleBackColor = true
            };

            // Load Button
            this.loadButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(330, 20),
                Name = "loadButton",
                Size = new System.Drawing.Size(75, 30),
                TabIndex = 3,
                Text = "Load",
                UseVisualStyleBackColor = true
            };

            // Reset Button
            this.resetButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(410, 20),
                Name = "resetButton",
                Size = new System.Drawing.Size(75, 30),
                TabIndex = 4,
                Text = "Reset",
                UseVisualStyleBackColor = true
            };

            // Initialize Game Board Buttons
            this._boardButtons = new System.Windows.Forms.Button[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    this._boardButtons[row, col] = new System.Windows.Forms.Button
                    {
                        Width = 100,
                        Height = 100,
                        Location = new System.Drawing.Point(100 * col + 10, 100 * row + 80),
                        Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold),
                        TabIndex = 5 + (row * 3 + col),
                        UseVisualStyleBackColor = true
                    };
                    this.Controls.Add(this._boardButtons[row, col]); // Add button to the form
                }
            }

            // Add all controls to the form
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.resetButton);
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
