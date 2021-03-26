using System;

namespace Master_slave_design_pattern.Utils
{
    public static class TwoDimensionalArrayUtils
    {
        /// <summary>
        /// write row into a 2D array at given index
        /// </summary>
        /// <typeparam name="T">type of elements in the array</typeparam>
        /// <param name="twoDimensionalArray">2D array we want to write the row into</param>
        /// <param name="row">row to write</param>
        /// <param name="rowIndex">index in <paramref name="twoDimensionalArray"/> where the <paramref name="row"/> will be written</param>
        public static void WriteRow<T>(this T[,] twoDimensionalArray, T[] row, int rowIndex)
        {
            int arrayWidth = twoDimensionalArray.GetLength(1);
            if (arrayWidth != row.Length)
            {
                throw new ArgumentException("row length and array width do not match");
            }

            for (int columnIndex = 0; columnIndex < arrayWidth; columnIndex++)
            {
                twoDimensionalArray[rowIndex, columnIndex] = row[columnIndex];
            }
        }
    }
}