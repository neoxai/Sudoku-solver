namespace Sudoku_solver
{
    public static class SamplePuzzles{

        public static string Sample1(){
            return 
                "79X XXX 3XX"+
                "XXX XX6 9XX"+
                "8XX X3X X76"+

                "XXX XX5 XX2"+
                "XX5 418 7XX"+
                "4XX 7XX XXX"+
                
                "61X X9X XX8"+
                "XX2 3XX XXX"+
                "XX9 XXX X54";

        }

        public static string HardPuzzle1(){
           return 
    "8 5 .      . . 2       4 . . "+ 
    "7 2 .      . . .       . . 9 "+
    ". . 4      . . .       . . . "+

    ". . .      1 . 7       . . 2 "+
    "3 . 5      . . .       9 . . "+
    ". 4 .      . . .       . . . "+

    ". . .      . 8 .       . 7 . "+
    ". 1 7      . . .       . . . "+
    ". . .      . 3 6       . 4 . "; 
        }
    }
}