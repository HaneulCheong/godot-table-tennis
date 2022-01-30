using System;
using Godot;


namespace Game.Internal
{
    /// <summary>C# <c>Random</c> 클래스를 이용하는
    /// 유용한 메서드를 모아놓은 정적 도구상자 클래스</summary>
    public static class RandomTools
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>싱글턴 인스턴스</value>
        private static Random Instance { get; } = new Random();

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>true 또는 false를 무작위로 반환합니다.</summary>
        public static bool Bool() => Instance.Next() % 2 == 0 ? true : false;

        /// <summary>
        /// 0.0부터 <c>maxValue</c>까지의 실수 중
        /// 무작위 실수를 반환합니다.
        /// </summary>
        /// <param name="maxValue">무작위 실수 최대값</param>
        public static float Float(float maxValue)
        {
            return (float)(Instance.NextDouble() * maxValue);
        }

        /// <summary>
        /// <c>minValue</c>부터 <c>maxValue</c>까지의 실수 중
        /// 무작위 실수를 반환합니다.
        /// </summary>
        /// <param name="minValue">무작위 실수 최소값</param>
        /// <param name="maxValue">무작위 실수 최대값</param>
        /// <exception cref="ArgumentException">
        /// <c>maxValue</c>보다 <c>minValue</c>가 큼</exception>
        public static float Float(float minValue, float maxValue)
        {
            if (maxValue < minValue)
            {
                var e = new ArgumentException($"{maxValue} < {minValue}");
                GD.PushError(e.ToString());
                (minValue, maxValue) = (maxValue, minValue);
            }

            float range = maxValue - minValue;
            return (float)(Instance.NextDouble() * range + minValue);
        }

        /// <summary>
        /// 0부터 <c>maxValue</c>까지의 정수 중
        /// 무작위 정수를 반환합니다.
        /// </summary>
        /// <param name="maxValue">무작위 정수 최대값</param>
        public static int Int(int maxValue)
        {
            return Instance.Next(maxValue);
        }

        /// <summary>
        /// <c>minValue</c>부터 <c>maxValue</c>까지의 정수 중
        /// 무작위 정수를 반환합니다.
        /// </summary>
        /// <param name="minValue">무작위 정수 최소값</param>
        /// <param name="maxValue">무작위 정수 최대값</param>
        /// <exception cref="ArgumentException">
        /// <c>maxValue</c>보다 <c>minValue</c>가 큼</exception>
        public static int Int(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                var e = new ArgumentException($"{maxValue} < {minValue}");
                GD.PushError(e.ToString());
                (minValue, maxValue) = (maxValue, minValue);
            }

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
}
