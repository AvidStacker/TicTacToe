using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Game.Player
{
    public interface IPlayer
    {
        string GetName();
        string GetSymbol();
        Color GetColor();
        int GetHighscore();

        void UpdateHighscore(int highscore);
        PlayerData GetPlayerData();
    }
}
