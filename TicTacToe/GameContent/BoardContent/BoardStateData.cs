
namespace TicTacToe.GameContent.BoardContent
{
    public class BoardStateData
    {
        // A 2D array representing the board state
        public char[,] Grid { get; set; }

        // The current state of the game
        public BoardState CurrentState { get; set; }

        // Constructor to initialize the grid
        public BoardStateData(char[,] grid, BoardState currentState)
        {
            Grid = grid ?? InitializeGrid(); // If grid is null, initialize it
            CurrentState = currentState; // Set the current state
        }

        // Initializes the grid with empty spaces and returns it
        private char[,] InitializeGrid()
        {
            char[,] newGrid = new char[3, 3]; // Create a new 3x3 grid
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newGrid[i, j] = ' '; // Empty cell represented by a space
                }
            }
            return newGrid;
        }

        public static implicit operator BoardState(BoardStateData v)
        {
            throw new NotImplementedException();
        }
    }
}
