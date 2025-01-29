using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.ValueObjects;

namespace SudokuSolver.Domain.Interfaces
{
    public interface ICellFinder
    {
        Position? FindEmptyCell(Grid grid);
    }
}