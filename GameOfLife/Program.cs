﻿using System;
using System.Threading;
using GoL;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[8, 8];
            int[,] blinker = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 } };
            int[,] toad = new int[6, 6] { { 0, 0, 0, 0, 0,0 }, { 0, 0, 0, 0, 0,0 }, { 0, 0, 1, 1, 1,0 }, { 0, 1, 1, 1, 0,0 }, { 0, 0, 0, 0, 0,0 }, { 0, 0, 0, 0, 0, 0 } };
            int[,] beacon = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 1, 1, 0, 0, 0 }, { 0, 1, 1, 0, 0, 0 }, { 0, 0, 0, 1, 1, 0 }, { 0, 0, 0, 1, 1, 0 }, { 0, 0, 0, 0, 0, 0 } };
            int[,] glider = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0, 0 }, { 0, 0, 1, 1, 0, 0 }, { 0, 0, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
            
            GameOfLife.InitializeMatrixwithRandomValues(matrix);
            //GameOfLife.CopyToMatrix(beacon, matrix);

            while (true)
            {
                matrix = GameOfLife.GetNextGeneration(matrix);
                WriteArrayToScreen(matrix);
                Thread.Sleep(200);
            }
        }

        private static void WriteArrayToScreen(int[,] array)
        {
            for (int y = 0; y < array.GetLength(1); y++)

            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    Console.SetCursorPosition(x, y);


                    if (array[y, x] == 1)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }


        


        

    }
}
