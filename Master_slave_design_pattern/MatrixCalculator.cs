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
        /// multiply one row with the matrix
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
        // we don't use master-slave design pattern here
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
                for (int columnIndex = 0; columnIndex < multipliedValues.GetLength(1); columnIndex++)
                {
                    var column = secondMatrix.GetColumn(columnIndex);
                    multipliedValues[rowIndex, columnIndex] = GetScalarProduct(row, column);
                }
            }
            return new Matrix(multipliedValues);
        }
    }

    /// <summary>
    /// parallel algorithm for matrix multiplication (using master-slave design pattern)
    /// </summary>
    public class ParallelMatrixCalculator : MatrixCalculator
    {
        // this method acts as a MASTER
        /// <inheritdoc/>
        public override Matrix Multiply(Matrix firstMatrix, Matrix secondMatrix)
        {
            var multipliedValues = new int[firstMatrix.Height, secondMatrix.Width];

            // allocate the slaves
            var slaves = new Task<int[]>[firstMatrix.Height];

            // assign the sub-problems to slaves and run them
            for (int rowIndex = 0; rowIndex < multipliedValues.GetLength(0); rowIndex++)
            {
                var row = firstMatrix.GetRow(rowIndex);

                // slaves compute sub-tasks - product of one row with the second matrix
                // arguments are passed as parameters
                // return value is stored within the Task instance
                slaves[rowIndex] = new Task<int[]>(() => MultiplyRowWithMatrix(row, secondMatrix));
            }

            /// start all the slaves
            foreach (var slave in slaves)
            {
                slave.Start();
            }

            // wait until all the slaves finish
            Task.WaitAll(slaves);

            // get the results from slaves (rows of matrix multiplication) and create the product matrix
            for (int index = 0; index < slaves.Length; index++)
            {
                int[] taskResult = slaves[index].Result;
                multipliedValues.WriteRow(taskResult, index);
            }

            return new Matrix(multipliedValues);
        }
    }
}