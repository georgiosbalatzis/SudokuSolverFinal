using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.ValueObjects;

namespace SudokuSolver.Domain.Interfaces
{
    public interface ISudokuValidator
    {
        bool IsValidMove(Grid grid, Position position, int value);
        bool IsComplete(Grid grid);
    }
}
