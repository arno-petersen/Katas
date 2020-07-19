using System.Runtime.CompilerServices;
using GoL;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class GameOfLifeTestCases
    {

        [Test]
        public void GameOfLifeMatrixShouldReturnNumberOfLivingNeighbors()
        {
            int[,] pattern = new int[4, 5] { { 1, 1, 1,1,1 }, { 1, 1, 1,1,1 }, { 1, 1, 1,1,1 }, { 1, 1, 1, 1, 1 } };

            IGameOfLifeMatrix matrix = new GameOfLifeMultiArray(5,4);
            GameOfLife.InitializeGameOfLife(matrix,pattern);

            CheckForLivingNeighbors(matrix);


            // Test the dictionary version
            matrix = new GameOfLifeHashSet(5,4);
            GameOfLife.InitializeGameOfLife(matrix, pattern);
            CheckForLivingNeighbors(matrix);


        }

        private static void CheckForLivingNeighbors(IGameOfLifeMatrix matrix)
        {
            Assert.AreEqual(3, matrix.CountLivingNeighbors(0, 0));
            Assert.AreEqual(3, matrix.CountLivingNeighbors(4, 3));
            Assert.AreEqual(3, matrix.CountLivingNeighbors(0, 3));
            Assert.AreEqual(3, matrix.CountLivingNeighbors(4, 0));

            for (int i = 1; i < 4; i++)
            {
                Assert.AreEqual(5, matrix.CountLivingNeighbors(i, 0));
                Assert.AreEqual(5, matrix.CountLivingNeighbors(i, 3));
            }

            for (int i = 1; i < 3; i++)
            {
                Assert.AreEqual(5, matrix.CountLivingNeighbors(0, i));
                Assert.AreEqual(5, matrix.CountLivingNeighbors(4, i));
            }

            for (int y = 1; y < 3; y++)
            {
                for (int x = 1; x < 4; x++)
                {
                    Assert.AreEqual(8, matrix.CountLivingNeighbors(x, y));
                }
            }
        }

        [Test]
        public void GetNextGenerationShouldReturnTheNextGeneration()
        {
            int[,] matrix = new int[5,5] {{0,0,0,0,0}, { 0, 0, 1, 0, 0 } , { 0, 0, 1, 0, 0 } , { 0, 0, 1, 0, 0 } , { 0, 0, 0, 0, 0 } };
            int[,] expected = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

            var gameOfLive = new GameOfLife();
            gameOfLive.InitializeMatrixWithPattern(5,5,InitPattern.Blinker);
            gameOfLive.GetNextGeneration();

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Assert.AreEqual(expected[y,x] == 1,gameOfLive.GameOfLifeMatrix.IsCellAlive(x,y), $"Cell {x},{y}");
                }
            }
        }

        

        [Test]
        public void MatrixContainInitialzationData()
        {
            GameOfLifeMultiArray matrix = new GameOfLifeMultiArray(6,6);

            int[,] blinker = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 } };

            GameOfLife.InitializeGameOfLife(matrix,blinker);


            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Assert.AreEqual(blinker[y,x] ==1,  matrix.IsCellAlive(x,y), $"Test of cell {x},{y}");
                }
            }



        }

        [Test]
        public void GoLDictionaryContainInitialzationData()
        {
            IGameOfLifeMatrix matrix = new GameOfLifeHashSet(6, 6);

            int[,] blinker = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 } };

            GameOfLife.InitializeGameOfLife(matrix, blinker);


            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Assert.AreEqual(blinker[y, x] == 1, matrix.IsCellAlive(x, y), $"Test of cell {x},{y}");
                }
            }



        }
    }
}