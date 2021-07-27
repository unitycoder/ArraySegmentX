/*
MIT License

Copyright (c) 2021, Michael W. (vis2k)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;

// no namespace. drop it in and use it directly anywhere.

public struct ArraySegmentX<T>
{
    // readonly instead of property to avoid two IL calls each time.
    public readonly T[] Array;
    public readonly int Offset;
    public readonly int Count;

    public ArraySegmentX(T[] array)
    {
        if (array == null) throw new ArgumentNullException("array");

        Array = array;
        Offset = 0;
        Count = array.Length;
    }

    public ArraySegmentX(T[] array, int offset, int count)
    {
        if (array == null) throw new ArgumentNullException("array");
        if (offset < 0) throw new ArgumentOutOfRangeException("offset");
        if (count < 0) throw new ArgumentOutOfRangeException("count");
        if (array.Length - offset < count) throw new ArgumentException("Length - offset needs to be >= count");

        Array = array;
        Offset = offset;
        Count = count;
    }

    // [] indexer for convenience.
    // this requires an IL call though.
    // use segment.Array[segment.Offset + i] directly when performance matters.
    public T this[int index]
    {
        get { return Array[Offset + index]; }
        set { Array[Offset + index] = value; }
    }

    // TODO equals, gethashcode, etc.
}