using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;

namespace SudokuSolver.Infrastructure.IO
{
    public class FileGridLoader : IGridLoader
    {
        public Grid LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Το αρχείο δεν βρέθηκε.", filePath);

            var lines = File.ReadAllLines(filePath);
            ValidateFileFormat(lines);

            var grid = new Grid();
            for (int row = 0; row < Grid.GridSize; row++)
            {
                var numbers = lines[row].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < Grid.GridSize; col++)
                {
                    if (!int.TryParse(numbers[col], out int value) || value < 0 || value > 9)
                        throw new FormatException($"Μη έγκυρος αριθμός στη θέση ({row}, {col})");
                    grid[row, col] = value;
                }
            }

            return grid;
        }

        private void ValidateFileFormat(string[] lines)
        {
            if (lines.Length != Grid.GridSize)
                throw new FormatException($"Το αρχείο πρέπει να περιέχει ακριβώς {Grid.GridSize} γραμμές");

            foreach (var (line, index) in lines.Select((value, i) => (value, i)))
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length != Grid.GridSize)
                    throw new FormatException($"Η γραμμή {index + 1} πρέπει να περιέχει ακριβώς {Grid.GridSize} αριθμούς");
            }
        }
    }
}