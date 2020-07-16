using System;
using System.Threading;
using GoL;
using CommandLine;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 16;
            int height = 16;
            int ticks = -1;
            int seed = 4;
            string outputFileName = string.Empty;
            bool helpCalled = false;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    width = o.Width;
                    height = o.Height;
                    ticks = o.Ticks;
                    seed = o.Seed;
                    outputFileName = o.OutputFileName;
                })
                .WithNotParsed<Options>(o =>
                {
                   helpCalled = true;
                });

            if (!helpCalled)
            {
                Console.Clear();

                int[,] matrix = new int[height, width];

                GameOfLife.InitializeMatrixWithPattern(matrix, (InitPattern)seed);

                Console.SetCursorPosition(0,matrix.GetLength(0)+3);
                Console.WriteLine("Press ESC to stop");
                Console.WriteLine("To display help screen run 'GameOfLifeConsole --help' ");
                
                int generation = 0;
                do
                {
                    while (!Console.KeyAvailable && (ticks < 0 || generation < ticks))
                    {
                        matrix = GameOfLife.GetNextGeneration(matrix);
                        WriteArrayToScreen(matrix);
                        generation++;
                        Thread.Sleep(200);

                    }

                    if (generation >= ticks)
                    {
                        break;
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape && (ticks < 0 || generation < ticks));

                if (!string.IsNullOrWhiteSpace(outputFileName))
                {
                    GameOfLife.DumpGridState(matrix, outputFileName);
                }

                Console.SetCursorPosition(0, matrix.GetLength(0) + 5);
            }

        }

        private static void WriteArrayToScreen(int[,] array)
        {
            for (int y = 0; y < array.GetLength(0); y++)

            {
                for (int x = 0; x < array.GetLength(1); x++)
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
