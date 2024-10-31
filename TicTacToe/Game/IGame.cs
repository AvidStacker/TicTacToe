using TicTacToe.Game.Board;

namespace TicTacToe.Core.Game
{
    public interface IGame // Changed to public if necessary
    {
        void InitializeGame();
        void CheckGameState();
        void OnBoardStateChanged(object sender, BoardState newState); // Added parameters
        void UpdateBoard(int row, int col); // Added parameters
    }
}