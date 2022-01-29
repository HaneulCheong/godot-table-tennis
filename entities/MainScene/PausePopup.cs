using Godot;


namespace Game.MainScene
{
    /// <summary>일시정지를 위한 <c>PausePopup</c> 노드</summary>
    public class PausePopup : Popup, IMatchPointGroup
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>활성화 여부</value>
        private bool Pauseable { get; set; } = true;

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready() => AddToGroup("MatchPointGroup");

        /// <summary>이 노드의 <c>_Process</c> 메서드입니다.</summary>
        public override void _Process(float delta)
        {
            // "ui_pause"로 게임을 일시정지합니다.
            if (Pauseable && Input.IsActionJustPressed("ui_pause"))
            {
                if (GetTree().Paused)
                {
                    Hide();
                    GetTree().Paused = false;
                }
                else
                {
                    PopupCentered();
                    GetTree().Paused = true;
                }

            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 비활성화합니다.</summary>
        public void MatchPoint() => Pauseable = false;

        /// <summary>이 노드를 활성화합니다.</summary>
        public void Reset() => Pauseable = true;
    }
}
