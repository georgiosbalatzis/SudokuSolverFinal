using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Models;

namespace SudokuSolver.Domain.Interfaces
{
    public interface ISudokuSolver
    {
        SolverResult Solve(Grid initialGrid);
    }
}