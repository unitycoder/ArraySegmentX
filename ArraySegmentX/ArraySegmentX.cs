﻿using System;

public struct ArraySegmentX<T>
{
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

    public readonly T[] Array;
    public readonly int Offset;
    public readonly int Count;

    // TODO equals, gethashcode, etc.
    // TODO []
}