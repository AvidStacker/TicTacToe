using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Core.Game;

namespace TicTacToe
{
    public partial class GameForm : Form
    {
        private IGame game; // refrence to IGame interface
        private Button[,] button;   // rebresents the game board

        public GameForm(IGame game)
        {
            this.game = game;
            InitializeComponent();
            InitializeBoard(); // Set up the board UI
        }

        private void InitializeComponent()
        {
            this.Text = "Game Form";
            this.Size = new System.Drawing.Size(400, 400);
        }

        // Sets up the board UI with the buttons
        private void InitializeBoard()
        {
            int rows = 3;
            int cols = 3;
            buttons = new Button[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new System.Drawing.Size(100, 100);
                    buttons[i, j].Location = new System.Drawing.Point(i * 100, j * 100);
                    buttons[i, j].Click += ButtonClicked;  // Links to ButtonClicked event
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        // Handel the button clicks on the board
        private void ButtonClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int row = button.Location.Y / 100;
            int col = button.Location.X / 100;

            if (game.MakeMove(row, col))
            {
                button.Text = game.GetGameState(); // Update button text with current player's symbol
                button.Enabled = false;

                if (game.IsGameOver())
                {
                    MessageBox.Show("Game Over! " + game.GetGameState());
                    ResetBoard();
                }
            }
            else
            {
                MessageBox.Show("Invalid move. Try again.");
            }
        }

        // Resets the board for a new game
        private void ResetBoard()
        {
            foreach (Button button in buttons)
            {
                button.Text = string.Empty;
                button.Enabled = true;
            }
            game.StartGame();  // Restarting the game through IGame interface
        }


    }

}