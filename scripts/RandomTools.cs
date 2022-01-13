using System;


internal class RandomTools
{
    private static readonly Random _random = new Random();

    public static T Choice<T>(T[] array)
    {
        int index = _random.Next(array.Length);
        return array[index];
    }
}
