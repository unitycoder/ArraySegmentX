using System;
using NUnit.Framework;

namespace ArraySegmentX
{
    public class Tests
    {
        byte[] bytes = new byte[1000];

        [Test]
        [TestCase(1_000_000)] // 1.5s
        public void ByteArrayTest(int iterations)
        {
            // ArraySegment always uses Array[Array.Offset + i].
            // let's use an offset here too for fairness.
            int offset = 0;
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < bytes.Length; ++j)
                {
                    int n = bytes[offset + j];
                }
            }
        }

        [Test]
        [TestCase(1_000_000)] // 3.6s
        public void ArraySegmentTest(int iterations)
        {
            // ArraySegment always uses Array[Array.Offset + i].
            // let's use an offset here too for fairness.
            ArraySegment<byte> segment = new ArraySegment<byte>(bytes);
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < bytes.Length; ++j)
                {
                    int n = segment.Array[segment.Offset + j];
                }
            }
        }

        [Test]
        [TestCase(1_000_000)] // 5.2s
        public void ArraySegmentXTest(int iterations)
        {
            // ArraySegment always uses Array[Array.Offset + i].
            // let's use an offset here too for fairness.
            ArraySegmentX<byte> segment = new ArraySegmentX<byte>(bytes);
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < bytes.Length; ++j)
                {
                    int n = segment.Array[segment.Offset + j];
                }
            }
        }
    }
}