namespace SudokuSolver.Domain.Entities;

public class Grid
{
    private readonly int[,] _cells;
    public const int GridSize = 9;
    public const int BoxSize = 3;

    public Grid()
    {
        _cells = new int[GridSize, GridSize];
    }

    public Grid(int[,] initialState)
    {
        _cells = new int[GridSize, GridSize];
        Array.Copy(initialState, _cells, GridSize * GridSize);
    }

    public int this[int row, int col]
    {
        get => _cells[row, col];
        set => _cells[row, col] = value;
    }

    public Grid Clone() => new Grid(_cells);
}