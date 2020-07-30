using Xunit;

namespace MDPyramid.Tests
{
    public class MdPyramidTests
    {
        [Fact]
        public void TestCompletePyarmid()
        {
            var inputArray = new int[4][];
            inputArray[0] = new int[] { 1 };
            inputArray[1] = new int[] { 8, 9 };
            inputArray[2] = new int[] { 1, 5, 9 };
            inputArray[3] = new int[] { 4, 5, 2, 3 };

            var sum = Program.GetMaxSumFromArray(inputArray);

            Assert.Equal(16, sum);
        }

        [Fact]
        public void TestPartialPyarmid()
        {
            var inputArray = new int[4][];
            inputArray[0] = new int[] { 2 };
            inputArray[1] = new int[] { 8, 9 };
            inputArray[2] = new int[] { 1, 5, 9 };
            inputArray[3] = new int[] { 4, 5, 2, 3 };

            var sum = Program.GetMaxSumFromArray(inputArray);

            Assert.Equal(11, sum);
        }
    }
}
