using Godot;


/// <summary>모든 라켓의 기본이 되는 <c>abstract</c> 클래스</summary>
public abstract class Paddle : KinematicBody2D
{
    /// <summary>초기 X 좌표</summary>
    protected float InitialX;
    /// <summary>기준 속력</summary>
    protected float Speed = 400;
    /// <summary>현재 속도</summary>
    public abstract Vector2 Velocity { get; }

    /// <summary>초기 X 좌표를 저장합니다.</summary>
    public override void _Ready()
    {
        InitialX = this.Position.x;
    }

    /// <summary>매 틱마다 <c>Velocity</c>에 <c>Speed</c>를 곱한 값 만큼 움직입니다.</summary>
    public override void _PhysicsProcess(float delta)
    {
        this.MoveAndSlide(this.Velocity * this.Speed);
    }

    /// <summary>초기 위치로 돌아갑니다.</summary>
    virtual public void Reset()
    {
        this.Position = new Vector2(this.InitialX, this.GetViewport().Size.y / 2);
    }
}
