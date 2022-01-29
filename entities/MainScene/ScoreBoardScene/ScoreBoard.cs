using System;
using Godot;


namespace Game.MainScene.ScoreBoardScene
{
    public class ScoreBoard : CanvasLayer, IMatchPointGroup
    {
        private int _playerOneScore = 0;
        private int _playerTwoScore = 0;

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
                GetNode<Label>("PlayerOneScore").Text = value.ToString();
            }
        }

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
                GetNode<Label>("PlayerTwoScore").Text = value.ToString();
            }
        }

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            Reset();
        }

        [Signal]
        private delegate void ScoreChanged(int playerOneScore, int playerTwoScore);

        public void Update(int playerOneScore, int playerTwoScore)
        {
            PlayerOneScore = playerOneScore;
            PlayerTwoScore = playerTwoScore;
            EmitSignal(nameof(ScoreChanged), PlayerOneScore, PlayerTwoScore);
        }

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

        public void Reset()
        {
            PlayerOneScore = 0;
            PlayerTwoScore = 0;
            EmitSignal(nameof(ScoreChanged), PlayerOneScore, PlayerTwoScore);
        }
    }
}
