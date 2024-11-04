using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameContent.BoardContent;
using TicTacToe.GameContent.PlayerContent;

namespace TicTacToe.GameContent
{
    public class GameState
    {
        public required List<PlayerData> Players { get; set; }
        public required char CurrentPlayerSymbol { get; set; }
        public required string CurrentPlayerName { get; set; }
        public required BoardStateData BoardStateData { get; set; }
    }
}
