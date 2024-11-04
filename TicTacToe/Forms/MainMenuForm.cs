using System;
using System.Diagnostics;
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
            // Build the path to the music file based on the application's startup path

            string musicFilePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "Desktop.mp3");
            Debug.WriteLine(musicFilePath);
            // Check if the file exists before trying to initialize the MusicManager
            if (System.IO.File.Exists(musicFilePath))
            {
                MusicManager.Initialize(musicFilePath);
            }
            else
            {
                MessageBox.Show("Music file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
