using Godot;


/// <summary>간단한 인공지능이 조종하는 적 라켓 노드</summary>
public class Opponent : Paddle
{
    /// <summary>인공지능이 공과 자신의 위치를 비교해 <c>Velocity.y</c>를 결정합니다.</summary>
    public override Vector2 Velocity
    {
        get
        {
            Vector2 value = new Vector2();

            if (
                (Ball.Position.x < this.Position.x) &&  // 게임 진행 중
                (System.Math.Abs(Ball.Position.y - this.Position.y) > 25)  // 공과 자신의 y 좌표가 기준치 이상으로 벌어짐
            )
            {
                if (Ball.Position.y > this.Position.y)
                {
                    value.y = 1;  // 위로
                }
                else if (Ball.Position.y < this.Position.y)
                {
                    value.y = -1;  // 아래로
                }
                else
                {
                    value.y = 0;
                }
            }

            return value;
        }
    }
    /// <summary><c>Ball</c> 노드를 저장해두는 속성</summary>
    private KinematicBody2D Ball;
    /// <summary>플레이어 점수에 따라 단계적으로 올라갈 기준 속력의 단위 변동량</summary>
    private static int SpeedStep = 20;

    public override void _Ready()
    {
        this.Speed = 400 - (SpeedStep * 11);
        this.Ball = this.GetNode<KinematicBody2D>("../Ball");  // Ball 노드 저장
        
        base._Ready();
    }

    /// <summary>플레이어 점수에 따라 기준 속력을
    /// 상향 조정한 뒤 초기 위치로 돌아갑니다.</summary>
    public override void Reset()
    {
        int playerScore = this.GetNode<Label>("../PlayerScore").Text.ToInt();
        this.Speed = 400 - (SpeedStep * (11 - playerScore));
        if (this.Speed > base.Speed)
        {
            this.Speed = base.Speed;
        }

        base.Reset();
    }
}
