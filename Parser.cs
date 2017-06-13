using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class Parser{
        public static string  ROWS = "ABCDEFGHI";
        public static string DIGITS = "123456789";
        public static List<string> BoxROWS = new List<string>{"ABC","DEF","GHI"};
        public static List<string> BoxCOLS = new List<string>{"123","456","789"};
        public static Dictionary<string,string> BlankPuzzle(){
            var toReturn =new Dictionary<string,string>();
            var rows=ROWS;
            var cols=DIGITS;
            foreach(var id in rows.Cross(cols)){
                toReturn.Add(id,cols);
            }
            return toReturn;
        }

        public static List<List<string>> Units(){
            var allUnits= new List<List<string>> ();

            foreach(char rw in ROWS.ToCharArray()){
                allUnits.Add(rw.Cross(DIGITS));
            }
            foreach(char col in DIGITS.ToCharArray()){
                allUnits.Add(ROWS.Cross(col));
            }
            foreach(var boxRw in BoxROWS){
                foreach(var boxCol in BoxCOLS){
                    allUnits.Add(boxRw.Cross(boxCol));
                }
            }

            return allUnits;
        }
        public static string[] CellMap(){
              string[] cellMap=new string[81];
              var i=0;
              foreach (var cell in ROWS.Cross(DIGITS)){
                cellMap[i]=cell;
                i++;
            }
            return cellMap;
        }
        public static Dictionary<string,IEnumerable<List<string>>> UnitMap(){
            var units = Units();
            var unitDict= new Dictionary<string,IEnumerable<List<string>>>();
            foreach (var cell in ROWS.Cross(DIGITS)){
                
                var ApplicableUnits=units.Where(x => x.Contains(cell));
                unitDict.Add(cell,ApplicableUnits);
            }
            return unitDict;
        }
        public static Dictionary<string,List<string>> Peers(){
            var unitMap = UnitMap();
            var peerDict= new Dictionary<string,List<string>>();
            foreach (var cell in ROWS.Cross(DIGITS)){
                var uniquePeers=new List<string>();
                foreach(var unit in unitMap[cell]){
                    foreach(var potentialPeer in unit){
                        if (potentialPeer != cell && !uniquePeers.Contains(potentialPeer)){
                            uniquePeers.Add(potentialPeer);
                        }
                    }
                }
                peerDict.Add(cell,uniquePeers);
            }
            return peerDict;
        }
         public static string RemoveWhitespace(this string input){
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        public static Dictionary<string,string> ParseInitialPuzzle(string initialConfig){
            var blank = BlankPuzzle();
            var cellMap=CellMap();
            var initial=initialConfig.RemoveWhitespace();
            for(int i =0;i<initial.Length;i++){
                if(DIGITS.Contains(initial[i])){
                    blank[cellMap[i]]=initial[i].ToString();
                    blank=blank.Assign(cellMap[i],initial[i].ToString());
                }
            }
            return blank;
        }
        
        public static void Print(this Dictionary<string,string> puzzle){
            Console.WriteLine();
            var width= 1+ puzzle.Values.Max(x => x.Length);
            var partLine = string.Concat(Enumerable.Repeat("-", (width)*3));
            var line = string.Concat(partLine, '+',partLine, '+',partLine);
            foreach(var r in ROWS.ToCharArray()){
                var printRow="";
                foreach (var c in DIGITS.ToCharArray()){
                    printRow+=puzzle[string.Concat(r,c)].Center(width);
                    if("36".Contains(c)){
                        printRow+="|";
                    }
                }
                Console.WriteLine(printRow);
                if("CF".Contains(r)){
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine();
        }

        public static string Center(this string input, int width){
            if(input.Length >= width) return input;
            if(input.Length >= width-1) return string.Concat(input," ").Center(width);
            return string.Concat(" ",input," ").Center(width);
        }
    }
}