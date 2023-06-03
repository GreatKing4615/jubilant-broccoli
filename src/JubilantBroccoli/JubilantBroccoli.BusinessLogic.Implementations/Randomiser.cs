namespace JubilantBroccoli.BusinessLogic.Implementations;

public static class Randomiser
{
    private static readonly Random Random = new Random();
    private const int MinSeconds = 10000;
    private const int MaxSeconds = 100000;

    public static TimeSpan GetRandomSpan(int min = MinSeconds, int max = MaxSeconds)
    {
        return new TimeSpan(ticks: Random.Next(min, max));
    }

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Next(min, max);
    }

    public static int GetRandomNumber(int limit = Int32.MaxValue)
    {
        return Random.Next(0, limit);
    }
}