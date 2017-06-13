using System;
using System.Collections.Generic;

namespace Sudoku_solver
{
    public static class Extensions{
        public static List<string> Cross(this string A, string B){
            var toReturn = new List<string>();
            foreach (char a in A.ToCharArray()){
                foreach (char b in B.ToCharArray()){
                    toReturn.Add(a+""+b);
                }
            }
            return toReturn;
        } 
    }
}