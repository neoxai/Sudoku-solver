﻿using System;

namespace Sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Parser.BlankPuzzle());
            Console.WriteLine(Parser.Peers());
            var puzzle = Parser.ParseInitialPuzzle(SamplePuzzles.Sample1());
            puzzle.Print();
        }
    }
}
