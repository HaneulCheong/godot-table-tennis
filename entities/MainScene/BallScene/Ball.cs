using System;
using Godot;


namespace Game.MainScene.BallScene
{
    using Internal;
    using PaddleScene;

    /// <summary>공을 구현하는 <c>KinematicBody2D</c> 노드</summary>
    public class Ball : KinematicBody2D, IMatchPointGroup
    {
        ////////////////////
        // 상수
        ////////////////////

        /// <summary>절반 파이, 즉 직각의 라디안 값</summary>
        private float HALF_PI = (float)(Math.PI / 2);

        ////////////////////
        // 속성
        ////////////////////

        /// <value>속력</value>
        [Export]
        private float Speed { get; set; } = Global.Settings["Ball_Speed"];

        /// <value>한계 속력</value>
        [Export]
        private float MaxVelocity { get; set; } = 5;

        /// <value>Paddle 충돌 후의 최대 반사각. 1에 가까울 수록
        /// 수직에 가깝게 반사될 수 있음</value>
        [Export]
        private float BounceRatio { get; set; } = 0.7f;

        /// <value>상대 속도</value>
        public Vector2 Velocity { get; private set; } = Vector2.Zero;

        /// <value>화면 크기</value>
        private Vector2 ScreenSize
        {
            get => GetViewport().GetVisibleRect().Size;
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            GetNode<VisibilityNotifier2D>("VisibilityNotifier2D").Connect(
                "screen_exited", this, nameof(OnScreenExited)
            );
            GetNode<Timer>("ServeTimer").Connect(
                "timeout", this, nameof(OnServeTimerTimeout)
            );
            Reset();
        }

        public override void _PhysicsProcess(float delta)
        {
            KinematicCollision2D collision = MoveAndCollide(
                Velocity * Speed * delta
            );

            // 충돌 시 반사각 계산 실행
            if (collision != null) { Bounce(collision); }
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>플레이어 중 하나가
        /// 득점했을 때 발신하는 신호입니다.</summary>
        [Signal]
        public delegate void Goal(PlayerNumber playerNumber);

        /// <summary>화면을 벗어나면 득점 계산을 한 후
        /// 물리 계산을 중단합니다.</summary>
        private void OnScreenExited()
        {
            if (Position.x > ScreenSize.x / 2)
            {
                EmitSignal(nameof(Goal), PlayerNumber.One);
            }
            else
            {
                EmitSignal(nameof(Goal), PlayerNumber.Two);
            }
            SetPhysicsProcess(false);
        }

        /// <summary>ServeTimer 노드의 Timeout 신호로 호출됩니다.
        /// 서브 방향을 설정한 뒤 물리 계산을 시작해 경기를 재개합니다.</summary>
        private void OnServeTimerTimeout()
        {
            // 서브 방향 설정
            Velocity = Vector2.Right.Rotated(RandomTools.Float(-1, 1));
            if (RandomTools.Bool()) { Velocity *= -1; }
            Velocity = Velocity.Normalized();
            // 서브, 경기 재개
            SetPhysicsProcess(true);
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>충돌한 물체에 대하여 반사각을 계산합니다.</summary>
        /// <param name="collision">충돌 정보</param>
        private void Bounce(KinematicCollision2D collision)
        {
            // 충돌 물체가 Paddle일 경우:
            if (collision.Collider is Paddle)
            {
                // TODO: 물리 계산과 소리 재생은 분리해야 함
                // Paddle 반사 소리 재생
                GetNode<AudioStreamPlayer2D>("PaddleBounce").Play();
                // 수평으로 충돌했으면 반사각 조정 알고리즘 실행
                if (Math.Abs(collision.Normal.x) == 1)
                {
                    // 충돌한 Paddle
                    Paddle collider = collision.Collider as Paddle;
                    // 반사 후의 대략적인 진행 방향. 1: 오른쪽, -1: 왼쪽
                    int direction = (int)collision.Normal.x;

                    // 1. 우선 반사각 수평으로 고정
                    Velocity = Velocity.Rotated(
                        Velocity.AngleTo(new Vector2(direction, 0))
                    );
                    // 2. 각도 비율 계산
                    // 2.1. 상하 치우침 계산, 즉 반사 지점이
                    // Paddle의 중심으로부터 얼마나 멀리 있는지 계산
                    // 끝으로 치우쳐졌을 수록 반사각이 깊어짐
                    float ratio = (
                        (Position.y - collider.Position.y)
                        / (collider.Height / 2)
                    );
                    ratio = Mathf.Clamp(ratio, -1, 1);
                    // 2.2. 각도 완화
                    // ratio가 1 이하이므로 제곱해서 완화한 뒤
                    // BounceRatio를 곱해서 다시 완화
                    if (ratio >= 0) { ratio *= ratio; }
                    else { ratio *= -ratio; }
                    ratio *= BounceRatio;
                    // 3. 산출한 각도 비율에 따라 반사각 회전
                    float newRadian = HALF_PI * ratio * direction;
                    Velocity = Velocity.Rotated(newRadian);
                }
                // 수평 충돌이 아니면:
                else
                {
                    // 일반적인 반사 처리
                    Velocity = Velocity.Bounce(collision.Normal);
                }
                // 최종적으로 속도 급격히 증가
                Velocity *= 1.05f;
            }
            // 충돌 물체가 Paddle이 아니면:
            else
            {
                // TODO: 물리 계산과 소리 재생은 분리해야 함
                // 일반 반사 소리 재생
                GetNode<AudioStreamPlayer2D>("Bounce").Play();
                // 일반적인 반사 처리
                Velocity = Velocity.Bounce(collision.Normal);
                // 속도 조금씩 증가
                Velocity *= 1.01f;
            }

            // 마지막으로 지정된 속도 제한 설정
            Velocity = Velocity.Clamped(MaxVelocity);
        }

        /// <summary>이 노드를 숨깁니다.</summary>
        public void MatchPoint() => Hide();

        /// <summary>
        /// * 물리 계산을 중단한 뒤 무작위 서브 위치로 이동합니다.
        /// * 이 노드를 드러냅니다.
        /// * ServeTimer 노드의 타이머를 시작합니다.
        /// </summary>
        public void Reset()
        {
            SetPhysicsProcess(false);
            Position = new Vector2(
                ScreenSize.x / 2,
                RandomTools.Int(80, (int)ScreenSize.y - 80)
            );
            Show();
            GetNode<Timer>("ServeTimer").Start();
        }
    }
}
