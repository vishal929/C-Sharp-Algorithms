

using DataStructures.Shared;

namespace Greedy.Tests
{
    public class TestHuffmanCoding
    {
        [Fact]
        public void TestCoding()
        {
            string[] symbols =  ["a", "b", "c", "d", "e", "f" ];
            double[] probabilities = [0.05, 0.09, 0.12, 0.13, 0.16, 0.45];

            (IDictionary<string,string> coding, TreeNode<(double,string?)> huffmanTree) = HuffmanCoding.GetEncoding(symbols, probabilities);


            Assert.Equal("1100", coding["a"]);
            Assert.Equal("1101", coding["b"]);
            Assert.Equal("111", coding["e"]);
            Assert.Equal("101", coding["d"]);
            Assert.Equal("100", coding["c"]);
            Assert.Equal("0", coding["f"]);





        }
    }
}
