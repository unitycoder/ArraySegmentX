# ArraySegmentX
high performance ArraySegment&lt;T> for C#

# Original ArraySegment<T> Bottleneck

When accessing an ArraySegment's content, we often do:
```csharp
int n = segment.Array[segment.Offset + i];
```
  
Built in ArraySegment<T> has .Array and .Offset as properties.

IL code then needs two extra call instructions:
![image](https://user-images.githubusercontent.com/16416509/127259946-fd44c0ac-34a0-4a73-ac6a-641ec9d92f8b.png)

ArraySegmentX<T> does not need two extra call instructions for the same code:
![image](https://user-images.githubusercontent.com/16416509/127260063-c5567416-80f5-4c40-b50a-dd1b00b88fcb.png)
