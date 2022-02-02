using System;
using Godot;


namespace Game.MainScene.PaddleScene
{
    using BallScene;
    using UserInterfaceScene;

    /// <summary>AI가 조종하는 라켓</summary>
    public class AIPaddle : Paddle
    {
        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>Difficulty</c>의 내부 필드</summary>
        private float _difficulty = 1;

        ////////////////////
        // 속성
        ////////////////////

        /// <value>AI가 설정하는 상대 속도</value>
        public override Vector2 Velocity
        {
            get
            {
                Vector2 value = Vector2.Zero;

                Ball ballNode = GetNode<Ball>("../Ball");
                float ballY = ballNode.Position.y;

                if (
                    // 공이 움직이는 중 (게임 진행 중)
                    ballNode.IsPhysicsProcessing()
                    // 공과 자신의 y 좌표가 기준치 이상으로 벌어짐
                    && (Math.Abs(ballY - Position.y) > 25)
                    // 공이 자신을 향해 움직이는 중
                    && (ballNode.Velocity.x > 0)
                )
                {
                    if (ballY > Position.y) { value = Vector2.Down; }
                    else if (ballY < Position.y) { value = Vector2.Up; }
                    else { value = Vector2.Zero; }
                    value *= Difficulty;
                }

                return value;
            }
        }

        /// <value>AI 난이도. 높을 수록 라켓 이동 속도가 빨라짐.
        /// 0~1 사이의 실수로 정의됨.</value>
        private float Difficulty
        {
            get => _difficulty;
            set
            {
                value = Mathf.Clamp((float)Math.Round(value, 4), 0f, 1f);
                _difficulty = value;
                GD.Print($"AI Difficulty: {Difficulty}");
            }
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            base._Ready();
            GetNode<UserInterface>("../UserInterface").Connect(
                nameof(UserInterface.ScoreChanged), this, nameof(OnScoreChanged)
            );
            // AI 난이도 초기화
            Difficulty = AdjustDifficulty(0);
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>플레이어 점수에 따라 AI 난이도를 설정합니다.</summary>
        /// <param name="playerScore">플레이어 점수</param>
        private void OnScoreChanged(int playerScore, int myScore)
        {
            Difficulty = AdjustDifficulty(playerScore);
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>플레이어 점수에 따라 산출한
        /// AI 난이도를 반환합니다.</summary>
        /// <param name="playerScore">플레이어 점수</param>
        /// <returns>AI 난이도</returns>
        private float AdjustDifficulty(int playerScore)
        {
            float matchPoint = GetParent<Main>().MatchPoint;

            float difficultyScale = (
                matchPoint > 1 ? (float)playerScore / (matchPoint - 1) : 1
            );

            return 0.5f + (float)Math.Pow(difficultyScale, 2) / 2;
        }
    }
}
