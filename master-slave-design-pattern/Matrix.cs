using System;
using System.Linq;
using System.Text;

namespace Master_slave_design_pattern
{
    public class Matrix
    {
        private readonly int[,] items;

        public Matrix(int[,] items)
        {
            this.items = items;
            Height = items.GetLength(0);
            Width = items.GetLength(1);
        }

        public int Width { get; }

        public int Height { get; }

        public int[] GetRow(int rowIndex)
        {
            return Enumerable.Range(0, Width)
                .Select(i => items[rowIndex, i])
                .ToArray();
        }

        public int[] GetColumn(int columnIndex)
        {
            return Enumerable.Range(0, Height)
                .Select(i => items[i, columnIndex])
                .ToArray();
        }

        public int GetPosition(int rowIndex, int columnIndex)
        {
            return items[rowIndex, columnIndex];
        }

        public static Matrix GenerateRandom(int height, int width)
        {
            int maximalValue = 10;
            var values = new int[height, width];

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < width; columnIndex++)
                {
                    values[rowIndex, columnIndex] = Program.randomGenerator.Next(maximalValue);
                }
            }

            return new Matrix(values);
        }

        public override string ToString()
        {
            var matrixString = new StringBuilder("");

            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Width; columnIndex++)
                {
                    double value = items[rowIndex, columnIndex];
                    matrixString.Append($" {value,3} ");
                }
                matrixString.Append('\n');
            }

            return matrixString.ToString();
        }
    }
}