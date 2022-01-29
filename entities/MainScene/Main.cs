using System;
using Godot;


namespace Game.MainScene
{
    using BallScene;
    using ScoreBoardScene;

    /// <summary>게임 전체를 관리하는 노드</summary>
    public class Main : Node
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>경기를 이기기 위해 필요한 점수, 즉 매치 포인트</value>
        public int MatchPoint
        {
            get => GetNode<Global>("Global").MatchPoint;
        }

        private ScoreBoard ScoreBoardLayer
        {
            get => GetNode<ScoreBoard>("ScoreBoard");
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
        public override void _Ready()
        {
            GetNode<Timer>("NextGameTimer").Connect(
                "timeout", this, nameof(OnNextGameTimerTimeout)
            );
            GetNode<Area2D>("GoalAreaLeft").Connect(
                "body_entered", this, nameof(OnLeftAreaBodyEntered)
            );
            GetNode<Area2D>("GoalAreaRight").Connect(
                "body_entered", this, nameof(OnRightAreaBodyEntered)
            );
        }

        /// <summary>이 노드의 <c>_Process</c> 메서드입니다.</summary>
        public override void _Process(float delta)
        {
            // "ui_exit"으로 메인 메뉴로 돌아가기
            if (Input.IsActionJustPressed("ui_exit"))
            {
                GetTree().ChangeScene(
                    "res://entities/MainMenuScene/MainMenu.tscn"
                );
            }

            // "ui_restart"로 점수 초기화 뒤 게임 다시 시작
            if (Input.IsActionJustPressed("ui_restart"))
            {
                ScoreBoardLayer.Reset();
                GetTree().CallGroup("MatchPointGroup", "Reset");
            }

            // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
            if (Input.IsActionJustPressed("ui_fullscreen"))
            {
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// GoalAreaLeft 노드에 다른 오브젝트(여기서는 공)이
        /// 닿았을 경우의 신호 의해 호출됩니다.
        private void OnLeftAreaBodyEntered(object body)
        {
            // 적 득점
            ScoreBoardLayer.Scored(2);
            GetNode<AudioStreamPlayer>("Scored").Play();

            // OpponentScore가 매치 포인트가 아닐 경우:
            if (ScoreBoardLayer.PlayerTwoScore < MatchPoint)
            {
                GetNode<Timer>("NextGameTimer").Start();
            }
            // OpponentScore가 매치 포인트일 경우:
            else
            {
                GetTree().CallGroup(
                    "MatchPointGroup", nameof(IMatchPointGroup.MatchPoint)
                );
            }
        }

        /// GoalAreaRight 노드에 다른 오브젝트(여기서는 공)이
        /// 닿았을 경우의 신호에 의해 호출됩니다.
        private void OnRightAreaBodyEntered(object body)
        {
            // 플레이어 득점
            ScoreBoardLayer.Scored(1);
            GetNode<AudioStreamPlayer>("Scored").Play();

            // PlayerScore가 매치 포인트가 아닐 경우:
            if (ScoreBoardLayer.PlayerOneScore < MatchPoint)
            {
                GetNode<Timer>("NextGameTimer").Start();
            }
            // PlayerScore가 매치 포인트일 경우:
            else
            {
                GetTree().CallGroup(
                    "MatchPointGroup", nameof(IMatchPointGroup.MatchPoint)
                );
            }
        }

        /// NextGameTimer 노드의 Timeout 신호에 의해 호출됩니다.
        private void OnNextGameTimerTimeout()
        {
            GetNode<Ball>("Ball").Reset();
        }
    }
}
