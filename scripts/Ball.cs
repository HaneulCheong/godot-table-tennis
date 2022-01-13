using Godot;


/// <summary>공을 구현하는 <c>KinematicBody2D</c> 노드</summary>
public class Ball : KinematicBody2D, IMatchPointGroup
{
    ////////////////////
    // 속성
    ////////////////////

    /// <value>움직임 여부</value>
    private bool Moving { get; set; } = true;

    /// <value>상대 속도</value>
    private Vector2 Velocity { get; set; }

    /// <value>기준 속력</value>
    private float Speed { get; } = 200;

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>이 노드의 <c>_Ready</c> 메소드입니다.</summary>
    public override void _Ready()
    {
        Reset();
    }

    /// <summary>이 노드의 <c>_PhysicsProcess</c> 메소드입니다.</summary>
    public override void _PhysicsProcess(float delta)
    {
        if (Moving)
        {
            KinematicCollision2D collisionObject = MoveAndCollide(
                Velocity * Speed * delta
            );

            // 충돌 시 튕겨져 나오며 Velocity를 조정합니다.
            if (collisionObject != null)
            {
                Velocity = Velocity.Bounce(collisionObject.Normal);

                // 시간이 지날 수록 위아래로 격하게 움직입니다.
                Velocity = new Vector2(
                    Velocity.x * 1.01f, Velocity.y * 1.02f
                );
            }
        }
    }

    ////////////////////
    // Godot 시그널 메서드
    ////////////////////

    /// ServeTimer 노드의 Timeout 시그널로 호출됩니다.
    private void _OnServeTimerTimeout()
    {
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

    /// <summary>정지 후 화면 가운데로 돌아간 뒤
    /// ServeTimer 노드의 타이머를 시작합니다.</summary>
    public void Reset()
    {
        Visible = true;
        Moving = false;
        Position = GetViewport().Size / 2;
        // 속도 초기화
        Velocity = new Vector2(
            RandomTools.Choice<float>(new float[] {1.0f, -1.0f}),
            RandomTools.Choice<float>(new float[] {0.8f, -0.8f})
        );
        GetNode<Timer>("ServeTimer").Start();
    }
}
