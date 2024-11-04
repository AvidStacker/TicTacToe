using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameContent.PlayerContent;

namespace TicTacToe.GameContent.PlayerContent
{
    public interface IPlayer
    {
        string GetName();
        char GetSymbol();
        Color GetColor();
        int GetHighScore();

        void UpdateHighScore(int highscore);

        PlayerData GetPlayerData();
    }
}
