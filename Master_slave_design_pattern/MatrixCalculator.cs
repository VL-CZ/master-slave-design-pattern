using Master_slave_design_pattern.Utils;
using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// multiply row with the matrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="matrix"></param>
        /// <returns>product of multiplication <paramref name="row"/> x <paramref name="matrix"/></returns>
        protected int[] MultiplyRowWithMatrix(int[] row, Matrix matrix)
        {
            if (row.Length != matrix.Height)
            {
                throw new ArgumentException("Row and column have different number of items");
            }

            int[] productsRow = new int[matrix.Width];
            for (int columnIndex = 0; columnIndex < matrix.Width; columnIndex++)
            {
                var column = matrix.GetColumn(columnIndex);
                productsRow[columnIndex] = GetScalarProduct(row, column);
            }
            return productsRow;
        }
    }

    /// <summary>
    /// simple algorithm for matrix multiplication
    /// </summary>
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
                var row = firstMatrix.GetRow(rowIndex);
                var productRow = MultiplyRowWithMatrix(row, secondMatrix);
                multipliedValues.WriteRow(productRow, rowIndex);
            }
            return new Matrix(multipliedValues);
        }
    }

    /// <summary>
    /// parallel algorithm for matrix multiplication (using master-slave design pattern)
    /// </summary>
    public class ParallelMatrixCalculator : MatrixCalculator
    {
        /// <inheritdoc/>
        public override Matrix Multiply(Matrix firstMatrix, Matrix secondMatrix)
        {
            var multipliedValues = new int[firstMatrix.Height, secondMatrix.Width];

            // allocate the slaves
            var tasks = new Task<int[]>[firstMatrix.Height];

            // assign the sub-problems to slaves and run them
            for (int rowIndex = 0; rowIndex < multipliedValues.GetLength(0); rowIndex++)
            {
                var row = firstMatrix.GetRow(rowIndex);
                tasks[rowIndex] = Task.Run(() => MultiplyRowWithMatrix(row, secondMatrix));
            }

            // wait until all the slaves finish
            Task.WaitAll(tasks);

            // get the results from slaves and write it into the result
            for (int index = 0; index < tasks.Length; index++)
            {
                int[] taskResult = tasks[index].Result;
                multipliedValues.WriteRow(taskResult, index);
            }

            return new Matrix(multipliedValues);
        }
    }
}