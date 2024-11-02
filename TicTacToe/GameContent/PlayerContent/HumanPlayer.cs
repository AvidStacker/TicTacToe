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
        private int highscore;

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

        public int GetHighscore()
        {
            return this.highscore;
        }

        public void UpdateHighscore(int highscore)
        {
            this.SetHighscore(highscore);
        }

        // Private method to set highscore
        void SetHighscore(int highscore)
        {
            if (highscore > this.highscore)
            {
                this.highscore = highscore;
            }
        }

        public PlayerData GetPlayerData()
        {
            return new PlayerData
            {
                Name = this.name,
                Symbol = this.symbol,
                HighScore = this.highscore
            };
        }
    }
}