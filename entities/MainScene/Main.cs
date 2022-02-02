using Godot;


namespace Game.MainScene
{
    using BallScene;
    using PaddleScene;
    using UserInterfaceScene;

    /// <summary>경기 전체를 관리하는 노드</summary>
    public class Main : Node
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <summary>인공지능 라켓의 스크립트</summary>
        [Export]
        private Script AIPaddleScript { get; set; }

        /// <value>경기를 이기기 위해 필요한 점수, 즉 매치 포인트</value>
        public int MatchPoint { get; } = Global.Settings["Match_Point"];

        /// <value>점수판 레이어 노드</value>
        private UserInterface ScoreBoardLayer
        {
            get => GetNode<UserInterface>("UserInterface");
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>게임을 초기화합니다.</summary>
        public override void _Ready()
        {
            // 싱글플레이어 모드면 2번 플레이어를 AI로 설정
            if (Global.GameState[GameStateType.GameMode] is GameMode.OnePlayer)
            {
                GetNode<Paddle>("PlayerTwo").SetScript(AIPaddleScript);
                GetNode<Paddle>("PlayerTwo").Notification(NotificationReady);
            }

            GetNode<Timer>("NextGameTimer").Connect(
                "timeout", this, nameof(OnNextGameTimerTimeout)
            );
            GetNode<Ball>("Ball").Connect(
                nameof(Ball.Goal), this, nameof(OnGoal)
            );
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary><c>공 노드의 <c>Goal</c>
        /// 신호에 의해 의해 호출됩니다.</summary>
        private void OnGoal(PlayerNumber playerNumber)
        {
            ScoreBoardLayer.Scored(playerNumber);
            GetNode<AudioStreamPlayer>("Scored").Play();

            // 매치 포인트일 경우:
            if (
                ScoreBoardLayer.PlayerOneScore >= MatchPoint
                || ScoreBoardLayer.PlayerTwoScore >= MatchPoint
            )
            {
                GetTree().CallGroup(
                    "MatchPointGroup", nameof(IMatchPointGroup.MatchPoint)
                );
                return;
            }
            else
            {
                GetNode<Timer>("NextGameTimer").Start();
            }
        }

        /// <summary><c>NextGameTimer</c> 노드의
        /// "timeout" 신호에 의해 호출됩니다.</summary>
        private void OnNextGameTimerTimeout()
        {
            GetNode<Ball>("Ball").Reset();
        }
    }
}
