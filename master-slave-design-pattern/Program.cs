using System;
using System.Diagnostics;

namespace Master_slave_design_pattern
{
    class Program
    {
        public static readonly Random randomGenerator = new Random();

        static void Main(string[] args)
        {
            int matrixDimension = 1000;
            MatrixCalculator matrixCalculator = new SimpleMatrixCalculator();
            var stopwatch = new Stopwatch();
            int repeats = 10;
            long totalElapsedMs = 0;

            for (int i = 0; i < repeats; i++)
            {
                Matrix matrix1 = Matrix.GenerateRandom(matrixDimension, matrixDimension);
                Matrix matrix2 = Matrix.GenerateRandom(matrixDimension, matrixDimension);

                stopwatch.Restart();
                Matrix result = matrixCalculator.Multiply(matrix1, matrix2);
                stopwatch.Stop();
                totalElapsedMs += stopwatch.ElapsedMilliseconds;

                //// print the result
                //Console.Write(matrix1);
                //Console.WriteLine("multiplied by");
                //Console.Write(matrix2);
                //Console.WriteLine("equals");
                //Console.Write(result);
            }

            Console.WriteLine($"Average: {totalElapsedMs / repeats} ms");
        }
    }
}