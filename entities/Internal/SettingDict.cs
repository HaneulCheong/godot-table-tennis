using System.Collections.Generic;


namespace Game.Internal
{
    /// <summary>게임 설정을 담고 있는 딕셔너리</summary>
    public class SettingDict : Dictionary<string, int>
    {
        // TODO: 더 나은 구현 방법 찾아보기
        /// <summary>같은 Key와 Value를 가지고 있는
        /// 딕셔너리를 복제해서 반환합니다.</summary>
        public SettingDict Clone()
        {
            var value = new SettingDict();
            foreach (KeyValuePair<string, int> kvp in this)
            {
                value.Add(kvp.Key, kvp.Value);
            }
            return value;
        }
    }
}
