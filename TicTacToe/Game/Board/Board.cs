using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks; //bibliotek som importeras

namespace TicTacToe.Game.Board
{
    public class Board
    {


        // Kolla upp status på brädet
        public BoardState CurrentState { get; private set; } = BoardState.Ongoinggame;

        // Händelse för att se förändring i spelet       
        public event EventHandler<BoardState> StateChanged;

        private char[,] grid; //tvådimensionell array med x, o eller tomt för om det är en spelare där
                              //
         private readonly int size = 3;//talar om brädets storlek i x och y-led: 3x3



        public Board() //boards konstruktor körs när ett objekt skapas
        {
            this.grid = new char[this.size, this.size];
            this.InitializeBoard();
        }

        // Initierar brädet med tomma fält. nestlad forloop som populerar grid med mellanslag. 
        private void InitializeBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.grid[i, j] = ' '; // Tomma fält representeras med mellanslag
                }
            }
        }

        // Metod för att göra ett drag
        public bool MakeMove(int row, int col, char playerSymbol) //sätter x eller 0 på brädet
        {
            if (row < 0 || row >= this.size || col < 0 || col >= this.size || this.grid[row, col] != ' ' || this.CurrentState != BoardState.Ongoinggame)
            {
                return false; // Ogiltigt drag eller spelet är redan avgjort
            }

            this.grid[row, col] = playerSymbol; // Sätt spelarens symbol på brädet
            this.SetBoardState(playerSymbol); // Kontrollera om spelet är vunnet eller oavgjort
            return true;
        }

        // Kontrollera spelstatus (vinst, förlust, eller oavgjort)
        private void SetBoardState(char playerSymbol)
        {
            if (this.CheckBoardState(playerSymbol))
            {
                this.CurrentState = playerSymbol == 'X' ? BoardState.XWins : BoardState.OWins;
                this.OnStateChanged(this.CurrentState);
            }
            else if (IsBoardFull())
            {
                this.CurrentState = BoardState.Draw;
                this.OnStateChanged(this.CurrentState);
            }
        }

        // Kontrollera om det finns en vinnare
        public bool CheckBoardState(char playerSymbol)
        {
            // Kontrollera rader och kolumner
            for (int i = 0; i < size; i++)
            {
                if (this.grid[i, 0] == playerSymbol && this.grid[i, 1] == playerSymbol && this.grid[i, 2] == playerSymbol ||
                    this.grid[0, i] == playerSymbol && this.grid[1, i] == playerSymbol && this.grid[2, i] == playerSymbol)
                {
                    return true;
                }
            }

            // Kontrollera diagonaler
            if (this.grid[0, 0] == playerSymbol && this.grid[1, 1] == playerSymbol && this.grid[2, 2] == playerSymbol ||
                this.grid[0, 2] == playerSymbol && this.grid[1, 1] == playerSymbol && this.grid[2, 0] == playerSymbol)
            {
                return true;
            }

            return false;
        }

        // Kontrollera om brädet är fullt (oavgjort)
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

        // Metod för att hantera tillståndsförändringar
        protected virtual void OnStateChanged(BoardState newState)
        {
            this.StateChanged?.Invoke(this, newState);
        }

        // Public metoder för att få tillståndet på brädet
        public string GetBoardState()
        {
            return this.CurrentState.ToString();
        }

    }
}
