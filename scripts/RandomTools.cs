using System;
using System.Collections.Generic;


/// <summary>C# 내장 <c>Random</c> 클래스를 이용하는
/// 유용한 메서드를 모아놓은 정적 싱글턴 클래스</summary>
internal static class RandomTools
{
    ////////////////////
    // 속성
    ////////////////////

    private static Random Instance { get; } = new Random();

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>
    /// 0부터 <c>maxValue</c>까지의 정수 중
    /// 무작위 정수를 반환합니다.
    /// </summary>
    /// <param name="maxValue">무작위 정수 최대값</param>
    public static int RandInt(int maxValue)
    {
        return Instance.Next(maxValue);
    }

    /// <summary>
    /// <c>minValue</c>부터 <c>maxValue</c>까지의 정수 중
    /// 무작위 정수를 반환합니다.
    /// </summary>
    /// <param name="minValue">무작위 정수 최소값</param>
    /// <param name="maxValue">무작위 정수 최대값</param>
    public static int RandInt(int minValue, int maxValue)
    {
        return Instance.Next(minValue, maxValue);
    }

    /// <summary>
    /// 주어진 <c>choices</c> 배열의 멤버 중
    /// 하나를 골라 반환합니다.
    /// </summary>
    /// <param name="choices">선택지의 배열</param>
    public static T Choice<T>(T[] choices)
    {
        int index = Instance.Next(choices.Length);
        return choices[index];
    }
}
