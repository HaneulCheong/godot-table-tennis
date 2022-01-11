using System;
using Godot;


/// <summary>공 노드</summary>
public class Ball : KinematicBody2D
{
    ////////////////////
    // 필드
    ////////////////////

    /// <summary>난수 생성기</summary>
    private readonly Random _randNumGen = new Random();
    /// <summary>기준 속력</summary>
    protected float Speed = 200;
    /// <summary>초기 기준 속력</summary>
    protected float InitialSpeed;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>현재 상대 속도</value>
    public Vector2 Velocity { get; private set; } = new Vector2();

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>초기 속도를 설정한 뒤 초기화합니다.</summary>
    public override void _Ready()
    {
        InitialSpeed = Speed;
        Reset();
    }

    /// <summary>매 틱마다 이동하며, 물체와 충돌 시 튕겨져 나옵니다.
    /// 또한, 시간이 지날 수록 격하게 움직입니다.</summary>
    public override void _PhysicsProcess(float delta)
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

    ////////////////////
    // Godot 시그널 메서드
    ////////////////////

    /// <summary>ServeTimer 노드에서 Timeout 시그널이 들어오면
    /// 공의 속력과 속도를 모두 초기화합니다.</summary>
    private void _OnServeTimerTimeout()
    {
        Speed = InitialSpeed;
        float x = _randNumGen.Next(2) == 1 ? 1.0f : -1.0f;
        float y = _randNumGen.Next(2) == 1 ? 0.8f : -0.8f;
        Velocity = new Vector2(x, y);
    }

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>정지 후 화면 가운데로 돌아간 뒤
    /// ServeTimer 노드의 타이머를 시작합니다.</summary>
    public void Reset()
    {
        Speed = 0;
        Position = GetViewport().Size / 2;
        GetNode<Timer>("ServeTimer").Start();
    }
}
