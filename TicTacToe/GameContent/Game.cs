using System.Text.Json;
using TicTacToe.GameContent.BoardContent;
using TicTacToe.GameContent.PlayerContent;
using TicTacToe.GameContent;

public class Game : IGame
{
    private PlayerManager _playerManager;
    private Board _board;

    // Stack to store the history of game states for undo functionality
    private Stack<GameState> _history;

    // Flag to track if undo is allowed
    private bool _canUndo;

    // Define events to notify the GameForm of state changes
    public event Action<string> TurnChanged;
    public event Action<string> GameWon;
    public event Action<string> IllegalMove;
    public event Action Draw;
    public event Action GameReset;

    public Game()
    {
        this._playerManager = new PlayerManager();
        this._board = new Board();
        this._history = new Stack<GameState>(); // Initialize the history stack
        this._board.StateChanged += this.OnBoardStateChanged;
        _canUndo = false; // Initialize the undo flag as false
    }

    public void StartNewGame()
    {
        this._board.Reset();
        this._playerManager.LoadPlayers();
        _history.Clear(); // Clear history at the start of a new game

        // Save the initial game state so it cannot be undone past this point
        _history.Push(CreateGameState());
        _canUndo = false; // No move to undo at the start

        GameReset?.Invoke();
        TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
    }

    public void SaveGame(string filePath)
    {
        var gameState = CreateGameState();
        string jsonString = JsonSerializer.Serialize(gameState);
        File.WriteAllText(filePath, jsonString);
    }

    public void LoadGame(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<GameState>(jsonString);

            RestoreGameState(gameState);

            OnBoardStateChanged(this, this._board.GetBoardState());
            TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
        }
    }

    private GameState CreateGameState()
    {
        return new GameState
        {
            Players = this._playerManager.GetPlayersData(),
            CurrentPlayerSymbol = this._playerManager.GetCurrentPlayerSymbol(),
            CurrentPlayerName = this._playerManager.GetCurrentPlayerName(),
            BoardStateData = this._board.GetBoardStateData()
        };
    }

    private void RestoreGameState(GameState gameState)
    {
        this._playerManager.RestorePlayers(gameState.Players);
        this._board.LoadState(gameState.BoardStateData);
        this._playerManager.SetCurrentPlayer(gameState.CurrentPlayerName);
    }

    public void UpdateBoard(int row, int col)
    {
        char currentSymbol = this._playerManager.GetCurrentPlayerSymbol();

        // Only save the current game state to history if the move is legal
        if (this._board.MakeMove(row, col, currentSymbol))
        {
            // Save the game state after making the move
            this._history.Push(CreateGameState());
            _canUndo = true; // Enable undo since a new move has been made

            // Notify listeners and switch player
            this.OnBoardStateChanged(this, this._board.GetBoardState());
            this._playerManager.SwitchPlayer();
            this.TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
        }
        else
        {
            this.IllegalMove?.Invoke("Illegal move");
        }
    }

    public void Undo()
    {
        // Only allow undo if there is exactly one move to undo
        if (_canUndo && this._history.Count > 1)
        {
            // Pop the last move from the history
            this._history.Pop();

            // Get the previous state to restore
            var previousState = this._history.Peek();
            RestoreGameState(previousState);

            _canUndo = false; // Disable further undos until another move is made

            // Notify listeners to update the UI based on the restored state
            OnBoardStateChanged(this, _board.GetBoardState());
            this.TurnChanged?.Invoke(this._playerManager.GetCurrentPlayerName());
        }
        else
        {
            // No move to undo (only initial state is in the history or undo was already used)
            IllegalMove?.Invoke("No moves to undo");
        }
    }

    public void OnBoardStateChanged(object sender, BoardState newState)
    {
        this.CheckGameState();
    }

    public void CheckGameState()
    {
        if (this._board.GetBoardState() == BoardState.Draw)
        {
            this.Draw?.Invoke();
        }
        else if (this._board.GetBoardState() == BoardState.XWins || this._board.GetBoardState() == BoardState.OWins)
        {
            string winningPlayer = this._playerManager.GetCurrentPlayerName();
            this.GameWon?.Invoke(winningPlayer);
            this._playerManager.UpdatePlayerHighscore(winningPlayer);
        }
    }

    public void ResetGame()
    {
        this._playerManager.Reset();
        this._board.Reset();
        this._history.Clear(); // Clear history on reset

        // Save the initial state after reset to prevent undo past this point
        _history.Push(CreateGameState());
        _canUndo = false; // No move to undo after reset

        this.GameReset?.Invoke();
    }

    public string CurrentPlayerName => this._playerManager.GetCurrentPlayerName();
    public char CurrentPlayerSymbol => this._playerManager.GetCurrentPlayerSymbol();
    public int CurrentPlayerHighScore => this._playerManager.GetCurrentPlayerHighScore();

    public char[,] GetBoardState()
    {
        return this._board.GetGridState();
    }
}
