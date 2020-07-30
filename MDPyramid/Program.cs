using System;
using System.IO;

namespace MDPyramid
{
    public class Program
    {
        private const int Zero = 0;
        private const int One = 1;
        private const int Two = 2;

        static void Main()
        {
            var inputFilePath = "input.txt";
            Console.WriteLine(GetMaxSumFromArray(ReadInputArray(inputFilePath)));
        }

        /// <summary>
        /// Returns the maximum sum possible
        /// </summary>
        /// <param name="leftTriangleArray"></param>
        /// <returns></returns>
        public static int GetMaxSumFromArray(int[][] leftTriangleArray)
        {
            int size = leftTriangleArray.Length - One;
            int[][] result = new int[size + One][];
            for (int i = 0; i < leftTriangleArray.Length; i++)
            {
                result[i] = new int[leftTriangleArray.Length];
            }

            //start from the bottom..move up
            for (var row = size - One; row >= Zero; row--)
            {
                UpdateSum(leftTriangleArray, result, row);
            }
            return result[Zero][Zero];
        }

        /// <summary>
        /// Checks if the number is even or odd
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsEvenNumber(int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Updates sum of columns in a row
        /// </summary>
        /// <param name="leftTriangleArray"></param>
        /// <param name="result"></param>
        /// <param name="row"></param>
        private static void UpdateSum(int[][] leftTriangleArray, int[][] result, int row)
        {
            for (int column = Zero; column <= row; column++)
            {
                var currentNumber = leftTriangleArray[row][column];
                var isEvenNumber = IsEvenNumber(currentNumber);

                // finding the left child number from the current
                int leftChild = (IsEvenNumber(leftTriangleArray[row + One][column]) == !isEvenNumber ? leftTriangleArray[row + One][column] : Zero);

                // finding the right child number from the current
                int rightChild = (IsEvenNumber(leftTriangleArray[row + One][column + One]) == !isEvenNumber ? leftTriangleArray[row + One][column + One] : Zero);

                // compare and take the max number
                int nextNode = GetValidChild(leftChild, rightChild);

                //if there is no way to go forward, use current number
                if (nextNode == 0)
                {
                    result[row][column] = currentNumber;
                }
                //if we are at the leaf nodes, then result array will be empty
                else if (row == leftTriangleArray.Length - Two)
                {
                    result[row][column] = currentNumber + (leftChild == nextNode ? leftTriangleArray[row + One][column] : leftTriangleArray[row + One][column + 1]);
                }
                else
                {
                    result[row][column] = currentNumber + (leftChild == nextNode ? result[row + One][column] : result[row + One][column + One]);
                }
            }
        }

        /// <summary>
        /// Returns the maximum of two numbers
        /// </summary>
        /// <param name="leftChild"></param>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        private static int GetValidChild(int leftChild, int rightChild)
        {
            if (leftChild != Zero && rightChild != Zero)
            {
                return leftChild > rightChild ? leftChild : rightChild;
            }
            else if (leftChild != Zero)
            {
                return leftChild;
            }
            else
            {
                return rightChild;
            }
        }

        /// <summary>
        /// Reads and converts the input to an array
        /// </summary>
        /// <returns></returns>
        private static int[][] ReadInputArray(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var array = new int[lines.Length][];
            var row = 0;

            foreach (var line in lines)
            {
                var column = 0;
                var values = GetArrayFromLine(line);
                array[row] = new int[values.Length];
                foreach (var value in values)
                {
                    array[row][column] = value;
                    column++;
                }
                row++;
            }

            return array;
        }

        /// <summary>
        /// Converts line to an array of numbers
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static int[] GetArrayFromLine(string line)
        {
            var arr = line.Trim().Split(" ");
            var ints = Array.ConvertAll(arr, input =>
            {
                return Int32.Parse(input);
            });

            return ints;
        }
    }
}
