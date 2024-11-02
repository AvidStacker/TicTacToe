namespace TicTacToe.Game.Board
{
    public class Board
    {
        public BoardState CurrentState { get; private set; } = BoardState.Ongoinggame;

        public event EventHandler<BoardState> StateChanged = delegate { };

        private char[,] grid; // 2D array representing the board
        private readonly int size = 3; // Size of the board

        public Board()
        {
            this.grid = new char[this.size, this.size];
            this.InitializeBoard();
        }

        // Initializes the board with empty fields
        private void InitializeBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.grid[i, j] = ' '; // Empty fields represented by spaces
                }
            }
        }

        // Makes a move on the board
        public bool MakeMove(int row, int col, char playerSymbol)
        {
            if (row < 0 || row >= this.size || col < 0 || col >= this.size || this.grid[row, col] != ' ' || this.CurrentState != BoardState.Ongoinggame)
            {
                return false; // Invalid move or game is already over
            }

            this.grid[row, col] = playerSymbol; // Place player's symbol on the board
            this.SetBoardState(playerSymbol); // Check if the game has been won or drawn
            return true;
        }

        // Resets the board
        public void Reset()
        {
            this.InitializeBoard(); // Reinitialize the grid
            this.CurrentState = BoardState.Ongoinggame; // Reset current state
            OnStateChanged(this.CurrentState); // Notify observers
        }

        // Loads the board state from a provided BoardStateData object
        public void LoadState(BoardStateData boardStateData)
        {
            // Ensure that the CurrentState is set from the BoardStateData
            this.CurrentState = boardStateData.CurrentState; // This should work if BoardStateData.CurrentState is of type BoardState
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.grid[i, j] = boardStateData.Grid[i, j]; // Restore grid state
                }
            }
            OnStateChanged(this.CurrentState); // Notify observers of state change
        }


        // Returns a copy of the current grid
        public char[,] GetCurrentGrid()
        {
            char[,] gridCopy = new char[this.size, this.size];
            Array.Copy(this.grid, gridCopy, this.grid.Length); // Create a copy of the grid
            return gridCopy;
        }

        // Checks for game status (win, loss, or draw)
        private void SetBoardState(char playerSymbol)
        {
            if (this.CheckBoardState(playerSymbol))
            {
                this.CurrentState = playerSymbol == 'X' ? BoardState.XWins : BoardState.OWins;
                OnStateChanged(this.CurrentState);
            }
            else if (IsBoardFull())
            {
                this.CurrentState = BoardState.Draw;
                OnStateChanged(this.CurrentState);
            }
        }

        // Checks if there's a winner
        public bool CheckBoardState(char playerSymbol)
        {
            // Check rows and columns
            for (int i = 0; i < size; i++)
            {
                if (this.grid[i, 0] == playerSymbol && this.grid[i, 1] == playerSymbol && this.grid[i, 2] == playerSymbol ||
                    this.grid[0, i] == playerSymbol && this.grid[1, i] == playerSymbol && this.grid[2, i] == playerSymbol)
                {
                    return true;
                }
            }

            // Check diagonals
            if (this.grid[0, 0] == playerSymbol && this.grid[1, 1] == playerSymbol && this.grid[2, 2] == playerSymbol ||
                this.grid[0, 2] == playerSymbol && this.grid[1, 1] == playerSymbol && this.grid[2, 0] == playerSymbol)
            {
                return true;
            }

            return false;
        }

        // Checks if the board is full (draw)
        private bool IsBoardFull()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (this.grid[i, j] == ' ')
                        return false;
                }
            }
            return true;
        }

        // Handles state change notifications
        protected virtual void OnStateChanged(BoardState newState)
        {
            StateChanged?.Invoke(this, newState);
        }

        public char[,] GetGridState()
        {
            char[,] gridClone = new char[this.size, this.size];
            Array.Copy(this.grid, gridClone, this.grid.Length); // Create a clone of the grid
            return gridClone; // Return the clone to avoid exposing the internal state
        }

        private BoardState GetBoardState()
        {
            return this.CurrentState;
        }

        // Public method to get the current state of the board as a string
        public BoardStateData GetBoardStateData()
        {
            return new BoardStateData(GetCurrentGrid(), GetBoardState());
        }
    }
}
