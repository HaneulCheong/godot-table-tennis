using Godot;


/// <summary>간단한 인공지능이 조종하는
/// 적 라켓 <c>KinematicBody2D</c> 노드</summary>
public class Opponent : Paddle
{
    ////////////////////
    // 상수
    ////////////////////

    /// <summary>
    /// 플레이어 점수에 따라 단계적으로  올라갈 기준 속력의 단위 변동량.
    /// 0부터 10(<c>Level.Matchpoint - 1</c>)까지를 상정합니다.
    /// </summary>
    public readonly float SpeedStep = (
        initialSpeed * 0.5f / (float)(Level.MatchPoint - 1)
    );

    ////////////////////
    // 속성
    ////////////////////

    /// <value>이동 방향. 인공지능이 공과
    /// 자신의 위치를 비교해 결정합니다.</value>
    public override Vector2 Direction
    {
        get
        {
            KinematicBody2D playernode = GetNode<KinematicBody2D>("../Player");
            KinematicBody2D ballnode = GetNode<KinematicBody2D>("../Ball");

            if (
                // 게임 진행 중
                (ballnode.Position.x < Position.x)
                && (ballnode.Position.x > playernode.Position.x)
                // 공과 자신의 y 좌표가 기준치 이상으로 벌어짐
                && (System.Math.Abs(ballnode.Position.y - Position.y) > 25)
            )
            {
                if (ballnode.Position.y > Position.y)
                {
                    return Vector2.Down;
                }
                else if (ballnode.Position.y < Position.y)
                {
                    return Vector2.Up;
                }
                else
                {
                    return Vector2.Zero;
                }
            }
            else
            {
                return Vector2.Zero;
            }
        }
    }

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>플레이어 점수에 따라 기준 속력을 조정합니다.</summary>
    public void AdjustSpeed()
    {
        float speedModifier = (float)(
            (Level.MatchPoint - 1) - GetNode<Level>("..").PlayerScore
        );
        Speed = initialSpeed - SpeedStep * speedModifier;
        if (Speed > initialSpeed) { Speed = initialSpeed; }
    }

    /// <summary><c>base.Reset()</c>을 실행한 뒤
    /// 플레이어 점수에 따라 기준 속력을 조정합니다.</summary>
    public override void Reset()
    {
        base.Reset();
        AdjustSpeed();
    }
}
