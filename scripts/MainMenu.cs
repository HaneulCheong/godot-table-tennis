using Godot;


public class MainMenu : Control
{
    /// <summary>이 노드의 <c>_Process</c> 메서드입니다.</summary>
    public override void _Process(float delta)
    {
        // "ui_exit"로 게임 종료
        if (Input.IsActionJustPressed("ui_exit"))
        {
            GetTree().Quit();
        }

        // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
        if (Input.IsActionJustPressed("ui_fullscreen"))
        {
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
    }
}
