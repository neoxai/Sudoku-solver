using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public interface ISolver{
        void Run();
    }
    public class Solver: ISolver{

        public Solver(ISearch search){
            _search=search;
        }
        ISearch _search;

        public void Run(){
            Console.WriteLine("Hello World!");
            Console.WriteLine(Parser.BlankPuzzle());
            Console.WriteLine(Parser.Peers());
            var puzzle = Parser.ParseInitialPuzzle(SamplePuzzles.HardPuzzle1()).RunConstraints();
            Console.WriteLine("Max solve with no guesses:");
            puzzle.Print();
            Console.WriteLine("Begin guess search-----------------");
            var guessPuzzle = _search.GenerateNewSearches(puzzle);
            guessPuzzle?.Print();
            Console.WriteLine("Program over");
        }
     
    }

}