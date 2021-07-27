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
                    // IL_000d: ldarg.0      // this
                    // IL_000e: ldfld        unsigned int8[] ArraySegmentX.Tests::bytes
                    // IL_0013: ldloc.0      // offset
                    // IL_0014: ldloc.2      // j
                    // IL_0015: add
                    // IL_0016: ldelem.u1
                    // IL_0017: stloc.3
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
                    // IL_0018: ldloca.s     segment
                    // IL_001a: call         instance !0/*unsigned int8*/[] valuetype [System.Runtime]System.ArraySegment`1<unsigned int8>::get_Array()
                    // IL_001f: ldloca.s     segment
                    // IL_0021: call         instance int32 valuetype [System.Runtime]System.ArraySegment`1<unsigned int8>::get_Offset()
                    // IL_0026: ldloc.2      // j
                    // IL_0027: add
                    // IL_0028: ldelem.u1
                    // IL_0029: stloc.3
                    int n = segment.Array[segment.Offset + j];
                }
            }
        }

        [Test]
        [TestCase(1_000_000)] // 1.4s
        public void ArraySegmentXTest(int iterations)
        {
            // ArraySegment always uses Array[Array.Offset + i].
            // let's use an offset here too for fairness.
            ArraySegmentX<byte> segment = new ArraySegmentX<byte>(bytes);
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < bytes.Length; ++j)
                {
                    // IL_0018: ldloc.0      // segment
                    // IL_0019: ldfld        !0/*unsigned int8*/[] valuetype ArraySegmentX`1<unsigned int8>::Array
                    // IL_001e: ldloc.0      // segment
                    // IL_001f: ldfld        int32 valuetype ArraySegmentX`1<unsigned int8>::Offset
                    // IL_0024: ldloc.2      // j
                    // IL_0025: add
                    // IL_0026: ldelem.u1
                    // IL_0027: stloc.3      // n
                    int n = segment.Array[segment.Offset + j];
                }
            }
        }

        // ArraySegmentX.[] indexer for convenience
        [Test]
        [TestCase(1_000_000)] // 3.6s
        public void ArraySegmentX_Indexer_Test(int iterations)
        {
            // ArraySegment always uses Array[Array.Offset + i].
            // let's use an offset here too for fairness.
            ArraySegmentX<byte> segment = new ArraySegmentX<byte>(bytes);
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < bytes.Length; ++j)
                {
                    int n = segment[j];
                }
            }
        }
    }
}