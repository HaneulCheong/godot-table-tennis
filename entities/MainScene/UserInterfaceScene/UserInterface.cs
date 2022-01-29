using System;
using Godot;


namespace Game.MainScene.UserInterfaceScene
{
    /// <summary>유저 인터페이스 레이어</summary>
    public class UserInterface : CanvasLayer, IMatchPointGroup
    {
        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>PlayerOneScore</c>의 내부 필드</summary>
        private int _playerOneScore = 0;
        /// <summary><c>PlayerTwoScore</c>의 내부 필드</summary>
        private int _playerTwoScore = 0;

        ////////////////////
        // 속성
        ////////////////////

        /// <value>플레이어 1의 점수</value>
        public int PlayerOneScore
        {
            get { return _playerOneScore; }
            private set
            {
                /// 음수가 전달되면 변경하지 않음
                if (value < 0)
                {
                    var e = new ArgumentOutOfRangeException(value.ToString());
                    GD.PushError(e.ToString());
                    return;
                }
                /// 변경할 때 해당 Label도 같이 변경
                _playerOneScore = value;
                GetNode<Label>(
                    "ScoreBoard/PlayerOneScore/Label"
                ).Text = value.ToString();
            }
        }

        /// <value>플레이어 2의 점수</value>
        public int PlayerTwoScore
        {
            get { return _playerTwoScore; }
            private set
            {
                /// 음수가 전달되면 변경하지 않음
                if (value < 0)
                {
                    var e = new ArgumentOutOfRangeException(value.ToString());
                    GD.PushError(e.ToString());
                    return;
                }
                /// 변경할 때 해당 Label도 같이 변경
                _playerTwoScore = value;
                GetNode<Label>(
                    "ScoreBoard/PlayerTwoScore/Label"
                ).Text = value.ToString();
            }
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            Reset();
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>점수가 바뀌었을 때 발신하는 신호입니다.</summary>
        /// <param name="playerOneScore">플레이어 1의 점수</param>
        /// <param name="playerTwoScore">플레이어 2의 점수</param>
        [Signal]
        private delegate void ScoreChanged(
            int playerOneScore, int playerTwoScore
        );

        /// <summary>양쪽의 점수를 모두 변경합니다.</summary>
        /// <param name="playerOneScore">플레이어 1의 점수</param>
        /// <param name="playerTwoScore">플레이어 2의 점수</param>
        public void Update(int playerOneScore, int playerTwoScore)
        {
            PlayerOneScore = playerOneScore;
            PlayerTwoScore = playerTwoScore;
            EmitSignal(nameof(ScoreChanged), PlayerOneScore, PlayerTwoScore);
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>전달한 번호의 플레이어가 득점합니다.</summary>
        /// <param name="playerNumber">득점한 플레이어의 번호. 1 또는 2.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 플레이어 번호가 1 또는 2가 아님</exception>
        public void Scored(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1:
                    PlayerOneScore++;
                    EmitSignal(
                        nameof(ScoreChanged), PlayerOneScore, PlayerTwoScore
                    );
                    break;
                case 2:
                    PlayerTwoScore++;
                    EmitSignal(
                        nameof(ScoreChanged), PlayerOneScore, PlayerTwoScore
                    );
                    break;
                default:
                    var e = new ArgumentOutOfRangeException(
                        playerNumber.ToString()
                    );
                    GD.PushError(e.ToString());
                    break;
            }
        }

        public void MatchPoint() {}

        public void Reset() => Update(0, 0);
    }
}
