using System;
using System.Diagnostics;

namespace Master_slave_design_pattern.Benchmark
{
    class Program
    {
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
            }

            return totalElapsedMs / repeats;
        }
        static void Main(string[] args)
        {
            int matrixDimension = 200;
            int repeats = 10;

            long simpleCalculatorTime = GetAverageComputationTime(matrixDimension, new SimpleMatrixCalculator(), repeats);
            Console.WriteLine($"Simple calculator: {simpleCalculatorTime} ms");

            long parallelCalculatorTime = GetAverageComputationTime(matrixDimension, new ParallelMatrixCalculator(), repeats);
            Console.WriteLine($"Parallel calculator: {parallelCalculatorTime} ms");
        }
    }
}
