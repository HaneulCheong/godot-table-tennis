using Godot;


namespace Game.SettingsScene
{
    using Shared;

    /// <summary>게임 설정을 바꾸는 창입니다.</summary>
    public class Settings : VBoxContainer
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            GetNode<SceneChangeButton>(
                "ButtonContainer/CancelButton"
            ).GrabFocus();
        }
    }
}
