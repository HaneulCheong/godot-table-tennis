using Godot;


namespace Game.Shared
{
    /// <summary>메인 메뉴 버튼 노드</summary>
    public class TextButton : Button
    {
        /// <value>Focus 되었을 때 <c>RawText</c> 앞에 표시할 문자열</value>
        [Export]
        protected string Prefix { get; set; } = ">";

        /// <value>Focus 되었을 때 <c>RawText</c> 뒤에 표시할 문자열</value>
        [Export]
        protected string Suffix { get; set; } = "<";

        /// <value>버튼의 문자열</value>
        protected string RawText { get; set; }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            RawText = Text;
            // Prefix와 Suffix의 길이를 맞춤
            if (Prefix.Length > Suffix.Length)
            {
                Suffix = Suffix + new string(
                    ' ', Prefix.Length - Suffix.Length
                );
            }
            else if (Prefix.Length < Suffix.Length)
            {
                Prefix = new string(
                    ' ', Suffix.Length - Prefix.Length
                ) + Prefix;
            }

            Connect("focus_entered", this, nameof(OnFocusChange));
            Connect("focus_exited", this, nameof(OnFocusChange));
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>Focus 여부에 따라 출력 텍스트를 꾸밉니다.</summary>
        private void OnFocusChange()
        {
            Text = HasFocus() ? $"{Prefix} {RawText} {Suffix}" : RawText;
        }
    }
}
