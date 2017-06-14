using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class Extensions{

        public static List<string> Cross(this char A, string B){
            return (A.ToString()).Cross(B);
        }
        public static List<string> Cross(this string A, char B){
            return (A).Cross(B.ToString());
        }
        public static List<string> Cross(this string A, string B){
            var toReturn = new List<string>();
            foreach (char a in A.ToCharArray()){
                foreach (char b in B.ToCharArray()){
                    toReturn.Add(a.ToString()+b.ToString());
                }
            }
            return toReturn;
        } 

        public static Dictionary<string,string> Copy(this Dictionary<string,string> puzzle){
            var toReturn= new Dictionary<string,string>();
            foreach(var i in puzzle.Keys){
                toReturn.Add(i,puzzle[i]+"");
            }
            return toReturn;
        }

        public static int Diff(this Dictionary<string,string> puzzle, Dictionary<string,string> otherPuzzle){
            var diffCount=0;
            foreach(var i in puzzle.Keys){
                if(otherPuzzle.ContainsKey(i) && otherPuzzle[i]==puzzle[i]){
                    //match
                }
                else{
                    diffCount++;
                }
            }
            return diffCount;
        }
    }
}