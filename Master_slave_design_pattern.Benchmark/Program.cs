using System;
using System.Diagnostics;

namespace Master_slave_design_pattern.Benchmark
{
    class Program
    {
        /// <summary>
        /// get average computation times of both algorithms
        /// </summary>
        /// <param name="matrixDimension">dimension of matrices to generate</param>
        /// <param name="repeats">how many times to repeat the algorithms</param>
        /// <returns></returns>
        static ElapsedCalculationTimesDto GetAverageComputationTime(int matrixDimension, int repeats)
        {
            var stopwatch = new Stopwatch();
            long simpleAlgoElapsedMs = 0;
            long parallelAlgoElapsedMs = 0;
            var simpleMatrixCalculator = new SimpleMatrixCalculator();
            var parallelMatrixCalculator = new ParallelMatrixCalculator();
            var averageElapsedTimes = new ElapsedCalculationTimesDto();

            for (int i = 0; i < repeats; i++)
            {
                Matrix matrix1 = Matrix.GenerateRandom(matrixDimension, matrixDimension);
                Matrix matrix2 = Matrix.GenerateRandom(matrixDimension, matrixDimension);

                // run simple algo
                stopwatch.Restart();
                Matrix simpleAlgoResult = simpleMatrixCalculator.Multiply(matrix1, matrix2);
                stopwatch.Stop();
                simpleAlgoElapsedMs += stopwatch.ElapsedMilliseconds;

                // run parallel (master-slave) algo
                stopwatch.Restart();
                Matrix parallelAlgoResult = parallelMatrixCalculator.Multiply(matrix1, matrix2);
                stopwatch.Stop();
                parallelAlgoElapsedMs += stopwatch.ElapsedMilliseconds;
            }

            averageElapsedTimes.SimpleAlgoAverageTime = simpleAlgoElapsedMs / repeats;
            averageElapsedTimes.ParallelAlgoAverageTime = parallelAlgoElapsedMs / repeats;
            return averageElapsedTimes;
        }

        static void Main(string[] args)
        {
            /// dimension of matrices (for simplicity, we consider only square matrices)
            int matrixDimension = 300;

            // how many times do we repeat the measurement?
            int repeats = 10;

            // start the benchmark and get the result
            var averageComputationTimes = GetAverageComputationTime(matrixDimension, repeats);

            Console.WriteLine($"Simple calculator: {averageComputationTimes.SimpleAlgoAverageTime} ms");
            Console.WriteLine($"Parallel calculator: {averageComputationTimes.ParallelAlgoAverageTime} ms");
        }
    }

    struct ElapsedCalculationTimesDto
    {
        public long SimpleAlgoAverageTime { get; set; }
        public long ParallelAlgoAverageTime { get; set; }
    }

}
