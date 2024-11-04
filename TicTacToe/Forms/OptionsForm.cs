using System;
using System.Windows.Forms;

namespace TicTacToe.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        // Event handler for form load event
        private void OptionsForm_Load(object sender, EventArgs e)
        {
            // Build the path to the music file based on the application's startup path
            string musicFilePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "Desktop.mp3");

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

        // Event handler for label click event
        private void label1_Click(object sender, EventArgs e)
        {
            // Handle Options_Label click event if necessary
        }

        // Event handler for turn off music button click event
        private void TurnOff_button_Click(object sender, EventArgs e)
        {
            // Check if music is currently on
            if (MusicManager.IsMusicOn())
            {
                // Turn off the music and update button text
                MusicManager.TurnOffMusic();
                TurnOff_button.Text = "Turn On Music";
            }
            else
            {
                // Turn on the music and update button text
                MusicManager.TurnOnMusic();
                TurnOff_button.Text = "Turn Off Music";
            }
        }

        private void ResetHighScore_Click(object sender, EventArgs e)
        {
            MessageBox.Show("High scores have been reset!", "Reset High Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Event handler for back to main menu button click event
        private void BackToMain_button_Click(object sender, EventArgs e)
        {
            // Close the options form and return to the main menu
            this.Close();
        }

        // Duplicate event handler for form load event (can be removed if not needed)
        private void OptionsForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
