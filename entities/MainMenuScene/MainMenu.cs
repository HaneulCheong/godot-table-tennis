using Godot;


namespace Game.MainMenuScene
{
    using Shared;

    /// <summary>메인 메뉴 창</summary>
    public class MainMenu : VBoxContainer
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>게임 상태를 초기화합니다.</summary>
        public override void _Ready()
        {
            Global.GameState.Clear();

            GetNode<SceneChangeButton>(
                "ButtonContainer/OnePlayerButton"
            ).Connect("pressed", this, nameof(OnOnePlayerButtonPressed));
            GetNode<SceneChangeButton>(
                "ButtonContainer/TwoPlayerButton"
            ).Connect("pressed", this, nameof(OnTwoPlayerButtonPressed));
            GetNode<TextButton>(
                "ButtonContainer/QuitButton"
            ).Connect("pressed", this, nameof(OnQuitButtonPressed));
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>게임을 싱글플레이어 모드로 설정합니다.</summary>
        private void OnOnePlayerButtonPressed()
        {
            Global.GameState.Add(GameStateType.GameMode, GameMode.OnePlayer);
        }

        /// <summary>게임을 멀티플레이어 모드로 설정합니다.</summary>
        private void OnTwoPlayerButtonPressed()
        {
            Global.GameState.Add(GameStateType.GameMode, GameMode.TwoPlayer);
        }

        /// <summary>게임을 정상적으로 종료합니다.</summary>
        private void OnQuitButtonPressed()
        {
            OS.ExitCode = 0;
            GetTree().Quit();
        }
    }
}
