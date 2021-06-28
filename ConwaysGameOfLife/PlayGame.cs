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
            List<Points> _seeds = new List<Points>();
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

            GameOfLife<Grid<Cell>, Cell> game = new GameOfLife<Grid<Cell>, Cell>(_seeds);
            //GameOfLife<Grid<Cell>, Cell> game = new GameOfLife<Grid<Cell>, Cell>(new List<Points> { new Points(4, 2), new Points(4, 3), new Points(4, 4), new Points(5, 3), new Points(5, 4), new Points(5, 5) });
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
