using SudokuSolver.Domain.Entities;

namespace SudokuSolver.Domain.Interfaces
{
    public interface IGridPrinter
    {
        void Print(Grid grid);
    }
}