using System;

namespace GoL
{
    public class GameOfLife
    {
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

        //Any live cell with two or three live neighbors survives.
        //Any dead cell with three live neighbors becomes a live cell.
        //All other live cells die in the next generation.Similarly, all other dead cells stay dead.

        public static int[,] GetNextGeneration(int[,] matrix)
        {

            int destinationWidth = matrix.GetLength(0);
            int destinationHeight = matrix.GetLength(1);

            int[,] nextGeneration = new int[destinationHeight, destinationWidth];

            for (int y = 0; y < destinationHeight; y++)
            {
                for (int x = 0; x < destinationWidth; x++)
                {
                    int[,] mask = CopyToSubMatrix(matrix, x, y);
                    int livingNeigbors = CountLivingNeighbors(mask);

                    bool cellIsAlive = matrix[y, x] == 1;

                    if (cellIsAlive && livingNeigbors == 2 || cellIsAlive && livingNeigbors == 3)
                    {
                        nextGeneration[y, x] = 1;
                    }
                    else
                    {
                        if (!cellIsAlive && livingNeigbors == 3 )
                        {
                            nextGeneration[y, x] = 1;
                        }
                        else
                        {
                            nextGeneration[y, x] = 0;
                        }
                    }
                }
            }

            return nextGeneration;
        }



        public static void CopyToMatrix(int[,] source, int[,] destination)
        {
            int sourceWidth = source.GetLength(1);
            int sourceHeight = source.GetLength(0);

            int destinationWidth = destination.GetLength(1);
            int destinationHeight = destination.GetLength(0);


            for (int y = 0; y < sourceHeight; y++)
            {
                for (int x = 0; x < sourceWidth; x++)
                {
                    if (x < destinationWidth && y < destinationHeight)
                    {
                        destination[y, x] = source[y, x];
                    }
                }
            }
        }

        public static void InitializeMatrixWithRandomValues( int[,] destination)
        {
            
            int destinationWidth = destination.GetLength(1);
            int destinationHeight = destination.GetLength(0);
            Random random = new Random();

            for (int y = 0; y < destinationHeight; y++)
            {
                for (int x = 0; x < destinationWidth; x++)
                {
                    var number = random.Next(0, 4);

                    bool isLive = number > 2;

                    if (isLive)
                    {
                        destination[y, x] =  1;
                    }
                    else
                    {
                        destination[y, x] = 0;
                    }
                }
            }

        }


        /// <summary>
        /// Copies the cell's neighbors to a submatrix to determine how many neighbors are alive
        /// </summary>
        /// <param name="source">SourceMatrix</param>
        /// <param name="xpos">The cell's x-position</param>
        /// <param name="ypos">the cell's y-position</param>
        public static int[,] CopyToSubMatrix(int[,] source, int xpos, int ypos)
        {
            // The mask's upper left corner position  is one field above and one field left of the cell's position
            int readOffset = -1;
            int destinationWidth = 3;
            int destinationHeight = 3;
            int sourceWidth = source.GetLength(1);
            int sourceHeight = source.GetLength(0);


            int[,] destination = new int[3, 3];


            for (int y = 0; y < destinationHeight; y++)
            {
                for (int x = 0; x < destinationWidth; x++)
                {
                    int xReadPos = x + xpos + readOffset;
                    int yReadPos = y + ypos + readOffset;

                    if (xReadPos < 0 || xReadPos >= sourceWidth || yReadPos < 0 || yReadPos >= sourceHeight)
                    {
                        destination[y, x] = 0;
                    }
                    else
                    {
                        destination[y, x] = source[ yReadPos, xReadPos];
                    }

                }
            }

            return destination;
        }

    }
}