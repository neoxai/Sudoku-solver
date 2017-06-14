using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public class SearchSpace{
        public Dictionary<string,string> Puzzle {get;set;}
        public IEnumerable<Guess> PreviousGuesses {get;set;}
    }

    public class Guess {
        public string Cell {get;set;}
        public string Value {get;set;}
    }
    public static class Search{

        public static Dictionary<string,string> GenerateNewSearches(Dictionary<string,string> puzzle){
            return GenerateNewSearches(new SearchSpace(){
                Puzzle=puzzle,
                PreviousGuesses = new List<Guess>()
            });
        }
        public static Dictionary<string,string> GenerateNewSearches(SearchSpace t){
            if(t.Puzzle == null) return null;
            if(t.Puzzle.IsSolved()) return t.Puzzle;
            var cell = ChooseBestCellToGuess(t.Puzzle, t.PreviousGuesses.Select(x => x.Value));
            if (cell == "") return null;
            var possibleValues = t.Puzzle[cell].ToCharArray().Select(x => x.ToString());
            
            foreach(var guess in possibleValues){
                var attempt = SearchAssumption(t,new Guess(){Cell=cell, Value=guess});
                if (attempt !=null && attempt.IsSolved()) return attempt;
            }
            return null;
           
        }
        public static Dictionary<string,string> SearchAssumption(SearchSpace t, Guess newGuess){
            if(t.Puzzle == null) return null;
            if(t.Puzzle.IsSolved()) return t.Puzzle;

            Console.WriteLine($"Guessing: {t.PreviousGuesses.Print()}/{newGuess.Print()}");

            var testPuzzle = t.Puzzle.Copy();
            testPuzzle = testPuzzle.Assign(newGuess.Cell,newGuess.Value);
            testPuzzle = testPuzzle.RunConstraints();
            if(testPuzzle == null) return null;
            if(testPuzzle.IsSolved()) return testPuzzle;

            return GenerateNewSearches(new SearchSpace(){
                PreviousGuesses = t.PreviousGuesses.Concat(new List<Guess>(){newGuess}),
                Puzzle = testPuzzle
            });                
        }
        
        public static Dictionary<string,string> BlindGuess(this Dictionary<string,string> puzzle, IEnumerable<string> alreadyTriedCells){
            if(puzzle == null) return null;
            if(puzzle.IsSolved()) return puzzle;

            var myPuzz=puzzle.Copy();
            var cell = ChooseBestCellToGuess(puzzle,alreadyTriedCells);
            if (cell == "") return puzzle;
            var possibleValues = puzzle[cell].ToCharArray().Select(x => x.ToString());
            
            foreach(var guess in possibleValues){
                if(myPuzz == null) return null;
                var testPuzzle= myPuzz.Copy();
                Console.WriteLine($"Guessing a value for {cell}, attempted = {guess}, out of possible {myPuzz[cell]}");
                testPuzzle = testPuzzle.Assign(cell,guess);
                if(testPuzzle != null){
                    testPuzzle = testPuzzle.RunConstraints();
                    if (testPuzzle.Validate()){
                        if(testPuzzle.IsSolved()) {
                            Console.WriteLine("SOLVED!");
                            testPuzzle.Print();
                            return testPuzzle;
                        }
                        return testPuzzle.BlindGuess(new List<string>());
                    }
                    else{
                        Console.WriteLine("  Invalid after running constraints check. Trying next...");
                        var unassigned = myPuzz.UnAssign(cell,guess);
                        if(unassigned != null) myPuzz=unassigned;

                    }
                }
                else{
                    Console.WriteLine("  Invalid, trying next...");
                    var unassigned = myPuzz.UnAssign(cell,guess);
                    if(unassigned != null) myPuzz=unassigned;
                }
                
            }

            return puzzle.BlindGuess(alreadyTriedCells.Concat(new List<string>{cell}));
        }
        public static string ChooseBestCellToGuess(Dictionary<string,string> puzzle,IEnumerable<string> alreadyTriedCells){
            if(puzzle == null) return "";
            var orderedCells = puzzle.Keys.Where(x => puzzle[x].Length > 1 && !alreadyTriedCells.Contains(x)).OrderBy( y => puzzle[y].Length);
            if (orderedCells.Count() ==0) return "";
            return orderedCells.First();
        }



    }
}