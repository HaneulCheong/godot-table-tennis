using Godot;


namespace Game
{
    /// <summary>게임 광역 노드</summary>
    public class Global : Node
    {
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
