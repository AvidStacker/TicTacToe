using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.GameContent.PlayerContent
{
    public class PlayerData
    {
        public required string Name { get; set; }
        public char Symbol { get; set; }
        public int HighScore { get; set; }

        public Color Color { get; set; }
    }
}
