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


        /// <summary>
        /// Copies the cell's neighbors to a submatrix to determine how many neighbors are alive
        /// </summary>
        /// <param name="source">SourceMatrix</param>
        /// <param name="destination">Submatrix with all neighbors</param>
        /// <param name="xpos">The cell's x-position</param>
        /// <param name="ypos">the cell's y-position</param>
        public static void CopyToSubArray(int[,] source, int[,] destination, int xpos, int ypos)
        {
            // The mask's upper left corner position  is one field above and one field left of the cell's position
            int readOffset = -1;
            int destinationWidth = destination.GetLength(0);
            int destinationHeight = destination.GetLength(1);


            for (int y = 0; y < destinationHeight; y++)
            {
                for (int x = 0; x < destinationWidth; x++)
                {
                    int xReadPos = x + xpos + readOffset;
                    int yReadPos = y + ypos + readOffset;

                    if (xReadPos < 0 || xReadPos >= destinationWidth || yReadPos < 0 || yReadPos >= destinationWidth)
                    {
                        destination[y, x] = 0;
                    }
                    else
                    {
                        destination[y, x] = source[xReadPos, yReadPos];
                    }

                }
            }
        }

    }
}