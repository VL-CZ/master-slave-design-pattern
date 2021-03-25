using System;
using System.Diagnostics;

namespace Master_slave_design_pattern
{
    class Program
    {
        public static readonly Random randomGenerator = new Random();

        static void Main(string[] args)
        {
            int dimension = 1000;
            Matrix matrix1 = Matrix.GenerateRandom(dimension, dimension);
            Matrix matrix2 = Matrix.GenerateRandom(dimension, dimension);

            MatrixCalculator matrixCalculator = new SimpleMatrixCalculator();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = matrixCalculator.Multiply(matrix1, matrix2);
            stopwatch.Stop();

            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");

            //// print the result
            //Console.Write(matrix1);
            //Console.WriteLine("multiplied by");
            //Console.Write(matrix2);
            //Console.WriteLine("equals");
            //Console.Write(result);
        }
    }
}
