using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;
using SudokuSolver.Domain.Models;

namespace SudokuSolver.Infrastructure.Solvers
{
    public class DFSArrayListSolver : ISudokuSolver
    {
        private readonly ISudokuValidator _validator;
        private readonly ICellFinder _cellFinder;

        public DFSArrayListSolver(ISudokuValidator validator, ICellFinder cellFinder)
        {
            _validator = validator;
            _cellFinder = cellFinder;
        }

        public SolverResult Solve(Grid initialGrid)
        {
            var startTime = DateTime.Now;
            // Δημιουργούμε μια λίστα και προσθέτουμε την αρχική κατάσταση
            var stack = new List<Grid> { initialGrid.Clone() };
            Grid solution = null;

            while (stack.Count > 0 && solution == null)
            {
                // Παίρνουμε την τελευταία κατάσταση από τη λίστα
                var current = stack[^1];
                stack.RemoveAt(stack.Count - 1);

                // Αν το παζλ είναι συμπληρωμένο, έχουμε λύση
                if (_validator.IsComplete(current))
                {
                    solution = current;
                    break;
                }

                // Βρίσκουμε το επόμενο κενό κελί
                var emptyCell = _cellFinder.FindEmptyCell(current);
                if (emptyCell == null) continue;

                // Δοκιμάζουμε όλες τις πιθανές τιμές (από το 9 προς το 1)
                for (int value = Grid.GridSize; value >= 1; value--)
                {
                    if (!_validator.IsValidMove(current, emptyCell, value)) continue;
            
                    // Δημιουργούμε νέα κατάσταση και την προσθέτουμε στη λίστα
                    var next = current.Clone();
                    next[emptyCell.Row, emptyCell.Col] = value;
                    stack.Add(next);
                }
            }

            var endTime = DateTime.Now;
            return new SolverResult(solution != null, solution, 
                endTime - startTime, "DFS ArrayList");
        }
    }
}