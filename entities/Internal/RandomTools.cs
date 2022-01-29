using System;


namespace Game.Internal
{
    /// <summary>C# 내장 <c>Random</c> 클래스를 이용하는
    /// 유용한 메서드를 모아놓은 정적 싱글턴 클래스</summary>
    internal static class RandomTools
    {
        ////////////////////
        // 속성
        ////////////////////

        /// 싱글턴 인스턴스
        private static Random Instance { get; } = new Random();

        /// <value>true 또는 false 중 무작위 값</value>
        public static bool RandBool
        {
            get
            {
                if (Instance.Next() % 2 == 0) { return true; }
                else { return false; }
            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>
        /// 0.0부터 <c>maxValue</c>까지의 실수 중
        /// 무작위 실수를 반환합니다.
        /// </summary>
        /// <param name="maxValue">무작위 실수 최대값</param>
        public static float Uniform(float maxValue)
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
        public static float Uniform(float minValue, float maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentException($"{maxValue} < {minValue}");
            }

            float range = maxValue - minValue;
            return (float)(Instance.NextDouble() * range + minValue);
        }

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
        /// <exception cref="ArgumentException">
        /// <c>maxValue</c>보다 <c>minValue</c>가 큼</exception>
        public static int RandInt(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentException($"{maxValue} < {minValue}");
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
