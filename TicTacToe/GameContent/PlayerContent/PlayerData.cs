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
        public required char Symbol { get; set; }
        public required int HighScore { get; set; }
        public required (int R, int G, int B) Color { get; set; }
    }
}
