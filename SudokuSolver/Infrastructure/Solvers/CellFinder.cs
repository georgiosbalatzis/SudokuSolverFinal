using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;
using SudokuSolver.Domain.ValueObjects;

namespace SudokuSolver.Infrastructure.Solvers
{
    public class CellFinder : ICellFinder
    {
        public Position FindEmptyCell(Grid grid)
        {
            for (int row = 0; row < Grid.GridSize; row++)
            {
                for (int col = 0; col < Grid.GridSize; col++)
                {
                    if (grid[row, col] == 0) 
                    {
                        return new Position(row, col);
                    }
                }
            }
            return null; 
        }
    }
}