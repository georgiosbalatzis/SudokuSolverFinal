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
            // Βρίσκουμε το επόμενο κενό κελί
            var emptyCell = _cellFinder.FindEmptyCell(grid);
            // Αν δεν υπάρχει κενό κελί, το παζλ έχει λυθεί
            if (emptyCell == null) return true;

            // Δοκιμάζουμε όλους τους πιθανούς αριθμούς (1-9)
            for (int value = 1; value <= Grid.GridSize; value++)
            {
                // Ελέγχουμε αν ο αριθμός είναι έγκυρος για αυτή τη θέση
                if (!_validator.IsValidMove(grid, emptyCell, value)) continue;
        
                // Τοποθετούμε τον αριθμό
                grid[emptyCell.Row, emptyCell.Col] = value;
        
                // Αναδρομική κλήση για το επόμενο κενό κελί
                if (SolveRecursive(grid)) return true;
        
                // Αν φτάσαμε σε αδιέξοδο, αναιρούμε την τοποθέτηση
                grid[emptyCell.Row, emptyCell.Col] = 0;
            }

            // Αν δεν βρέθηκε λύση, επιστρέφουμε false
            return false;
        }
    }
}