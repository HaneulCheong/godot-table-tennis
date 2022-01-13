using Godot;


/// <summary>모든 라켓의 기본이 되는 추상 클래스</summary>
public abstract class Paddle : KinematicBody2D, IMatchPointGroup
{
    ////////////////////
    // 상수
    ////////////////////

    protected const float initialSpeed = 400;

    ////////////////////
    // 속성
    ////////////////////

    /// <summary>초기 위치</summary>
    private Vector2 InitialPosition { get; set; }
    /// <value>기준 속력</value>
    protected float Speed { get; set; } = initialSpeed;
    /// <value>현재 상대 속도</value>
    public abstract Vector2 Direction { get; }

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
        MoveAndCollide(Direction * Speed * delta);
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
