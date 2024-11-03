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
        private Random _random;
        private readonly string _jsonFilepath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty,"GameContent\\PlayerContent\\PlayerSettings.json"); 
        //Relativ sökväg till PlayerSettings.json (tror jag, inte testat om den fungerar). Har försökt undvika ett eventuellt null värde, därav conditional alla conditional opperators. 

        //Skapa ett event som kan notifiera andra klasser vid uppdatering av players?

        private List<IPlayer> _players { get; set; }

        public PlayerManager()
        {
            this._players = LoadPlayers(); //Laddar in spelare direkt när ett PlayerManager objekt skapas för att inte råka skriva över de existerande spelarna
        }

        private void OnPlayerSettingsUpdated(string symbol, Color color)
        {
            //Implementera Obsever pattern med hjälp av t.ex. ett event
            //Implementeras vid senare tillfälle
        }

        public void InitializePlayers(string name, char symbol, Color color) //  !!  Se till att ha laddat in alla spelare innan en ny spleare skapas för att inte skriva över existerande splelare !!
        {
            try
            {
                IPlayer player = new HumanPlayer(name, symbol, color);  //Skapar ett nytt spelarobjekt och lägger till spelaren i spelarlistan.
                this._players.Add(player);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); //Retard-catch; Fångar eventuella exceptions
            }
        }

        public void UpdatePlayerHighscore(int highscore, string name)
        {
            foreach (IPlayer player in this._players)
            {
                if (player.GetName() == name)
                {
                    player.UpdateHighscore(highscore);
                }
            }
        }

        public int GetPlayerHighscore(string name)
        {

            foreach (IPlayer player in this._players)
            {
                if (player.GetName() == name)
                {
                    return player.GetHighscore();
                }
            }

            return 0; //Default-return om spelaren inte finns 
        }

        public void SavePlayers()
        {
            try
            {
                // Convert each Player object in _players to a PlayerData object
                List<PlayerData> playersData = this._players.Select(p => new PlayerData
                    {
                        Name = p.GetName(),
                        HighScore = p.GetHighscore(),
                        Symbol = p.GetSymbol(),
                        Color = (p.GetColor().R, p.GetColor().G, p.GetColor().B) // Assuming GetColor() returns a Color object
                    })
                    .ToList();

                // Serialize the list of PlayerData objects to JSON
                string jsonPlayers = JsonSerializer.Serialize(playersData);
                File.WriteAllText(this._jsonFilepath, jsonPlayers); // Save JSON to file
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // LoadPlayers method
        public List<IPlayer> LoadPlayers()
        {
            try
            {
                // Read the JSON data from file
                string existingPlayers = File.ReadAllText(this._jsonFilepath);

                // Deserialize the JSON to a list of PlayerData objects
                List<PlayerData> playersData = JsonSerializer.Deserialize<List<PlayerData>>(existingPlayers) ?? new List<PlayerData>();

                // Rebuild the _players list from the deserialized PlayerData objects
                this._players = new List<IPlayer>();
                foreach (var data in playersData)
                {
                    var player = new HumanPlayer(
                        data.Name,
                        data.Symbol,
                        Color.FromArgb(data.Color.R, data.Color.G, data.Color.B)
                    );
                    player.UpdateHighscore(data.HighScore);
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

        public string RemovePlayer(string name)
        {
            try
            {
                foreach (IPlayer player in this._players)
                {
                    if (player.GetName() == name)
                    {
                        this._players.Remove(player);

                        return $"Player \"{name}\" removed!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return $"Could not remove player \"{name}\"; player not found!";
        }

        public void SetCurrentPlayer(string name)
        {
            for (int i = 0; i < this._players.Count; i++)
            {
                if (this._players[i].GetName() == name)
                {
                    this._currentPlayerIndex = i; // Set current player index to the found player's index
                    return;
                }
            }

            throw new InvalidOperationException($"Player \"{name}\" not found.");
        }

        public char GetCurrentPlayerSymbol()
        {
            return this._players[this._currentPlayerIndex].GetSymbol();
        }

        public string GetCurrentPlayerName()
        {
            return this._players[this._currentPlayerIndex].GetName();
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
                playerDataList.Add(player.GetPlayerData());
            }
            return playerDataList;                      
        }

        public void RestorePlayers(List<PlayerData> playersData)            //Fattar verkligen inte syftet med dessa metoderna
        {
            // Clear existing players and restore from the data
            this._players.Clear();
            foreach (var data in playersData)
            {
                var player = new HumanPlayer(data.Name, data.Symbol, Color.FromArgb(data.Color.R, data.Color.G, data.Color.B)); // Replace with the appropriate color
                player.UpdateHighscore(data.HighScore);
                this._players.Add(player);
            }
            // Reset the current player index
            this._currentPlayerIndex = 0; // Or set based on saved data if needed
        }




    }
}
