# ArraySegmentX
High performance ArraySegment&lt;T> for C#.

## The Bottleneck

Consider the following code for built in ArraySegment<T>.
```csharp
int n = segment.Array[segment.Offset + i];
```

IL generates two call instructions because .Array and .Offset are properties:
![image](https://user-images.githubusercontent.com/16416509/127259946-fd44c0ac-34a0-4a73-ac6a-641ec9d92f8b.png)
  
ArraySegmentX<T> does not need two extra call instructions:
![image](https://user-images.githubusercontent.com/16416509/127260063-c5567416-80f5-4c40-b50a-dd1b00b88fcb.png)

This is a micro optimization.
In most cases, it doesn't matter.
For heavy number crunching like in [Mirror](https://github.com/vis2k/Mirror/)'s delta compression, it does matter.
  
## Benchmarks
Very simple benchmarks are included in Tests.cs:
<img width="365" alt="2021-07-28_11-45-22@2x" src="https://user-images.githubusercontent.com/16416509/127260325-6025f444-9b80-4b18-97df-b2f8be6cfd24.png">

Mirror's delta compression algorithm is dramatically faster:
<img width="367" alt="2021-07-28_11-51-15@2x" src="https://user-images.githubusercontent.com/16416509/127260747-fff70deb-64fe-46af-8fd0-9f5ceaf2dc5b.png">
