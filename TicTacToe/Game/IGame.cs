using TicTacToe.Game.Board;

namespace TicTacToe.Core.Game
{
    public interface IGame
    {
        void CheckGameState();
        void OnBoardStateChanged(object sender, BoardState newState);
        void UpdateBoard(int row, int col);
    }
}