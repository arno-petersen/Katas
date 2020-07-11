using System;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[8, 8] { { 1, 1, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 1, 0, 0, 0 }, { 0, 0, 1, 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0, 0, 1, 0 }, { 0, 1, 0, 0, 0, 0, 1, 0 }, { 0, 0, 1, 0, 0, 1, 0, 0 }, { 0, 0, 0, 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };

            int[,] mask = new int[3, 3];






            WriteArrayToScreen(matrix);

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
