using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameContent.PlayerContent;

namespace TicTacToe.GameContent.PlayerContent
{
    public class HumanPlayer : IPlayer
    {
        private string name;
        private char symbol;
        private Color color;
        private int HighScore;

        public HumanPlayer(string name, char symbol, Color color)
        {
            this.name = name;
            this.symbol = symbol;
            this.color = color;
        }

        public string GetName()
        {
            return this.name;
        }

        public char GetSymbol()
        {
            return this.symbol;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public int GetHighScore()
        {
            return this.HighScore;
        }

        public void UpdateHighScore(int HighScore)
        {
            this.SetHighScore(HighScore);
        }

        // Private method to set HighScore
        void SetHighScore(int HighScore)
        {
            this.HighScore = HighScore;
        }

        public PlayerData GetPlayerData()
        {
            return new PlayerData
            {
                Name = this.name,
                Symbol = this.symbol,
                HighScore = this.HighScore,
                Color = (this.color.R, this.color.G, this.color.B)
            };
        }
    }
}