using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.GameEngine
{
    public class Cell : ICell
    {

        public States state { get; set; } = States.Dead;
        public Points Location { get; set; }

        public Cell()
        { }

        public Cell(int x, int y)
        {
            Location = new Points(x, y);
        }

        public void Draw()
        {
            if (state.Equals(States.Alive))
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write("  ");
            }

        }
    }

}
