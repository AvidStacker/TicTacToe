using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public static class MusicManager
    {
        private static WaveOutEvent wavePlayer; // Use WaveOutEvent instead of IWavePlayer
        private static AudioFileReader audioFileReader; // Reads audio files
        private static bool isMusicOn = true; // Tracks if music is currently playing

        // Initializes the music player with the specified music file
        public static void Initialize(string musicFilePath)
        {
            try
            {
                wavePlayer = new WaveOutEvent(); // Creates a new WaveOutEvent for playback
                audioFileReader = new AudioFileReader(musicFilePath); // Reads the specified audio file
                wavePlayer.Init(audioFileReader); // Initializes the player with the audio file
                wavePlayer.Play(); // Starts playing the music
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing music: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Stops the music and updates the isMusicOn flag
        public static void TurnOffMusic()
        {
            wavePlayer.Stop(); // Stops the music
            isMusicOn = false; // Updates the flag to indicate music is off
        }

        // Starts the music and updates the isMusicOn flag
        public static void TurnOnMusic()
        {
            wavePlayer.Play(); // Starts playing the music
            isMusicOn = true; // Updates the flag to indicate music is on
        }

        // Returns the current state of the music (on or off)
        public static bool IsMusicOn()
        {
            return isMusicOn; // Returns the value of isMusicOn
        }
    }
}
