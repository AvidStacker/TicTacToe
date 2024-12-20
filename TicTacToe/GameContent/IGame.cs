﻿using TicTacToe.GameContent.BoardContent;

namespace TicTacToe.GameContent
{
    public interface IGame
    {
        void StartNewGame();
        void CheckGameState();
        void OnBoardStateChanged(object sender, BoardState newState);
        void UpdateBoard(int row, int col);
    }
}