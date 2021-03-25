using System;

namespace Master_slave_design_pattern
{
    /// <summary>
    /// abstract matrix calculator with common methods for all matrix calculators
    /// </summary>
    public abstract class MatrixCalculator
    {
        /// <summary>
        /// multiply these matrices
        /// </summary>
        /// <param name="firstMatrix">first argument of the multiplication</param>
        /// <param name="secondMatrix">second argument of the multiplication</param>
        /// <returns>result of the multiplication: <paramref name="firstMatrix"/> x <paramref name="secondMatrix"/></returns>
        public abstract Matrix Multiply(Matrix firstMatrix, Matrix secondMatrix);

        /// <summary>
        /// get scalar product (e.g. multiply row and column vectors and return the result)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        protected int GetScalarProduct(int[] row, int[] column)
        {
            if (row.Length != column.Length)
            {
                throw new ArgumentException("Row and column have different number of items");
            }

            int scalarProduct = 0;
            for (int index = 0; index < row.Length; index++)
            {
                int product = row[index] * column[index];
                scalarProduct += product;
            }
            return scalarProduct;
        }
    }

    public class SimpleMatrixCalculator : MatrixCalculator
    {
        /// <inheritdoc/>
        public override Matrix Multiply(Matrix firstMatrix, Matrix secondMatrix)
        {
            if (firstMatrix.Width != secondMatrix.Height)
            {
                throw new ArgumentException("Dimensions not equal. Width of the first matrix must be equal to the height of the second matrix!");
            }

            var multipliedValues = new int[firstMatrix.Height, secondMatrix.Width];
            for (int rowIndex = 0; rowIndex < multipliedValues.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < multipliedValues.GetLength(1); columnIndex++)
                {
                    var row = firstMatrix.GetRow(rowIndex);
                    var column = secondMatrix.GetColumn(columnIndex);
                    multipliedValues[rowIndex, columnIndex] = GetScalarProduct(row, column);
                }
            }
            return new Matrix(multipliedValues);
        }
    }
}