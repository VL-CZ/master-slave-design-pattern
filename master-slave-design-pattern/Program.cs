using System;

namespace Master_slave_design_pattern
{
    class Program
    {
        public static readonly Random randomGenerator = new Random();

        private static void Main(string[] args)
        {
            // setup
            int matrixDimension = 3;

            //MatrixCalculator matrixCalculator = new SimpleMatrixCalculator();
            MatrixCalculator matrixCalculator = new ParallelMatrixCalculator();

            var matrix1 = Matrix.GenerateRandom(matrixDimension, matrixDimension);
            var matrix2 = Matrix.GenerateRandom(matrixDimension, matrixDimension);

            // compute the result
            var result = matrixCalculator.Multiply(matrix1, matrix2);

            // print
            Console.Write(matrix1);
            Console.WriteLine("multiplied by");
            Console.Write(matrix2);
            Console.WriteLine("equals");
            Console.Write(result);
        }
    }
}