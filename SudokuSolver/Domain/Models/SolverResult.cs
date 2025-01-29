using SudokuSolver.Domain.Entities;

namespace SudokuSolver.Domain.Models
{
    public class SolverResult
    {
        public bool Success { get; }
        public Grid Solution { get; }
        public TimeSpan SolvingTime { get; }
        public string AlgorithmUsed { get; }

        public SolverResult(bool success, Grid solution, TimeSpan solvingTime, string algorithmUsed)
        {
            Success = success;
            Solution = solution;
            SolvingTime = solvingTime;
            AlgorithmUsed = algorithmUsed;
        }
    }
}