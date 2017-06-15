using System.Collections.Generic;

namespace Sudoku_solver
{
    public class SearchSpace{
        public Dictionary<string,string> Puzzle {get;set;}
        public IEnumerable<Guess> PreviousGuesses {get;set;}
    }
}