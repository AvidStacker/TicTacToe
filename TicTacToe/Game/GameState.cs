using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Game.Board;
using TicTacToe.Game.Player;

namespace TicTacToe.Game
{
    public class GameState
    {
        public List<PlayerData> Players { get; set; }
        public string CurrentPlayerSymbol { get; set; }
        public string CurrentPlayerName { get; set; }
        public BoardState BoardState { get; set; }
    }
}
