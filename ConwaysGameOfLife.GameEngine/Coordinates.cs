using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.GameEngine
{
    public class Points
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Points()
        { }

        public Points(int Row, int Col)
        {
            this.Row = Row;
            this.Col = Col;
        }
        public override string ToString()
        {
            return string.Format("({0},{1})", Row, Col);
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Points p = (Points)obj;
                return (Row == p.Row) && (Col == p.Col);
            }
        }

    }
}
