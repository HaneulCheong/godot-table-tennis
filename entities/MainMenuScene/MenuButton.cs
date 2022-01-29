using Godot;


namespace Game.MainMenuScene
{
    /// <summary>메인 메뉴 버튼 노드</summary>
    public class MenuButton : Button
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>버튼의 문자열</value>
        [Export]
        private string RawText { get; set; }

        /// <value>눌렸을 때 진입할 씬</value>
        [Export]
        private PackedScene Scene { get; set; } = null;

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            Connect("focus_entered", this, nameof(OnFocusChange));
            Connect("focus_exited", this, nameof(OnFocusChange));
        }

        public override void _Pressed()
        {
            // 지정된 씬이 없으면 게임 종료로 간주
            if (Scene == null) { GetTree().Quit(); }
            // 씬이 지정되어있으면 해당 씬에 진입
            GetTree().ChangeSceneTo(Scene);
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>Focus 여부에 따라 '> RawText <' 형태로
        /// 현재 선택되었음을 나타냅니다.</summary>
        private void OnFocusChange()
        {
            Text = HasFocus() ? $"> {RawText} <" : RawText;
        }
    }
}
