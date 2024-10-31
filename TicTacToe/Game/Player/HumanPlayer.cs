using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Game.Player
{
    public class HumanPlayer : IPlayer
    {
        private string name;
        private string symbol;
        private Color color;
        private int highscore;

        public HumanPlayer(string name, string symbol, Color color)
        {
            this.name = name;
            this.symbol = symbol;
            this.color = color;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSymbol()
        {
            return symbol;
        }

        public Color GetColor()
        {
            return color;
        }

        public int GetHighscore()
        {
            return highscore;
        }

        public void UpdateHighscore(int highscore)
        {
            SetHighscore(highscore);
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
                Name = name,
                Symbol = symbol,
                HighScore = highscore
            };
        }

    }

}