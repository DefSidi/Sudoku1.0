using System;
using System.Linq;

internal class Program
{
    private static void Main()
    {
        int[][] sudoku = {
            new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
            new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
            new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
            new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
            new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
            new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
            new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
            new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
            new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9}
        };

        bool isValid = CheckSudoku(sudoku);
        Console.WriteLine("Sudoku is " + (isValid ? "valid" : "invalid"));
    }

    public static bool CheckSudoku(int[][] grid)
    {
        int size = grid.Length;
        int subgridDimension = (int)Math.Sqrt(size);

        return ValidateGridStructure(grid, size, subgridDimension) &&
               CheckRowsAndColumns(grid, size) &&
               VerifySubgrids(grid, size, subgridDimension);
    }

    private static bool ValidateGridStructure(int[][] grid, int size, int subgridDimension)
    {
        if (size == 0 || subgridDimension * subgridDimension != size)
        {
            Console.WriteLine("Grid dimension is invalid.");
            return false;
        }
        return true;
    }

    private static bool CheckRowsAndColumns(int[][] grid, int size)
    {
        for (int i = 0; i < size; i++)
        {
            int[] row = grid[i];
            int[] column = grid.Select(row => row[i]).ToArray();

            if (!HasUniqueValues(row, size) || !HasUniqueValues(column, size))
            {
                Console.WriteLine("Validation failed for rows or columns.");
                return false;
            }
        }
        return true;
    }

    private static bool VerifySubgrids(int[][] grid, int size, int subgridDimension)
    {
        for (int row = 0; row < size; row += subgridDimension)
        {
            for (int col = 0; col < size; col += subgridDimension)
            {
                int[] subgrid = ExtractSubgridValues(grid, row, col, subgridDimension);
                if (!HasUniqueValues(subgrid, size))
                {
                    Console.WriteLine("Subgrid validation failed.");
                    return false;
                }
            }
        }
        return true;
    }

    private static bool HasUniqueValues(int[] values, int size)
    {
        return values.Distinct().Count() == size && values.All(v => v >= 1 && v <= size);
    }

    private static int[] ExtractSubgridValues(int[][] grid, int startRow, int startCol, int subgridDimension)
    {
        return Enumerable.Range(startRow, subgridDimension)
                         .SelectMany(r => Enumerable.Range(startCol, subgridDimension)
                                                    .Select(c => grid[r][c]))
                         .ToArray();
    }
}
