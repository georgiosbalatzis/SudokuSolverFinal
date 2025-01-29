using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;

namespace SudokuSolver.Presentation.ConsoleUI
{
    public class GridPrinter : IGridPrinter
    {
        public void Print(Grid grid)
        {
            Console.WriteLine("  -------------------------");
            for (int row = 0; row < Grid.GridSize; row++)
            {
                Console.Write("  |");
                for (int col = 0; col < Grid.GridSize; col++)
                {
                    Console.Write($" {(grid[row, col] == 0 ? "." : grid[row, col].ToString())}");
                    if ((col + 1) % Grid.BoxSize == 0) Console.Write(" |");
                }
                Console.WriteLine();
                if ((row + 1) % Grid.BoxSize == 0)
                    Console.WriteLine("  -------------------------");
            }
        }
    }
}