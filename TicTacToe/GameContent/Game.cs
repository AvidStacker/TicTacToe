using System.Text.Json;
using TicTacToe.GameContent.BoardContent;
using TicTacToe.GameContent.PlayerContent;
using TicTacToe.GameContent;

public class Game : IGame
{
    private PlayerManager _playerManager;
    private Board _board;

    // Define events to notify the GameForm of state changes
    public event Action TurnChanged;
    public event Action<string> GameWon;
    public event Action<string> IllegalMove;
    public event Action Draw;
    public event Action GameReset;

    public Game()
    {
        this._playerManager = new PlayerManager();
        this._board = new Board();
        this._board.StateChanged += this.OnBoardStateChanged;
    }

    public void StartNewGame()
    {
        this._board.Reset();
        this._playerManager.LoadPlayers();

        this.GameReset?.Invoke();
        this.TurnChanged?.Invoke();
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

            this.RestoreGameState(gameState);

            this.OnBoardStateChanged(this, this._board.GetBoardState());
            this.TurnChanged?.Invoke();
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

            // Notify listeners and switch player
            this.OnBoardStateChanged(this, this._board.GetBoardState());
            this._playerManager.SwitchPlayer();
            this.TurnChanged?.Invoke();
        }
        else
        {
            this.IllegalMove?.Invoke("Illegal move");
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
            this.GameWon?.Invoke(this._playerManager.GetCurrentPlayerName());
            this._playerManager.SavePlayers();
            this._playerManager.UpdatePlayerHighScore(this._playerManager.GetCurrentPlayerName());
        }
    }

    public void ResetGame()
    {
        this._playerManager.Reset();
        this._board.Reset();
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
