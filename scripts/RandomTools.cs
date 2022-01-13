using System;


/// <summary>C# 내장 <c>Random</c> 클래스를 이용하는
/// 유용한 메서드를 모아놓은 정적 클래스</summary>
internal static class RandomTools
{
    ////////////////////
    // 속성
    ////////////////////

    private static Random RNG { get; } = new Random();

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>주어진 <c>choices</c> 배열의 멤버 중 하나를 골라 반환합니다.</summary>
    /// <param name="choices">선택지의 배열</param>
    public static T Choice<T>(T[] choices)
    {
        int index = RNG.Next(choices.Length);
        return choices[index];
    }
}
