using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
[assembly: InternalsVisibleTo("GameOfLifeTests")]
namespace GoL
{
    /// <summary>
    /// Humble implementation of Conway's Game of Life
    /// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    /// </summary>
    public class GameOfLife
    {
        private static int[,] blinker = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 } };
        private static int[,] toad = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 1, 1, 1, 0 }, { 0, 1, 1, 1, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        private static int[,] beacon = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 1, 1, 0, 0, 0 }, { 0, 1, 1, 0, 0, 0 }, { 0, 0, 0, 1, 1, 0 }, { 0, 0, 0, 1, 1, 0 }, { 0, 0, 0, 0, 0, 0 } };
        private static int[,] glider = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0, 0 }, { 0, 0, 1, 1, 0, 0 }, { 0, 0, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };

        public IGameOfLifeMatrix GameOfLifeMatrix { get; private set; }
        
        /// <summary>
        /// Returns the number of living neighbors
        /// </summary>
        /// <param name="mask">mask with the cell and all of it's neighbors</param>
        /// <returns></returns>
        public static int CountLivingNeighbors(int[,] mask)
        {
            return mask[0, 0] + mask[0, 1] + mask[0, 2] + mask[1, 0] + mask[1, 2] + mask[2, 0] + mask[2, 1] +
                   mask[2, 2];
        }

        /// <summary>
        /// Generates the next generation matrix
        /// Any live cell with two or three live neighbors survives.
        /// Any dead cell with three live neighbors becomes a live cell.
        /// All other live cells die in the next generation.Similarly, all other dead cells stay dead./// </summary>
        /// <param name="matrix">matrix with the state of the current generation</param>
        /// <returns>matrix with the state of the next generation</returns>
        public void GetNextGeneration()
        {
            var nextGeneration = CreateGameOfLifeMatrix(this.GameOfLifeMatrix.Width, this.GameOfLifeMatrix.Height);

            for (int y = 0; y < this.GameOfLifeMatrix.Height; y++)
            {
                for (int x = 0; x < this.GameOfLifeMatrix.Width; x++)
                {
                    
                    int livingNeighbors = this.GameOfLifeMatrix.CountLivingNeighbors(x,y);

                    

                    if (this.GameOfLifeMatrix.IsCellAlive(x,y) && livingNeighbors == 2 || this.GameOfLifeMatrix.IsCellAlive(x, y) && livingNeighbors == 3)
                    {
                        nextGeneration.SetCellState(x,y,true);
                    }
                    else
                    {
                        if (!this.GameOfLifeMatrix.IsCellAlive(x, y) && livingNeighbors == 3)
                        {
                            nextGeneration.SetCellState(x,y,true);
                        }
                        else
                        {
                            nextGeneration.SetCellState(x, y, false);
                        }
                    }
                }
            }

            this.GameOfLifeMatrix = nextGeneration;
        }

        private IGameOfLifeMatrix CreateGameOfLifeMatrix(int width, int height)
        {
            IGameOfLifeMatrix nextGeneration =
                new GameOfLifeMultiArray(width, height);
            return nextGeneration;
        }

        /// <summary>
        /// Initializes the matrix with pattern data or with random data
        /// </summary>
        /// <param name="destination">Destination matrix</param>
        /// <param name="pattern">Pattern name</param>
        public void InitializeMatrixWithPattern(int width, int height, InitPattern pattern)
        {

            this.GameOfLifeMatrix = CreateGameOfLifeMatrix(width, height);

            switch (pattern)
            {
                case InitPattern.Beacon:
                    InitializeGameOfLife(this.GameOfLifeMatrix, beacon);
                    break;
                case InitPattern.Blinker:
                    InitializeGameOfLife(this.GameOfLifeMatrix, blinker);
                    break;
                case InitPattern.Toad:
                    InitializeGameOfLife(this.GameOfLifeMatrix, toad);
                    break;
                case InitPattern.Glider:
                    InitializeGameOfLife(this.GameOfLifeMatrix, glider);
                    break;
                default:
                    InitializeMatrixWithRandomValues();
                    break;
            }
        }


        

        /// <summary>
        /// Initializes the matrix with random values
        /// </summary>
        internal void InitializeMatrixWithRandomValues()
        {
            Random random = new Random();

            for (int y = 0; y < this.GameOfLifeMatrix.Height; y++)
            {
                for (int x = 0; x < this.GameOfLifeMatrix.Width; x++)
                {
                    var number = random.Next(0, 4);

                    bool isLive = number > 2;

                    this.GameOfLifeMatrix.SetCellState(x,y,isLive);
                }
            }

        }
        

        

        /// <summary>
        /// Dumps the current matrix state into a file
        /// </summary>
        /// <param name="outputFileName">Filename of the output file</param>
        public void DumpGridState(string outputFileName)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < this.GameOfLifeMatrix.Height; y++)

            {
                for (int x = 0; x < this.GameOfLifeMatrix.Width; x++)
                {
                    if (this.GameOfLifeMatrix.IsCellAlive(x,y))
                    {
                        sb.Append("█");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                sb.AppendLine();
            }

            using (StreamWriter sw = new StreamWriter(outputFileName))
            {
                sw.Write(sb.ToString());
            }
        }

        internal static void InitializeGameOfLife(IGameOfLifeMatrix matrix, int[,] pattern)
        {
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    matrix.SetCellState(x, y, pattern[y, x] == 1);
                }
            }
        }
    }

    public interface IGameOfLifeMatrix
    {

        int Width { get; }

        int Height { get; }
        bool IsCellAlive(int x, int y);
        void SetCellState(int x, int y, bool isAlive);

        int CountLivingNeighbors(int x, int y);
    }

    

    internal class GameOfLifeDictionary : IGameOfLifeMatrix
    {
        public int Width { get; internal set; }
        
        public int Height { get; internal set; }

        private Dictionary<Point, bool> cells = new Dictionary<Point, bool>();

        


        public GameOfLifeDictionary(int width, int height)
        {
            Width = width;
            Height = height;


        }


        public bool IsCellAlive(int x, int y)
        {
            var position = new Point(x,y);

            if (cells.ContainsKey(position))
            {
                return this.cells[position];
            }


            return false;
        }

        public void SetCellState(int x, int y, bool isAlive)
        {
            var position = new Point(x,y);

            if (cells.ContainsKey(position))
            {
                if (isAlive)
                {
                    cells[position] = true;
                }
                else
                {
                    cells.Remove(position);
                }
            }
            else
            {
                if (isAlive)
                {
                    cells.Add(position,true);
                }
            }
        }

        public int CountLivingNeighbors(int x, int y)
        {
            throw new NotImplementedException();
        }
    }


    internal class GameOfLifeMultiArray : IGameOfLifeMatrix
    {
        private int[,] matrix;

        public GameOfLifeMultiArray(int width, int height)
        {
            this.matrix = new int[height,width];
        }


        public int Width => this.matrix.GetLength(1);

        public int Height => this.matrix.GetLength(0);

        public bool IsCellAlive(int x, int y)
        {
            return matrix[y, x] == 1;
        }

        public void SetCellState(int x, int y, bool isAlive)
        {
            if (x < 0 || x >= matrix.GetLength(1) || y <0 || y>= matrix.GetLength(0))
            {
                return;
            }

            if (isAlive)
            {
                matrix[y, x] = 1;
            }
            else
            {
                matrix[y, x] = 0;
            }
        }

        public int CountLivingNeighbors(int x, int y)
        {
            int width = matrix.GetLength(1);
            int heigth = matrix.GetLength(0);

            int livingNeighbors = 0;

            for (int ypos = y-1; ypos <= y+1; ypos++)
            {
                for (int xpos = x-1; xpos <= x+1; xpos++)
                {
                    // don't count the requested cell 
                    if (xpos == x && ypos == y)
                    {
                        continue;
                    }

                    if (xpos >= 0 && xpos < width && ypos >= 0 && ypos < heigth  )
                    {
                        if (matrix[ypos,xpos] == 1)
                        {
                            livingNeighbors++;
                        }

                    }
                }
            }


            return livingNeighbors;
        }
    }
}