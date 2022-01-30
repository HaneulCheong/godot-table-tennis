using Godot;


namespace Game.MainMenuScene
{
    using Shared;

    /// <summary>게임 종료 버튼</summary>
    public class QuitButton : TextButton
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>눌렀을 때 게임을 종료합니다.</summary>
        public override void _Pressed()
        {
            OS.ExitCode = 0;
            GetTree().Quit();
        }
    }
}
