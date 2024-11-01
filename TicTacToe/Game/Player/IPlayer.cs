﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Game.Player;

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
