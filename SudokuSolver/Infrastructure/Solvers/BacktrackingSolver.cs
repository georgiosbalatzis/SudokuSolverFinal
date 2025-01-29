using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;
using SudokuSolver.Domain.Models;

namespace SudokuSolver.Infrastructure.Solvers
{
    public class BacktrackingSolver : ISudokuSolver
    {
        private readonly ISudokuValidator _validator;
        private readonly ICellFinder _cellFinder;

        public BacktrackingSolver(ISudokuValidator validator, ICellFinder cellFinder)
        {
            _validator = validator;
            _cellFinder = cellFinder;
        }

        public SolverResult Solve(Grid initialGrid)
        {
            var startTime = DateTime.Now;
            var workingGrid = initialGrid.Clone();
            
            var success = SolveRecursive(workingGrid);
            var endTime = DateTime.Now;

            return new SolverResult(
                success,
                success ? workingGrid : null,
                endTime - startTime,
                "Backtracking"
            );
        }

        private bool SolveRecursive(Grid grid)
        {
            var emptyCell = _cellFinder.FindEmptyCell(grid);
            if (emptyCell == null) return true;

            for (int value = 1; value <= Grid.GridSize; value++)
            {
                if (!_validator.IsValidMove(grid, emptyCell, value)) continue;
                
                grid[emptyCell.Row, emptyCell.Col] = value;
                if (SolveRecursive(grid)) return true;
                grid[emptyCell.Row, emptyCell.Col] = 0;
            }

            return false;
        }
    }
}