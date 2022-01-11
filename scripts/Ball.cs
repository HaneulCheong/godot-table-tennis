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
    public readonly float Speed = 200;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>현재 상대 속도</value>
    public Vector2 Velocity { get; private set; } = new Vector2();

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>Godot 내장 <c>_Ready</c> 메소드입니다.</summary>
    public override void _Ready()
    {
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
    // 메서드
    ////////////////////

    /// <summary>초기 위치로 돌아간 뒤 무작위 대각선 방향을
    /// <c>Velocity</c>로 선택합니다.</summary>
    public void Reset()
    {
        Position = GetViewport().Size / 2;
        float x = _randNumGen.Next(2) == 1 ? 1.0f : -1.0f;
        float y = _randNumGen.Next(2) == 1 ? 0.8f : -0.8f;
        Velocity = new Vector2(x, y);
    }
}
