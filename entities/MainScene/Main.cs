using System;
using Godot;


namespace Game.MainScene
{
    using BallScene;
    using UserInterfaceScene;

    /// <summary>게임 전체를 관리하는 노드</summary>
    public class Main : Node
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>경기를 이기기 위해 필요한 점수, 즉 매치 포인트</value>
        [Export]
        private int MatchPoint { get; set; } = 11;

        /// <value>점수판 레이어 노드</value>
        private UserInterface ScoreBoardLayer
        {
            get => GetNode<UserInterface>("UserInterface");
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            GetNode<Timer>("NextGameTimer").Connect(
                "timeout", this, nameof(OnNextGameTimerTimeout)
            );
            GetNode<Area2D>("GoalAreaRight").Connect(
                "body_entered", this, nameof(OnPlayerOneScored)
            );
            GetNode<Area2D>("GoalAreaLeft").Connect(
                "body_entered", this, nameof(OnPlayerTwoScored)
            );
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary><c>GoalAreaRight</c> 노드에 다른 오브젝트(여기서는 공)이
        /// 닿았을 경우의 신호에 의해 호출됩니다.</summary>
        private void OnPlayerOneScored(object body)
        {
            // 플레이어 득점
            ScoreBoardLayer.Scored(PlayerNumber.One);
            GetNode<AudioStreamPlayer>("Scored").Play();

            // 매치 포인트가 아닐 경우:
            if (ScoreBoardLayer.PlayerOneScore < MatchPoint)
            {
                GetNode<Timer>("NextGameTimer").Start();
            }
            // 매치 포인트일 경우:
            else
            {
                GetTree().CallGroup(
                    "MatchPointGroup", nameof(IMatchPointGroup.MatchPoint)
                );
            }
        }

        /// <summary><c>GoalAreaLeft</c> 노드에 다른 오브젝트(여기서는 공)이
        /// 닿았을 경우의 신호 의해 호출됩니다.</summary>
        private void OnPlayerTwoScored(object body)
        {
            // 적 득점
            ScoreBoardLayer.Scored(PlayerNumber.Two);
            GetNode<AudioStreamPlayer>("Scored").Play();

            // 매치 포인트가 아닐 경우:
            if (ScoreBoardLayer.PlayerTwoScore < MatchPoint)
            {
                GetNode<Timer>("NextGameTimer").Start();
            }
            // 매치 포인트일 경우:
            else
            {
                GetTree().CallGroup(
                    "MatchPointGroup", nameof(IMatchPointGroup.MatchPoint)
                );
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
