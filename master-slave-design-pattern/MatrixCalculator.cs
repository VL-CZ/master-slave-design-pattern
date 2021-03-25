using System;
using System.Linq;

namespace Master_slave_design_pattern
{
    public abstract class MatrixCalculator
    {
        public abstract Matrix Multiply(Matrix firstMatrix, Matrix secondMatrix);

        protected int MultiplyRowWithColumn(int[] row, int[] column)
        {
            if (row.Length != column.Length)
            {
                throw new ArgumentException("Row and column have different number of items");
            }

            return row.Select((rowValue, index) => rowValue * column[index]).Sum();
        }
    }

    public class SimpleMatrixCalculator : MatrixCalculator
    {
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
                    multipliedValues[rowIndex, columnIndex] = MultiplyRowWithColumn(row, column);
                }
            }
            return new Matrix(multipliedValues);
        }
    }
}