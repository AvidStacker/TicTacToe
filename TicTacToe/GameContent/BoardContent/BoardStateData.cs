namespace TicTacToe.GameContent.BoardContent
{
    public class BoardStateData
    {
        // A 1D array representing the board state
        public char[] Grid { get; set; }

        // The current state of the game
        public BoardState CurrentState { get; set; }

        // Constructor to initialize the grid
        public BoardStateData(char[] grid, BoardState currentState)
        {
            this.Grid = grid ?? this.InitializeGrid(); // If grid is null, initialize it
            this.CurrentState = currentState; // Set the current state
        }

        // Initializes the grid with empty spaces and returns it
        private char[] InitializeGrid()
        {
            return new char[9]; // Create a new 1D array for a 3x3 grid
        }

        // Convert 1D array back to 2D array
        public char[,] To2DArray()
        {
            char[,] grid2D = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid2D[i, j] = this.Grid[i * 3 + j];
                }
            }
            return grid2D;
        }

        // Convert 2D array to 1D array
        public static char[] From2DArray(char[,] grid)
        {
            char[] flatArray = new char[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    flatArray[i * 3 + j] = grid[i, j];
                }
            }
            return flatArray;
        }
    }
}
