using SudokuSolver.Domain.Entities;
using SudokuSolver.Domain.Interfaces;
using SudokuSolver.Domain.Models;
using SudokuSolver.Infrastructure.IO;
using SudokuSolver.Infrastructure.Solvers;
using SudokuSolver.Infrastructure.Validators;
using SudokuSolver.Presentation.ConsoleUI;

public class Program
{
    private readonly ISudokuSolver[] _solvers;
    private readonly IGridLoader _gridLoader;
    private readonly IGridPrinter _gridPrinter;

    public Program(
        ISudokuSolver[] solvers,
        IGridLoader gridLoader,
        IGridPrinter gridPrinter)
    {
        _solvers = solvers;
        _gridLoader = gridLoader;
        _gridPrinter = gridPrinter;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu();
                var choice = GetUserChoice();
                if (choice == _solvers.Length + 1) break;

                var grid = LoadGrid();
                if (grid == null) continue;

                Console.WriteLine("\nΑρχικό Sudoku:");
                _gridPrinter.Print(grid);

                var result = _solvers[choice - 1].Solve(grid);
                DisplayResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nΣφάλμα: {ex.Message}");
            }

            Console.WriteLine("\nΠατήστε οποιοδήποτε πλήκτρο για συνέχεια...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Καλώς ήρθατε στον Επιλυτή Sudoku!");
        Console.WriteLine("Επιλέξτε μέθοδο επίλυσης:");
        for (int i = 0; i < _solvers.Length; i++)
            Console.WriteLine($"{i + 1}: {_solvers[i].GetType().Name}");
        Console.WriteLine($"{_solvers.Length + 1}: Έξοδος");
    }

    private Grid LoadGrid()
    {
        Console.Write("\nΕισάγετε το όνομα του αρχείου εισόδου: ");
        var fileName = Console.ReadLine();
        return _gridLoader.LoadFromFile(fileName);
    }

    private void DisplayResult(SolverResult result)
    {
        if (result.Success)
        {
            Console.WriteLine("\nΛύση βρέθηκε!");
            Console.WriteLine($"Μέθοδος: {result.AlgorithmUsed}");
            Console.WriteLine($"Χρόνος επίλυσης: {result.SolvingTime.TotalSeconds:F3} δευτερόλεπτα");
            Console.WriteLine("\nΤελική λύση:");
            _gridPrinter.Print(result.Solution);
        }
        else
        {
            Console.WriteLine("\nΔεν βρέθηκε λύση για το συγκεκριμένο Sudoku.");
        }
    }

    private int GetUserChoice()
    {
        while (true)
        {
            Console.Write("\nΕπιλογή: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice >= 1 && choice <= _solvers.Length + 1)
                return choice;
            
            Console.WriteLine("Μη έγκυρη επιλογή. Παρακαλώ προσπαθήστε ξανά.");
        }
    }
    
    public static void Main(string[] args)
    {
        var validator = new SudokuValidator();
        var cellFinder = new CellFinder(); 
        
        var solvers = new ISudokuSolver[]
        {
            new BacktrackingSolver(validator, cellFinder),  // Backtracking solver
            new DFSArrayListSolver(validator, cellFinder)    // DFS solver
        };
        
        var gridLoader = new FileGridLoader();
        var gridPrinter = new GridPrinter();
        
        var program = new Program(solvers, gridLoader, gridPrinter);
        program.Run();
    }

}