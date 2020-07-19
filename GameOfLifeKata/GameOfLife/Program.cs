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

                var gameOfLife = new GameOfLife();

                gameOfLife.InitializeMatrixWithPattern(width, height, (InitPattern)seed, MatrixType.HashSet);

                Console.SetCursorPosition(0,height+3);
                Console.WriteLine("Press ESC to stop");
                Console.WriteLine("To display help screen run 'GameOfLifeConsole --help' ");
                
                int generation = 0;
                do
                {
                    while (!Console.KeyAvailable && (ticks < 0 || generation < ticks))
                    {
                        gameOfLife.GetNextGeneration();
                        WriteArrayToScreen(gameOfLife.GameOfLifeMatrix);
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
                    gameOfLife.DumpGridState(outputFileName);
                }

                Console.SetCursorPosition(0, height + 5);
            }

        }

        private static void WriteArrayToScreen(IGameOfLifeMatrix matrix)
        {
            for (int y = 0; y < matrix.Height; y++)

            {
                for (int x = 0; x < matrix.Width; x++)
                {
                    Console.SetCursorPosition(x, y);


                    if (matrix.IsCellAlive(x,y))
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
