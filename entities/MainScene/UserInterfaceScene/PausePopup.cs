using Godot;


namespace Game.MainScene
{
    using Shared;

    /// <summary>일시정지 팝업</summary>
    public class PausePopup : Popup, IMatchPointGroup
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>활성화 여부. 참일 경우 팝업시킬 수 있는 상황.</value>
        private bool Pausable { get; set; } = true;

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");

            // 드러낼 때와 숨길 때의 행동 정의
            Connect("popup_hide", this, nameof(OnHide));
            Connect("about_to_show", this, nameof(OnAboutToShow));

            // 재개 버튼 연결
            var buttonContainer = GetNode<ButtonContainer>(
                "VBoxContainer/ButtonContainer"
            );
            buttonContainer.GetNode<TextButton>("ResumeButton").Connect(
                "pressed", this, nameof(OnResumeButtonPressed)
            );

            Reset();
        }

        /// <summary><c>Pauseable</c>이 참인 상태에서
        /// "ui_pause"나 "ui_cancel"에 반응합니다.</summary>
        public override void _Input(InputEvent @event)
        {
            if (!Pausable) { return; }

            if (@event.IsActionPressed("ui_pause"))
            {
                if (!GetTree().Paused) { PopupCentered(); }
                else { Hide(); }
            }
            else if (@event.IsActionPressed("ui_cancel"))
            {
                Hide();
            }
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>팝업 시 게임을 일시정지합니다.</summary>
        private void OnAboutToShow() => GetTree().Paused = true;

        /// <summary>숨길 시 게임을 재개합니다.</summary>
        private void OnHide() => GetTree().Paused = false;

        /// <summary>재개 버튼을 누를 시 이 팝업을 숨깁니다.</summary>
        private void OnResumeButtonPressed() => Hide();

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 비활성화합니다.</summary>
        public void MatchPoint() => Pausable = false;

        /// <summary>이 노드를 활성화합니다.</summary>
        public void Reset() => Pausable = true;
    }
}
