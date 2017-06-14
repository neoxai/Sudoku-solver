using System;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Parser.BlankPuzzle());
            Console.WriteLine(Parser.Peers());
            var puzzle = Parser.ParseInitialPuzzle(SamplePuzzles.HardPuzzle1()).RunConstraints();
            Console.WriteLine("Max solve with no guesses:");
            puzzle.Print();
            Console.WriteLine("Begin guess search-----------------");
            var guessPuzzle = Search.GenerateNewSearches(puzzle);
            guessPuzzle?.Print();
            Console.WriteLine("Program over");
            
        }
    }
}
