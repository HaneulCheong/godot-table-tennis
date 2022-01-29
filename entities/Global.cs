using Godot;


namespace Game
{
    public class Global : Node
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>경기를 이기기 위해 필요한 점수, 즉 매치 포인트</value>
        [Export]
        public int MatchPoint { get; private set; } = 11;

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Process(float delta)
        {
            // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
            if (Input.IsActionJustPressed("ui_fullscreen"))
            {
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
        }
    }
}
