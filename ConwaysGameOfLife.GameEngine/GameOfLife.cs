using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife.GameEngine
{
    public class GameOfLife<S, T> where T : ICell where S : IGrid<T>, new()
    {

        private const int _lifeByReproduction = 3;
        private const int _overPopulation = 4;
        private const int _underPopulation = 1;
        private const int _defaultGridSize = 20;

        public List<Points> _liveCells { get; set; }
        public IGrid<T> _grid { get; }


        public GameOfLife(List<Points> seed, int gridSize = _defaultGridSize)
        {
            _liveCells = seed.Where(x => (x.Row < gridSize && x.Col < gridSize && x.Row >= 0 && x.Col >= 0)).ToList();
            _grid = (S)Activator.CreateInstance(
            typeof(S)
             , new object[] { gridSize }
            );
            seedGrid();

        }

        public void Tick()
        {
            //Lists need to be generated before method calls to maintain previous generation state.
            var _blacklist = Blacklist().ToList();
            var _reproduction = Reproduction().ToList();
            Assassinate(_blacklist);
            Reproduce(_reproduction);
            _liveCells = _grid.Where(x => x.state.Equals(States.Alive)).Select(x => x.Location).ToList();
        }

        private void seedGrid()
        {
            foreach (var value in _liveCells)
            {
                if (value.Row < _grid.rows && value.Col < _grid.columns)
                    _grid[value.Row, value.Col].state = States.Alive;
            }
        }

        public IGrid<T> currentState()
        {
            return _grid;
        }

        public IEnumerable<T> Neighbours(Points point)
        {
            var Neighbours = new List<T>();

            var NeighbourPoints = new List<Points>
            {
                 new Points(point.Row-1, point.Col-1),
                 new Points(point.Row+0, point.Col-1),
                 new Points(point.Row+1, point.Col-1),
                 new Points(point.Row-1, point.Col-0),
                 new Points(point.Row+1, point.Col+0),
                 new Points(point.Row-1, point.Col+1),
                 new Points(point.Row+0, point.Col+1),
                 new Points(point.Row+1, point.Col+1)
            };


            foreach (var value in NeighbourPoints.Where(Npoint => Npoint.Row >= 0
                                                               && Npoint.Row < _grid.rows
                                                               && Npoint.Col >= 0
                                                               && Npoint.Col < _grid.columns))
            {
                Neighbours.Add(_grid[value.Row, value.Col]);

            }

            return Neighbours;

        }

        public IEnumerable<T> DeadNeighbours(Points point)
        {
            return Neighbours(point)
                .Where(neighbours => neighbours.state.Equals(States.Dead))
                .ToList();
        }

        public IEnumerable<T> LiveNeighbours(Points point)
        {
            return Neighbours(point)
                .Where(neighbours => neighbours.state.Equals(States.Alive))
                .ToList();
        }

        public IEnumerable<T> Reproduction()
        {
            var NewLife = new List<T>();
            var probableReproduction = _grid.Where(x => x.state.Equals(States.Dead));

            foreach (var cell in probableReproduction)
            {
                if (LiveNeighbours(cell.Location).Count() == _lifeByReproduction)
                    NewLife.Add(cell);
            }

            return NewLife;
        }

        public IEnumerable<T> Blacklist()
        {
            var AssassinationList = new List<T>();
            var probableCulprits = _grid.Where(x => x.state.Equals(States.Alive));

            foreach (var cell in probableCulprits)
            {
                if (LiveNeighbours(cell.Location).Count() >= _overPopulation || LiveNeighbours(cell.Location).Count() <= _underPopulation)
                    AssassinationList.Add(cell);
            }

            return AssassinationList;
        }

        public void Assassinate(List<T> blackList)
        {
            blackList.Select(c => { c.state = States.Dead; return c; }).ToList();
        }

        public void Reproduce(List<T> NewLife)
        {
            NewLife.Select(c => { c.state = States.Alive; return c; }).ToList();
        }

        public void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, Console.WindowTop);
            _grid.Draw();
        }
    }
}
