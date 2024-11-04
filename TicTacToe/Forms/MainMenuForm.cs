using System;
using System.Windows.Forms;
using TicTacToe.Forms;

namespace TicTacToe
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label click event
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.FormClosed += (s, args) => this.Show(); // Show MainMenuForm when GameForm is closed
            gameForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.FormClosed += (s, args) => this.Show(); // Show MainMenuForm when OptionsForm is closed
            optionsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Handle Exit button click event
            this.Close();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // Initialize MusicManager with a valid music file path
            MusicManager.Initialize("C:\\Users\\Eshdi\\Source\\Repos\\TicTacToe911\\TicTacToe\\Resources\\Desktop.mp3");
        }
    }
}
