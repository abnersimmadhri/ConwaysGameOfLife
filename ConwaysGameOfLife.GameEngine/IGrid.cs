using System.Collections;
using System.Collections.Generic;

namespace ConwaysGameOfLife.GameEngine
{
    public interface IGrid<T> : IEnumerable<T>
    {

        void populateGrid();
        int rows { get; set; }
        int columns { get; set; }
        T this[int x, int y]
        {
            get;
            set;
        }
        void Draw();
    }
}