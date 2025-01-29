using SudokuSolver.Domain.Entities;

namespace SudokuSolver.Domain.Interfaces
{
    public interface IGridLoader
    {
        Grid LoadFromFile(string filePath);
    }
}