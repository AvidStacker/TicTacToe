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
        public List<PlayerData> Players { get; set; }
        public string CurrentPlayerSymbol { get; set; }
        public string CurrentPlayerName { get; set; }
        public BoardStateData BoardStateData { get; set; }
    }
}
