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
        // 속성
        ////////////////////

        [Export]
        private float Speed { get; set; } = 400;

        /// <value>움직임 여부</value>
        public bool Moving { get; private set; } = true;

        /// <value>상대 속도</value>
        public Vector2 Velocity { get; private set; } = Vector2.Zero;

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            GetNode<Timer>("ServeTimer").Connect(
                "timeout", this, nameof(_OnServeTimerTimeout)
            );
            Reset();
        }

        /// <summary>이 노드의 <c>_PhysicsProcess</c> 메서드입니다.</summary>
        public override void _PhysicsProcess(float delta)
        {
            if (Moving)
            {
                KinematicCollision2D collision = MoveAndCollide(
                    Velocity * Speed * delta
                );

                // 충돌 시 반사각 계산 실행
                if (collision != null) { Bounce(collision); }
            }
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// ServeTimer 노드의 Timeout 신호로 호출됩니다.
        private void _OnServeTimerTimeout()
        {
            // 서브 방향 설정
            Velocity = Vector2.Right.Rotated(RandomTools.Uniform(-1, 1));
            if (RandomTools.RandBool) { Velocity *= -1; }
            Velocity = Velocity.Normalized();
            // 서브, 경기 재개
            Moving = true;
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
                // Paddle 반사 소리 재생
                GetNode<AudioStreamPlayer2D>("PaddleBounce").Play();
                // 수평으로 충돌했으면 반사각 조정 알고리즘 실행
                if (Math.Abs(collision.Normal.x) == 1)
                {
                    Paddle collider = collision.Collider as Paddle;
                    float direction = collision.Normal.x;

                    // 1. 우선 반사각 수평으로 고정
                    Velocity = (
                        Velocity.Rotated(Velocity.AngleTo(new Vector2(1, 0)))
                        * direction
                    );
                    // 2. 상하 치우침 계산
                    float offset = (
                        (Position.y - collider.Position.y) / (collider.Height / 2)
                    );
                    if (offset >= 0)
                    {
                        offset *= offset;
                        offset = Math.Min(offset, 0.9f);
                    }
                    else
                    {
                        offset *= -offset;
                        offset = Math.Max(offset, -0.9f);
                    }
                    // 3. 오차에 따라 반사각 회전
                    Velocity = Velocity.Rotated(
                        (float)(Math.PI / 2) * offset * direction
                    );
                }
                // 아닐 경우:
                else
                {
                    // 일반적인 반사 처리
                    Velocity = Velocity.Bounce(collision.Normal);
                }
                // 최종적으로 속도 급격히 증가
                Velocity *= 1.05f;
            }
            // 충돌 물체가 Paddle이 아닐 경우:
            else
            {
                // 일반 반사 소리 재생
                GetNode<AudioStreamPlayer2D>("Bounce").Play();
                // 일반적인 반사 처리
                Velocity = Velocity.Bounce(collision.Normal);
                // 속도 조금씩 증가
                Velocity *= 1.01f;
            }
        }

        /// <summary>이 노드를 숨깁니다.</summary>
        public void MatchPoint()
        {
            Visible = false;
        }

        /// <summary>
        /// * 이 노드를 드러냅니다.
        /// * 정지 후 무작위 서브 위치로 이동합니다.
        /// * ServeTimer 노드의 타이머를 시작합니다.
        /// </summary>
        public void Reset()
        {
            Visible = true;
            Moving = false;
            // 무작위 서브 위치로 이동
            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            Position = new Vector2(
                screenSize.x / 2,
                RandomTools.RandInt(80, (int)screenSize.y - 80)
            );
            GetNode<Timer>("ServeTimer").Start();
        }
    }
}
