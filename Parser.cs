using System;
using System.Collections.Generic;

namespace Sudoku_solver
{
    public static class Parser{

 
        public static Dictionary<string,string> BlankPuzzle(){
            var toReturn =new Dictionary<string,string>();
            var rows="ABCDEFGHI";
            var cols="123456789";
            foreach(var id in rows.Cross(cols)){
                toReturn.Add(id,cols);
            }
            return toReturn;
        }

        public static Dictionary<string,string> ApplyInitialPuzzle(this Dictionary<string,string> puzzle, string initialConfig){
            foreach(char input in initialConfig.ToCharArray()){
               
            }
            return null;
        }
    }
}