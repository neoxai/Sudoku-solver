using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class Constraint{

        public static bool Validate(this Dictionary<string,string> puzzle){

            return false;
        }

        public static Dictionary<string,string> RunConstraints(this Dictionary<string,string> puzzle){
            var previousPuzzle=puzzle.Copy();
            var newPuzzle=puzzle.EliminateAllSingletons().EliminateAllTwins();
            newPuzzle=newPuzzle.EliminateAllSingletons().LocateAllOnlyPossibles();
            Console.WriteLine($"Found {previousPuzzle.Diff(newPuzzle)} diffs, rerunning.");

            if(previousPuzzle.Diff(newPuzzle)==0) return newPuzzle;
            
            return newPuzzle.RunConstraints();
        }

        public static Dictionary<string,string> LocateAllOnlyPossibles(this Dictionary<string,string> puzzle){
            var toReturn  = puzzle.Copy();
            var units=Parser.Units();
            foreach( var u in units){
                toReturn = toReturn.LocateOnlyPossible(u);
            }
            return toReturn;
        }
        public static Dictionary<string,string> LocateOnlyPossible(this Dictionary<string,string> puzzle, List<string> Unit){
            var toReturn = puzzle.Copy();
            var totalUnfinishedCells= String.Join("",Unit.Select(x => toReturn[x]).Where(y => y.Length > 1)).ToCharArray().ToList();
            var oneOccurance =totalUnfinishedCells.GroupBy(s => s).Where(x => x.Count() == 1);
            foreach(var g in oneOccurance){
                Console.WriteLine("Located Only Possible Value:" + g.Key);
                foreach(var cell in Unit){
                    if(puzzle[cell].Contains(g.Key)){
                        toReturn=toReturn.Assign(cell,g.Key.ToString());
                    }
                }
            }
            return toReturn;
        }

        public static Dictionary<string,string> EliminateAllSingletons(this Dictionary<string,string> puzzle){
            var toReturn = puzzle.Copy();

            foreach(var key in puzzle.Keys){
                if(toReturn[key].Length==1){
                    toReturn=toReturn.EliminateSingleton(key,toReturn[key]);
                }
            }
            return toReturn;
        }
        public static Dictionary<string,string> EliminateSingleton(this Dictionary<string,string> puzzle, string cell, string value){
            var peers = Parser.Peers()[cell];
            foreach(var peerCell in peers.Where(x => x!=cell)){
                puzzle[peerCell]=puzzle[peerCell].Replace(value,"");
            }

            return puzzle;
        }

        public static Dictionary<string,string> EliminateAllTwins(this Dictionary<string,string> puzzle){
            var toReturn = puzzle.Copy();
            var units = Parser.Units();
            foreach(var unit in units){
                var unitValues=unit.Select(x => toReturn[x]).Where(y => y.Length > 1);
                var repeated =unitValues.GroupBy(s => s).Where(x => x.Count() > 1);
                
                foreach(var group in repeated){
                    var key = group.Key;
                    var count = group.Count();
                    if(key.Length == count){
                        Console.WriteLine(key);
                        toReturn=toReturn.EliminateTwin(unit,key);
                    }
                }
                
  
            }
            return toReturn;
        }

        public static Dictionary<string,string>  EliminateTwin(this Dictionary<string,string> puzzle, List<string> unit, string value){
            var toReturn = puzzle.Copy();
            var nonPairedCells = unit.Where(x => toReturn[x]!=value);
            foreach(var nonCell in nonPairedCells){
                foreach(var v in value.ToCharArray()){
                    toReturn[nonCell]=toReturn[nonCell].Replace(v.ToString(),"");
                }
            }   
            return toReturn;
        }
        public static Dictionary<string,string> Assign(this Dictionary<string,string> puzzle, string cell, string value){
            //If this value wasn't an allowed value, then quit
            if(!puzzle[cell].Contains(value)){return null;}
            
            //if this value was already defined in the 
            puzzle[cell]=value;

            return puzzle.EliminateSingleton(cell,value);

        }
    }
}