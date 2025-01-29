
using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;
using SudokuSolver.Domain.ValueObjects;

namespace SudokuSolver.Infrastructure.Validators
{
    public class SudokuValidator : ISudokuValidator
    {
        public bool IsValidMove(Grid grid, Position position, int value)
        {
            return !IsUsedInRow(grid, position.Row, value) &&
                   !IsUsedInColumn(grid, position.Col, value) &&
                   !IsUsedInBox(grid, position.Row - position.Row % Grid.BoxSize, 
                       position.Col - position.Col % Grid.BoxSize, value);
        }

        public bool IsComplete(Grid grid)
        {
            for (int row = 0; row < Grid.GridSize; row++)
            for (int col = 0; col < Grid.GridSize; col++)
                if (grid[row, col] == 0)
                    return false;
            return true;
        }

        private bool IsUsedInRow(Grid grid, int row, int value)
        {
            for (int col = 0; col < Grid.GridSize; col++)
                if (grid[row, col] == value)
                    return true;
            return false;
        }

        private bool IsUsedInColumn(Grid grid, int col, int value)
        {
            for (int row = 0; row < Grid.GridSize; row++)
                if (grid[row, col] == value)
                    return true;
            return false;
        }

        private bool IsUsedInBox(Grid grid, int boxStartRow, int boxStartCol, int value)
        {
            for (int row = 0; row < Grid.BoxSize; row++)
            for (int col = 0; col < Grid.BoxSize; col++)
                if (grid[boxStartRow + row, boxStartCol + col] == value)
                    return true;
            return false;
        }
    }
}