using Godot;


/// <summary>공을 구현하는 <c>KinematicBody2D</c> 노드</summary>
public class Ball : KinematicBody2D, IMatchPointGroup
{
    ////////////////////
    // 속성
    ////////////////////

    /// <value>움직임 여부</value>
    public bool Moving { get; private set; } = true;

    /// <value>상대 속도</value>
    public Vector2 Velocity { get; private set; } = Vector2.Zero;

    /// <value>기준 속력</value>
    private float Speed { get; } = 300;

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
    public override void _Ready()
    {
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

            // 충돌 시
            if (collision != null)
            {
                // 튕겨져 나오며 Velocity 조정
                Velocity = Velocity.Bounce(collision.Normal);

                // 시간이 지날 수록 위아래로 더 빠르게 움직임
                Velocity = new Vector2(Velocity.x * 1.01f, Velocity.y * 1.02f);

                // 충돌 물체에 따라 효과음 재생
                if (collision.Collider is Paddle)
                {
                    GetNode<AudioStreamPlayer2D>("PaddleBounce").Play();
                }
                else
                {
                    GetNode<AudioStreamPlayer2D>("WallBounce").Play();
                }
            }
        }
    }

    ////////////////////
    // Godot 시그널 메서드
    ////////////////////

    /// ServeTimer 노드의 Timeout 시그널로 호출됩니다.
    private void _OnServeTimerTimeout()
    {
        // 서브로 경기 재개
        Moving = true;
    }

    ////////////////////
    // 메서드
    ////////////////////

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
        Position = new Vector2(
            GetViewport().Size.x / 2,
            RandomTools.RandInt(80, (int)GetViewport().Size.y - 80)
        );
        // 속도 초기화
        Velocity = new Vector2(
            RandomTools.Choice<float>(new float[] { 1.0f, -1.0f }),
            RandomTools.Choice<float>(new float[] { 0.8f, -0.8f })
        );
        GetNode<Timer>("ServeTimer").Start();
    }
}
