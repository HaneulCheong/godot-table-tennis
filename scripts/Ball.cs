using System;
using Godot;


/// <summary>공 노드</summary>
public class Ball : KinematicBody2D
{
    private Random RandNumGen = new Random();
    /// <summary>기준 속력</summary>
    private float Speed = 200;
    /// <summary>현재 속도</summary>
    public Vector2 Velocity = new Vector2();

    /// <summary>초기 Velocity를 설정합니다.</summary>
    public override void _Ready()
    {
        this.Velocity.x = this.RandNumGen.Next(2) == 1 ? 1.0f : -1.0f;
        this.Velocity.y = this.RandNumGen.Next(2) == 1 ? 0.8f : -0.8f;
    }

    /// <summary>매 틱마다 이동하며, 물체와 충돌 시 튕겨져 나옵니다.
    /// 또한, 시간이 지날 수록 격하게 움직입니다.</summary>
    public override void _PhysicsProcess(float delta)
    {
        KinematicCollision2D collisionObject = this.MoveAndCollide(
            this.Velocity * this.Speed * delta
        );

        // 충돌 시 튕겨져 나오며 Velocity를 조정합니다.
        if (collisionObject != null)
        {
            this.Velocity = this.Velocity.Bounce(collisionObject.Normal);

            // 시간이 지날 수록 위아래로 격하게 움직입니다.
            this.Velocity.x *= 1.01f;
            this.Velocity.y *= 1.02f;
        }
    }

    /// <summary>스페이스 바를 누를 경우 게임을 다시 시작합니다.</summary>
    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("ui_select"))
        {
            this.Position = this.GetViewport().Size / 2;
            this.Velocity.x = this.RandNumGen.Next(2) == 1 ? 1.0f : -1.0f;
            this.Velocity.y = this.RandNumGen.Next(2) == 1 ? 0.8f : -0.8f;
        }
    }
}
