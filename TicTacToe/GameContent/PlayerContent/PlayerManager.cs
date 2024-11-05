using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TicTacToe.GameContent.PlayerContent;

namespace TicTacToe.GameContent.PlayerContent
{
    internal class PlayerManager
    {
        private int _currentPlayerIndex;
        private readonly string _jsonFilepath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty,"GameContent\\PlayerContent\\PlayerSettings.json"); 
        //Relative filepath to PlayerSettings.json. Trying to avoild a null value - therefore all of the question mark.

        private List<IPlayer> _players { get; set; }

        public PlayerManager()
        {
            this._players = LoadPlayers(); //Laddar in spelare direkt när ett PlayerManager objekt skapas för att inte råka skriva över de existerande spelarna
        }

        public void UpdatePlayerHighscore(string name)
        {
            foreach (IPlayer player in this._players)
            {
                if (player.GetName() == name)
                {
                    player.UpdateHighScore(player.GetHighScore() +1);
                }
            }
        }

        public int GetPlayerHighscore(string name)
        {

            foreach (IPlayer player in this._players)
            {
                if (player.GetName() == name)
                {
                    return player.GetHighScore();
                }
            }

            return 0; //Default-return if the players doesn't exist
        }

        public int GetHighestHighscore() //Returns the highest highscore of all players
        {
            int highscore = 0;
            foreach(IPlayer player in _players)
            {
                if(player.GetHighScore() > highscore)
                {
                    highscore = player.GetHighScore();
                }
            }

            return highscore;
        }

        public void SavePlayers()
        {
            try
            {
                // Converts each Player object in _players to a PlayerData object
                List<PlayerData> playersData = this._players.Select(p => new PlayerData
                {
                    Name = p.GetName(),
                    HighScore = p.GetHighScore(),
                    Symbol = p.GetSymbol(),
                    Color = (p.GetColor().R, p.GetColor().G, p.GetColor().B)
                }).ToList();

                // Serialize the list of PlayerData objects to JSON
                string jsonPlayers = JsonSerializer.Serialize(playersData);
                File.WriteAllText(this._jsonFilepath, jsonPlayers); // Save to json file
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        
        public List<IPlayer> LoadPlayers()
        {
            try
            {
                //Read the JSON data from file
                string existingPlayers = File.ReadAllText(this._jsonFilepath);

                //Deserialize the JSON to a list of Playerdata objects
                List<PlayerData> playersData = JsonSerializer.Deserialize<List<PlayerData>>(existingPlayers) ?? new List<PlayerData>();

                //Rebuild the _players list from the playerdata objects
                this._players = new List<IPlayer>();
                foreach (var data in playersData)
                {
                    var player = new HumanPlayer(
                        data.Name,
                        data.Symbol,
                        Color.FromArgb(data.Color.R, data.Color.G, data.Color.B)
                    );
                    player.UpdateHighScore(data.HighScore);
                    this._players.Add(player);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<IPlayer>();
            }

            return this._players;
        }

        public void SetCurrentPlayer(string name)
        {
            bool playerFound = false;

            for (int i = 0; i < this._players.Count; i++)
            {
                if (this._players[i].GetName() == name)
                {
                    this._currentPlayerIndex = i;
                    playerFound = true;
                    break;
                }
            }

            if (!playerFound)
            {
                throw new InvalidOperationException($"Player \"{name}\" not found.");
            }
        }

        public char GetCurrentPlayerSymbol()
        {
            return Convert.ToChar(this._players[this._currentPlayerIndex].GetSymbol());
        }

        public string GetCurrentPlayerName()
        {
            return this._players[this._currentPlayerIndex].GetName();
        }

        public int GetCurrentPlayerHighScore()
        {
            return this._players[this._currentPlayerIndex].GetHighScore();
        }
        public void SwitchPlayer()
        {
            this._currentPlayerIndex = (this._currentPlayerIndex + 1) % this._players.Count; 
        }

        public void Reset()
        {
            this._currentPlayerIndex = 0; 
        }

        public List<PlayerData> GetPlayersData()
        {
            var playerDataList = new List<PlayerData>(); 
            foreach (var player in this._players)
            {
                playerDataList.Add(player.GetPlayerData()); //Converts Player objects in _player to Playerdata obejcts
            }
            return playerDataList;                      
        }

        public void RestorePlayers(List<PlayerData> playersData) 
        {
            // Clear existing players and restore from the data
            this._players.Clear();
            foreach (var data in playersData)
            {
                var player = new HumanPlayer(data.Name, data.Symbol, Color.FromArgb(data.Color.R, data.Color.G, data.Color.B)); // Replace with the appropriate color
                player.UpdateHighScore(data.HighScore);
                this._players.Add(player);
            }
            // Reset the current player index
            this._currentPlayerIndex = 0; // Or set based on saved data if needed
        }
    }
}
