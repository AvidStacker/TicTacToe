using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicTacToe.Core.Game.Player
{
    internal class PlayerManager
    {

        //private string name { get; set; }
        //private string symbol { get; set; }
        //private Color color { get; set; }

        private readonly string jsonFilepath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "PlayerManager.json"); //Relativ sökväg till PlayerSettings.json (tror jag, inte testat om den fungerar)

        //Skapa ett event som kan notifiera andra klasser vid uppdatering av players?

        private List<IPlayer> players { get; set; }


        public PlayerManager()
        {
            players = LoadPlayers().ToList(); //Laddar in spelare direkt när ett PlayerManager objekt skapas för att inte råka skriva över de existerande spelarna
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

                IPlayer player = new HumanPlayer(name, symbol, color);  //Skapar ett nytt spelarobjekt och läggeer till spelaren i spelarlistan.
                players.Add(player);

                string jsonPlayers = JsonSerializer.Serialize(players); //Seraliserar alla spelare i listan och sparar dom till en JSON-fil
                File.WriteAllText(jsonFilepath, jsonPlayers);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); //Retard-catch; Fångar eventuella exceptions
            }



        }

        public void UpdatePlayerHighscore(int highscore, string name)
        {

            foreach (IPlayer player in players)
            {
                if (player.GetName() == name)
                {
                    player.UpdateHighscore(highscore);
                }
            }

        }

        public int GetPlayerHighscore(string name)
        {

            foreach (IPlayer player in players)
            {
                if (player.GetName() == name)
                {
                    return player.GetHighscore();
                }
            }

            return 0; //Default-return om spelaren inte finns 
        }

        public IPlayer[] LoadPlayers() //Se till att denna metoden körs innan en ny spelare skapas
        {

            string existingPLayers = File.ReadAllText(jsonFilepath); //Läser in all text från JSON-filen till en sträng
            players = JsonSerializer.Deserialize<List<IPlayer>>(existingPLayers) ?? new List<IPlayer>(); //Deserialiserar alla existerande spleare och lägger till dom i spelarlistan
                                                                                                         //Skapar en ny tom lista om strängen = null för att undvika eventuella fel
            return players.ToArray(); //Returnerar alla spelare som en array
        }

        public string RemovePlayer(string name)
        {

            foreach (IPlayer player in players)
            {
                if (player.GetName() == name)
                {
                    players.Remove(player);

                    string jsonPlayers = JsonSerializer.Serialize(players); //Seraliserar alla spelare i den nya uppdaterade listan och sparar dom till en JSON-fil
                    File.WriteAllText(jsonFilepath, jsonPlayers);

                    return $"Player \"{name}\" removed!";
                }
            }

            return $"Could not remove player \"{name}\"; player not found!";

        }




    }
}
