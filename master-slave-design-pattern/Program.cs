using System;

namespace Master_slave_design_pattern
{
    class Program
    {
        public static readonly Random randomGenerator = new Random();

        static void Main(string[] args)
        {
            int dimension = 5;
            Matrix matrix1 = Matrix.GenerateRandom(dimension, dimension);
            Matrix matrix2 = Matrix.GenerateRandom(dimension, dimension);

            MatrixCalculator matrixCalculator = new SimpleMatrixCalculator();

            var result = matrixCalculator.Multiply(matrix1, matrix2);
            Console.WriteLine(result);
        }
    }
}
