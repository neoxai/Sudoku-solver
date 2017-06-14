using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class Constraint{

        public static bool Validate(this Dictionary<string,string> puzzle){
            if(puzzle == null) return false;
            //Any cells with no possible values = contradiction.
            if(puzzle.Values.Where(x => x.Length ==0).Count()>0) return false;
            
            return true;
        }

        public static bool IsSolved(this Dictionary<string,string> puzzle){
            if(puzzle == null) return false;
            return puzzle.Validate() && (puzzle.Evaluate_SolvedCells()==81);
        }
        public static int Evaluate_SolvedCells(this Dictionary<string,string> puzzle){
            return puzzle.Values.Where(x => x.Length ==1).Count();
        }
        public static int Evaluate_RemainingPossibilities(this Dictionary<string,string> puzzle){
            return String.Concat("",puzzle.Values).Length;
        }

        public static Dictionary<string,string> RunConstraints(this Dictionary<string,string> puzzle){
            if(puzzle == null) return null;
            if(puzzle.IsSolved()) return puzzle;
            var previousPuzzle=puzzle.Copy();
            var newPuzzle = puzzle.Copy();
            newPuzzle=newPuzzle.EliminateAllSingletons().EliminateAllTwins();
            newPuzzle=newPuzzle.EliminateAllSingletons().LocateAllOnlyPossibles();
            //Console.WriteLine($"Found {previousPuzzle.Diff(newPuzzle)} diffs, rerunning.");

            if(previousPuzzle.Diff(newPuzzle)==0) return newPuzzle;
            
            return newPuzzle.RunConstraints();
        }

        public static Dictionary<string,string> LocateAllOnlyPossibles(this Dictionary<string,string> puzzle){
            if(puzzle == null) return null;
            var toReturn  = puzzle.Copy();
            var units=Parser.Units();
            foreach( var u in units){
                toReturn = toReturn.LocateOnlyPossible(u);
            }
            return toReturn;
        }
        public static Dictionary<string,string> LocateOnlyPossible(this Dictionary<string,string> puzzle, List<string> Unit){
            if(puzzle == null) return null;
            var toReturn = puzzle.Copy();
            var totalUnfinishedCells= String.Join("",Unit.Select(x => toReturn[x]).Where(y => y.Length > 1)).ToCharArray().ToList();
            var oneOccurance =totalUnfinishedCells.GroupBy(s => s).Where(x => x.Count() == 1);
            foreach(var g in oneOccurance){
                //Console.WriteLine("Located Only Possible Value:" + g.Key);
                foreach(var cell in Unit){
                    if(puzzle[cell].Contains(g.Key)){
                        toReturn=toReturn.Assign(cell,g.Key.ToString())?.RunConstraints();
                        if(toReturn.IsSolved()) return toReturn;
                        if(toReturn == null) return null;
                    }
                }
            }
            return toReturn;
        }

        public static Dictionary<string,string> EliminateAllSingletons(this Dictionary<string,string> puzzle){
            if(puzzle == null) return null;
            var toReturn = puzzle.Copy();

            foreach(var key in puzzle.Keys){
                if(toReturn[key].Length==1){
                    toReturn=toReturn.EliminateSingleton(key,toReturn[key]);
                }
            }
            return toReturn;
        }
        public static Dictionary<string,string> EliminateSingleton(this Dictionary<string,string> puzzle, string cell, string value){
            if(puzzle == null) return null;
            var puzz = puzzle.Copy();
            var peers = Parser.Peers()[cell];
            foreach(var peerCell in peers.Where(x => x!=cell)){
                puzz[peerCell]=puzz[peerCell].Replace(value,"");
            }

            return puzz;
        }

        public static Dictionary<string,string> EliminateAllTwins(this Dictionary<string,string> puzzle){
            if(puzzle == null) return null;
            var toReturn = puzzle.Copy();
            var units = Parser.Units();
            foreach(var unit in units){
                var unitValues=unit.Select(x => toReturn[x]).Where(y => y.Length > 1);
                var repeated =unitValues.GroupBy(s => s).Where(x => x.Count() > 1);
                
                foreach(var group in repeated){
                    var key = group.Key;
                    var count = group.Count();
                    if(key.Length == count){
                        toReturn=toReturn.EliminateTwin(unit,key);
                    }
                }
            }
            return toReturn;
        }
        public static Dictionary<string,string>  EliminateTwin(this Dictionary<string,string> puzzle, List<string> unit, string value){
            if(puzzle == null) return null;
            var toReturn = puzzle.Copy();
            var nonPairedCells = unit.Where(x => toReturn[x]!=value);
            foreach(var nonCell in nonPairedCells){
                foreach(var v in value.ToCharArray()){
                    toReturn[nonCell]=toReturn[nonCell].Replace(v.ToString(),"");
                }
            }   
            return toReturn;
        }
        public static Dictionary<string,string> UnAssign(this Dictionary<string,string> puzzle, string cell, string value){
            if(puzzle == null) return null;
            //If this value wasn't an allowed value, then quit
            try{
                if(!puzzle[cell].Contains(value)){return null;}
            
                puzzle[cell]=puzzle[cell].Replace(value,"");
                return puzzle.RunConstraints();
            }
            catch(Exception ex){
                return null;
            }
        }
        public static Dictionary<string,string> Assign(this Dictionary<string,string> puzzle, string cell, string value){
            if(puzzle == null) return null;

            //If this value wasn't an allowed value, then quit
            try{
                if(!puzzle[cell].Contains(value)){return null;}
            
                puzzle[cell]=value;

                return puzzle.EliminateSingleton(cell,value);
            }
            catch(Exception ex){
                return null;
            }

          

        }
    }
}