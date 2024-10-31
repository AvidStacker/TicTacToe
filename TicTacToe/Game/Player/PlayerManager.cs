using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicTacToe.Core.Game.Player
{
    internal class PlayerManager
    {

        private int _currentPlayerIndex;
        private Random _random;
        private readonly string _jsonFilepath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty,"PlayerManager.json"); 
        //Relativ sökväg till PlayerSettings.json (tror jag, inte testat om den fungerar). Har försökt undvika ett eventuellt null värde, därav conditional alla conditional opperators. 

        //Skapa ett event som kan notifiera andra klasser vid uppdatering av players?

        private List<IPlayer> _players { get; set; }


        public PlayerManager()
        {
            _players = LoadPlayers(); //Laddar in spelare direkt när ett PlayerManager objekt skapas för att inte råka skriva över de existerande spelarna
            UpdatePlayerHighscore(0, "Player 1");
            UpdatePlayerHighscore(0, "Player 2");
        }

        private void OnPlayerSettingsUpdated(string symbol, Color color)
        {
            //Implementera Obsever pattern med hjälp av t.ex. ett event
            //Implementeras vid senare tillfälle
        }

        public void InitializePlayers(string name, string symbol, Color color) //  !!  Se till att ha laddat in alla spelare innan en ny spleare skapas för att inte skriva över existerande splelare !!
        {
            try
            {
                IPlayer player = new HumanPlayer(name, symbol, color);  //Skapar ett nytt spelarobjekt och lägger till spelaren i spelarlistan.
                _players.Add(player);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); //Retard-catch; Fångar eventuella exceptions
            }
        }

        public void UpdatePlayerHighscore(int highscore, string name)
        {
            foreach (IPlayer player in _players)
            {
                if (player.GetName() == name)
                {
                    player.UpdateHighscore(highscore);
                }
            }
        }

        public int GetPlayerHighscore(string name)
        {

            foreach (IPlayer player in _players)
            {
                if (player.GetName() == name)
                {
                    return player.GetHighscore();
                }
            }

            return 0; //Default-return om spelaren inte finns 
        }

        public List<IPlayer> LoadPlayers() //Se till att denna metoden körs innan en ny spelare skapas
        {
            try
            {
                string existingPLayers = File.ReadAllText(_jsonFilepath); //Läser in all text från JSON-filen till en sträng
                _players = JsonSerializer.Deserialize<List<IPlayer>>(existingPLayers) ?? new List<IPlayer>(); //Deserialiserar alla existerande spleare och lägger till dom i spelarlistan
                                                                                                             //Skapar en ny tom lista om strängen = null för att undvika eventuella fel
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return _players; //Returnerar alla spelare som en lista
        }

        public void SavePlayers() //Kör denna metoden innan spelet stänger ner
        {
            try
            {
                string jsonPlayers = JsonSerializer.Serialize(_players); //Seraliserar alla spelare i den nya uppdaterade listan och sparar dom till en JSON-fil
                File.WriteAllText(_jsonFilepath, jsonPlayers);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString()); 
            }
            
        }

        public string RemovePlayer(string name)
        {
            try
            {
                foreach (IPlayer player in _players)
                {
                    if (player.GetName() == name)
                    {
                        _players.Remove(player);

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

        private void SetStartingPlayer()
        {
            _currentPlayerIndex = _random.Next(0, _players.Count);
        }

        public string GetCurrentPlayerSymbol()
        {
            return _players[_currentPlayerIndex].GetSymbol();
        }

        public void SwitchPlayer()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count; 
        }

        public void Reset()
        {
            _currentPlayerIndex = 0; 
        }

        public List<PlayerData> GetPlayersData()
        {
            var playerDataList = new List<PlayerData>();
            foreach (var player in _players)
            {
                playerDataList.Add(player.GetPlayerData());
            }
            return playerDataList;                      
        }

        public void RestorePlayers(List<PlayerData> playersData)            //Fattar verkligen inte syftet med dessa metoderna
        {
            // Clear existing players and restore from the data
            _players.Clear();
            foreach (var data in playersData)
            {
                var player = new HumanPlayer(data.Name, data.Symbol, data.Color); // Replace with the appropriate color
                player.UpdateHighscore(data.HighScore);
                _players.Add(player);
            }
            // Reset the current player index
            _currentPlayerIndex = 0; // Or set based on saved data if needed
        }




    }
}
