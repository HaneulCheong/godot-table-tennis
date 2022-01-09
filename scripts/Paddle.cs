using Godot;


/// <summary>모든 라켓의 기본이 되는 <c>abstract</c> 클래스</summary>
public abstract class Paddle : KinematicBody2D
{
    /// <summary>기준 속력</summary>
    protected float Speed = 400;
    /// <summary>현재 속도</summary>
    public abstract Vector2 Velocity { get; }

    /// <summary>매 틱마다 <c>Velocity</c>에 <c>Speed</c>를 곱한 값 만큼 움직입니다.</summary>
    public override void _PhysicsProcess(float delta)
    {
        this.MoveAndSlide(this.Velocity * this.Speed);
    }

    /// <summary>스페이스 바를 누를 경우 게임을 다시 시작합니다.</summary>
    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("ui_select"))
        {
            this.Position = new Vector2(this.Position.x, this.GetViewport().Size.y / 2);
        }
    }
}
