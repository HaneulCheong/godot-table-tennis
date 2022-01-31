using Godot;


namespace Game.SettingsScene.SpinBoxContainerScene
{
    /// <summary>라벨과 그 문자열로 구현한 스핀박스입니다.</summary>
    [Tool]
    public class SpinBoxLabel : Label
    {
        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>Value</c>의 내부 필드</summary>
        private int _value = 69;
        /// <summary><c>Formatted</c>의 내부 필드</summary>
        private bool _formatted = false;

        ////////////////////
        // 속성
        ////////////////////

        /// <value>Focus 되었을 때 <c>Value</c> 앞에 표시할 문자열</value>
        [Export]
        private string Prefix { get; set; } = "<=- [";

        /// <value>Focus 되었을 때 <c>Value</c> 뒤에 표시할 문자열</value>
        [Export]
        private string Suffix { get; set; } = "] -=>";

        /// <value>Text에 넣을 값</value>
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                Text = GetNewText(Formatted, Value);
            }
        }

        /// <value><c>Text</c>의 꾸밈 여부</value>
        public bool Formatted
        {
            get => _formatted;
            set
            {
                _formatted = value;
                Text = GetNewText(Formatted, Value);
            }
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>Prefix와 Suffix의 길이를 맞춥니다.</summary>
        public override void _Ready()
        {
            // Prefix의 길이를 맞춤
            if (Prefix.Length > Suffix.Length)
            {
                Suffix = Suffix + new string(
                    ' ', Prefix.Length - Suffix.Length
                );
            }
            // Suffix의 길이를 맞춤
            else if (Prefix.Length < Suffix.Length)
            {
                Prefix = new string(
                    ' ', Suffix.Length - Prefix.Length
                ) + Prefix;
            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>현재 상태에 따라 출력할 텍스트를 반환합니다.</summary>
        /// <param name="formatted">꾸밈 여부</param>
        /// <param name="value">출력할 값</param>
        private string GetNewText(bool formatted, int value)
        {
            return formatted ? $"{Prefix} {value} {Suffix}" : $"{value}";
        }
    }
}
