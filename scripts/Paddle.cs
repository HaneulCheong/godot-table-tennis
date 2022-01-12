using Godot;


/// <summary>모든 라켓의 기본이 되는 추상 클래스</summary>
public abstract class Paddle : KinematicBody2D, IMatchPointGroup
{
    ////////////////////
    // 필드
    ////////////////////

    /// <summary>초기 위치</summary>
    protected Vector2 InitialPosition;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>기준 속력</value>
    public float Speed { get; protected set; } = 400;
    /// <value>현재 상대 속도</value>
    public abstract Vector2 Velocity { get; }

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>Godot 내장 <c>_Ready</c> 메소드입니다.</summary>
    public override void _Ready()
    {
        InitialPosition = Position;  // 초기 위치 저장
        Reset();
    }

    /// <summary>매 틱마다 <c>Velocity</c>에
    /// <c>Speed</c>를 곱한 값 만큼 움직입니다.</summary>
    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(Velocity * Speed);
    }

    ////////////////////
    // 메서드
    ////////////////////

    public void MatchPoint()
    {
        Visible = false;
    }

    /// <summary>초기 위치로 돌아갑니다.</summary>
    virtual public void Reset()
    {
        Visible = true;
        Position = InitialPosition;
    }
}
