using System;
using System.Diagnostics;

namespace Master_slave_design_pattern
{
    class Program
    {
        public static readonly Random randomGenerator = new Random();

        static long GetAverageComputationTime(int matrixDimension, MatrixCalculator matrixCalculator, int repeats)
        {
            var stopwatch = new Stopwatch();
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

            return totalElapsedMs / repeats;
        }

        static void Main(string[] args)
        {
            int matrixDimension = 50;
            int repeats = 10;

            long simpleCalculatorTime = GetAverageComputationTime(matrixDimension, new SimpleMatrixCalculator(), repeats);
            Console.WriteLine($"Simple calculator: {simpleCalculatorTime} ms");

            long parallelCalculatorTime = GetAverageComputationTime(matrixDimension, new ParallelMatrixCalculator(), repeats);
            Console.WriteLine($"Simple calculator: {parallelCalculatorTime} ms");
        }
    }
}