using ConwaysGameOfLife.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConwaysGameOfLife.Test
{
    public class GameOfLifeTest
    {
        /*
         * Seeds that exceed the maximum col and row lenght are discarded. 
         */
        [Fact]
        public void Seed_value_overFlow_discarded()
        {
            var seed = new List<Points> { new Points(50, 50), new Points(0, 2), new Points(20, 22) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);
            var g = gameOfLife.currentState();

            Assert.Single(gameOfLife._liveCells);

        }

        /*
         * Edge cells will not look for neighbours beyond the edge.
         * Since the grid is limited cells at the edge will discard neighbours that are over the boundry.
         */
        [Fact]
        public void neighbours_for_edges_are_discarded()
        {
            var seed = new List<Points> { new Points(1, 1) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);
            var neighboursPoints = gameOfLife.Neighbours(new Points(0, 0)).Select(x => x.Location);

            Assert.Equal(3, neighboursPoints.Count());
            Assert.DoesNotContain(new Points(-1, -1), neighboursPoints);
            Assert.DoesNotContain(new Points(0, -1), neighboursPoints);
            Assert.DoesNotContain(new Points(-1, 0), neighboursPoints);
            Assert.DoesNotContain(new Points(-1, 1), neighboursPoints);
            Assert.DoesNotContain(new Points(1, -1), neighboursPoints);

            Assert.Contains(new Points(1, 0), neighboursPoints);
            Assert.Contains(new Points(1, 1), neighboursPoints);
            Assert.Contains(new Points(0, 1), neighboursPoints);
        }

        /*
         * returns all the neighbours for a given cell.
         */
        [Fact]
        public void returns_cell_neighbours()
        {
            var seed = new List<Points> { new Points(1, 1) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);
            var neighboursPoints = gameOfLife.Neighbours(new Points(1, 1)).Select(x => x.Location);

            Assert.Equal(8, neighboursPoints.Count());
            Assert.DoesNotContain(new Points(1, 1), neighboursPoints);
            Assert.DoesNotContain(new Points(3, 3), neighboursPoints);

            Assert.Contains(new Points(0, 0), neighboursPoints);
            Assert.Contains(new Points(0, 1), neighboursPoints);
            Assert.Contains(new Points(0, 2), neighboursPoints);
            Assert.Contains(new Points(1, 0), neighboursPoints);
            Assert.Contains(new Points(2, 0), neighboursPoints);
            Assert.Contains(new Points(2, 1), neighboursPoints);
            Assert.Contains(new Points(2, 2), neighboursPoints);
            Assert.Contains(new Points(1, 2), neighboursPoints);
        }

        /*
         * Death of cell by over population.
         */
        [Fact]
        public void Death_of_cell_by_over_population()
        {
            var seed = new List<Points> { new Points(0, 0), new Points(0, 1), new Points(1, 0), new Points(1, 1), new Points(2, 2) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);
            var liveNeighboursPoints = gameOfLife.LiveNeighbours(new Points(1, 1)).Select(x => x.Location);

            Assert.InRange(liveNeighboursPoints.Count(), 4, 8);
            Assert.Equal(States.Alive, gameOfLife._grid[1, 1].state);
            gameOfLife.Tick();

            Assert.Equal(States.Dead, gameOfLife._grid[1, 1].state);
        }

        /*
         * Death of cell by under population.
         */
        [Fact]
        public void Death_of_cell_by_under_population()
        {
            var seed = new List<Points> { new Points(0, 0), new Points(0, 1), new Points(3, 3) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);
            var oneLiveNeighbour = gameOfLife.LiveNeighbours(new Points(0, 0)).ToList();
            var noLiveNeighbour = gameOfLife.LiveNeighbours(new Points(3, 3)).ToList();
            var liveNeighboursPoints = gameOfLife.LiveNeighbours(new Points(1, 1)).Select(x => x.Location);

            Assert.InRange(oneLiveNeighbour.Count(), 0, 1);
            Assert.Equal(States.Alive, gameOfLife._grid[0, 0].state);

            Assert.InRange(noLiveNeighbour.Count(), 0, 1);
            Assert.Equal(States.Alive, gameOfLife._grid[3, 3].state);
            gameOfLife.Tick();

            Assert.Equal(States.Dead, gameOfLife._grid[0, 0].state);
            Assert.Equal(States.Dead, gameOfLife._grid[3, 3].state);
        }


        [Fact]
        public void Life_By_Reproduction()
        {
            var seed = new List<Points> { new Points(0, 0), new Points(0, 1), new Points(1, 0) };
            var gameOfLife = new GameOfLife<Grid<Cell>, Cell>(seed);

            var threeLiveNeighbours = gameOfLife.LiveNeighbours(new Points(1, 1)).ToList();
            Assert.Equal(3, threeLiveNeighbours.Count());
            Assert.Equal(States.Dead, gameOfLife._grid[1, 1].state);

            gameOfLife.Tick();

            Assert.Equal(States.Alive, gameOfLife._grid[1, 1].state);
        }


    }
}
