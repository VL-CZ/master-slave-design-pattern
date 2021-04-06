using System.Linq;
using System.Text;

namespace Master_slave_design_pattern
{
    /// <summary>
    /// class representing a matrix
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// content of the matrix
        /// </summary>
        private readonly int[,] items;

        public Matrix(int[,] items)
        {
            this.items = items;
            Height = items.GetLength(0);
            Width = items.GetLength(1);
        }

        /// <summary>
        /// number of columns of this matrix
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// number of rows of this matrix
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// get row by its index
        /// </summary>
        /// <param name="rowIndex">index of the row we want to get</param>
        /// <returns></returns>
        public int[] GetRow(int rowIndex)
        {
            return Enumerable.Range(0, Width)
                .Select(i => items[rowIndex, i])
                .ToArray();
        }

        /// <summary>
        /// get column by its index
        /// </summary>
        /// <param name="columnIndex">index of the column we want to get</param>
        /// <returns></returns>
        public int[] GetColumn(int columnIndex)
        {
            return Enumerable.Range(0, Height)
                .Select(i => items[i, columnIndex])
                .ToArray();
        }

        /// <summary>
        /// get matrix element at the given position
        /// </summary>
        /// <param name="rowIndex">row index of the position</param>
        /// <param name="columnIndex">column index of the position</param>
        /// <returns></returns>
        public int GetPosition(int rowIndex, int columnIndex)
        {
            return items[rowIndex, columnIndex];
        }

        /// <summary>
        /// generate random matrix with given <paramref name="height"/> and <paramref name="width"/>
        /// </summary>
        /// <param name="height">height of the matrix to create</param>
        /// <param name="width">width of the matrix to create</param>
        /// <param name="minimalValue">minimal inclusive value of the matrix item (default 0)</param>
        /// <param name="minimalValue">maximal exclusive value of the matrix item (default 2)</param>
        /// <returns></returns>
        public static Matrix GenerateRandom(int height, int width, int minimalValue = 0, int maximalValue = 2)
        {
            var values = new int[height, width];

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < width; columnIndex++)
                {
                    values[rowIndex, columnIndex] = Program.randomGenerator.Next(minimalValue, maximalValue);
                }
            }

            return new Matrix(values);
        }

        /// <summary>
        /// get string representation of the matrix
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var matrixString = new StringBuilder();

            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Width; columnIndex++)
                {
                    double value = items[rowIndex, columnIndex];
                    matrixString.Append($" {value,3} ");
                }
                matrixString.Append('\n');
            }

            return matrixString.ToString();
        }
    }
}