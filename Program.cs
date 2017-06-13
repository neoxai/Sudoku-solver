using System;

namespace Sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Parser.BlankPuzzle());
            Console.WriteLine(Parser.Peers());
            var puzzle = Parser.ParseInitialPuzzle(SamplePuzzles.Sample1()).EliminateAllSingletons();
            puzzle.Print();
            puzzle = puzzle.EliminateAllTwins();
            puzzle.Print();
            puzzle = puzzle.EliminateAllSingletons();
            puzzle.Print();
        }
    }
}
