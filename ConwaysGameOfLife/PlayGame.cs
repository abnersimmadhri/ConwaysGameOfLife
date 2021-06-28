using System;
using System.Collections.Generic;
using System.Threading;
using ConwaysGameOfLife.GameEngine;

namespace ConwaysGameOfLife
{
    public class PlayGame
    {
        static void Main(string[] args)
        {
            int noOfSeeds = -1;
            int row = -1;
            int col = -1;
            int seedType = 0;
            int gametype = 0;
            List<Points> _seeds = new List<Points>();
            GameOfLife<Grid<Cell>, Cell> game = new GameOfLife<Grid<Cell>, Cell>(_seeds);


           

            while (seedType < 1 || seedType > 2)
            {
                Console.WriteLine("Enter 1 to provide your own seed and 2 to view Oscilator Templates");
                Int32.TryParse(Console.ReadLine(), out seedType);
            }

            if (seedType == 1)
            {
                while (noOfSeeds == -1)
                {
                    Console.WriteLine("how many seed values would you like to provide?");
                    Int32.TryParse(Console.ReadLine(), out noOfSeeds);
                }
                Console.WriteLine("Please Enter Seed Values, Row then Column");
                for (int i = 0; i < noOfSeeds; i++)
                {
                    row = -1;
                    col = -1;
                    while (row == -1)
                    {
                        Console.Write("Enter Row value: ");
                        if (!Int32.TryParse(Console.ReadLine(), out row))
                        {
                            Console.WriteLine("Invalid values, please re-enter an integer greated than or equal to 0");
                            row = -1;
                        }

                    }

                    while (col == -1)
                    {
                        Console.Write("Enter Col value: ");
                        if (!Int32.TryParse(Console.ReadLine(), out col))
                        {
                            Console.WriteLine("Invalid values, please re-enter an integer greated than or equal to 0");
                            col = -1;
                        }

                    }

                    _seeds.Add(new Points(row, col));
                }

                game = new GameOfLife<Grid<Cell>, Cell>(_seeds);
            }
            else
            {


                while (gametype < 1 || gametype > 2)
                {
                    Console.WriteLine("Enter 1 to view the Toad Oscillator and 2 to view the Blinker");
                    Int32.TryParse(Console.ReadLine(), out gametype);
                }
                if (gametype == 1)
                {
                    //Toad
                    game = new GameOfLife<Grid<Cell>, Cell>(new List<Points> { new Points(4, 2), new Points(4, 3), new Points(4, 4), new Points(5, 3), new Points(5, 4), new Points(5, 5) });
                }
                else
                {
                    //Left as default 
                    //Blinker
                    game = new GameOfLife<Grid<Cell>, Cell>(new List<Points> { new Points(3, 2), new Points(3, 3), new Points(3, 4) });
                }           

                
            }



            
            game.Draw();



            var neighbours = game.Neighbours(new Points(1, 1));
            for (int i = 0; i < 300; i++)
            {
                Thread.Sleep(1000);
                game.Tick();
                game.Draw();
            }
            Console.ReadLine();
        }



    }
}
