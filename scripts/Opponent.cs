using Godot;


/// <summary>간단한 인공지능이 조종하는 적 라켓 노드</summary>
public class Opponent : Paddle
{
    ////////////////////
    // 필드
    ////////////////////

    /// <summary>플레이어 점수에 따라 단계적으로 올라갈 기준 속력의 단위 변동량</summary>
    public const int SpeedStep = 25;
    /// <summary>초기 기준 속력</summary>
    public readonly float InitialSpeed;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>
    /// 현재 상대 속도.
    /// 인공지능이 공과 자신의 위치를 비교해
    /// <c>Velocity.y</c>를 결정합니다.
    /// </value>
    public override Vector2 Velocity
    {
        get
        {
            Vector2 value = new Vector2();

            KinematicBody2D Player = GetNode<KinematicBody2D>("../Player");
            KinematicBody2D Ball = GetNode<KinematicBody2D>("../Ball");

            if (
                // 게임 진행 중
                (Ball.Position.x < Position.x) &&
                (Ball.Position.x > Player.Position.x) &&
                // 공과 자신의 y 좌표가 기준치 이상으로 벌어짐
                (System.Math.Abs(Ball.Position.y - Position.y) > 25)
            )
            {
                if (Ball.Position.y > Position.y) { value.y = 1; }  // 위로 
                else if (Ball.Position.y < Position.y) { value.y = -1; }  // 아래로
                else { value.y = 0; }  // 현상 유지
            }

            return value;
        }
    }

    ////////////////////
    // 생성자
    ////////////////////

    /// <summary><c>Opponent</c> 클래스의 생성자입니다.</summary>
    public Opponent()
    {
        InitialSpeed = Speed;  // 초기 기준 속력 저장
    }

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>Godot 내장 <c>_Ready</c> 메소드입니다.</summary>
    public override void _Ready()
    {
        base._Ready();
        AdjustSpeed();
    }

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>플레이어 점수에 따라 기준 속력을 상향 조정합니다.</summary>
    public void AdjustSpeed()
    {
        int playerScore = GetNode<Level>("..").PlayerScore;
        Speed = InitialSpeed - SpeedStep * (11 - playerScore);
        if (Speed > InitialSpeed) { Speed = InitialSpeed; }
    }

    /// <summary>초기 위치로 돌아간 뒤 플레이어 점수에 따라
    /// 기준 속력을 상향 조정합니다.</summary>
    public override void Reset()
    {
        base.Reset();
        AdjustSpeed();
    }
}
