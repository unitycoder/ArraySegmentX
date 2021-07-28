# ArraySegmentX
high performance ArraySegment&lt;T> for C#

# Original ArraySegment<T> Bottleneck

When accessing an ArraySegment's content, we often do:
```csharp
int n = segment.Array[segment.Offset + i];
```

Built in ArraySegment<T> has .Array and .Offset as properties, which requires two additional IL calls:


# ArraySegment<T> IL
