using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.GameEngine
{
    public class Grid<T> : IGrid<T> where T : ICell, new()
    {
        private T[,] _cells;
        public int rows { get; set; }
        public int columns { get; set; }
        public Points _coordinates { get; set; }

        public Grid()
        { }
        /*
         * Creates a grid with the specified number of columns and rows.
         */
        public Grid(int rows, int columns)
        {
            _cells = new T[rows, columns];
            this.rows = rows;
            this.columns = columns;
            populateGrid();
        }

        /*
         * Creates a Square grid with number of rows = number of columns.
         */
        public Grid(int rows_columns)
            : this(rows_columns, rows_columns)
        {

        }

        public IEnumerator GetEnumerator()
        {
            foreach (var cell in _cells)
            {
                yield return cell;
            }

        }

        public T this[int row, int col]
        {
            get
            {
                return _cells[row, col];
            }
            set
            {
                _cells[row, col] = value;
            }
        }

        public void populateGrid()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = (T)Activator.CreateInstance(
                                    typeof(T)
                                     , new object[] { i, j });
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Draw();
                }
                Console.WriteLine("\r");
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var cell in _cells)
            {
                yield return cell;
            }
        }
    }

}
