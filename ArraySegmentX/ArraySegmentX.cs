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

// TODO link source and description above file
public readonly struct ArraySegmentX<T>
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
        // TODO make sure it's within count.
        // don't allow accesing outside of segment.
        get { return Array[Offset + index]; }
        set { Array[Offset + index] = value; }
    }

    // GetHashCode from netcore ArraySegment<T>
    public override int GetHashCode() =>
        Array is null ? 0 : HashCode.Combine(Offset, Count, Array.GetHashCode());

    // Equals from netcore ArraySegment<T>
    public override bool Equals(object? obj) =>
        obj is ArraySegmentX<T> && Equals((ArraySegmentX<T>)obj);

    public bool Equals(ArraySegmentX<T> obj) =>
        obj.Array == Array && obj.Offset == Offset && obj.Count == Count;

    // operators
    public static bool operator ==(ArraySegmentX<T> a, ArraySegmentX<T> b) => a.Equals(b);
    public static bool operator !=(ArraySegmentX<T> a, ArraySegmentX<T> b) => !(a == b);

    // implicit conversions to / from original ArraySegment for ease of use
    public static implicit operator ArraySegment<T>(ArraySegmentX<T> segment) =>
        new ArraySegment<T>(segment.Array, segment.Offset, segment.Count);

    public static implicit operator ArraySegmentX<T>(ArraySegment<T> segment) =>
        new ArraySegmentX<T>(segment.Array, segment.Offset, segment.Count);
}