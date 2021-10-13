using BenchmarkDotNet.Attributes;

namespace SpaceLib.Benchmarks;

public class TestBenchmark
{
    byte[] data = new byte[1024];

    public TestBenchmark()
    {
        Random.Shared.NextBytes(data);
    }

    [Benchmark]
    public void SumRandom()
    {
        int sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i];
        }

    }
}