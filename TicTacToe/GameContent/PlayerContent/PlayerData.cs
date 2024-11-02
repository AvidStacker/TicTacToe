using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.GameContent.PlayerContent
{
    public class PlayerData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int HighScore { get; set; }

        public Color Color { get; set; }
    }
}
